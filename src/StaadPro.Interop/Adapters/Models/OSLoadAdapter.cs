using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>
    /// Managed adapter for STAAD.Pro OpenSTAAD Load operations wrapping the underlying COM object.
    /// </summary>
    public class OSLoadAdapter : OSBaseAdapter, IOSLoad
    {
        public OSLoadAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            ComObject = wrapper.RawLoad ?? throw new ArgumentNullException(nameof(wrapper.RawLoad), "STAAD Load COM object is not initialized.");
        }

        /// <summary>
        /// Gets the underlying COM load object.
        /// </summary>
        public dynamic ComObject { get; }

        #region Load Case Clearing

        public bool ClearPrimaryLoadCase(ILoadCase loadCase, bool isReferenceLoad) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase }, isReferenceLoad);

        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases, bool isReferenceLoad)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id), isReferenceLoad);
        }

        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds, bool isReferenceLoad)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, isReferenceLoad);
            return result != null && (int)result == 1;
        }

        public bool ClearPrimaryLoadCase(ILoadCase loadCase) =>
            ClearPrimaryLoadCases(new List<ILoadCase> { loadCase });

        public bool ClearPrimaryLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> primaryLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.PrimaryLoad);
            return ClearPrimaryLoadCases(primaryLcs.Select(lc => lc.Id));
        }

        public bool ClearPrimaryLoadCases(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearPrimaryLoadCase(ids, false);
            return result != null && (int)result == 1;
        }

        public bool ClearReferenceLoadCase(ILoadCase loadCase) =>
            ClearReferenceLoadCases(new List<ILoadCase> { loadCase });

        public bool ClearReferenceLoadCases(IEnumerable<ILoadCase> loadCases)
        {
            if (loadCases == null) throw new ArgumentNullException(nameof(loadCases));
            IEnumerable<ILoadCase> referenceLcs = loadCases.Where(lc => lc != null && lc.CaseType == LoadCaseType.ReferenceLoad);
            return ClearReferenceLoadCases(referenceLcs.Select(lc => lc.Id));
        }

        public bool ClearReferenceLoadCases(IEnumerable<int> lcIds)
        {
            if (lcIds == null) throw new ArgumentNullException(nameof(lcIds));
            int[] ids = lcIds.ToArray();
            if (ids.Length == 0) return true;
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.ClearReferenceLoadCase(ids);
            return result != null && (int)result == 1;
        }

        #endregion

        #region Load Case Creation & Titles

        public int CreateNewPrimaryLoad(string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx(lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        public int CreateNewPrimaryLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewPrimaryLoadEx2(lcTitle, (int)loadType, lcId);
            return Convert.ToInt32(result);
        }

        public int CreateNewPrimaryLoadEx(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Title, lc.Type);
        }

        public int CreateNewPrimaryLoadEx2(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewPrimaryLoad(lc.Id, lc.Title, lc.Type);
        }

        public int CreateNewReferenceLoad(int lcId, string lcTitle, LoadType loadType)
        {
            if (string.IsNullOrEmpty(lcTitle)) throw new ArgumentNullException(nameof(lcTitle));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.CreateNewReferenceLoad(lcId, lcTitle, (int)loadType);
            return Convert.ToInt32(result);
        }

        public int CreateNewReferenceLoad(LoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            return CreateNewReferenceLoad(lc.Id, lc.Title, lc.Type);
        }

        public string GetLoadCaseTitle(int id)
        {
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");
            dynamic result = ComObject.GetLoadCaseTitle(id);
            return result != null ? result.ToString() : string.Empty;
        }

        public IOSLoad SetLoadCaseActive(ILoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            if (lc.CaseType == LoadCaseType.ReferenceLoad)
            {
                ComObject.SetReferenceLoadActive(lc.Id);
            }
            else if (lc.CaseType == LoadCaseType.PrimaryLoad)
            {
                ComObject.SetLoadActive(lc.Id);
            }

            return this;
        }

        public IOSLoad CreateNewLoadCase(ILoadCase lc)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            if (lc.CaseType == LoadCaseType.ReferenceLoad)
            {
                CreateNewReferenceLoad(lc.Id, lc.Title, lc.Type);
            }
            else if (lc.CaseType == LoadCaseType.PrimaryLoad)
            {
                if (lc.Id > 0)
                {
                    CreateNewPrimaryLoad(lc.Id, lc.Title, lc.Type);
                }
                else
                {
                    CreateNewPrimaryLoad(lc.Title, lc.Type);
                }
            }
            else
            {
                throw new NotSupportedException($"Load case type '{lc.CaseType}' is not supported for creation via OpenSTAAD.");
            }

            return this;
        }

        #endregion

        #region Support Settlement

        public IOSLoad AddSupportSettlement(ILoadCase lc, int nodeId, SettlementDirection direction, double mmSettlement)
        {
            return AddSupportSettlement(lc, new List<int> { nodeId }, direction, mmSettlement);
        }

        public IOSLoad AddSupportSettlement(ILoadCase lc, Node node, SettlementDirection direction, double mmSettlement)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddSupportSettlement(lc, new List<int> { node.Id }, direction, mmSettlement);
        }

        public IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<Node> nodes, SettlementDirection direction, double mmSettlement)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            return AddSupportSettlement(lc, nodes.Select(n => n.Id), direction, mmSettlement);
        }

        public IOSLoad AddSupportSettlement(ILoadCase lc, IEnumerable<int> nodeIds, SettlementDirection direction, double mmSettlement)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (nodeIds == null) throw new ArgumentNullException(nameof(nodeIds));

            SetLoadCaseActive(lc);
            int[] ids = nodeIds.ToArray();
            if (ids.Length > 0 && ComObject != null)
            {
                ComObject.AddSupportDisplacement(ids, (int)direction, mmSettlement / 1000.0);
            }

            return this;
        }

        #endregion

        #region Batch Load Creation

        public IOSLoad CreatePrimaryLoadCases(HashSet<ILoadCase> primaryLoadCases) =>
            CreatePrimaryLoadCases((IEnumerable<ILoadCase>)primaryLoadCases);

        public IOSLoad CreatePrimaryLoadCases(IEnumerable<ILoadCase> primaryLoadCases)
        {
            if (primaryLoadCases != null)
            {
                foreach (ILoadCase lc in primaryLoadCases)
                {
                    if (lc is LoadCase pl)
                    {
                        CreateNewPrimaryLoad(pl.Title, pl.Type);
                    }
                    else if (lc != null)
                    {
                        CreateNewPrimaryLoad(lc.Title, LoadType.Dead);
                    }
                }
            }

            return this;
        }

        public IOSLoad CreateReferenceLoadCases(HashSet<ILoadCase> referenceLoads) =>
            CreateReferenceLoadCases((IEnumerable<ILoadCase>)referenceLoads);

        public IOSLoad CreateReferenceLoadCases(IEnumerable<ILoadCase> referenceLoads)
        {
            if (referenceLoads != null)
            {
                foreach (ILoadCase lc in referenceLoads)
                {
                    if (lc is LoadCase rl)
                    {
                        CreateNewReferenceLoad(rl.Id, rl.Title, rl.Type);
                    }
                    else if (lc != null)
                    {
                        CreateNewReferenceLoad(lc.Id, lc.Title, LoadType.Dead);
                    }
                }
            }

            return this;
        }

        #endregion

        #region Nodal Loading

        public IOSLoad AddNodalLoad(ILoadCase lc, Node node, NodalLoad nl)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            return AddNodalLoad(lc, new[] { node }, nl);
        }

        public IOSLoad AddNodalLoad(ILoadCase lc, IEnumerable<Node> nodes, NodalLoad nl)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));
            if (nl == null) throw new ArgumentNullException(nameof(nl));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] nodeIds = nodes.Where(n => n != null).Select(n => n.Id).ToArray();
            if (nodeIds.Length > 0 && nl.Forces != null)
            {
                ComObject.AddNodalLoad(nodeIds, nl.Forces.Fx, nl.Forces.Fy, nl.Forces.Fz, nl.Forces.Mx, nl.Forces.My, nl.Forces.Mz);
            }

            return this;
        }

        #endregion

        #region Member Loading

        public IOSLoad AddMemberLoad(ILoadCase lc, Beam beam, MemberLoad ml)
        {
            if (beam == null) throw new ArgumentNullException(nameof(beam));
            return AddMemberLoad(lc, new[] { beam }, ml);
        }

        public IOSLoad AddMemberLoad(ILoadCase lc, IEnumerable<Beam> beams, MemberLoad ml)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (beams == null) throw new ArgumentNullException(nameof(beams));
            if (ml == null) throw new ArgumentNullException(nameof(ml));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] beamIds = beams.Where(b => b != null).Select(b => b.Id).ToArray();
            if (beamIds.Length > 0)
            {
                ml.ApplyTo(ComObject, beamIds);
            }

            return this;
        }

        #endregion

        #region Plate Pressure Loading

        public IOSLoad AddPlateUniformPressure(ILoadCase lc, int plateId, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            return AddPlateUniformPressure(lc, new[] { plateId }, pressure, direction);
        }

        public IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<int> plateIds, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            if (lc == null) throw new ArgumentNullException(nameof(lc));
            if (plateIds == null) throw new ArgumentNullException(nameof(plateIds));
            if (ComObject == null) throw new InvalidOperationException("STAAD Load COM object is not initialized.");

            SetLoadCaseActive(lc);
            int[] ids = plateIds.ToArray();
            if (ids.Length > 0)
            {
                ComObject.AddElementPressure(ids, (int)direction, pressure);
            }

            return this;
        }

        public IOSLoad AddPlateUniformPressure(ILoadCase lc, IEnumerable<Plate> plates, double pressure, LoadDirection direction = LoadDirection.LocalZ)
        {
            if (plates == null) throw new ArgumentNullException(nameof(plates));
            return AddPlateUniformPressure(lc, plates.Where(p => p != null).Select(p => p.Id), pressure, direction);
        }

        #endregion
    }
}
