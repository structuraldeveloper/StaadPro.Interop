using StaadPro.Interop.Entities;

using System.Collections.Generic;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// A collection of physical members that share the same plan angle, within the angular
    /// tolerance used to build the group.
    /// </summary>
    /// <remarks>Source-compatible mutable result model. Collections are newly allocated; contained structural entities remain mutable.
    /// Generated results own their list containers, not the lifetime of the referenced native application.</remarks>
    public class PhysicalMemberPlanAngleGroup
    {
        /// <summary>
        /// The representative plan angle of this group, in degrees measured in the range [0, 180),
        /// normalized so that opposite directions along the same line (0/180, 90/270, and so on)
        /// resolve to the same group.
        /// </summary>
        public double PlanAngleInDegrees { get; set; }

        /// <summary>
        /// The original physical members, including repeated inputs, that share <see cref="PlanAngleInDegrees"/>.
        /// </summary>
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
