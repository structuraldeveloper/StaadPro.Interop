namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies international seismic design codes supported by OpenSTAAD for Seismic Definitions.
    /// </summary>
    /// <remarks>Definition codes only; do not cast these values into member-weight codes. NRC 2020 requires a reviewed 2025/2026 native profile.</remarks>
    public enum SeismicCode
    {
        /// <summary>UBC 1985 (Uniform Building Code 1985)</summary>
        UBC1985 = 0,

        /// <summary>UBC 1994 (Uniform Building Code 1994)</summary>
        UBC1994 = 1,

        /// <summary>UBC 1997 (Uniform Building Code 1997)</summary>
        UBC1997 = 2,

        /// <summary>Indian Standard IS 1893-1984</summary>
        IS1893_1984 = 3,

        /// <summary>Indian Standard IS 1893-2002/2005 (Part 1)</summary>
        IS1893_2002 = 4,

        /// <summary>International Building Code IBC 2000</summary>
        IBC2000 = 5,

        /// <summary>International Building Code IBC 2003</summary>
        IBC2003 = 6,

        /// <summary>Colombian NSR 98</summary>
        NSR98 = 7,

        /// <summary>Japanese Architectural Institute of Japan (AIJ)</summary>
        AIJ = 8,

        /// <summary>Algerian RPA 99</summary>
        RPA = 9,

        /// <summary>Mexican CFE 1993</summary>
        CFE1993 = 10,

        /// <summary>Mexican NTC 1987</summary>
        NTC1987 = 11,

        /// <summary>Indian Standard IS 1893-2016 (Part 1 Criteria for Earthquake Resistant Design)</summary>
        IS1893_2016 = 12,

        /// <summary>Indian Standard IS 1893 (Part 4) 2015 (Industrial Structures)</summary>
        IS1893_Part4_2015 = 13,

        /// <summary>International Building Code IBC 2006 / ASCE 7-05</summary>
        IBC2006 = 14,

        /// <summary>International Building Code IBC 2012 / ASCE 7-10</summary>
        IBC2012 = 15,

        /// <summary>International Building Code IBC 2015 / ASCE 7-10</summary>
        IBC2015 = 16,

        /// <summary>International Building Code IBC 2018 / ASCE 7-16</summary>
        IBC2018 = 17,

        /// <summary>National Building Code of Canada NRC 2005</summary>
        NRC2005 = 18,

        /// <summary>National Building Code of Canada NRC 2010</summary>
        NRC2010 = 19,

        /// <summary>National Building Code of Canada NRC 1995</summary>
        NRC1995 = 20,

        /// <summary>Colombian NSR 2010</summary>
        NSR2010 = 21,

        /// <summary>Chinese GB 50011-2001 (Code for Seismic Design of Buildings)</summary>
        GB50011_2001 = 22,

        /// <summary>Chinese GB 50011-2010 (Code for Seismic Design of Buildings)</summary>
        GB50011_2010 = 23,

        /// <summary>Turkish Earthquake Code</summary>
        Turkish = 24,

        /// <summary>National Building Code of Canada NRC 2020</summary>
        NRC2020 = 25
    }
}
