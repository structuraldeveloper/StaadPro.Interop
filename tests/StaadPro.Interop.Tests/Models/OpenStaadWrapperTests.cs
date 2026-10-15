using System;
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
            public int GetInputUnitForForce(ref object key) { key = "kN"; return 5; }
            public int GetInputUnitForLength(ref object key) { key = "m"; return 4; }
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
        public void OpenStaadWrapper_NullComObject_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OpenStaadWrapper(null, isDedicated: false));
        }

        [Test]
        public void OpenStaadWrapper_OffersDefaultAndInjectedSourceConstructorForms()
        {
            var constructors = typeof(OpenStaadWrapper).GetConstructors();
            Assert.AreEqual(2, constructors.Length);
            Assert.That(Array.ConvertAll(constructors, constructor => constructor.GetParameters().Length), Is.EquivalentTo(new[] { 2, 4 }));
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
    }
}
