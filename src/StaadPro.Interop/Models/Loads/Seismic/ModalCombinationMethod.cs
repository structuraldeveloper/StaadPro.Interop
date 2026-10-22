namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies modal combination methods for Response Spectrum Analysis.
    /// </summary>
    public enum ModalCombinationMethod
    {
        /// <summary>Square Root of Sum of Squares (SRSS)</summary>
        SRSS = 0,

        /// <summary>Absolute Sum (ABS)</summary>
        ABS = 1,

        /// <summary>Complete Quadratic Combination (CQC)</summary>
        CQC = 2,

        /// <summary>ASCE 4-98 / ASCE 43-05 Combination Rule</summary>
        ASCE = 3,

        /// <summary>Ten Percent Combination Rule (TEN)</summary>
        TEN = 4,

        /// <summary>Closest Search Method (CSM)</summary>
        CSM = 5,

        /// <summary>Grouping Method (GRP)</summary>
        GRP = 6
    }
}
