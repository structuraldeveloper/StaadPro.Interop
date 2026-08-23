using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class BeamTests
    {
        [Test]
        public void Beam_LengthAndVectors_ComputeCorrectly()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 0.0, 10.0, 0.0);
            var beam = new Beam(101, n1, n2);

            Assert.AreEqual(10.0, beam.Length, 1e-6);
            Assert.IsTrue(beam.IsParallelToY);
            Assert.IsFalse(beam.IsParallelToX);
        }

        [Test]
        public void Beam_DetermineRelationship_DetectsParallelAndOrthogonal()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 5.0, 0.0, 0.0);
            var n3 = new Node(3, 0.0, 5.0, 0.0);
            var n4 = new Node(4, 5.0, 5.0, 0.0);

            var b1 = new Beam(1, n1, n2);
            var b2 = new Beam(2, n3, n4);
            var b3 = new Beam(3, n1, n3);

            Assert.AreEqual(BeamRelation.Parallel, b1.DetermineBeamRelationship(b2));
            Assert.AreEqual(BeamRelation.Orthogonal, b1.DetermineBeamRelationship(b3));
        }
    }
}
