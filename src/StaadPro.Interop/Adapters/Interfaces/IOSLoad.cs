using System.Collections.Generic;
using StaadPro.Interop.Entities;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Managed contract for STAAD.Pro OpenSTAAD Load operations.
    /// </summary>
    public interface IOSLoad : IOSBase
    {
        /// <summary>
        /// Clears a primary load case from the active model.
        /// </summary>
        /// <param name="loadCase">The load case to clear.</param>
        /// <returns>True if the load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase);

        /// <summary>
        /// Clears a primary load case from the active model with explicit reference load flag.
        /// </summary>
        /// <param name="loadCase">The load case to clear.</param>
        /// <param name="isReferenceLoad">True if the load case is a reference load.</param>
        /// <returns>True if the load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad);

        /// <summary>
        /// Clears multiple primary load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of load cases to clear.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <summary>
        /// Clears multiple primary load cases from the active model with explicit reference load flag.
        /// </summary>
        /// <param name="loadCases">Collection of load cases to clear.</param>
        /// <param name="isReferenceLoad">True if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad);

        /// <summary>
        /// Clears primary load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of load case numbers.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds);

        /// <summary>
        /// Clears primary load cases by their integer identifiers with explicit reference load flag.
        /// </summary>
        /// <param name="lcIds">Collection of load case numbers.</param>
        /// <param name="isReferenceLoad">True if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad);

        /// <summary>
        /// Clears a reference load case from the active model.
        /// </summary>
        /// <param name="loadCase">The reference load case to clear.</param>
        /// <returns>True if the reference load case was cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCase(ILoadCase loadCase);

        /// <summary>
        /// Clears multiple reference load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of reference load cases to clear.</param>
        /// <returns>True if the reference load cases were cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <summary>
        /// Clears reference load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of reference load case numbers.</param>
        /// <returns>True if the reference load cases were cleared successfully; otherwise false.</returns>
        bool ClearReferenceLoadCases(IEnumerable<int> lcIds);
    }
}
