namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the two native six-component static-check totals for one load case.</summary>
    /// <remarks>Loads and reactions are retained separately with native signs and reference-point
    /// conventions. No assumed addition/subtraction is labeled an equilibrium residual.</remarks>
    public sealed class StaticCheckResult
    {
        internal StaticCheckResult(double[] forces, double[] reactions)
        { Forces = new ForceComponents(forces); Reactions = new ForceComponents(reactions); }
        /// <summary>Gets native FX/FY/FZ/MX/MY/MZ applied-load totals.</summary>
        public ForceComponents Forces { get; }
        /// <summary>Gets native FX/FY/FZ/MX/MY/MZ reaction totals.</summary>
        public ForceComponents Reactions { get; }
    }
}
