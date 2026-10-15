using System;
using NUnit.Framework;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Tests.Models
{
    [TestFixture]
    public class ApplicationVersionTests
    {
        [TestCase("24.00.02.354", 24, 0, 2, 354, "v24.00.02.354")]
        [TestCase("25.0.1.424", 25, 0, 1, 424, "v25.00.01.424")]
        [TestCase("26.0.0.340", 26, 0, 0, 340, "v26.00.00.340")]
        [TestCase("1.2.3.4", 1, 2, 3, 4, "v01.02.03.004")]
        public void Parse_PreservesFourPartMeaning(string text, int major, int update, int revision, int build, string display)
        {
            ApplicationVersion result = ApplicationVersion.Parse(text);
            Assert.Multiple(() =>
            {
                Assert.That(result.Major, Is.EqualTo(major));
                Assert.That(result.Update, Is.EqualTo(update));
                Assert.That(result.Revision, Is.EqualTo(revision));
                Assert.That(result.Build, Is.EqualTo(build));
                Assert.That(result.Display, Is.EqualTo(display));
            });
        }

        [Test]
        public void Parse_ClrVersion_UsesMainProjectComponentMapping()
        {
            ApplicationVersion result = ApplicationVersion.Parse(new Version(8, 7, 6, 5));
            Assert.That(result.Display, Is.EqualTo("v08.07.06.005"));
            Assert.That(ApplicationVersion.Parse(new Version(8, 7)).Revision, Is.EqualTo(-1));
            Assert.That(ApplicationVersion.Parse(new Version(8, 7)).Build, Is.EqualTo(-1));
        }

        [Test]
        public void Parse_RejectsNullForBothOverloads()
        {
            Assert.Throws<ArgumentNullException>(() => ApplicationVersion.Parse((string)null));
            Assert.Throws<ArgumentNullException>(() => ApplicationVersion.Parse((Version)null));
        }

        [TestCase("")]
        [TestCase("1.2.3")]
        [TestCase("1.2.3.4.5")]
        [TestCase("-1.2.3.4")]
        public void Parse_RejectsInvalidComponentShape(string value)
            => Assert.Throws<ArgumentException>(() => ApplicationVersion.Parse(value));

        [Test]
        public void Parse_RejectsMalformedAndOverflowingComponents()
        {
            Assert.Throws<FormatException>(() => ApplicationVersion.Parse("v26.0.0.340"));
            Assert.Throws<OverflowException>(() => ApplicationVersion.Parse("26.0.0.2147483648"));
        }
    }
}
