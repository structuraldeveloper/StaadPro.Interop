namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Specifies modal combination type for AddRSLoad.
    /// </summary>
    public enum ResponseSpectrumLoadType
    {
        /// <summary>Square Root of Sum of Squares (SRSS = 1)</summary>
        Srss = 1,

        /// <summary>Complete Quadratic Combination (CQC = 2)</summary>
        Cqc = 2,

        /// <summary>Absolute Sum (ABS = 3)</summary>
        Absolute = 3,

        /// <summary>ASCE Method (ASCE = 4)</summary>
        Asce = 4,

        /// <summary>Ten Percent Method (TEN = 5)</summary>
        Ten = 5,

        /// <summary>Closest Search Method (CSM = 6)</summary>
        Csm = 6
    }
}
