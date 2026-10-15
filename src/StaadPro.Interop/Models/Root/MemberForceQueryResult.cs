using System;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains one native end-force success flag and its qualified managed record.</summary>
    /// <remarks>Identity is retained on failure. The optional Forces model is editable managed data;
    /// changes do not affect native state or the recorded success flag.</remarks>
    public sealed class MemberForceQueryResult
    {
        internal MemberForceQueryResult(int beamId, int loadCaseId, string loadCaseTitle, MemberEnd end, bool success, MemberForces forces)
        {
            if (success != (forces != null)) throw new ArgumentException("Only successful queries have force data.", nameof(forces));
            BeamId = beamId; LoadCaseId = loadCaseId; LoadCaseTitle = loadCaseTitle;
            End = end; IsSuccessful = success; Forces = forces;
        }
        /// <summary>Gets the requested beam identifier.</summary>
        /// <value>The validated positive member number.</value>
        public int BeamId { get; }
        /// <summary>Gets the requested load-case identifier.</summary>
        /// <value>The validated positive case number.</value>
        public int LoadCaseId { get; }
        /// <summary>Gets the queried native load-case title.</summary>
        /// <value>Exact title text, including empty text.</value>
        public string LoadCaseTitle { get; }
        /// <summary>Gets the requested native member end.</summary>
        /// <value>Start or End.</value>
        public MemberEnd End { get; }
        /// <summary>Gets the native end-force retrieval success flag.</summary>
        /// <value>True only after native success and six valid written Double values.</value>
        public bool IsSuccessful { get; }
        /// <summary>Gets the qualified force/moment record.</summary>
        /// <value>A non-null editable managed MemberForces on native success; null on native false, never partial data.</value>
        public MemberForces Forces { get; }
    }
}
