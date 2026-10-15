using System;
using NUnit.Framework;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Services.Wrapper;

namespace StaadPro.Interop.Tests.Models
{
    [TestFixture]
    public class WrapperUnitParserTests
    {
        [TestCase("kN", ForceInputUnit.KiloNewton)]
        [TestCase("Kilo-Newtons", ForceInputUnit.KiloNewton)]
        [TestCase("kip", ForceInputUnit.KiloPound)]
        [TestCase("lb", ForceInputUnit.Pound)]
        [TestCase("kgf", ForceInputUnit.KiloGram)]
        [TestCase("Metric Ton", ForceInputUnit.MetricTon)]
        [TestCase("MTon", ForceInputUnit.MetricTon)]
        [TestCase("N", ForceInputUnit.Newton)]
        [TestCase("MN", ForceInputUnit.MegaNewton)]
        [TestCase("daN", ForceInputUnit.DecaNewton)]
        [TestCase("DN", ForceInputUnit.DecaNewton)]
        public void Force_ParsesNativeNamesAndAbbreviations(string text, ForceInputUnit expected)
        {
            Assert.That(WrapperUnitParser.TryParseForceInputUnit(text, out ForceInputUnit actual), Is.True);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("inch", LengthInputUnit.Inch)]
        [TestCase("ft", LengthInputUnit.Feet)]
        [TestCase("centimetres", LengthInputUnit.CentiMeter)]
        [TestCase("metres", LengthInputUnit.Meter)]
        [TestCase("millimeters", LengthInputUnit.MilliMeter)]
        [TestCase("dm", LengthInputUnit.DeciMeter)]
        [TestCase("km", LengthInputUnit.KiloMeter)]
        public void Length_ParsesNativeNamesAndAbbreviations(string text, LengthInputUnit expected)
        {
            Assert.That(WrapperUnitParser.TryParseLengthInputUnit(text, out LengthInputUnit actual), Is.True);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NumericUnits_PreserveZeroAndNormalizeFeetAlias()
        {
            for (int code = 0; code <= 7; code++)
            {
                Assert.That(WrapperUnitParser.TryParseForceInputUnit(code, out ForceInputUnit force), Is.True);
                Assert.That((int)force, Is.EqualTo(code));
                Assert.That(WrapperUnitParser.TryParseLengthInputUnit(code, out LengthInputUnit length), Is.True);
                Assert.That((int)length, Is.EqualTo(code == 1 ? 2 : code));
            }
        }

        [Test]
        public void InvalidValues_DoNotBecomeValidUnits()
        {
            foreach (object value in new object[] { null, DBNull.Value, true, false, -1, 8, "unknown", "", 0.5, 2.0, 3m, ulong.MaxValue })
            {
                Assert.That(WrapperUnitParser.TryParseForceInputUnit(value, out _), Is.False);
                Assert.That(WrapperUnitParser.TryParseLengthInputUnit(value, out _), Is.False);
            }
        }
    }
}
