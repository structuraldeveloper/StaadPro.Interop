using NUnit.Framework;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Models
{
    [TestFixture]
    public class OpenStaadWrapperTests
    {
        public class MockStaadRootCom
        {
            public MockStaadRootCom()
            {
                Geometry = new MockGeometryCom();
                Load = new MockLoadCom();
            }

            public MockGeometryCom Geometry { get; }
            public MockLoadCom Load { get; }

            public int GetBaseUnit() => 2; // Metric
            public int GetInputUnitForForce(string key) => 5; // KiloNewton
            public int GetInputUnitForLength(string key) => 4; // Meter
        }

        public class MockGeometryCom
        {
            public int GetNodeCount() => 42;
        }

        public class MockLoadCom
        {
            public int GetPrimaryLoadCaseCount() => 5;
        }

        [Test]
        public void OpenStaadWrapper_NullComObject_InitializesDisconnected()
        {
            using (var wrapper = new OpenStaadWrapper(null, isDedicated: false))
            {
                Assert.IsFalse(wrapper.IsConnected);
                Assert.IsNull(wrapper.Geometry);
                Assert.IsNull(wrapper.Load);
                Assert.AreEqual(BaseUnitSystem.Unknown, wrapper.BaseUnitSystem);
            }
        }

        [Test]
        public void OpenStaadWrapper_WithRootCom_InitializesPropertiesAndAdapters()
        {
            var mockRoot = new MockStaadRootCom();
            using (var wrapper = new OpenStaadWrapper(mockRoot, isDedicated: true))
            {
                Assert.IsTrue(wrapper.IsConnected);
                Assert.IsTrue(wrapper.IsDedicated);
                Assert.IsNotNull(wrapper.Geometry);
                Assert.IsNotNull(wrapper.Load);
                Assert.AreEqual(BaseUnitSystem.Metric, wrapper.BaseUnitSystem);
                Assert.AreEqual(ForceInputUnit.KiloNewton, wrapper.InputForceUnit);
                Assert.AreEqual(LengthInputUnit.Meter, wrapper.InputLengthUnit);
            }
        }

        [Test]
        public void OpenStaadWrapper_WithSeparateComs_InitializesAdapters()
        {
            var geomCom = new MockGeometryCom();
            var loadCom = new MockLoadCom();

            using (var wrapper = new OpenStaadWrapper(geomCom, loadCom, isDedicated: false))
            {
                Assert.IsTrue(wrapper.IsConnected);
                Assert.IsNotNull(wrapper.Geometry);
                Assert.IsNotNull(wrapper.Load);
            }
        }
    }
}
