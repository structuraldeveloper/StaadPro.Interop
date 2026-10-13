using System.Collections.Generic;
using StaadPro.Interop.Base;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Models
{
    /// <summary>Managed snapshot of an ordinary native combination and its primary-case coefficients.</summary>
    public class StaadLoadCombination : LoadCombination
    {
        /// <summary>Initializes metadata with an independent empty coefficient dictionary.</summary>
        /// <param name="id">Combination ID.</param>
        /// <param name="title">Native title, stored unchanged.</param>
        public StaadLoadCombination(int id, string title) : base(id, title) { Factors = new Dictionary<ILoadCase, double>(); }
        /// <summary>Gets or replaces primary case metadata and dimensionless signed coefficients.</summary>
        /// <remarks>This is managed data; editing it does not update STAAD. The native reader supplies independent case metadata. There is no field for an SRSS overall multiplier.</remarks>
        public IDictionary<ILoadCase, double> Factors { get; set; }
    }
}
