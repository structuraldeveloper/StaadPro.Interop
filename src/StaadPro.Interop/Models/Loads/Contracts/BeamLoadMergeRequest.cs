using System;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Identifies a retained beam and the beam whose loads are to be merged into it.</summary>
    /// <remarks>Shape follows passive MergeLoadsOnBeam metadata only. Native packet shape, topology,
    /// model effects and success semantics remain unverified; this does not enable merging.</remarks>
    public sealed class BeamLoadMergeRequest
    {
        /// <summary>Creates an explicit source/retained-beam pair without accessing STAAD.</summary>
        /// <param name="retainedBeamId">Positive beam number to keep.</param>
        /// <param name="sourceBeamId">Positive, distinct beam number whose loads are candidates for merging.</param>
        /// <exception cref="ArgumentOutOfRangeException">A beam number is nonpositive.</exception>
        /// <exception cref="ArgumentException">The same beam is used for both roles.</exception>
        public BeamLoadMergeRequest(int retainedBeamId, int sourceBeamId)
        {
            RetainedBeamId = LoadContractArguments.Positive(retainedBeamId, nameof(retainedBeamId));
            SourceBeamId = LoadContractArguments.Positive(sourceBeamId, nameof(sourceBeamId));
            if (retainedBeamId == sourceBeamId) throw new ArgumentException("A merge requires two distinct beams.", nameof(sourceBeamId));
        }
        /// <summary>Gets the beam number to retain.</summary>
        public int RetainedBeamId { get; }
        /// <summary>Gets the distinct source beam number.</summary>
        public int SourceBeamId { get; }
    }
}
