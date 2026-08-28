using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Managed contract for STAAD.Pro OpenSTAAD Load operations.
    /// </summary>
    public interface IOSLoad : IOSBase
    {
        #region Load Case Clearing

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

        #endregion

        #region Load Case Creation & Titles

        /// <summary>
        /// Creates a new primary load case in the STAAD model with automatic ID assignment.
        /// </summary>
        /// <param name="lcTitle">Descriptive title of the primary load case.</param>
        /// <param name="loadType">Engineering load type category (Dead, Live, Wind, etc.).</param>
        /// <returns>The assigned primary load case ID number.</returns>
        int CreateNewPrimaryLoad(string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new primary load case in the STAAD model with explicit ID assignment.
        /// </summary>
        /// <param name="lcId">Explicit load case identifier number.</param>
        /// <param name="lcTitle">Descriptive title of the primary load case.</param>
        /// <param name="loadType">Engineering load type category (Dead, Live, Wind, etc.).</param>
        /// <returns>The created primary load case ID number.</returns>
        int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new primary load case from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing title and load type.</param>
        /// <returns>The assigned primary load case ID number.</returns>
        int CreateNewPrimaryLoadEx(LoadCase lc);

        /// <summary>
        /// Creates a new primary load case with explicit ID from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing explicit ID, title, and load type.</param>
        /// <returns>The created primary load case ID number.</returns>
        int CreateNewPrimaryLoadEx2(LoadCase lc);

        /// <summary>
        /// Creates a new reference load case in the STAAD model.
        /// </summary>
        /// <param name="lcId">Explicit reference load case identifier number.</param>
        /// <param name="lcTitle">Descriptive title of the reference load case.</param>
        /// <param name="loadType">Engineering load type category.</param>
        /// <returns>The created reference load case ID number.</returns>
        int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType);

        /// <summary>
        /// Creates a new reference load case from a strongly-typed <see cref="LoadCase"/> definition.
        /// </summary>
        /// <param name="lc">Load case definition containing ID, title, and load type.</param>
        /// <returns>The created reference load case ID number.</returns>
        int CreateNewReferenceLoad(LoadCase lc);

        /// <summary>
        /// Retrieves the title or description of a load case by its integer identifier.
        /// </summary>
        /// <param name="id">Load case ID.</param>
        /// <returns>The string title of the load case.</returns>
        string GetLoadCaseTitle(int id);

        /// <summary>
        /// Activates the specified load case in the STAAD model for subsequent load application or querying.
        /// </summary>
        /// <param name="lc">The load case to activate.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad SetLoadCaseActive(ILoadCase lc);

        /// <summary>
        /// Creates a new load case (primary or reference) based on the case type of the provided <see cref="ILoadCase"/>.
        /// </summary>
        /// <param name="lc">The load case to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateNewLoadCase(ILoadCase lc);

        #endregion
    }
}
