using System.Windows.Media.Media3D;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class PlateTests
    {
        [Test]
        public void Plate_TriangularPlateArea_ComputesAccurately()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 4.0, 0.0, 0.0);
            var n3 = new Node(3, 0.0, 3.0, 0.0);

            var plate = new Plate(10, n1, n2, n3);

            Assert.IsTrue(plate.IsTriangle);
            Assert.AreEqual(6.0, plate.Area, 1e-6);
        }

        [Test]
        public void Plate_QuadPlateArea_ComputesAccurately()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 5.0, 0.0, 0.0);
            var n3 = new Node(3, 5.0, 4.0, 0.0);
            var n4 = new Node(4, 0.0, 4.0, 0.0);

            var plate = new Plate(11, n1, n2, n3, n4);

            Assert.IsFalse(plate.IsTriangle);
            Assert.AreEqual(20.0, plate.Area, 1e-6);
        }

        [Test]
        public void Plate_LocalCoordinateBasis_ReturnsOrthogonalVectors()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 5.0, 0.0, 0.0);
            var n3 = new Node(3, 5.0, 0.0, 5.0);
            var n4 = new Node(4, 0.0, 0.0, 5.0);

            var plate = new Plate(12, n1, n2, n3, n4);
            bool success = plate.TryGetLocalCoordinateSystemBasis(out Vector3D lx, out Vector3D ly, out Vector3D lz);

            Assert.IsTrue(success);
            Assert.AreEqual(0.0, Vector3D.DotProduct(lx, ly), 1e-6);
            Assert.AreEqual(0.0, Vector3D.DotProduct(lx, lz), 1e-6);
            Assert.AreEqual(0.0, Vector3D.DotProduct(ly, lz), 1e-6);
            Assert.AreEqual(PlateOrientation.Horizontal, plate.Orientation);
        }
    }
}
