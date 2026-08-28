using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSLoadAdapterTests
    {
        public class MockStaadLoadCom
        {
            public List<int> ClearedPrimaryLoadCases { get; } = new List<int>();
            public List<int> ClearedReferenceLoadCases { get; } = new List<int>();
            public bool LastIsReferenceLoadFlag { get; private set; }

            public List<(string Title, int Type)> CreatedPrimaryLoads { get; } = new List<(string, int)>();
            public List<(int Id, string Title, int Type)> CreatedPrimaryLoadsEx2 { get; } = new List<(int, string, int)>();
            public List<(int Id, string Title, int Type)> CreatedReferenceLoads { get; } = new List<(int, string, int)>();
            public Dictionary<int, string> LoadCaseTitles { get; } = new Dictionary<int, string>();
            public int LastActiveLoadCaseId { get; private set; }
            public int LastActiveRefLoadCaseId { get; private set; }

            public int ClearPrimaryLoadCase(object lcIds, object isRefLoad)
            {
                int[] ids = (int[])lcIds;
                ClearedPrimaryLoadCases.AddRange(ids);
                LastIsReferenceLoadFlag = Convert.ToBoolean(isRefLoad);
                return 1;
            }

            public int ClearReferenceLoadCase(object lcIds)
            {
                int[] ids = (int[])lcIds;
                ClearedReferenceLoadCases.AddRange(ids);
                return 1;
            }

            public int CreateNewPrimaryLoadEx(object lcTitle, object loadType)
            {
                int assignedId = CreatedPrimaryLoads.Count + 1;
                CreatedPrimaryLoads.Add((lcTitle.ToString(), Convert.ToInt32(loadType)));
                LoadCaseTitles[assignedId] = lcTitle.ToString();
                return assignedId;
            }

            public int CreateNewPrimaryLoadEx2(object lcTitle, object loadType, object lcId)
            {
                int id = Convert.ToInt32(lcId);
                CreatedPrimaryLoadsEx2.Add((id, lcTitle.ToString(), Convert.ToInt32(loadType)));
                LoadCaseTitles[id] = lcTitle.ToString();
                return id;
            }

            public int CreateNewReferenceLoad(object lcId, object lcTitle, object loadType)
            {
                int id = Convert.ToInt32(lcId);
                CreatedReferenceLoads.Add((id, lcTitle.ToString(), Convert.ToInt32(loadType)));
                LoadCaseTitles[id] = lcTitle.ToString();
                return id;
            }

            public object GetLoadCaseTitle(object id)
            {
                int key = Convert.ToInt32(id);
                return LoadCaseTitles.TryGetValue(key, out string title) ? title : $"LOAD_CASE_{key}";
            }

            public void SetLoadActive(object lcId)
            {
                LastActiveLoadCaseId = Convert.ToInt32(lcId);
            }

            public void SetReferenceLoadActive(object lcId)
            {
                LastActiveRefLoadCaseId = Convert.ToInt32(lcId);
            }
        }

        [Test]
        public void IOSLoad_ExposesAllRequiredInterfaceMembers()
        {
            string[] requiredMethods =
            {
                "ClearPrimaryLoadCase",
                "ClearPrimaryLoadCases",
                "ClearReferenceLoadCase",
                "ClearReferenceLoadCases",
                "CreateNewPrimaryLoad",
                "CreateNewPrimaryLoadEx",
                "CreateNewPrimaryLoadEx2",
                "CreateNewReferenceLoad",
                "GetLoadCaseTitle",
                "SetLoadCaseActive",
                "CreateNewLoadCase"
            };

            var methods = typeof(IOSLoad).GetMethods().Select(m => m.Name).ToHashSet();

            foreach (var req in requiredMethods)
            {
                Assert.IsTrue(methods.Contains(req), $"Required method '{req}' is absent from IOSLoad.");
            }
        }

        [Test]
        public void OSLoadAdapter_ClearPrimaryLoadCase_InvokesComMethodSuccessfully()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            var lc = new LoadCase(101, "DL_1", LoadCaseType.PrimaryLoad);
            bool result = loadAdapter.ClearPrimaryLoadCase(lc);

            Assert.IsTrue(result);
            Assert.Contains(101, mockCom.ClearedPrimaryLoadCases);
            Assert.IsFalse(mockCom.LastIsReferenceLoadFlag);
        }

        [Test]
        public void OSLoadAdapter_ClearReferenceLoadCase_InvokesComMethodSuccessfully()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            var refLc = new LoadCase(201, "REF_DEAD", LoadCaseType.ReferenceLoad);
            bool result = loadAdapter.ClearReferenceLoadCase(refLc);

            Assert.IsTrue(result);
            Assert.Contains(201, mockCom.ClearedReferenceLoadCases);
        }

        [Test]
        public void OSLoadAdapter_CreateNewPrimaryLoad_CreatesPrimaryLoadViaCom()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            int lcId = loadAdapter.CreateNewPrimaryLoad("LIVE_LOAD_ROOF", LoadType.RoofLive);

            Assert.AreEqual(1, lcId);
            Assert.AreEqual(1, mockCom.CreatedPrimaryLoads.Count);
            Assert.AreEqual("LIVE_LOAD_ROOF", mockCom.CreatedPrimaryLoads[0].Title);
            Assert.AreEqual((int)LoadType.RoofLive, mockCom.CreatedPrimaryLoads[0].Type);
        }

        [Test]
        public void OSLoadAdapter_CreateNewPrimaryLoad_WithExplicitId_CreatesPrimaryLoad()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            int lcId = loadAdapter.CreateNewPrimaryLoad(10, "WIND_X_LOAD", LoadType.Wind);

            Assert.AreEqual(10, lcId);
            Assert.AreEqual(1, mockCom.CreatedPrimaryLoadsEx2.Count);
            Assert.AreEqual(10, mockCom.CreatedPrimaryLoadsEx2[0].Id);
            Assert.AreEqual("WIND_X_LOAD", mockCom.CreatedPrimaryLoadsEx2[0].Title);
        }

        [Test]
        public void OSLoadAdapter_CreateNewReferenceLoad_CreatesReferenceLoadViaCom()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            int refId = loadAdapter.CreateNewReferenceLoad(101, "EQUIPMENT_DEAD", LoadType.Dead);

            Assert.AreEqual(101, refId);
            Assert.AreEqual(1, mockCom.CreatedReferenceLoads.Count);
            Assert.AreEqual(101, mockCom.CreatedReferenceLoads[0].Id);
            Assert.AreEqual("EQUIPMENT_DEAD", mockCom.CreatedReferenceLoads[0].Title);
        }

        [Test]
        public void OSLoadAdapter_GetLoadCaseTitle_ReturnsTitleFromCom()
        {
            var mockCom = new MockStaadLoadCom();
            mockCom.LoadCaseTitles[5] = "SEISMIC_SPECTRUM_X";
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            string title = loadAdapter.GetLoadCaseTitle(5);

            Assert.AreEqual("SEISMIC_SPECTRUM_X", title);
        }

        [Test]
        public void OSLoadAdapter_SetLoadCaseActive_ActivatesCorrectType()
        {
            var mockCom = new MockStaadLoadCom();
            var wrapper = new OpenStaadWrapper(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = wrapper.Load;

            var primaryLc = new LoadCase(12, "LIVE", LoadCaseType.PrimaryLoad);
            var refLc = new LoadCase(102, "REF_LIVE", LoadCaseType.ReferenceLoad);

            loadAdapter.SetLoadCaseActive(primaryLc);
            Assert.AreEqual(12, mockCom.LastActiveLoadCaseId);

            loadAdapter.SetLoadCaseActive(refLc);
            Assert.AreEqual(102, mockCom.LastActiveRefLoadCaseId);
        }
    }
}
