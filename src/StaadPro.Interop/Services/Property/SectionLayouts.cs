using System;
using System.Collections.Generic;

namespace StaadPro.Interop.Services.Property
{
    // Reviewed installed vendor layouts. 2025 and 2026 agree; use native STAAD major, never host version.
    internal static class SectionLayouts
    {
        internal static string[] Fields(int major, int code)
        {
            if (major != 24 && major != 25 && major != 26) throw new NotSupportedException("Section layout parsing is reviewed only for native STAAD 24, 25 and 26.");
            var layouts = major == 24 ? Version24 : Version25;
            if (!layouts.TryGetValue(code, out var fields)) throw new NotSupportedException("No reviewed section layout exists for native type " + code + ".");
            return (string[])fields.Clone();
        }
        private static readonly Dictionary<int, string[]> Version24 = new Dictionary<int, string[]>
        {
            [610] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [616] = "D Bf Tf Tw Iz Iy Ix SP".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [613] = "Ax D Bf Tf Tw Iz Iy Ix WP TH".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [614] = "Ax D Bf Tf Tw Iz Iy Ix WP TH".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [615] = "Ax D Bf Tf Tw Iz Iy Ix WP TH BW BT".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [611] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [612] = "Ax D Bf Tf Tw Iz Iy Ix CT FC CW".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [630] = "Ax D Bf Tf Tw Iz Iy Ix CT".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [631] = "Ax D Bf Tf Tw Iz Iy Ix SP".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [633] = "Ax D Bf Tf Tw Iz Iy Ix FR".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [640] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [642] = "Ax D B T Iz Iy Ix LD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [643] = "Ax D B T Iz Iy Ix SD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [641] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [646] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [660] = "Ax OD Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [654] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [655] = "Ax OD Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [656] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [650] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [620] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [666] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [644] = "Ax D B T Iz Iy Ix R LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [645] = "Ax D B T Iz Iy Ix R LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [634] = "Ax Bf T R Iz Iy Ix LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [635] = "Ax Bf T R Iz Iy Ix LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [662] = "Ax D B T LIP <LIP R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [663] = "Ax D B T LIP <LIP R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [664] = "Ax D B T BOT_F R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [680] = "F1 F2 F3 F4 F5 F6 F7".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [675] = "Ax Iz Iy Ix D1 D2 TH SECTION_TYPE".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [671] = "Ax Iz Iy Ix YD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [672] = "Ax Iz Iy Ix YD ZD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [674] = "Ax Iz Iy Ix YD ZD ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [673] = "Ax Iz Iy Ix YD ZD YB ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [676] = "Ax Ay Az Ix Iy Iz YD ZD YB ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [668] = "Ax OD Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [699] = "Ax Iz Iy Ix Ay Az YD ZD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [697] = "Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [690] = "Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [691] = "Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [692] = "Ax D Wf Tf R Ay Az Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [693] = "Ax D Wf Tf SP Iz Iy Ix Cy Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [694] = "Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [695] = "Ax OD ID Ay Az Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [696] = "Ax D Wf Tf Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [698] = "Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
        };
        private static readonly Dictionary<int, string[]> Version25 = new Dictionary<int, string[]>
        {
            [610] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [616] = "D Bf Tf Tw Iz Iy Ix SP".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [613] = "Ax D Bf Tf Tw Iz Iy Ix WP TH".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [614] = "Ax D Bf Tf Tw Iz Iy Ix WP TH".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [615] = "Ax D Bf Tf Tw Iz Iy Ix WP TH BW BT".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [611] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [612] = "Ax D Bf Tf Tw Iz Iy Ix CT FC CW CD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [630] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [631] = "Ax D Bf Tf Tw Iz Iy Ix SP".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [633] = "Ax D Bf Tf Tw Iz Iy Ix FR".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [640] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [642] = "Ax D B T Iz Iy Ix LD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [643] = "Ax D B T Iz Iy Ix SD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [641] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [646] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [660] = "Ax OD Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [654] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [655] = "Ax OD Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [656] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [650] = "Ax D B T Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [620] = "Ax D Bf Tf Tw Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [666] = "Ax D B Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [644] = "Ax D B T Iz Iy Ix R Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [645] = "Ax D B T Iz Iy Ix R LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [634] = "Ax D Bf T R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [635] = "Ax D Bf T R Iz Iy Ix LIP Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [662] = "Ax D B T R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [663] = "Ax D B T LIP LIP_Angle R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [664] = "Ax D B T BOT_F R Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [680] = "F1 F2 F3 F4 F5 F6 F7".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [675] = "Ax Iz Iy Ix D1 D2 TH SECTION_TYPE".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [671] = "Ax Iz Iy Ix YD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [672] = "Ax Iz Iy Ix YD ZD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [674] = "Ax Iz Iy Ix YD ZD ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [673] = "Ax Iz Iy Ix YD ZD YB ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [676] = "Ax Ay Az Ix Iy Iz YD ZD YB ZB".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [668] = "Ax OD Tw Iz Iy Ix Z".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [699] = "Ax Iz Iy Ix Ay Az YD ZD".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [697] = "Ax D Td B Tb Iz Iy Ix Sz Sy Ay Az Pz Py Hss Dee".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [690] = "Ax D Tw Wf Tf Iz Iy Ix Ay Az Wf1 Tf1".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [691] = "Ax D Tw Wf Tf Iz Iy Ix Cz Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [692] = "Ax D Wf Tf R Ay Az Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [693] = "Ax D Wf Tf SP Iz Iy Ix Cy Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [694] = "Ax D Wf Tf Tw Iz Iy Ix Cy Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [695] = "Ax OD ID Ay Az Iz Iy Ix".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [696] = "Ax D Wf Tf Iz Iy Ix Ay Az".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
            [698] = "Dww Tww Dww1 Bff Tff Bff1 Tff1 Ayf Azf Xif".Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
        };
    }
}
