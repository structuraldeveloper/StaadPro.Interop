using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services
{
    /// <summary>Groups managed beams, members and plates by connectivity without calling OpenSTAAD.</summary>
    public static class ConnectivityService
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='ConnectivityService.SegregateBeamsByConnectivity`0(IEnumerable&lt;Beam&gt;)']/*" />
        public static IEnumerable<IEnumerable<Beam>> SegregateBeamsByConnectivity(IEnumerable<Beam> beams)
        {
            if (beams == null) throw new ArgumentNullException(nameof(beams));

            List<Beam> distinctBeams = beams.Where(beam => beam != null)
                .GroupBy(beam => beam.Id).Select(group => group.First()).ToList();

            Dictionary<int, List<Beam>> incidentBeamsByNodeId = BuildIncidentBeamsByNodeId(distinctBeams);

            List<List<Beam>> groups = new List<List<Beam>>();
            HashSet<int> remainingBeamIds = new HashSet<int>(distinctBeams.Select(beam => beam.Id));
            Dictionary<int, Beam> beamsById = distinctBeams.ToDictionary(beam => beam.Id);

            while (remainingBeamIds.Count > 0)
            {
                int seedBeamId = remainingBeamIds.First();
                List<Beam> connectedBeams = CollectConnectedComponent(seedBeamId, beamsById, incidentBeamsByNodeId, remainingBeamIds);
                groups.Add(OrderBeamsSequentially(connectedBeams, incidentBeamsByNodeId));
            }

            return groups;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='ConnectivityService.SegregateMembersByConnectivity`0(IEnumerable&lt;Member&gt;,MemberConnectivityScope)']/*" />
        public static IEnumerable<IEnumerable<Member>> SegregateMembersByConnectivity(
            IEnumerable<Member> members,
            MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)
        {
            if (members == null) throw new ArgumentNullException(nameof(members));

            List<Member> distinctMembers = members.Where(member => member != null).Distinct().ToList();
            List<List<Member>> groups = new List<List<Member>>();
            HashSet<Member> remaining = new HashSet<Member>(distinctMembers);

            while (remaining.Count > 0)
            {
                Member seed = remaining.First();
                groups.Add(CollectConnectedComponent(
                    seed,
                    remaining,
                    (candidate, current) => AreMembersConnected(candidate, current, scope)));
            }

            return groups;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='ConnectivityService.SegregatePlatesByConnectivity`0(IEnumerable&lt;Plate&gt;,PlateConnectivityScope)']/*" />
        public static IEnumerable<IEnumerable<Plate>> SegregatePlatesByConnectivity(
            IEnumerable<Plate> plates,
            PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)
        {
            if (plates == null) throw new ArgumentNullException(nameof(plates));

            List<Plate> distinctPlates = plates.Where(plate => plate != null).Distinct().ToList();
            List<List<Plate>> groups = new List<List<Plate>>();
            HashSet<Plate> remaining = new HashSet<Plate>(distinctPlates);

            while (remaining.Count > 0)
            {
                Plate seed = remaining.First();
                groups.Add(CollectConnectedComponent(
                    seed,
                    remaining,
                    (candidate, current) => ArePlatesConnected(candidate, current, scope)));
            }

            return groups;
        }

        private static Dictionary<int, List<Beam>> BuildIncidentBeamsByNodeId(IEnumerable<Beam> beams)
        {
            Dictionary<int, List<Beam>> incidentBeamsByNodeId = new Dictionary<int, List<Beam>>();
            foreach (Beam beam in beams)
            {
                AddIncidentBeam(incidentBeamsByNodeId, beam.StartNode.Id, beam);
                AddIncidentBeam(incidentBeamsByNodeId, beam.EndNode.Id, beam);
            }

            return incidentBeamsByNodeId;
        }

        private static void AddIncidentBeam(Dictionary<int, List<Beam>> incidentBeamsByNodeId, int nodeId, Beam beam)
        {
            if (!incidentBeamsByNodeId.TryGetValue(nodeId, out List<Beam> incidentBeams))
            {
                incidentBeams = new List<Beam>();
                incidentBeamsByNodeId.Add(nodeId, incidentBeams);
            }

            incidentBeams.Add(beam);
        }

        private static List<Beam> CollectConnectedComponent(
            int seedBeamId,
            IReadOnlyDictionary<int, Beam> beamsById,
            IReadOnlyDictionary<int, List<Beam>> incidentBeamsByNodeId,
            HashSet<int> remainingBeamIds)
        {
            List<Beam> component = new List<Beam>();
            Stack<int> pendingBeamIds = new Stack<int>();
            pendingBeamIds.Push(seedBeamId);

            while (pendingBeamIds.Count > 0)
            {
                int beamId = pendingBeamIds.Pop();
                if (!remainingBeamIds.Remove(beamId))
                {
                    continue;
                }

                Beam beam = beamsById[beamId];
                component.Add(beam);

                foreach (int nodeId in new[] { beam.StartNode.Id, beam.EndNode.Id })
                {
                    foreach (Beam adjacentBeam in incidentBeamsByNodeId[nodeId])
                    {
                        if (remainingBeamIds.Contains(adjacentBeam.Id))
                        {
                            pendingBeamIds.Push(adjacentBeam.Id);
                        }
                    }
                }
            }

            return component;
        }

        private static List<Beam> OrderBeamsSequentially(List<Beam> component, IReadOnlyDictionary<int, List<Beam>> incidentBeamsByNodeId)
        {
            if (component.Count <= 1)
            {
                return component;
            }

            HashSet<int> componentBeamIds = new HashSet<int>(component.Select(beam => beam.Id));
            int startNodeId = FindTraversalStartNodeId(component, componentBeamIds, incidentBeamsByNodeId);

            List<Beam> orderedBeams = new List<Beam>();
            HashSet<int> visitedBeamIds = new HashSet<int>();
            Queue<int> pendingNodeIds = new Queue<int>();
            pendingNodeIds.Enqueue(startNodeId);

            while (pendingNodeIds.Count > 0 && visitedBeamIds.Count < component.Count)
            {
                int nodeId = pendingNodeIds.Dequeue();
                foreach (Beam beam in incidentBeamsByNodeId[nodeId]
                    .Where(candidate => componentBeamIds.Contains(candidate.Id) && !visitedBeamIds.Contains(candidate.Id)))
                {
                    visitedBeamIds.Add(beam.Id);
                    orderedBeams.Add(beam);

                    int otherNodeId = beam.StartNode.Id == nodeId ? beam.EndNode.Id : beam.StartNode.Id;
                    pendingNodeIds.Enqueue(otherNodeId);
                }
            }

            // Any beam not reached by the node-by-node walk (should not normally occur for a
            // connected component) is appended so that no beam from the input is ever lost.
            orderedBeams.AddRange(component.Where(beam => !visitedBeamIds.Contains(beam.Id)));
            return orderedBeams;
        }

        private static int FindTraversalStartNodeId(
            List<Beam> component,
            HashSet<int> componentBeamIds,
            IReadOnlyDictionary<int, List<Beam>> incidentBeamsByNodeId)
        {
            Dictionary<int, int> degreeByNodeId = component
                .SelectMany(beam => new[] { beam.StartNode.Id, beam.EndNode.Id })
                .Distinct()
                .ToDictionary(
                    nodeId => nodeId,
                    nodeId => incidentBeamsByNodeId[nodeId].Count(beam => componentBeamIds.Contains(beam.Id)));

            // Prefer a leaf (degree 1) node so a simple chain is walked end to end; fall back to any
            // node in the component when every node has degree greater than one (a closed loop).
            return degreeByNodeId
                .OrderBy(entry => entry.Value)
                .First()
                .Key;
        }

        private static List<T> CollectConnectedComponent<T>(T seed, HashSet<T> remaining, Func<T, T, bool> areConnected)
        {
            List<T> component = new List<T>();
            Queue<T> pending = new Queue<T>();
            pending.Enqueue(seed);
            remaining.Remove(seed);

            while (pending.Count > 0)
            {
                T current = pending.Dequeue();
                component.Add(current);

                foreach (T candidate in remaining.Where(candidate => areConnected(candidate, current)).ToList())
                {
                    remaining.Remove(candidate);
                    pending.Enqueue(candidate);
                }
            }

            return component;
        }

        private static bool AreMembersConnected(Member member, Member targetMember, MemberConnectivityScope scope)
        {
            IEnumerable<Node> memberNodes = scope == MemberConnectivityScope.AnyNode
                ? (IEnumerable<Node>)member.Nodes : new[] { member.StartNode, member.EndNode };
            IEnumerable<Node> targetNodes = scope == MemberConnectivityScope.AnyNode
                ? (IEnumerable<Node>)targetMember.Nodes : new[] { targetMember.StartNode, targetMember.EndNode };
            return memberNodes.Any(node => targetNodes.Contains(node));
        }

        private static bool ArePlatesConnected(Plate plate, Plate targetPlate, PlateConnectivityScope scope)
        {
            var corners = GetPlateCornerLoop(plate);
            var targetCorners = new HashSet<Node>(GetPlateCornerLoop(targetPlate));
            if (scope == PlateConnectivityScope.AnyCorner) return corners.Any(targetCorners.Contains);
            // Preserve the source predicate: an edge of the candidate with both endpoints in the target.
            for (int index = 0; index < corners.Count; index++)
                if (targetCorners.Contains(corners[index]) && targetCorners.Contains(corners[(index + 1) % corners.Count]))
                    return true;
            return false;
        }

        private static List<Node> GetPlateCornerLoop(Plate plate)
        {
            var loop = new List<Node>();
            foreach (Node corner in new[] { plate.A, plate.B, plate.C, plate.D })
                if (corner != null && !loop.Contains(corner)) loop.Add(corner);
            return loop;
        }
    }
}
