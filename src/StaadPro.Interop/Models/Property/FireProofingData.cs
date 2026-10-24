using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains qualified immutable fireproofing data with explicit member or specification-index correspondence.</summary>
    /// <remarks>The nonzero-based getter index is not the zero-based creation index. No offset conversion,
    /// assignment reference inference, mass/weight conversion or fire-analysis certification occurs.</remarks>
    public sealed class FireProofingData
    {
        internal FireProofingData(int? memberId, int? specificationIndex, FireProofingType type, double thickness, double density, int? assignedMemberCount)
        { MemberId = memberId; SpecificationIndex = specificationIndex; Type = type; Thickness = thickness; Density = density; AssignedMemberCount = assignedMemberCount; }
        /// <summary>Gets the requested member ID for a per-beam query; null for specification details.</summary>
        public int? MemberId { get; }
        /// <summary>Gets the requested positive nonzero-based native getter index for specification details; null for member queries. Never derived by adding one to a creation index.</summary>
        public int? SpecificationIndex { get; }
        /// <summary>Gets the verified native Block/BFP=1 or Contour/CFP=2 classification.</summary>
        public FireProofingType Type { get; }
        /// <summary>Gets the finite nonnegative native thickness, including an explicitly returned zero, without length conversion.</summary>
        public double Thickness { get; }
        /// <summary>Gets the finite nonnegative native density without inferring mass versus weight or changing units.</summary>
        public double Density { get; }
        /// <summary>Gets the nonnegative scalar assigned-member count supplied by the specification-details query; null for per-member data. The vendor VBA example establishes a Long scalar despite an array typo in its parameter table.</summary>
        public int? AssignedMemberCount { get; }
    }
}
