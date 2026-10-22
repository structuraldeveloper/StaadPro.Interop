using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

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


        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='GeometryCompletion.IdentifyPhysicalMembers']/*" />
        public static PhysicalMemberIdentificationResult IdentifyPhysicalMembers(IEnumerable<Beam> beams, double angularToleranceDegrees = 0.005)
        {
            if (beams == null) throw new ArgumentNullException(nameof(beams));
            ValidateAngleTolerance(angularToleranceDegrees);
            var snapshot = beams.Where(beam => beam != null).ToList();
            var nodeCoordinates = new Dictionary<int, Node>();
            var byId = new Dictionary<int, Beam>();
            foreach (Beam beam in snapshot)
            {
                ValidateEndpoints(beam.StartNode, beam.EndNode, nameof(beams));
                if (beam.Id <= 0) throw new ArgumentException("Beam IDs must be positive.", nameof(beams));
                foreach (Node node in new[] { beam.StartNode, beam.EndNode })
                {
                    if (nodeCoordinates.TryGetValue(node.Id, out Node previous) &&
                        (node.X != previous.X || node.Y != previous.Y || node.Z != previous.Z))
                        throw new ArgumentException("A node ID has inconsistent coordinates.", nameof(beams));
                    nodeCoordinates[node.Id] = node;
                }
                if (byId.TryGetValue(beam.Id, out Beam existing))
                {
                    if (existing.StartNode.Id != beam.StartNode.Id || existing.EndNode.Id != beam.EndNode.Id)
                        throw new ArgumentException("A beam ID has inconsistent endpoints.", nameof(beams));
                }
                else byId.Add(beam.Id, beam);
            }
            var result = new PhysicalMemberIdentificationResult();
            var distinctBeams = byId.Values.ToList();
            var incidence = BuildIncidentBeamsByNodeId(distinctBeams);
            int nextMemberId = 1;
            foreach (var component in SegregateBeamsByConnectivity(distinctBeams))
            {
                var ordered = component.ToList();
                var degrees = ordered.SelectMany(beam => new[] { beam.StartNode.Id, beam.EndNode.Id })
                    .Distinct().Select(id => incidence[id].Count).ToList();
                // A physical run must have exactly two leaves: cycles and branches are ambiguous.
                if (degrees.Any(degree => degree > 2) || degrees.Count(degree => degree == 1) != 2)
                {
                    result.DiscardedBeams.AddRange(ordered);
                    continue;
                }
                var run = new List<Beam>();
                foreach (Beam beam in ordered)
                {
                    if (run.Count > 0 && !AreParallel(run[run.Count - 1], beam, angularToleranceDegrees))
                    {
                        AddPhysicalRun(result, run, nextMemberId++);
                        run = new List<Beam>();
                    }
                    run.Add(beam);
                }
                AddPhysicalRun(result, run, nextMemberId++);
            }
            return result;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='GeometryCompletion.GroupPhysicalMembersByPlanAngle']/*" />
        public static IEnumerable<PhysicalMemberPlanAngleGroup> GroupPhysicalMembersByPlanAngle(IEnumerable<Member> members, double angularToleranceDegrees = 1e-3)
        {
            if (members == null) throw new ArgumentNullException(nameof(members));
            ValidateAngleTolerance(angularToleranceDegrees);
            var snapshot = members.Where(member => member != null).ToList();
            foreach (Member member in snapshot) ValidateEndpoints(member.StartNode, member.EndNode, nameof(members));
            var groups = new List<PhysicalMemberPlanAngleGroup>();
            foreach (Member member in snapshot)
            {
                // Same XZ plan convention as Node.PlanAngleIn360DegreesOf; opposite bearings coincide.
                double angle = Node.PlanAngleIn360DegreesOf(member.EndNode, member.StartNode) % 180.0;
                if (angle < 0) angle += 180;
                var group = groups.FirstOrDefault(candidate =>
                {
                    double difference = Math.Abs(candidate.PlanAngleInDegrees - angle);
                    return Math.Min(difference, 180 - difference) <= angularToleranceDegrees;
                });
                if (group == null)
                {
                    group = new PhysicalMemberPlanAngleGroup { PlanAngleInDegrees = angle };
                    groups.Add(group);
                }
                group.Members.Add(member);
            }
            return groups.OrderBy(group => group.PlanAngleInDegrees).ToList();
        }

        private static void ValidateAngleTolerance(double tolerance)
        {
            if (double.IsNaN(tolerance) || double.IsInfinity(tolerance) || tolerance < 0 || tolerance >= 90)
                throw new ArgumentOutOfRangeException("angularToleranceDegrees", "Use a finite tolerance from zero up to, but not including, 90 degrees.");
        }

        private static void ValidateEndpoints(Node start, Node end, string parameterName)
        {
            if (start == null || end == null || start.Id <= 0 || end.Id <= 0 || start.Id == end.Id ||
                !Finite(start.X) || !Finite(start.Y) || !Finite(start.Z) ||
                !Finite(end.X) || !Finite(end.Y) || !Finite(end.Z))
                throw new ArgumentException("Endpoints must have distinct positive IDs and finite coordinates.", parameterName);
            var vector = Node.GetVector3D(start, end);
            double scale = Math.Max(Math.Abs(vector.X), Math.Max(Math.Abs(vector.Y), Math.Abs(vector.Z)));
            if (!Finite(scale) || scale == 0)
                throw new ArgumentException("Endpoints must define a finite nonzero segment.", parameterName);
        }

        private static bool Finite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);

        private static bool AreParallel(Beam first, Beam second, double tolerance)
        {
            var a = first.ToVector;
            var b = second.ToVector;
            a /= Math.Max(Math.Abs(a.X), Math.Max(Math.Abs(a.Y), Math.Abs(a.Z)));
            b /= Math.Max(Math.Abs(b.X), Math.Max(Math.Abs(b.Y), Math.Abs(b.Z)));
            a.Normalize(); b.Normalize();
            double dot = Math.Abs(System.Windows.Media.Media3D.Vector3D.DotProduct(a, b));
            double cross = System.Windows.Media.Media3D.Vector3D.CrossProduct(a, b).Length;
            return Math.Atan2(cross, dot) * 180.0 / Math.PI <= tolerance;
        }

        private static void AddPhysicalRun(PhysicalMemberIdentificationResult result, List<Beam> run, int memberId)
        {
            Node start = run[0].StartNode;
            if (run.Count > 1 && (start.Id == run[1].StartNode.Id || start.Id == run[1].EndNode.Id))
                start = run[0].EndNode;
            Node end = start;
            foreach (Beam beam in run) end = beam.StartNode.Id == end.Id ? beam.EndNode : beam.StartNode;
            var member = new Member(memberId, start, end)
            {
                Beams = new HashSet<Beam>(run),
                Nodes = new HashSet<Node>(run.SelectMany(beam => new[] { beam.StartNode, beam.EndNode }))
            };
            foreach (Beam beam in run) beam.Member = member;
            result.Members.Add(member);
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
