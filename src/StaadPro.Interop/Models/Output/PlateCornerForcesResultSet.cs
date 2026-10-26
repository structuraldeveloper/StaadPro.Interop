using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns and indexes unique plate-corner outcomes, including ordinary failed observations.</summary>
    /// <remarks>Adapted from the main indexed result set with immutable family-aware keys. First-seen request order is retained. Missing and failed results remain distinct; no failed native buffer is published as a force packet.</remarks>
    public sealed class PlateCornerForcesResultSet : IReadOnlyCollection<OutputQueryResult<PlateCornerForces>>
    {
        private readonly Dictionary<PlateCornerForcesKey, OutputQueryResult<PlateCornerForces>> byKey;
        internal PlateCornerForcesResultSet(IEnumerable<OutputQueryResult<PlateCornerForces>> items)
        {
            Items = Array.AsReadOnly(items.ToArray());
            byKey = Items.ToDictionary(r => new PlateCornerForcesKey(r.LoadCaseId.Value, r.EntityId.Value, r.CornerNodeId.Value, r.LoadCaseType));
            Keys = Array.AsReadOnly(Items.Select(r => new PlateCornerForcesKey(r.LoadCaseId.Value, r.EntityId.Value, r.CornerNodeId.Value, r.LoadCaseType)).ToArray());
            LoadCaseIds = Array.AsReadOnly(Keys.Select(k => k.LoadCaseId).Distinct().OrderBy(x => x).ToArray());
            PlateIds = Array.AsReadOnly(Keys.Select(k => k.PlateId).Distinct().OrderBy(x => x).ToArray());
            CornerNodeIds = Array.AsReadOnly(Keys.Select(k => k.CornerNodeId).Distinct().OrderBy(x => x).ToArray());
        }
        /// <summary>Gets every owned result in first-seen request order.</summary>
        public IReadOnlyList<OutputQueryResult<PlateCornerForces>> Items
        {
            get;
        }
        /// <summary>Gets captured keys aligned one-for-one with Items.</summary>
        public IReadOnlyList<PlateCornerForcesKey> Keys
        {
            get;
        }
        /// <summary>Gets the number of unique requested observations.</summary>
        public int Count => Items.Count;
        /// <summary>Gets whether every observation succeeded; empty sets are successful.</summary>
        public bool IsSuccessful => Items.All(r => r.IsSuccessful);
        /// <summary>Gets the number of successful observations.</summary>
        public int SucceededCount => Items.Count(r => r.IsSuccessful);
        /// <summary>Gets the number of unsuccessful observations.</summary>
        public int FailedCount => Count - SucceededCount;
        /// <summary>Gets distinct case numbers in ascending order; family distinctions remain in Keys.</summary>
        public IReadOnlyList<int> LoadCaseIds
        {
            get;
        }
        /// <summary>Gets distinct plate IDs in ascending order.</summary>
        public IReadOnlyList<int> PlateIds
        {
            get;
        }
        /// <summary>Gets distinct corner-node IDs in ascending order.</summary>
        public IReadOnlyList<int> CornerNodeIds
        {
            get;
        }
        /// <summary>Gets an exact-family observation, or an unambiguous family-free identity, including failure.</summary><param name="key">Valid captured key.</param><returns>The matching observation.</returns><exception cref="KeyNotFoundException">No request has this exact key.</exception>
        /// <exception cref="InvalidOperationException">A family-free key matches multiple metadata families.</exception>
        public OutputQueryResult<PlateCornerForces> this[PlateCornerForcesKey key] => Find(key) ?? throw new KeyNotFoundException("No corner observation has the requested identity.");
        /// <summary>Finds an exact-family observation or an unambiguous family-free identity without a reference-output parameter.</summary><param name="key">Requested family-aware key.</param><returns>The matching observation, or null when no such request was made.</returns>
        /// <exception cref="InvalidOperationException">A family-free key matches multiple families.</exception>
        public OutputQueryResult<PlateCornerForces> Find(PlateCornerForcesKey key)
        {
            if (byKey.TryGetValue(key, out var result))
                return result;
            if (key.LoadCaseType.HasValue)
                return null;
            var matches = Items.Where(r => r.LoadCaseId == key.LoadCaseId && r.EntityId == key.PlateId && r.CornerNodeId == key.CornerNodeId).ToArray();
            if (matches.Length > 1)
                throw new InvalidOperationException("Automatic lookup is ambiguous; provide the captured case family.");
            return matches.SingleOrDefault();
        }
        /// <summary>Filters observations by captured case number, retaining all families.</summary><param name="loadCaseId">Positive case number.</param><returns>An owned ordered selection, possibly empty.</returns><exception cref="ArgumentOutOfRangeException">The ID is not positive.</exception>
        public IReadOnlyList<OutputQueryResult<PlateCornerForces>> GetForLoadCase(int loadCaseId) => Filter(loadCaseId, r => r.LoadCaseId == loadCaseId);
        /// <summary>Filters observations by captured plate ID.</summary><param name="plateId">Positive plate ID.</param><returns>An owned ordered selection, possibly empty.</returns><exception cref="ArgumentOutOfRangeException">The ID is not positive.</exception>
        public IReadOnlyList<OutputQueryResult<PlateCornerForces>> GetForPlate(int plateId) => Filter(plateId, r => r.EntityId == plateId);
        /// <summary>Filters observations by captured corner-node ID.</summary><param name="cornerNodeId">Positive node ID.</param><returns>An owned ordered selection, possibly empty.</returns><exception cref="ArgumentOutOfRangeException">The ID is not positive.</exception>
        public IReadOnlyList<OutputQueryResult<PlateCornerForces>> GetForCornerNode(int cornerNodeId) => Filter(cornerNodeId, r => r.CornerNodeId == cornerNodeId);
        private IReadOnlyList<OutputQueryResult<PlateCornerForces>> Filter(int id, Func<OutputQueryResult<PlateCornerForces>, bool> predicate)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return Array.AsReadOnly(Items.Where(predicate).ToArray());
        }
        /// <summary>Enumerates eager owned observations without COM calls.</summary><returns>An enumerator over Items.</returns>
        public IEnumerator<OutputQueryResult<PlateCornerForces>> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
