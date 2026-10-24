using System;
using System.Collections.Generic;

namespace StaadPro.Interop.Services.Property
{
    internal static class UptContractProfiles
    {
        // Exact installed-help profiles; these are contract candidates, not live-runtime certification.
        internal static bool IsReviewed(Version version) => version != null &&
            (version.Equals(new Version(24, 0, 2, 354)) || version.Equals(new Version(25, 0, 1, 424)) || version.Equals(new Version(26, 0, 0, 340)));
        internal static string[] Fields(int code)
        {
            if (!Layouts.TryGetValue(code, out var fields)) throw new NotSupportedException("No reviewed UPT layout exists for native type " + code + ".");
            return (string[])fields.Clone();
        }
        private static readonly Dictionary<int, string[]> Layouts = new Dictionary<int, string[]>
        {
            [502] = "Ax Iz Iy Ix Ay Az YD ZD".Split(' '),
            [482] = "Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee".Split(' '),
            [412] = "Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1".Split(' '),
            [422] = "Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az".Split(' '),
            [432] = "D Wf Tf R Ay Az".Split(' '),
            [442] = "D Wf Tf SP Iz Iy Ix Cy Ay Az".Split(' '),
            [452] = "Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az".Split(' '),
            [462] = "OD ID Ay Az".Split(' '),
            [472] = "Ax D Wf Tf Iz Iy Ix Ay Az".Split(' '),
            [492] = "Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif".Split(' ')
        };
    }
}
