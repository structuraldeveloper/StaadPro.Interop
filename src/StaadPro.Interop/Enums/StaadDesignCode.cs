using System.ComponentModel;

namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the main-source StaadDesignCode names and native numeric identities.</summary>
    /// <remarks>A defined code does not establish installed licensing or command support. Unknown (zero) is an observation sentinel and cannot create a brief. Unlisted observed codes remain available as raw numeric observations.</remarks>
    public enum StaadDesignCode
    {
        /// <summary>Unknown; native code 0.</summary>
        [Description("Unknown")]
        Unknown = 0,
        /// <summary>AASHTO ASD; native code 1001.</summary>
        [Description("AASHTO ASD")]
        AASHTOASD = 1001,
        /// <summary>AISC ASD; native code 1002.</summary>
        [Description("AISC ASD")]
        AISCASD = 1002,
        /// <summary>AS 4100-1998; native code 1003.</summary>
        [Description("AS 4100-1998")]
        AS4100Year1998 = 1003,
        /// <summary>BS 5950-1:2000; native code 1004.</summary>
        [Description("BS 5950-1:2000")]
        BS5950Part1Year2000 = 1004,
        /// <summary>BS 5400:Part 3:1982; native code 1005.</summary>
        [Description("BS 5400:Part 3:1982")]
        BS5400Part3Year1982 = 1005,
        /// <summary>CAN/CSA-S16-01; native code 1006.</summary>
        [Description("CAN/CSA-S16-01")]
        CANCSAS16Year01 = 1006,
        /// <summary>French CM66 1977; native code 1007.</summary>
        [Description("French CM66 1977")]
        FrenchCM66Year1977 = 1007,
        /// <summary>DIN 18 800 Part 1; native code 1008.</summary>
        [Description("DIN 18 800 Part 1")]
        DIN18800Part1 = 1008,
        /// <summary>IS 800 1984, ASD; native code 1009.</summary>
        [Description("IS 800 1984, ASD")]
        IS800Year1984ASD = 1009,
        /// <summary>Japan AIJ 2002; native code 1010.</summary>
        [Description("Japan AIJ 2002")]
        JapanAIJ2002 = 1010,
        /// <summary>AISC LRFD; native code 1011.</summary>
        [Description("AISC LRFD")]
        AISCLRFD = 1011,
        /// <summary>Norway NS 3472 2001; native code 1012.</summary>
        [Description("Norway NS 3472 2001")]
        NorwayNS3472Year2001 = 1012,
        /// <summary>Norway NPD 1993; native code 1014.</summary>
        [Description("Norway NPD 1993")]
        NorwayNPD1993 = 1014,
        /// <summary>API 2A-WSD; native code 1016.</summary>
        [Description("API 2A-WSD")]
        API2AWSD = 1016,
        /// <summary>ASCE 10-97; native code 1020.</summary>
        [Description("ASCE 10-97")]
        ASCE10Year97 = 1020,
        /// <summary>Russia SNiP 2.23-81* 1990; native code 1025.</summary>
        [Description("Russia SNiP 2.23-81* 1990")]
        RussiaSNiP2Dot23Dash81Year1990 = 1025,
        /// <summary>Canada S136-94; native code 1027.</summary>
        [Description("Canada S136-94")]
        CanadaS136Year94 = 1027,
        /// <summary>IS 801-1975; native code 1028.</summary>
        [Description("IS 801-1975")]
        IS801Year1975 = 1028,
        /// <summary>IS 802-1995; native code 1029.</summary>
        [Description("IS 802-1995")]
        IS802Year1995 = 1029,
        /// <summary>Mexico NTC 1987; native code 1030.</summary>
        [Description("Mexico NTC 1987")]
        MexicoNTC1987 = 1030,
        /// <summary>IS 800-2007 LSD; native code 1032.</summary>
        [Description("IS 800-2007 LSD")]
        IS800Y2007LSD = 1032,
        /// <summary>IS 800-2007 WSD; native code 1052.</summary>
        [Description("IS 800-2007 WSD")]
        IS800Y2007WSD = 1052,
        /// <summary>BS 5950-5:1998; native code 1033.</summary>
        [Description("BS 5950-5:1998")]
        BS5950Part5Year1998 = 1033,
        /// <summary>SANS 10162-1:2011; native code 1034.</summary>
        [Description("SANS 10162-1:2011")]
        SouthAfricanSANS10162Part1Year2011 = 1034,
        /// <summary>AASHTO LRFD; native code 1044.</summary>
        [Description("AASHTO LRFD")]
        AASHTOLRFD = 1044,
        /// <summary>ANSI/AISC 360-05; native code 1045.</summary>
        [Description("ANSI/AISC 360-05")]
        ANSIAISC360Year05 = 1045,
        /// <summary>ASME NF 3000 1989; native code 1046.</summary>
        [Description("ASME NF 3000 1989")]
        ASMENF3000Year1989 = 1046,
        /// <summary>ASME NF 3000 1998; native code 1047.</summary>
        [Description("ASME NF 3000 1998")]
        ASMENF3000Year1998 = 1047,
        /// <summary>ASME NF 3000 1974; native code 1048.</summary>
        [Description("ASME NF 3000 1974")]
        ASMENF3000Year1974 = 1048,
        /// <summary>ASME NF 3000 1977; native code 1049.</summary>
        [Description("ASME NF 3000 1977")]
        ASMENF3000Year1977 = 1049,
        /// <summary>Norway NORSOK N-004; native code 1050.</summary>
        [Description("Norway NORSOK N-004")]
        NorwayNORSOKN004 = 1050,
        /// <summary>ASME NF 3000 2004; native code 1051.</summary>
        [Description("ASME NF 3000 2004")]
        ASMENF3000Year2004 = 1051,
        /// <summary>ASME NF 3000 2001; native code 1053.</summary>
        [Description("ASME NF 3000 2001")]
        ASMENF3000Year2001 = 1053,
        /// <summary>ASCE 52; native code 1060.</summary>
        [Description("ASCE 52")]
        ASCE52 = 1060,
        /// <summary>ANSI/AISC 360-10; native code 1061.</summary>
        [Description("ANSI/AISC 360-10")]
        ANSIAISC360Year10 = 1061,
        /// <summary>Canadian S16-09; native code 1062.</summary>
        [Description("Canadian S16-09")]
        CanadianS16Year09 = 1062,
        /// <summary>Russian SP 16.13330.2011; native code 1063.</summary>
        [Description("Russian SP 16.13330.2011")]
        RussianSP16Dot13330Year2011 = 1063,
        /// <summary>SANS10162-1:1993; native code 1064.</summary>
        [Description("SANS10162-1:1993")]
        SouthAfricaSANS10162Part1Year1993 = 1064,
        /// <summary>Canadian S16-14; native code 1065.</summary>
        [Description("Canadian S16-14")]
        CanadianS16Year14 = 1065,
        /// <summary>NZS3404-1997; native code 1066.</summary>
        [Description("NZS3404-1997")]
        NZS3404Year1997 = 1066,
        /// <summary>ANSI/AISC 360-16; native code 1067.</summary>
        [Description("ANSI/AISC 360-16")]
        ANSIAISC360Year16 = 1067,
        /// <summary>AISI S100-2016; native code 1068.</summary>
        [Description("AISI S100-2016")]
        AISIS100Year2016 = 1068,
        /// <summary>Canadian S16-19; native code 1069.</summary>
        [Description("Canadian S16-19")]
        CanadianS16Year19 = 1069,
        /// <summary>AISC Castellated; native code 1102.</summary>
        [Description("AISC Castellated")]
        AISCCastellated = 1102,
        /// <summary>AISC N690 1994; native code 1202.</summary>
        [Description("AISC N690 1994")]
        AISCN690Year1994 = 1202,
        /// <summary>AISC N690 1984; native code 1204.</summary>
        [Description("AISC N690 1984")]
        AISCN690Year1984 = 1204,
        /// <summary>Japan AIJ 2005; native code 1210.</summary>
        [Description("Japan AIJ 2005")]
        JapanAIJ2005 = 1210,
        /// <summary>BS EN 1993-1-1:2005; native code 1220.</summary>
        [Description("BS EN 1993-1-1:2005")]
        BSEN1993Part1Dash1Year2005 = 1220,
        /// <summary>Russian SP 16.13330.2017; native code 1221.</summary>
        [Description("Russian SP 16.13330.2017")]
        RussianSP16Dot13330Year2017 = 1221
    }
}
