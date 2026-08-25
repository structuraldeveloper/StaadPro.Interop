using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class LoadCaseTests
    {
        [Test]
        public void LoadCase_DefaultProperties_InitializeCorrectly()
        {
            var lc = new LoadCase(1, "DEAD_LOAD", LoadCaseType.PrimaryLoad);
            Assert.AreEqual(1, lc.Id);
            Assert.AreEqual("DEAD_LOAD", lc.Title);
            Assert.AreEqual(LoadCaseType.PrimaryLoad, lc.CaseType);
        }

        [Test]
        public void LoadCase_Equality_ComparesByIdAndCaseType()
        {
            var lc1 = new LoadCase(5, "WIND_X", LoadCaseType.PrimaryLoad);
            var lc2 = new LoadCase(5, "WIND_X_MODIFIED", LoadCaseType.PrimaryLoad);
            var lc3 = new LoadCase(5, "WIND_X", LoadCaseType.ReferenceLoad);

            Assert.AreEqual(lc1, lc2);
            Assert.IsTrue(lc1 == lc2);
            Assert.AreNotEqual(lc1, lc3);
        }
    }
}
