namespace StaadPro.Interop.Models.Output
{
    /// <summary>Reports staged ratio reads and whether they were applied to caller beam objects.</summary>
    /// <remarks>Ordinary failed observations prevent every managed update. A binding or PropertyChanged/setter exception can still interrupt the apply phase after earlier managed changes; there is no rollback. No STAAD model is written.</remarks>
    public sealed class OutputPopulationResult
    {
        internal OutputPopulationResult(OutputBatchResult<double> reads, bool applied)
        {
            Observations = reads;
            WasApplied = applied;
        }
        /// <summary>Gets every staged identified ratio read, including native failures/sentinels.</summary>
        public OutputBatchResult<double> Observations
        {
            get;
        }
        /// <summary>Gets whether all staged reads succeeded; an empty input is successful with no application.</summary>
        public bool IsSuccessful => Observations.IsSuccessful;
        /// <summary>Gets whether a nonempty fully qualified set was applied before return.</summary>
        public bool WasApplied
        {
            get;
        }
        /// <summary>Gets the number of beam objects updated when the apply phase completed.</summary>
        public int AppliedCount => WasApplied ? Observations.Items.Count : 0;
    }
}
