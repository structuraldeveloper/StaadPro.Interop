using StaadPro.Interop.Entities;

using System.Collections.Generic;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Result of identifying physical members from a collection of individual beams.
    /// </summary>
    /// <remarks>
    /// A physical member is a run of beams that are connected sequentially, end to end, along a
    /// straight line. Beams that could not be reconciled into such a run (for example because they
    /// branch at a node shared by more than two beams from the input collection, or form a closed loop) are reported
    /// separately in <see cref="DiscardedBeams"/> instead of being silently dropped.
    /// </remarks>
    /// <remarks>Source-compatible mutable result model. Collections are newly allocated; contained structural entities remain mutable.
    /// Generated results own their list containers, not the lifetime of the referenced native application.</remarks>
    public class PhysicalMemberIdentificationResult
    {
        /// <summary>
        /// The physical members identified from the input beams. Each <see cref="Member"/> carries
        /// its constituent <see cref="Member.Beams"/> as an unordered set, together
        /// with the resolved <see cref="Member.StartNode"/> and <see cref="Member.EndNode"/> of the run.
        /// </summary>
        public List<Member> Members { get; set; } = new List<Member>();

        /// <summary>
        /// Beams from the input collection that could not be assigned to any identified physical
        /// member.
        /// </summary>
        public List<Beam> DiscardedBeams { get; set; } = new List<Beam>();
    }
}
