using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Extensions;

namespace StaadPro.Interop.Extensions
{
    public static class NodeExtensions
    {
        public static int LastId(this IEnumerable<Node> nodes) => nodes.Any() ? nodes.Max(n => n.Id) : 0;

        public static Node GetCenter(this IEnumerable<Node> nodes)
        {
            if (nodes == null || !nodes.Any()) return null;
            return new Node(nodes.Average(n => n.X), nodes.Average(n => n.Y), nodes.Average(n => n.Z));
        }

        public static List<(int NodeId, double Fx, double Fz)> DistributeTorque(this IEnumerable<Node> nodes, Node center, double torque)
        {
            var result = new List<(int NodeId, double Fx, double Fz)>();
            if (nodes == null || !nodes.Any() || center == null) return result;

            var nodeList = nodes.ToList();
            double sumR2 = nodeList.Sum(n => (n.X - center.X).Square() + (n.Z - center.Z).Square());
            if (sumR2 <= 1e-6) return result;

            foreach (var node in nodeList)
            {
                double rx = node.X - center.X;
                double rz = node.Z - center.Z;
                double fx = -torque * rz / sumR2;
                double fz = torque * rx / sumR2;
                result.Add((node.Id, fx, fz));
            }

            return result;
        }
    }
}
