using StaadPro.Interop.Entities;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Associates a qualified complete native steel-design packet with a caller beam and resolved governing case identity.</summary>
    /// <remarks>Beam is the original mutable caller object, not an immutable geometry clone. MemberId and CriticalLoadCase are captured identities. NativeResults reuses the immutable existing packet; missing FY/FZ/MX actions remain unavailable.</remarks>
    public sealed class BeamSteelDesignResults
    {
        internal BeamSteelDesignResults(Beam beam, int id, MemberSteelDesignResults results, OutputLoadCaseIdentity loadCase)
        {
            Beam = beam;
            MemberId = id;
            NativeResults = results;
            CriticalLoadCase = loadCase;
        }
        /// <summary>Gets the original caller-owned beam; later edits do not alter the captured MemberId or native packet.</summary>
        public Beam Beam
        {
            get;
        }
        /// <summary>Gets the positive member ID captured for this observation.</summary>
        public int MemberId
        {
            get;
        }
        /// <summary>Gets the existing complete native design packet with its independently qualified decision/status fields.</summary>
        public MemberSteelDesignResults NativeResults
        {
            get;
        }
        /// <summary>Gets the uniquely resolved governing case metadata snapshot.</summary>
        public OutputLoadCaseIdentity CriticalLoadCase
        {
            get;
        }
    }
}
