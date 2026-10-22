namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies supported international design codes for OpenSTAAD Response Spectrum Load Ex (AddResponseSpectrumLoadEx).
    /// </summary>
    public enum ResponseSpectrumCode
    {
        /// <summary>Generic custom response spectrum curve</summary>
        GenericCustom = 0,

        /// <summary>Indian Standard IS 1893 (Part 1): 2002</summary>
        IS1893_2002 = 1,

        /// <summary>Indian Standard IS 1893 (Part 1): 2016</summary>
        IS1893_2016 = 2,

        /// <summary>Eurocode ENV 1998-1:1994</summary>
        Eurocode8_1994 = 4,

        /// <summary>Eurocode EN 1998-1:2004</summary>
        Eurocode8_2004 = 5,

        /// <summary>International Building Code IBC 2006</summary>
        IBC2006 = 6,

        /// <summary>International Building Code IBC 2012</summary>
        IBC2012 = 7,

        /// <summary>International Building Code IBC 2015</summary>
        IBC2015 = 8,

        /// <summary>Russian SNiP II-7-81</summary>
        SNiP_II_7_81 = 10,

        /// <summary>Russian SP 14.13330.2011</summary>
        SP_14_13330_2011 = 11,

        /// <summary>Canadian NRC 2005</summary>
        NRC2005 = 12,

        /// <summary>Canadian NRC 2010</summary>
        NRC2010 = 13,

        /// <summary>Chinese GB 50011-2010</summary>
        GB50011_2010 = 14
    }
}
