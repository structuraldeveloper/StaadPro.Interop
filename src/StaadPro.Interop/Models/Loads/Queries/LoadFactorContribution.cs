namespace StaadPro.Interop.Models.Loads.Queries
{
    /// <summary>Contains one ordered repeat-load case/factor pair in native model conventions.</summary>
    /// <remarks>Duplicates and signed/zero factors are preserved. This is a snapshot, not a live case binding.</remarks>
    public sealed class LoadFactorContribution
    {
        internal LoadFactorContribution(int loadCaseId, double factor) { LoadCaseId = loadCaseId; Factor = factor; }
        /// <summary>Gets the positive contributing primary load-case number.</summary>
        public int LoadCaseId { get; }
        /// <summary>Gets the finite, dimensionless factor exactly as returned.</summary>
        public double Factor { get; }
    }
}
