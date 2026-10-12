using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Extensions
{
    public static class PlateExtensions
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='PlateExtensions.LastId`0(thisIEnumerable&lt;Plate&gt;)']/*" />
        public static int LastId(this IEnumerable<Plate> plates) => plates.Any() ? plates.Max(p => p.Id) : 0;
    }
}
