namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Contains the documented values for AdinaExportStatus.</summary>
    public enum AdinaExportStatus
    {
        /// <summary>Native export success. Value 0.</summary>
        Succeeded = 0,

        /// <summary>Native export success with warnings. Value -14501.</summary>
        SucceededWithWarnings = -14501,

        /// <summary>Native export failure. Value -14502.</summary>
        Failed = -14502,

        /// <summary>No model is open. Value -101.</summary>
        NoOpenModel = -101
    }
}
