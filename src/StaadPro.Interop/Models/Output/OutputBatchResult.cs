using System;
using System.Collections.Generic;
using System.Linq;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains owned eager results for one bounded managed Output batch.</summary>
    /// <typeparam name="T">The existing named or scalar payload returned by each item.</typeparam>
    /// <remarks>Ordinary native failures remain per-item results and do not stop subsequent items. Transport, binding and malformed-success exceptions abort the batch. Input order and duplicate requests are preserved. This is not an atomic STAAD snapshot.</remarks>
    public sealed class OutputBatchResult<T>
    {
        internal OutputBatchResult(IEnumerable<OutputQueryResult<T>> results)
        {
            Items = Array.AsReadOnly(results.ToArray());
        }
        /// <summary>Gets every identified item, including ordinary native failures, in request order.</summary>
        public IReadOnlyList<OutputQueryResult<T>> Items
        {
            get;
        }
        /// <summary>Gets true when every item succeeded; an empty validated batch is successful.</summary>
        public bool IsSuccessful => Items.All(x => x.IsSuccessful);
        /// <summary>Gets the number of successful item observations.</summary>
        public int SucceededCount => Items.Count(x => x.IsSuccessful);
        /// <summary>Gets the number of unsuccessful item observations.</summary>
        public int FailedCount => Items.Count - SucceededCount;
    }
}
