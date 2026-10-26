using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums.Property;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Audits native global member-face actions and their managed transfer to the physical incidence joint.</summary>
    /// <remarks>Transferred moments use Mjoint=Mface+offset cross F, with offset from joint to face in the same native length units. No beta-based global force reconstruction occurs.</remarks>
    public sealed class OffsetAwareMemberEndForces
    {
        internal OffsetAwareMemberEndForces(IdentifiedMemberForces face, IdentifiedMemberForces joint, Point3D jointPoint, Point3D facePoint, Vector3D offset, PropertyQueryStatus status)
        {
            MemberFaceForces = face;
            PhysicalJointForces = joint;
            PhysicalJoint = jointPoint;
            MemberFace = facePoint;
            GlobalOffset = offset;
            OffsetStatus = status;
        }
        /// <summary>Gets the native global member-face force/moment observation.</summary>
        public IdentifiedMemberForces MemberFaceForces
        {
            get;
        }
        /// <summary>Gets the same force-couple transferred to the recorded physical joint.</summary>
        public IdentifiedMemberForces PhysicalJointForces
        {
            get;
        }
        /// <summary>Gets the captured analytical incidence/physical-joint point.</summary>
        public Point3D PhysicalJoint
        {
            get;
        }
        /// <summary>Gets joint point plus the observed global member offset.</summary>
        public Point3D MemberFace
        {
            get;
        }
        /// <summary>Gets the observed global offset; documented NoSpecification is represented as zero with provenance retained.</summary>
        public Vector3D GlobalOffset
        {
            get;
        }
        /// <summary>Gets Success or explicit NoSpecification from the Property observation.</summary>
        public PropertyQueryStatus OffsetStatus
        {
            get;
        }
        /// <summary>Gets whether any captured global offset component is nonzero; no tolerance silently discards a small offset.</summary>
        public bool HasOffset => GlobalOffset.X != 0 || GlobalOffset.Y != 0 || GlobalOffset.Z != 0;
    }
}
