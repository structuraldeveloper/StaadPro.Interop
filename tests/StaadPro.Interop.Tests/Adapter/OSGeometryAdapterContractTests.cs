using System;
using System.Linq;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSGeometryAdapterContractTests
    {
        [Test]
        public void IOSGeometry_ExposesRequiredGeometryInterfaceMembers()
        {
            string[] requiredMethods =
            {
                "CreateNode",
                "CreateBeam",
                "CreatePlate",
                "CreateAnnularParametricSurface",
                "CreateSolidCircularParametricSurface",
                "AddDensityPointToSurface",
                "AddDensityLineToSurface",
                "AddPolygonalRegionToSurface",
                "AddCircularRegionToSurface",
                "GetPlatesConnectedAtNode",
                "GetBeamsConnectedAtNode",
                "GetAllEntities",
                "GetEntityGroups",
                "DistributeTorque",
                "IsZUp",
                "GetGlobalVerticalAxis"
            };

            var methods = typeof(IOSGeometry).GetMethods().Select(m => m.Name).ToHashSet();

            foreach (var req in requiredMethods)
            {
                Assert.IsTrue(methods.Contains(req), $"Required method '{req}' is absent from IOSGeometry.");
            }
        }

        [Test]
        public void IOSGeometry_ContainsAsyncAndParallelMethods()
        {
            string[] requiredAsyncMethods =
            {
                "GetAllNodesListAsync",
                "GetAllBeamsListAsync",
                "GetAllPlatesListAsync",
                "GetPlatesConnectedAtNodeAsync",
                "DistributeTorqueAsync"
            };

            var methods = typeof(IOSGeometry).GetMethods().Select(m => m.Name).ToHashSet();

            foreach (var req in requiredAsyncMethods)
            {
                Assert.IsTrue(methods.Contains(req), $"Required async method '{req}' is absent from IOSGeometry.");
            }
        }
    }
}
