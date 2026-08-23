using System;
using System.Collections.Generic;
using NUnit.Framework;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Extensions;

namespace StaadPro.Interop.Tests.Entities
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void Node_CoordinateAssignment_ReturnsCorrectValues()
        {
            var node = new Node(10, 1.5, 2.5, 3.5);
            Assert.AreEqual(10, node.Id);
            Assert.AreEqual(1.5, node.X, 1e-6);
            Assert.AreEqual(2.5, node.Y, 1e-6);
            Assert.AreEqual(3.5, node.Z, 1e-6);
        }

        [Test]
        public void Node_DistanceCalculation_CalculatesEuclideanDistance()
        {
            var n1 = new Node(1, 0.0, 0.0, 0.0);
            var n2 = new Node(2, 3.0, 4.0, 0.0);

            double distance = Node.DistanceBetweenNodes(n1, n2);
            Assert.AreEqual(5.0, distance, 1e-6);
        }

        [Test]
        public void Node_IntermediateNodesGeneration_DividesLineAccurately()
        {
            var sNode = new Node(1, 0.0, 0.0, 0.0);
            var eNode = new Node(2, 10.0, 0.0, 0.0);

            List<Node> intermediateNodes = Node.GenerateIntermediateNodes(sNode, eNode, spacing: 2.5, currentNodeId: 100);

            Assert.AreEqual(3, intermediateNodes.Count);
            Assert.AreEqual(2.5, intermediateNodes[0].X, 1e-6);
            Assert.AreEqual(5.0, intermediateNodes[1].X, 1e-6);
            Assert.AreEqual(7.5, intermediateNodes[2].X, 1e-6);
        }

        [Test]
        public void Node_CircularPathGeneration_CreatesCorrectCountAndRadius()
        {
            var center = new Node(1, 0.0, 0.0, 0.0);
            HashSet<Node> nodes = Node.GenerateNodesOnCircularPath(center, radius: 5.0, divisions: 8, dy: 2.0, lastUsedId: 10);

            Assert.AreEqual(8, nodes.Count);
            foreach (var n in nodes)
            {
                Assert.AreEqual(2.0, n.Y, 1e-6);
                double radialDist = Math.Sqrt(n.X * n.X + n.Z * n.Z);
                Assert.AreEqual(5.0, radialDist, 1e-6);
            }
        }

        [Test]
        public void Node_DistributeTorque_CalculatesEquilibriumForces()
        {
            var center = new Node(100, 0.0, 0.0, 0.0);
            var nodes = new List<Node>
            {
                new Node(1, 5.0, 0.0, 0.0),
                new Node(2, -5.0, 0.0, 0.0),
                new Node(3, 0.0, 0.0, 5.0),
                new Node(4, 0.0, 0.0, -5.0)
            };

            double torque = 100.0;
            var distribution = nodes.DistributeTorque(center, torque);

            Assert.AreEqual(4, distribution.Count);
            double totalTorque = 0.0;
            foreach (var item in distribution)
            {
                var n = nodes.Find(x => x.Id == item.NodeId);
                totalTorque += (n.X * item.Fz - n.Z * item.Fx);
            }
            Assert.AreEqual(torque, totalTorque, 1e-6);
        }
    }
}
