using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns an identified analytical member-end force packet with explicit native coordinate system.</summary>
    public sealed class IdentifiedMemberForces
    {
        internal IdentifiedMemberForces(int member, string title, int loadCase, MemberEnd end, ResultCoordinateSystem axes, ForceComponents values)
        {
            MemberId = member;
            LoadCaseTitle = title;
            LoadCaseId = loadCase;
            MemberEnd = end;
            CoordinateSystem = axes;
            Components = values;
        }
        /// <summary>Gets the positive analytical member ID.</summary>
        public int MemberId
        {
            get;
        }
        /// <summary>Gets exact caller-supplied case-title metadata; it is not read from STAAD by this query.</summary>
        public string LoadCaseTitle
        {
            get;
        }
        /// <summary>Gets the positive requested native case ID.</summary>
        public int LoadCaseId
        {
            get;
        }
        /// <summary>Gets native analytical Start/End identity without changing end-action signs.</summary>
        public MemberEnd MemberEnd
        {
            get;
        }
        /// <summary>Gets the explicitly requested local/global native frame.</summary>
        public ResultCoordinateSystem CoordinateSystem
        {
            get;
        }
        /// <summary>Gets immutable force/moment components in the recorded frame and native units.</summary>
        public ForceComponents Components
        {
            get;
        }
        /// <summary>Creates a fresh editable copy using the existing MemberForces model.</summary>
        /// <returns>An independent copy; the containing CoordinateSystem must be retained separately by the caller.</returns>
        public MemberForces ToMemberForces() => new MemberForces { BeamId = MemberId, LoadCaseId = LoadCaseId, LoadCaseTitle = LoadCaseTitle, End = MemberEnd, Fx = Components.Fx, Fy = Components.Fy, Fz = Components.Fz, Mx = Components.Mx, My = Components.My, Mz = Components.Mz };
    }
}
