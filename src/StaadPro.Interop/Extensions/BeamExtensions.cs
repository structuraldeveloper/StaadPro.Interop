using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Extensions
{
    public static class BeamExtensions
    {
        public static int LastId(this IEnumerable<Beam> beams) => beams.Any() ? beams.Max(b => b.Id) : 0;
    }
}
