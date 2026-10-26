using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns a plate-corner force packet rotated into an explicitly described managed member basis.</summary>
    /// <remarks>This changes axes at the same corner point; it does not reverse action signs, shift moments, aggregate adjacent plates or obtain native member design forces. A conceptual direction has no native member identity.</remarks>
    public sealed class PlateCornerMemberForces
    {
        internal PlateCornerMemberForces(PlateCornerForces source, int? member, MemberEnd? end, ForceComponents components, Vector3D x, Vector3D y, Vector3D z, double beta)
        {
            Source = source;
            MemberId = member;
            MemberEnd = end;
            Components = components;
            XAxis = x;
            YAxis = y;
            ZAxis = z;
            BetaAngleDegrees = beta;
        }
        /// <summary>Gets the immutable original global corner observation.</summary>
        public PlateCornerForces Source
        {
            get;
        }
        /// <summary>Gets the original stable case/plate/node identity.</summary>
        public PlateCornerForcesKey Key => Source.Key;
        /// <summary>Gets a real supplied member ID, or null for the corner-to-node conceptual direction.</summary>
        public int? MemberId
        {
            get;
        }
        /// <summary>Gets the matched supplied member end, or null when no real member was specified.</summary>
        public MemberEnd? MemberEnd
        {
            get;
        }
        /// <summary>Gets the original corner reference point; no moment transfer occurred.</summary>
        public Point3D ReferencePoint => Source.CornerPosition;
        /// <summary>Gets the transformed force/moment components in the recorded managed local basis.</summary>
        public ForceComponents Components
        {
            get;
        }
        /// <summary>Gets the unit local X direction used for projection.</summary>
        public Vector3D XAxis
        {
            get;
        }
        /// <summary>Gets the unit local Y direction after the supplied beta rotation.</summary>
        public Vector3D YAxis
        {
            get;
        }
        /// <summary>Gets the unit local Z direction after the supplied beta rotation.</summary>
        public Vector3D ZAxis
        {
            get;
        }
        /// <summary>Gets beta degrees; zero for a conceptual corner-to-node direction.</summary>
        public double BetaAngleDegrees
        {
            get;
        }
    }
}
