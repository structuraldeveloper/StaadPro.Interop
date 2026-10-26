using StaadPro.Interop.Enums.Output;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the complete native single-block member steel-design observation.</summary>
    /// <remarks>Reuses the main-project design field meanings without hydrating Beam/ILoadCase objects, substituting zero actions or mapping every non-PASS status to failure. Retrieval success is independent of the design decision.</remarks>
    public sealed class MemberSteelDesignResults
    {
        internal MemberSteelDesignResults(int memberId, string code, SteelDesignStatus status, string statusText, double? statusNumber, bool? statusBoolean,
            double ratio, double allowable, int loadCaseId, double position, string clause, string section, SteelDesignForces forces, double slenderness)
        { MemberId=memberId; Code=code; DesignStatus=status; NativeStatusText=statusText; NativeStatusNumber=statusNumber; NativeStatusBoolean=statusBoolean;
          UtilizationRatio=ratio; AllowableRatio=allowable; CriticalLoadCaseId=loadCaseId; CriticalSection=position; CriticalClause=clause; DesignSection=section; DesignForces=forces; SlendernessRatio=slenderness; }
        /// <summary>Gets the requested positive analytical member ID.</summary>
        public int MemberId { get; }
        /// <summary>Gets the exact native design-code text.</summary>
        public string Code { get; }
        /// <summary>Gets Passed/Failed only for explicit PASS/FAIL text; other status variants remain Unknown.</summary>
        public SteelDesignStatus DesignStatus { get; }
        /// <summary>Gets exact native status text, or null for a non-text status.</summary>
        public string NativeStatusText { get; }
        /// <summary>Gets an observed finite numeric native status, without interpreting it as a design decision; otherwise null.</summary>
        public double? NativeStatusNumber { get; }
        /// <summary>Gets an actual Boolean native status without interpreting it as a design decision; otherwise null.</summary>
        public bool? NativeStatusBoolean { get; }
        /// <summary>Gets the nonnegative native utilization ratio, independent of the PASS/FAIL classification.</summary>
        public double UtilizationRatio { get; }
        /// <summary>Gets the native nonnegative allowable ratio.</summary>
        public double AllowableRatio { get; }
        /// <summary>Gets the positive governing native load-case ID; no case is activated or hydrated.</summary>
        public int CriticalLoadCaseId { get; }
        /// <summary>Gets the nonnegative native critical station from the member start in current base length units.</summary>
        public double CriticalSection { get; }
        /// <summary>Gets exact native governing-clause text.</summary>
        public string CriticalClause { get; }
        /// <summary>Gets exact native design-section text, without catalog inference.</summary>
        public string DesignSection { get; }
        /// <summary>Gets only the three native FX/MY/MZ critical-section actions.</summary>
        public SteelDesignForces DesignForces { get; }
        /// <summary>Gets the finite native KL/R observation without recalculation.</summary>
        public double SlendernessRatio { get; }
    }
}
