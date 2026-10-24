using System;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class PropertyApiNamingTests
    {
        [TestCase("AddUPTPropertyWIDEFLANGE", "AddUptPropertyWideFlange")]
        [TestCase("AddUPTPropertyWIDEFLANGEUNEQUAL", "AddUptPropertyWideFlangeUnequal")]
        [TestCase("AddUPTPropertyWIDEFLANGECOMPOSITE", "AddUptPropertyWideFlangeComposite")]
        [TestCase("AddUPTPropertyCHANNEL", "AddUptPropertyChannel")]
        [TestCase("AddUPTPropertyANGLE", "AddUptPropertyAngle")]
        [TestCase("AddUPTPropertyDOUBLEANGLE", "AddUptPropertyDoubleAngle")]
        [TestCase("AddUPTPropertyTEE", "AddUptPropertyTee")]
        [TestCase("AddUPTPropertyPIPE", "AddUptPropertyPipe")]
        [TestCase("AddUPTPropertyTUBE", "AddUptPropertyTube")]
        [TestCase("AddUPTPropertyGENERAL", "AddUptPropertyGeneral")]
        [TestCase("AddUPTPropertyISECTION", "AddUptPropertyISection")]
        [TestCase("AddUPTPropertyPRISMATIC", "AddUptPropertyPrismatic")]
        [TestCase("CreateUPTTable", "CreateUptTable")]
        [TestCase("CreateUPTTableEx", "CreateUptTableEx")]
        [TestCase("CreatePropertyFromUPTTable", "CreatePropertyFromUptTable")]
        [TestCase("RemoveUPTTable", "RemoveUptTable")]
        [TestCase("RemovePropertyFromUPTTable", "RemovePropertyFromUptTable")]
        public void PublicUptNamesUsePascalCaseWhileNativeTransportNamesRemainUnchanged(string previous, string current)
        {
            foreach(var type in new[]{typeof(IOSProperty),typeof(OSPropertyAdapter)})
            { Assert.That(type.GetMethods().Any(m=>m.Name==previous),Is.False);Assert.That(type.GetMethods().Count(m=>m.Name==current),Is.EqualTo(1)); }
            Assert.That(typeof(PackageVerification.PropertyUptFixture.Native).GetMethods().Any(m=>m.Name==previous)||typeof(PackageVerification.PropertyAttributeFixture.Native).GetMethods().Any(m=>m.Name==previous),Is.True);
        }
    }
}
