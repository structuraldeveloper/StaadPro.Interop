using System.Collections.Generic;
using System.Linq;

using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Extensions
{
    /// <summary>Managed duplicate-assignment bucketing helpers.</summary>
    public static class MemberLoadExtensions
    {
        /// <summary>Splits repeated entity IDs into separate equivalent load-definition buckets.</summary>
        /// <param name="groups">Initialized definitions and entity lists. Entries must remain stable during enumeration.</param>
        /// <returns>A lazy sequence with at most one occurrence of each entity ID per bucket.</returns>
        /// <remarks>Preserves multiplicity rather than dropping repeated assignments. The first bucket reuses its original load; later buckets use DeepCopy with independent numeric fields and the same LoadCase reference. Entity objects are reused. No native access or mutation of input lists occurs. Null sequences or uninitialized entries fail during enumeration.</remarks>
        public static IEnumerable<(MemberLoad Load, List<Beam> Beams)> SplitDuplicateBeams(this IEnumerable<(MemberLoad Load, List<Beam> Beams)> groups)
        {
            foreach (var (load, beams) in groups)
            {
                var duplicates = beams.GroupBy(b => b.Id).Select(g => g.ToList()).ToList();

                // No duplicates → return as is
                if (duplicates.All(g => g.Count == 1))
                {
                    yield return (load, beams);
                    continue;
                }

                // Split duplicates into parallel buckets
                var buckets = new List<List<Beam>>();
                foreach (var group in duplicates)
                {
                    for (int i = 0; i < group.Count; i++)
                    {
                        if (buckets.Count <= i)
                            buckets.Add(new List<Beam>());

                        buckets[i].Add(group[i]);
                    }
                }

                // Clone loads for additional buckets
                for (int i = 0; i < buckets.Count; i++)
                {
                    var targetLoad = i == 0 ? load : load.DeepCopy();
                    yield return (targetLoad, buckets[i]);
                }
            }
        }
    }
}
