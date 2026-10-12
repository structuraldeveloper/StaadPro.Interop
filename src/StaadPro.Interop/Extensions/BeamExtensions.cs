using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Extensions
{
    public static class BeamExtensions
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='BeamExtensions.LastId`0(thisIEnumerable&lt;Beam&gt;)']/*" />
        public static int LastId(this IEnumerable<Beam> beams) => beams.Any() ? beams.Max(b => b.Id) : 0;
    }
}
