using StaadPro.Interop.Enums.Output;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains only the fields returned by the named-parameter-block steel-design native query.</summary>
    /// <remarks>Unlike MemberSteelDesignResults, this packet has no station, actions or slenderness. Those unavailable quantities are not synthesized as zero. Retrieval success is separate from PASS/FAIL.</remarks>
    public sealed class ParameterBlockSteelDesignResults
    {
        internal ParameterBlockSteelDesignResults(int value0, string value1, string value2, SteelDesignStatus value3, string value4, double value5, double value6, int value7, string value8, string value9)
        { MemberId=value0; ParameterBlockName=value1; Code=value2; DesignStatus=value3; NativeStatusText=value4; UtilizationRatio=value5; AllowableRatio=value6; CriticalLoadCaseId=value7; CriticalClause=value8; DesignSection=value9; }
        /// <summary>Gets the requested positive analytical member ID.</summary>
        public int MemberId { get; }
        /// <summary>Gets the exact requested block name, without trimming or inferred normalization.</summary>
        public string ParameterBlockName { get; }
        /// <summary>Gets the exact native design-code text.</summary>
        public string Code { get; }
        /// <summary>Gets the passed/Failed only for explicit PASS/FAIL text; otherwise Unknown.</summary>
        public SteelDesignStatus DesignStatus { get; }
        /// <summary>Gets the exact native design status text, including unknown text.</summary>
        public string NativeStatusText { get; }
        /// <summary>Gets the finite nonnegative native utilization ratio.</summary>
        public double UtilizationRatio { get; }
        /// <summary>Gets the finite nonnegative native allowable ratio.</summary>
        public double AllowableRatio { get; }
        /// <summary>Gets the positive native governing load-case ID, not hydrated or activated.</summary>
        public int CriticalLoadCaseId { get; }
        /// <summary>Gets the exact native governing clause text.</summary>
        public string CriticalClause { get; }
        /// <summary>Gets the exact native design-section name.</summary>
        public string DesignSection { get; }
    }
}
