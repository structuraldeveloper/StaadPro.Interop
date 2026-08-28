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
            ComObject = wrapper?.RawLoad;
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
                throw new NotSupportedException($"Load case type '{lc.CaseType}' is not supported for direct creation.");
            }

            return this;
        }

        #endregion
    }
}
