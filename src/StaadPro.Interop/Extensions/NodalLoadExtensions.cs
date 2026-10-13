using System.Collections.Generic;
using System.Linq;


using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Extensions
{
    /// <summary>Managed duplicate-assignment bucketing helpers.</summary>
    public static class NodalLoadExtensions
    {
        /// <summary>Splits repeated entity IDs into separate equivalent load-definition buckets.</summary>
        /// <param name="groups">Initialized definitions and entity lists. Entries must remain stable during enumeration.</param>
        /// <returns>A lazy sequence with at most one occurrence of each entity ID per bucket.</returns>
        /// <remarks>Preserves multiplicity rather than dropping repeated assignments. The first bucket reuses its original load; later buckets use DeepCopy with independent numeric fields and the same LoadCase reference. Entity objects are reused. No native access or mutation of input lists occurs. Null sequences or uninitialized entries fail during enumeration.</remarks>
        public static IEnumerable<(NodalLoad Load, List<Node> Nodes)> SplitDuplicateNodes(this IEnumerable<(NodalLoad Load, List<Node> Nodes)> groups)
        {
            foreach (var (load, nodes) in groups)
            {
                // Group nodes by Id (or any uniqueness criterion)
                var duplicates = nodes.GroupBy(n => n.Id).Select(g => g.ToList()).ToList();

                // If each Id appears once, just yield the original
                if (duplicates.All(g => g.Count == 1))
                {
                    yield return (load, nodes);
                    continue;
                }

                // Otherwise, split into chunks where each chunk has one of each Id
                var buckets = new List<List<Node>>();
                foreach (var group in duplicates)
                {
                    for (int i = 0; i < group.Count; i++)
                    {
                        if (buckets.Count <= i)
                            buckets.Add(new List<Node>());
                        buckets[i].Add(group[i]);
                    }
                }

                // Yield one entry per bucket (cloning load except first for STAAD uniqueness)
                for (int i = 0; i < buckets.Count; i++)
                {
                    var targetLoad = i == 0 ? load : load.DeepCopy();
                    yield return (targetLoad, buckets[i]);
                }
            }
        }
    }
}
