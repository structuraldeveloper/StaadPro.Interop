namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies damping type for Response Spectrum Analysis.
    /// </summary>
    public enum SpectrumDampingType
    {
        /// <summary>Modal damping (DAMP = 1)</summary>
        Damp = 1,

        /// <summary>Composite damping (CDAMP = 2)</summary>
        Cdamp = 2,

        /// <summary>Material damping (MDAMP = 3)</summary>
        Mdamp = 3
    }
}
