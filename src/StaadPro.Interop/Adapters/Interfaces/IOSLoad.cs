using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Contract for performing Load operations and load case management via OpenSTAAD.
    /// </summary>
    public interface IOSLoad : IOSBase
    {
        #region Load Case Clearing

        /// <summary>
        /// Clears a primary load case from the active model.
        /// </summary>
        /// <param name="loadCase">The primary load case to clear.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load case is reference load.</param>
        /// <returns>True if the load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad = false);

        /// <summary>
        /// Clears multiple primary load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of primary load cases to clear.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad = false);

        /// <summary>
        /// Clears primary load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of primary load case numbers.</param>
        /// <param name="isReferenceLoad">Flag indicating if the load cases are reference loads.</param>
        /// <returns>True if the load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad = false);

        /// <summary>
        /// Clears a primary load case from the active model.
        /// </summary>
        /// <param name="loadCase">The primary load case to clear.</param>
        /// <returns>True if the primary load case was cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCase(ILoadCase loadCase);

        /// <summary>
        /// Clears multiple primary load cases from the active model.
        /// </summary>
        /// <param name="loadCases">Collection of primary load cases to clear.</param>
        /// <returns>True if the primary load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases);

        /// <summary>
        /// Clears primary load cases by their integer identifiers.
        /// </summary>
        /// <param name="lcIds">Collection of primary load case numbers.</param>
        /// <returns>True if the primary load cases were cleared successfully; otherwise false.</returns>
        bool ClearPrimaryLoadCases(IEnumerable<int> lcIds);

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

        #region Support Settlement

        /// <summary>
        /// Applies support settlement/displacement to a single node by node ID.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodeId">Target node ID.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to a single node.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="node">Target node entity.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to multiple nodes.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodes">Target node entities.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement);

        /// <summary>
        /// Applies support settlement/displacement to multiple nodes by node IDs.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodeIds">Target node IDs.</param>
        /// <param name="direction">Settlement direction (Fx, Fy, Fz, Mx, My, Mz).</param>
        /// <param name="mmSettlement">Settlement displacement in millimeters.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement);

        #endregion

        #region Batch Load Creation

        /// <summary>
        /// Creates a batch of primary load cases in the active STAAD model.
        /// </summary>
        /// <param name="primaryLoadCases">Collection of primary load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases);

        /// <summary>
        /// Creates a batch of primary load cases in the active STAAD model.
        /// </summary>
        /// <param name="primaryLoadCases">Sequence of primary load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases);

        /// <summary>
        /// Creates a batch of reference load cases in the active STAAD model.
        /// </summary>
        /// <param name="referenceLoads">Collection of reference load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads);

        /// <summary>
        /// Creates a batch of reference load cases in the active STAAD model.
        /// </summary>
        /// <param name="referenceLoads">Sequence of reference load cases to create.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads);

        #endregion

        #region Nodal Loading

        /// <summary>
        /// Applies a concentrated force and/or moment to a single node.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="node">Target node entity.</param>
        /// <param name="nl">Nodal load definition containing 6-DOF forces and moments.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl);

        /// <summary>
        /// Applies a concentrated force and/or moment to multiple nodes.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="nodes">Target node entities.</param>
        /// <param name="nl">Nodal load definition containing 6-DOF forces and moments.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl);

        #endregion

        #region Member Loading

        /// <summary>
        /// Applies a member load (uniformly distributed load or concentrated point load) to a single beam element.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="beam">Target beam entity.</param>
        /// <param name="ml">Member load definition.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml);

        /// <summary>
        /// Applies a member load (uniformly distributed load or concentrated point load) to multiple beam elements.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="beams">Collection of target beam entities.</param>
        /// <param name="ml">Member load definition.</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml);

        #endregion

        #region Plate Pressure Loading

        /// <summary>
        /// Applies a uniform surface pressure load to a single plate element by plate ID.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plateId">Target plate element ID.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <summary>
        /// Applies a uniform surface pressure load to multiple plate elements by their integer IDs.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plateIds">Collection of target plate IDs.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        /// <summary>
        /// Applies a uniform surface pressure load to multiple plate entities.
        /// </summary>
        /// <param name="lc">Target load case.</param>
        /// <param name="plates">Collection of target Plate entities.</param>
        /// <param name="pressure">Pressure magnitude (force per unit area).</param>
        /// <param name="direction">Direction of pressure application (LocalZ, GlobalX, GlobalY, GlobalZ).</param>
        /// <returns>The current <see cref="IOSLoad"/> instance for fluent chaining.</returns>
        IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ);

        #endregion
    }
}
