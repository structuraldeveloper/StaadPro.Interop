using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
namespace StaadPro.Interop.Entities
{
    /// <summary>Contains a source-compatible editable six-component member-end force record.</summary>
    /// <remarks>Values are native local-coordinate results without unit conversion or offset transfer.
    /// Editing this managed model does not update STAAD. Inherited Forces equality compares the six
    /// components, not member/load identity; do not use a HashSet to preserve distinct physical results.</remarks>
    public class MemberForces : Forces, IForces
    {
        /// <summary>Gets or sets the native load-case identifier.</summary>
        /// <value>The load case associated with this record.</value>
        public int LoadCaseId { get; set; }
        /// <summary>Gets or sets the native load-case title.</summary>
        /// <value>Exact native text, including an explicitly empty title.</value>
        public string LoadCaseTitle { get; set; }
        /// <summary>Gets or sets the member identifier.</summary>
        /// <value>The native beam/member number.</value>
        public int BeamId { get; set; }
        /// <summary>Gets or sets the member end.</summary>
        /// <value>Start or End for a qualified native query.</value>
        public MemberEnd End { get; set; }
        /// <summary>Describes the member, end, load case and six force/moment components.</summary>
        /// <returns>A culture-formatted description using G6 component formatting and the optional load title.</returns>
        /// <remarks>Matches the source model's formatting and performs no native access or unit conversion.</remarks>
        /// <example><code>
        /// var forces = new StaadPro.Interop.Entities.MemberForces { BeamId = 1, LoadCaseId = 2 };
        /// Console.WriteLine(forces.ToString());
        /// </code></example>
        public override string ToString() =>
            $"Member {BeamId}, End {End}, Load Case {LoadCaseId}" +
            (string.IsNullOrWhiteSpace(LoadCaseTitle) ? string.Empty : $" ({LoadCaseTitle})") +
            $": Fx={Fx:G6}, Fy={Fy:G6}, Fz={Fz:G6}, Mx={Mx:G6}, My={My:G6}, Mz={Mz:G6}";
    }
}
