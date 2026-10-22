namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies the member weight distribution type for seismic definition member loading.
    /// </summary>
    public enum SeismicMemberLoadType
    {
        /// <summary>Uniformly distributed member weight (UNI)</summary>
        Uniform = 1,

        /// <summary>Concentrated member weight (CON)</summary>
        Concentrated = 2
    }
}
