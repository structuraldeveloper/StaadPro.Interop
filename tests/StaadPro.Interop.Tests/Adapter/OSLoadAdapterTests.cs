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
        }

        [Test]
        public void IOSLoad_ExposesAllRequiredClearLoadCaseMethods()
        {
            string[] requiredMethods =
            {
                "ClearPrimaryLoadCase",
                "ClearPrimaryLoadCases",
                "ClearReferenceLoadCase",
                "ClearReferenceLoadCases"
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
            var session = new StaadGeometrySession(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = session.Load;

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
            var session = new StaadGeometrySession(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = session.Load;

            var refLc = new LoadCase(201, "REF_DEAD", LoadCaseType.ReferenceLoad);
            bool result = loadAdapter.ClearReferenceLoadCase(refLc);

            Assert.IsTrue(result);
            Assert.Contains(201, mockCom.ClearedReferenceLoadCases);
        }

        [Test]
        public void OSLoadAdapter_ClearReferenceLoadCases_Collection_InvokesComMethod()
        {
            var mockCom = new MockStaadLoadCom();
            var session = new StaadGeometrySession(geometryComObject: null, loadComObject: mockCom);
            IOSLoad loadAdapter = session.Load;

            var lcs = new List<ILoadCase>
            {
                new LoadCase(201, "REF_1", LoadCaseType.ReferenceLoad),
                new LoadCase(202, "REF_2", LoadCaseType.ReferenceLoad)
            };

            bool result = loadAdapter.ClearReferenceLoadCases(lcs);

            Assert.IsTrue(result);
            Assert.Contains(201, mockCom.ClearedReferenceLoadCases);
            Assert.Contains(202, mockCom.ClearedReferenceLoadCases);
        }
    }
}
