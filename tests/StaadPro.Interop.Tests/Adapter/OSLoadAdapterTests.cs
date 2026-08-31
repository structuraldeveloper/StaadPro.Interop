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
        public class MockStaadRootCom
        {
            public MockStaadRootCom(MockStaadLoadCom loadCom = null)
            {
                Geometry = new MockStaadGeomCom();
                Load = loadCom ?? new MockStaadLoadCom();
            }

            public MockStaadGeomCom Geometry { get; }
            public MockStaadLoadCom Load { get; }

            public int GetBaseUnit() => 2;
            public int GetInputUnitForForce(string key) => 5;
            public int GetInputUnitForLength(string key) => 4;
        }

        public class MockStaadGeomCom
        {
            public int GetNodeCount() => 10;
        }

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
            public List<(int[] NodeIds, int Direction, double Displacement)> SupportDisplacements { get; } = new List<(int[], int, double)>();

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

            public void AddSupportDisplacement(object nodeIds, object direction, object displacement)
            {
                SupportDisplacements.Add(((int[])nodeIds, Convert.ToInt32(direction), Convert.ToDouble(displacement)));
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
                "CreateNewLoadCase",
                "AddSupportSettlement",
                "CreatePrimaryLoadCases",
                "CreateReferenceLoadCases"
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
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var lc = new LoadCase(101, "DL_1", LoadCaseType.PrimaryLoad);
                bool result = loadAdapter.ClearPrimaryLoadCase(lc);

                Assert.IsTrue(result);
                Assert.Contains(101, mockCom.ClearedPrimaryLoadCases);
                Assert.IsFalse(mockCom.LastIsReferenceLoadFlag);
            }
        }

        [Test]
        public void OSLoadAdapter_ClearReferenceLoadCase_InvokesComMethodSuccessfully()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var refLc = new LoadCase(201, "REF_DEAD", LoadCaseType.ReferenceLoad);
                bool result = loadAdapter.ClearReferenceLoadCase(refLc);

                Assert.IsTrue(result);
                Assert.Contains(201, mockCom.ClearedReferenceLoadCases);
            }
        }

        [Test]
        public void OSLoadAdapter_CreateNewPrimaryLoad_CreatesPrimaryLoadViaCom()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                int lcId = loadAdapter.CreateNewPrimaryLoad("LIVE_LOAD_ROOF", LoadType.RoofLive);

                Assert.AreEqual(1, lcId);
                Assert.AreEqual(1, mockCom.CreatedPrimaryLoads.Count);
                Assert.AreEqual("LIVE_LOAD_ROOF", mockCom.CreatedPrimaryLoads[0].Title);
                Assert.AreEqual((int)LoadType.RoofLive, mockCom.CreatedPrimaryLoads[0].Type);
            }
        }

        [Test]
        public void OSLoadAdapter_CreateNewPrimaryLoad_WithExplicitId_CreatesPrimaryLoad()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                int lcId = loadAdapter.CreateNewPrimaryLoad(10, "WIND_X_LOAD", LoadType.Wind);

                Assert.AreEqual(10, lcId);
                Assert.AreEqual(1, mockCom.CreatedPrimaryLoadsEx2.Count);
                Assert.AreEqual(10, mockCom.CreatedPrimaryLoadsEx2[0].Id);
                Assert.AreEqual("WIND_X_LOAD", mockCom.CreatedPrimaryLoadsEx2[0].Title);
            }
        }

        [Test]
        public void OSLoadAdapter_CreateNewReferenceLoad_CreatesReferenceLoadViaCom()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                int refId = loadAdapter.CreateNewReferenceLoad(101, "EQUIPMENT_DEAD", LoadType.Dead);

                Assert.AreEqual(101, refId);
                Assert.AreEqual(1, mockCom.CreatedReferenceLoads.Count);
                Assert.AreEqual(101, mockCom.CreatedReferenceLoads[0].Id);
                Assert.AreEqual("EQUIPMENT_DEAD", mockCom.CreatedReferenceLoads[0].Title);
            }
        }

        [Test]
        public void OSLoadAdapter_GetLoadCaseTitle_ReturnsTitleFromCom()
        {
            var mockCom = new MockStaadLoadCom();
            mockCom.LoadCaseTitles[5] = "SEISMIC_SPECTRUM_X";
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                string title = loadAdapter.GetLoadCaseTitle(5);

                Assert.AreEqual("SEISMIC_SPECTRUM_X", title);
            }
        }

        [Test]
        public void OSLoadAdapter_SetLoadCaseActive_ActivatesCorrectType()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var primaryLc = new LoadCase(12, "LIVE", LoadCaseType.PrimaryLoad);
                var refLc = new LoadCase(102, "REF_LIVE", LoadCaseType.ReferenceLoad);

                loadAdapter.SetLoadCaseActive(primaryLc);
                Assert.AreEqual(12, mockCom.LastActiveLoadCaseId);

                loadAdapter.SetLoadCaseActive(refLc);
                Assert.AreEqual(102, mockCom.LastActiveRefLoadCaseId);
            }
        }

        [Test]
        public void OSLoadAdapter_AddSupportSettlement_AppliesDisplacementViaCom()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var lc = new LoadCase(1, "SETTLEMENT_LC", LoadCaseType.PrimaryLoad);

                loadAdapter.AddSupportSettlement(lc, nodeId: 10, SettlementDirection.Fy, mmSettlement: -25.0);

                Assert.AreEqual(1, mockCom.LastActiveLoadCaseId);
                Assert.AreEqual(1, mockCom.SupportDisplacements.Count);
                Assert.AreEqual(10, mockCom.SupportDisplacements[0].NodeIds[0]);
                Assert.AreEqual((int)SettlementDirection.Fy, mockCom.SupportDisplacements[0].Direction);
                Assert.AreEqual(-0.025, mockCom.SupportDisplacements[0].Displacement, 1e-6);
            }
        }

        [Test]
        public void OSLoadAdapter_CreatePrimaryLoadCases_BatchCreatesLoads()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var primaryLoads = new HashSet<ILoadCase>
                {
                    new LoadCase(1, "DEAD_LOAD", LoadCaseType.PrimaryLoad) { Type = LoadType.Dead },
                    new LoadCase(2, "LIVE_LOAD", LoadCaseType.PrimaryLoad) { Type = LoadType.Live }
                };

                loadAdapter.CreatePrimaryLoadCases(primaryLoads);

                Assert.AreEqual(2, mockCom.CreatedPrimaryLoads.Count);
                Assert.AreEqual("DEAD_LOAD", mockCom.CreatedPrimaryLoads[0].Title);
                Assert.AreEqual("LIVE_LOAD", mockCom.CreatedPrimaryLoads[1].Title);
            }
        }

        [Test]
        public void OSLoadAdapter_CreateReferenceLoadCases_BatchCreatesLoads()
        {
            var mockCom = new MockStaadLoadCom();
            var mockRoot = new MockStaadRootCom(mockCom);
            using (var wrapper = new OpenStaadWrapper(mockRoot))
            {
                IOSLoad loadAdapter = wrapper.Load;
                var refLoads = new HashSet<ILoadCase>
                {
                    new LoadCase(101, "REF_DEAD", LoadCaseType.ReferenceLoad) { Type = LoadType.Dead },
                    new LoadCase(102, "REF_LIVE", LoadCaseType.ReferenceLoad) { Type = LoadType.Live }
                };

                loadAdapter.CreateReferenceLoadCases(refLoads);

                Assert.AreEqual(2, mockCom.CreatedReferenceLoads.Count);
                Assert.AreEqual(101, mockCom.CreatedReferenceLoads[0].Id);
                Assert.AreEqual("REF_DEAD", mockCom.CreatedReferenceLoads[0].Title);
                Assert.AreEqual(102, mockCom.CreatedReferenceLoads[1].Id);
                Assert.AreEqual("REF_LIVE", mockCom.CreatedReferenceLoads[1].Title);
            }
        }
    }
}
