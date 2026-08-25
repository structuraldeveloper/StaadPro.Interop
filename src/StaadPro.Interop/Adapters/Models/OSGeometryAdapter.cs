using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Extensions;
using StaadPro.Interop.Helpers;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    public class OSGeometryAdapter : OSBaseAdapter, IOSGeometry, IOSBase
    {
        private readonly object _geometryComSyncRoot = new object();

        #region Properties

        public dynamic ComObject { get; }

        #endregion

        #region Constructor

        public OSGeometryAdapter(StaadGeometrySession session) : base(session)
        {
            ComObject = session.RawGeometry ?? throw new ArgumentNullException(nameof(session.RawGeometry));
        }

        #endregion

        #region Coordinate Convention

        public bool IsZUp()
        {
            object result = ComObject.IsZUp();
            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException("OpenSTAAD did not return the model vertical-axis convention.");
            }

            if (result is bool boolean) return boolean;

            try
            {
                return Convert.ToInt32(result, CultureInfo.InvariantCulture) != 0;
            }
            catch (Exception exception) when (
                exception is FormatException ||
                exception is InvalidCastException ||
                exception is OverflowException)
            {
                throw new InvalidOperationException("OpenSTAAD returned an invalid model vertical-axis convention.", exception);
            }
        }

        public GlobalVerticalAxis GetGlobalVerticalAxis()
        {
            return IsZUp() ? GlobalVerticalAxis.Z : GlobalVerticalAxis.Y;
        }

        #endregion

        #region Fluent Functions

        public IOSGeometry CreateNode(Node node)
        {
            ComObject.CreateNode(node.Id, node.X, node.Y, node.Z);
            return this;
        }

        public IOSGeometry CreateBeam(Beam beam)
        {
            ComObject.CreateBeam(beam.Id, beam.StartNode.Id, beam.EndNode.Id);
            return this;
        }

        public IOSGeometry CreatePlate(Plate plate)
        {
            if (plate.D == null)
            {
                ComObject.CreatePlate(plate.Id, plate.A.Id, plate.B.Id, plate.C.Id, plate.A.Id);
            }
            else
            {
                ComObject.CreatePlate(plate.Id, plate.A.Id, plate.B.Id, plate.C.Id, plate.D.Id);
            }

            return this;
        }

        public IOSGeometry CreateGroupFrom(IEnumerable<EntityGroup> entityGroups)
        {
            entityGroups.ToList().ForEach(eg => CreateGroupFrom(eg));
            return this;
        }

        public IOSGeometry CreateGroupFrom(EntityGroup entityGroup)
        {
            ComObject.CreateGroupEx(GroupTypeHelpers.GetGroupType(entityGroup.EntityType), entityGroup.GroupName, entityGroup.Entities.Count, entityGroup.Entities.ToArray());
            return this;
        }

        #endregion

        #region Torque Distribution

        public List<(int NodeId, double Fx, double Fz)> DistributeTorque(string nodeGroupName, double torqueToBeDistributed, int nThreads)
        {
            HashSet<Node> nodes = GetEntitiesInGroup<Node>(nodeGroupName, nThreads);
            Node center = nodes.GetCenter();

            return nodes.DistributeTorque(center, torqueToBeDistributed);
        }

        #endregion

        #region Parametric Surfaces

        public int GetParametricSurfaceCount()
        {
            return ComObject.GetParametricSurfaceCount();
        }

        public bool RemoveParametricSurfaceMesh(int surfaceId)
        {
            ComObject.RemoveParametricSurfaceMesh(surfaceId);
            return true;
        }

        public (int SurfaceId, IEnumerable<Node> PeripheralNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, IEnumerable<Node> openingNodes, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> peripheralNodes = Node.GenerateNodesOnCircularPath(center, rOuter, divOuter, idLastUsedNode);
            CreateMultipleNodes(peripheralNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), peripheralNodes);
        }

        public (int SurfaceId, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, IEnumerable<Node> peripheralNodes, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> openingNodes = Node.GenerateNodesOnCircularPath(center, rInner, divInner, idLastUsedNode);
            CreateMultipleNodes(openingNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), openingNodes);
        }

        public (int SurfaceId, IEnumerable<Node> PeripheralNodes, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> peripheralNodes = Node.GenerateNodesOnCircularPath(center, rOuter, divOuter, idLastUsedNode);
            HashSet<Node> openingNodes = Node.GenerateNodesOnCircularPath(center, rInner, divInner, peripheralNodes.LastId());

            CreateMultipleNodes(peripheralNodes, 1);
            CreateMultipleNodes(openingNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), peripheralNodes, openingNodes);
        }

        public int CreateAnnularParametricSurface(string surfaceName, IEnumerable<Node> outerVertices, IEnumerable<Node> innerVertices,
                                                  SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            int surfaceId = DefineParametricSurface(surfaceName, outerVertices, type, autoGenerate);
            AddPolygonalRegionToSurface(surfaceId, innerVertices, RegionType.Opening);

            return surfaceId;
        }

        public (int SurfaceId, IEnumerable<Node> GeneratedNodes) CreateSolidCircularParametricSurface(string surfaceName, Node center, double radius, int divisions, int idLastUsedNode = 0,
                                                                                                      AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> nodes = Node.GenerateNodesOnCircularPath(center, radius, divisions, 0.0, idLastUsedNode);
            CreateMultipleNodes(nodes.ToHashSet(), 1);

            return (CreateSolidCircularParametricSurface(surfaceName, center, nodes, autoGenerate), nodes);
        }

        public int CreateSolidCircularParametricSurface(string surfaceName, Node center, IEnumerable<Node> vertices, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            int surfaceId = DefineParametricSurface(surfaceName, vertices, SurfaceType.None, autoGenerate);

            AddDensityPointToSurface(surfaceId, center, 1);
            AddParametricSurfaceToModel(surfaceId);

            return surfaceId;
        }

        public int AddDensityPointToSurface(int surfaceId, Node densityPoint, int density)
        {
            return AddDensityPointToSurface(surfaceId, densityPoint.X, densityPoint.Y, densityPoint.Z, density);
        }

        public int AddDensityPointToSurface(int surfaceId, double xDensityPoint, double yDensityPoint, double zDensityPoint, int density)
        {
            return ComObject.AddDensityPointToSurface(surfaceId, xDensityPoint, yDensityPoint, zDensityPoint, density);
        }

        public int AddDensityLineToSurface(int surfaceId, Node sNodeDensityLine, Node eNodeDensityLine, int sDensity, int eDensity, int nDivisions)
        {
            return ComObject.AddDensityLineToSurface(surfaceId, sNodeDensityLine.X, sNodeDensityLine.Y, sNodeDensityLine.Z, sDensity,
                                                                eNodeDensityLine.X, eNodeDensityLine.Y, eNodeDensityLine.Z, eDensity, nDivisions);
        }

        public int AddDensityLineToSurface(int surfaceId, double xDensityLineSNode, double yDensityLineSNode, double zDensityLineSNode, int sDensity,
                                                          double xDensityLineENode, double yDensityLineENode, double zDensityLineENode, int eDensity, int nDivisions)
        {
            return ComObject.AddDensityLineToSurface(surfaceId, xDensityLineSNode, yDensityLineSNode, zDensityLineSNode, sDensity,
                                                                xDensityLineENode, yDensityLineENode, zDensityLineENode, eDensity, nDivisions);
        }

        public int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, RegionType regionType)
        {
            int[] densities = Enumerable.Repeat(1, polygonNodes.Count()).ToArray();
            int[] edgeDivs = Enumerable.Repeat(1, polygonNodes.Count()).ToArray();

            return AddPolygonalRegionToSurface(surfaceId, polygonNodes, densities, edgeDivs, regionType);
        }

        public int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, int[] densities, int[] edgeDivs, RegionType regionType)
        {
            double[] xArrayRegion = polygonNodes.Select(n => n.X).ToArray();
            double[] yArrayRegion = polygonNodes.Select(n => n.Y).ToArray();
            double[] zArrayRegion = polygonNodes.Select(n => n.Z).ToArray();

            return AddPolygonalRegionToSurface(surfaceId, polygonNodes.Count(), xArrayRegion, yArrayRegion, zArrayRegion, densities, edgeDivs, regionType);
        }

        public int AddPolygonalRegionToSurface(int surfaceId, int countVertices, double[] xArrayRegion, double[] yArrayRegion, double[] zArrayRegion,
                                               int[] densities, int[] edgeDivs, RegionType regionType)
        {
            return ComObject.AddPolygonalRegionToSurface(surfaceId, countVertices, xArrayRegion, yArrayRegion, zArrayRegion, densities, edgeDivs, regionType);
        }

        public (int RegionId, int IdLastUsedNode) AddCircularRegionToSurface(int surfaceId, Node center, double radius, int divisions, RegionType regionType, int idLastUsedNode = 0)
        {
            HashSet<Node> nodes = Node.GenerateNodesOnCircularPath(center, radius, divisions, 0.0, idLastUsedNode);
            return (AddCircularRegionToSurface(surfaceId, nodes, regionType), nodes.LastId());
        }

        public int AddCircularRegionToSurface(int surfaceId, IEnumerable<Node> regionNodes, RegionType regionType)
        {
            return AddPolygonalRegionToSurface(surfaceId, regionNodes, regionType);
        }

        public int AddCircularRegionToSurface(int surfaceId, double xCenter, double yCenter, double zCenter, double rRegion, int nDivisions, int density, RegionType regionType)
        {
            return ComObject.AddCircularRegionToSurface(surfaceId, xCenter, yCenter, zCenter, rRegion, nDivisions, density, regionType);
        }

        public int DefineParametricSurface(string surfaceName, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            List<Node> verticesList = vertices.ToList();
            Node originNode = verticesList[0];
            Node xVertex = verticesList[1];
            Node yVertex = verticesList[2];

            return DefineParametricSurface(surfaceName, originNode, xVertex, yVertex, vertices, type, autoGenerate);
        }

        public int DefineParametricSurface(string surfaceName, Node originNode, Node localXNode, Node localYNode, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            return DefineParametricSurface(surfaceName, type, originNode.Id, localXNode.Id, localYNode.Id, vertices.Count(), vertices.Select(v => v.Id).ToArray(), (int)autoGenerate);
        }

        public int DefineParametricSurface(string surfaceName, SurfaceType type, int idOriginNode, int idLocalXNode, int idLocalYNode, int countVertices, int[] idsVertices, int autoGenerate)
        {
            return ComObject.DefineParametricSurface(surfaceName, type, idOriginNode, idLocalXNode, idLocalYNode, countVertices, idsVertices, autoGenerate);
        }

        public IOSGeometry AddAndCommitParametricSurfaceToModel(int surfaceId)
        {
            _ = AddParametricSurfaceToModel(surfaceId);
            _ = CommitParametricSurfaceMesh(surfaceId);
            return this;
        }

        public int AddParametricSurfaceToModel(int surfaceId) => ComObject.AddParametricSurfaceToModel(surfaceId);
        public int CommitParametricSurfaceMesh(int surfaceId) => ComObject.CommitParametricSurfaceMesh(surfaceId);

        #endregion

        #region Selections

        public IOSGeometry ClearNodeSelection() { ComObject.ClearNodeSelection(); return this; }
        public IOSGeometry ClearMemberSelection() { ComObject.ClearMemberSelection(); return this; }
        public IOSGeometry ClearPhysicalMemberSelection() { ComObject.ClearPhysicalMemberSelection(); return this; }
        public IOSGeometry ClearPlateSelection() { ComObject.ClearPlateSelection(); return this; }
        public IOSGeometry ClearSolidSelection() { ComObject.ClearSolidSelection(); return this; }
        public IOSGeometry ClearSurfaceSelection() { ComObject.ClearSurfaceSelection(); return this; }

        public bool SelectMultipleNodes(IEnumerable<Node> nodes) => SelectMultipleNodes(nodes.Select(n => n.Id));
        public bool SelectMultipleBeams(IEnumerable<Beam> beams) => SelectMultipleBeams(beams.Select(b => b.Id));
        public IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<Member> members) => SelectMultiplePhysicalMembers(members.Select(m => m.Id).ToArray());
        public bool SelectMultiplePlates(IEnumerable<Plate> plates) => SelectMultiplePlates(plates.Select(p => p.Id));

        public bool SelectMultipleNodes(IEnumerable<int> nodeIds) { ComObject.SelectMultipleNodes(nodeIds.ToArray()); return true; }
        public bool SelectMultipleBeams(IEnumerable<int> beamIds) { ComObject.SelectMultipleBeams(beamIds.ToArray()); return true; }
        public bool SelectMultiplePlates(IEnumerable<int> plateIds) { ComObject.SelectMultiplePlates(plateIds.ToArray()); return true; }
        public int SelectMultipleSolids(IEnumerable<int> solidIds) { var arr = solidIds.ToArray(); ComObject.SelectMultipleSolids(arr); return arr.Length; }
        public IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<int> memberIds) { ComObject.SelectMultiplePhysicalMembers(memberIds.ToArray()); return this; }

        public IOSGeometry SetNodeCoordinate(int id, double x, double y, double z) { ComObject.SetNodeCoordinate(id, x, y, z); return this; }

        public int GetLastNodeNo() => ComObject.GetLastNodeNo();
        public int GetLastBeamNo() => ComObject.GetLastBeamNo();
        public int GetLastPlateNo() => ComObject.GetLastPlateNo();

        public bool AnyItemsSelected()
        {
            return ComObject.GetNoOfSelectedNodes() > 0 || ComObject.GetNoOfSelectedBeams() > 0 || ComObject.GetNoOfSelectedPlates() > 0 ||
                   ComObject.GetNoOfSelectedSolids() > 0 || ComObject.GetNoOfSelectedSurfaces() > 0 || ComObject.GetNoOfSelectedPhysicalMembers() > 0;
        }

        public HashSet<int> GetSelectedPlatesIds()
        {
            dynamic count = ComObject.GetNoOfSelectedPlates();
            object plateNumbers = new int[count];
            ComObject.GetSelectedPlates(ref plateNumbers, 0);
            return ((int[])plateNumbers).ToHashSet();
        }

        public HashSet<Plate> GetSelectedPlatesExt()
        {
            HashSet<int> plateNumbersList = GetSelectedPlatesIds();
            HashSet<Plate> plates = new HashSet<Plate>();
            foreach (int p in plateNumbersList) { _ = plates.Add(GetPlate(p)); }
            return plates;
        }

        public HashSet<Plate> GetSelectedPlatesExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> plateNumbersList = GetSelectedPlatesIds();
            HashSet<Plate> plates = new HashSet<Plate>();
            foreach (int p in plateNumbersList) { _ = plates.Add(GetPlate(p, fromModelNodes)); }
            return plates;
        }

        public HashSet<int> GetSelectedBeamsIds()
        {
            dynamic count = ComObject.GetNoOfSelectedBeams();
            object beamNumbers = new int[count];
            ComObject.GetSelectedBeams(ref beamNumbers, 0);
            return ((int[])beamNumbers).ToHashSet();
        }

        public HashSet<Beam> GetSelectedBeamsExt()
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b)); }
            return beams;
        }

        public HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Beam> fromModelBeams)
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b, fromModelBeams)); }
            return beams;
        }

        public HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b, fromModelNodes)); }
            return beams;
        }

        public HashSet<int> GetSelectedNodesIds()
        {
            dynamic count = ComObject.GetNoOfSelectedNodes();
            object nodeNumbers = new int[count];
            ComObject.GetSelectedNodes(ref nodeNumbers, 0);
            return ((int[])nodeNumbers).ToHashSet();
        }

        public HashSet<Node> GetSelectedNodesExt()
        {
            HashSet<int> nodeNumbersList = GetSelectedNodesIds();
            HashSet<Node> nodes = new HashSet<Node>();
            foreach (int n in nodeNumbersList) { _ = nodes.Add(GetNode(n)); }
            return nodes;
        }

        public HashSet<Node> GetSelectedNodesExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> nodeNumbersList = GetSelectedNodesIds();
            HashSet<Node> nodes = new HashSet<Node>();
            foreach (int n in nodeNumbersList) { _ = nodes.Add(GetNode(n, fromModelNodes)); }
            return nodes;
        }

        public HashSet<int> GetSelectedEntitiesIds<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesIds();
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsIds();
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesIds();
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        public HashSet<T> GetSelectedEntities<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesExt() as HashSet<T>;
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsExt() as HashSet<T>;
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesExt() as HashSet<T>;
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        public HashSet<T> GetSelectedEntities<T>(IEnumerable<Node> fromModelNodes) where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesExt(fromModelNodes) as HashSet<T>;
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsExt(fromModelNodes) as HashSet<T>;
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesExt(fromModelNodes) as HashSet<T>;
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        #endregion

        #region Entity Retrieval

        public int AddNode(Node node) => ComObject.AddNode(node.X, node.Y, node.Z);

        public IEnumerable<string> GetGroupNames(GroupType groupType)
        {
            int groupCount = ComObject.GetGroupCount(groupType);
            object groupNames = new string[groupCount];
            ComObject.GetGroupNames(groupType, ref groupNames);
            return (string[])groupNames;
        }

        public IEnumerable<string> GetAllGroupNames()
        {
            List<string> groupNames = new List<string>();
            groupNames.AddRange(GetGroupNames(GroupType.Nodes));
            groupNames.AddRange(GetGroupNames(GroupType.Beams));
            groupNames.AddRange(GetGroupNames(GroupType.Plates));
            return groupNames;
        }

        public IEnumerable<int> GetIdsOfEntitiesInGroup(string groupName)
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityNumbers = new int[entityCount];
            ComObject.GetGroupEntities(groupName, ref entityNumbers);
            return ((int[])entityNumbers).AsEnumerable();
        }

        public IEnumerable<Node> GetPlateIncidence(int pId)
        {
            HashSet<Node> plateIncidence = new HashSet<Node>(4);
            object nodeAId = 0, nodeBId = 0, nodeCId = 0, nodeDId = 0;
            ComObject.GetPlateIncidence(pId, ref nodeAId, ref nodeBId, ref nodeCId, ref nodeDId);

            _ = plateIncidence.Add(GetNode((int)nodeAId));
            _ = plateIncidence.Add(GetNode((int)nodeBId));
            _ = plateIncidence.Add(GetNode((int)nodeCId));
            _ = plateIncidence.Add(GetNode((int)nodeDId));

            return plateIncidence.Where(n => n != null);
        }

        public Plate GetPlate(int pId)
        {
            Plate plate = new Plate { Id = pId };
            object nodeAId = 0, nodeBId = 0, nodeCId = 0, nodeDId = 0;
            ComObject.GetPlateIncidence(pId, ref nodeAId, ref nodeBId, ref nodeCId, ref nodeDId);

            plate.A = GetNode((int)nodeAId);
            plate.B = GetNode((int)nodeBId);
            plate.C = GetNode((int)nodeCId);
            plate.D = GetNode((int)nodeDId);

            return plate;
        }

        public Plate GetPlate(int pId, IEnumerable<Node> fromModelNodes)
        {
            Plate plate = new Plate { Id = pId };
            object nodeAId = 0, nodeBId = 0, nodeCId = 0, nodeDId = 0;
            ComObject.GetPlateIncidence(pId, ref nodeAId, ref nodeBId, ref nodeCId, ref nodeDId);

            plate.A = fromModelNodes.FirstOrDefault(n => n.Id == (int)nodeAId);
            plate.B = fromModelNodes.FirstOrDefault(n => n.Id == (int)nodeBId);
            plate.C = fromModelNodes.FirstOrDefault(n => n.Id == (int)nodeCId);
            plate.D = fromModelNodes.FirstOrDefault(n => n.Id == (int)nodeDId);

            return plate;
        }

        public Beam GetBeam(int beamNumber)
        {
            if (beamNumber == 0) return null;
            object sNodeId = 0, eNodeId = 0;
            ComObject.GetMemberIncidence(beamNumber, ref sNodeId, ref eNodeId);
            Node sNode = GetNode((int)sNodeId);
            Node eNode = GetNode((int)eNodeId);
            return new Beam(beamNumber, sNode, eNode);
        }

        public Beam GetBeam(int bId, IEnumerable<Node> fromModelNodes)
        {
            if (bId == 0) return null;
            object sNodeNumber = 0, eNodeNumber = 0;
            ComObject.GetMemberIncidence(bId, ref sNodeNumber, ref eNodeNumber);
            Node sNode = fromModelNodes.FirstOrDefault(n => n.Id == (int)sNodeNumber);
            Node eNode = fromModelNodes.FirstOrDefault(n => n.Id == (int)eNodeNumber);
            return new Beam(bId, sNode, eNode);
        }

        public Beam GetBeam(int bId, IEnumerable<Beam> fromModelNodes)
        {
            return bId != 0 ? fromModelNodes.FirstOrDefault(b => b.Id == bId) : null;
        }

        public Node GetNode(int nId)
        {
            if (nId == 0) return null;
            object x = 0.0, y = 0.0, z = 0.0;
            ComObject.GetNodeCoordinates(nId, ref x, ref y, ref z);
            return new Node(nId, (double)x, (double)y, (double)z);
        }

        public Node GetNode(int nId, IEnumerable<Node> fromModelNodes)
        {
            return fromModelNodes.FirstOrDefault(n => n.Id == nId);
        }

        public HashSet<int> GetAllEntitiesIds<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetAllNodesList().ToHashSet();
            if (typeof(T) == typeof(Beam)) return GetAllBeamsList().ToHashSet();
            if (typeof(T) == typeof(Plate)) return GetAllPlatesList().ToHashSet();
            return new HashSet<int>();
        }

        public IEnumerable<int> GetAllSolidsList()
        {
            dynamic nSolids = ComObject.GetSolidCount();
            object solidsList = new int[nSolids];
            ComObject.GetSolidList(ref solidsList);
            return (int[])solidsList;
        }

        public IEnumerable<int> GetAllPlatesList()
        {
            dynamic nPlates = ComObject.GetPlateCount();
            object platesList = new int[nPlates];
            ComObject.GetPlateList(ref platesList);
            return (int[])platesList;
        }

        public IEnumerable<int> GetAllBeamsList()
        {
            dynamic nBeams = ComObject.GetMemberCount();
            object beamsList = new int[nBeams];
            ComObject.GetBeamList(ref beamsList);
            return (int[])beamsList;
        }

        public IEnumerable<int> GetAllNodesList()
        {
            dynamic nNodes = ComObject.GetNodeCount();
            object nodesList = new int[nNodes];
            ComObject.GetNodeList(ref nodesList);
            return (int[])nodesList;
        }

        public IEnumerable<Beam> GetBeamsConnectedAtNode(Node node) => GetBeamsConnectedAtNode(node, 1);

        public IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId)
        {
            ValidateNodeId(nodeId);
            try
            {
                lock (_geometryComSyncRoot)
                {
                    List<Plate> connectedPlates = new List<Plate>();
                    foreach (int plateId in GetAllPlateIdsFromCom())
                    {
                        (int A, int B, int C, int D) incidence = GetPlateIncidenceIdsFromCom(plateId);
                        if (!ContainsNode(incidence, nodeId)) continue;
                        connectedPlates.Add(CreatePlateFromIncidence(plateId, incidence));
                    }
                    return connectedPlates;
                }
            }
            catch (COMException exception)
            {
                throw new InvalidOperationException($"OpenSTAAD failed to retrieve plates connected at node {nodeId}.", exception);
            }
        }

        public IEnumerable<Plate> GetPlatesConnectedAtNode(Node node)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNode(node.Id);
        }

        public IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId, IEnumerable<Plate> allPlates)
        {
            ValidateNodeId(nodeId);
            if (allPlates == null) throw new ArgumentNullException(nameof(allPlates));

            List<Plate> connectedPlates = new List<Plate>();
            foreach (Plate plate in allPlates)
            {
                if (plate != null && ContainsNode(plate, nodeId)) connectedPlates.Add(plate);
            }
            return connectedPlates;
        }

        public IEnumerable<Plate> GetPlatesConnectedAtNode(Node node, IEnumerable<Plate> allPlates)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNode(node.Id, allPlates);
        }

        #endregion

        #region Parallel

        public IOSGeometry CreateMultipleNodes(HashSet<Node> nodes, int nThreads)
        {
            nodes.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(n => ComObject.CreateNode(n.Id, n.X, n.Y, n.Z));
            return this;
        }

        public IOSGeometry CreateMultipleBeams(HashSet<Beam> beams, int nThreads)
        {
            beams.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(b => ComObject.CreateBeam(b.Id, b.StartNode.Id, b.EndNode.Id));
            return this;
        }

        public IOSGeometry CreateMultiplePlates(HashSet<Plate> plates, int nThreads)
        {
            plates.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(plate => ComObject.CreatePlate(plate.Id, plate.A.Id, plate.B.Id, plate.C.Id, plate.D.Id));
            return this;
        }

        public IOSGeometry DeleteExistingGeometry(int nThreads = 1)
        {
            DeleteAllSolids(nThreads);
            DeleteAllPlates(nThreads);
            DeleteAllBeams(nThreads);
            DeleteAllNodes(nThreads);
            return this;
        }

        public IOSGeometry DeleteAllSolids(int nThreads)
        {
            GetAllSolidsList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(s => ComObject.DeleteSolid(s));
            return this;
        }

        public IOSGeometry DeleteAllPlates(int nThreads)
        {
            GetAllPlatesList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(p => ComObject.DeletePlate(p));
            return this;
        }

        public IOSGeometry DeleteAllBeams(int nThreads)
        {
            GetAllBeamsList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(b => ComObject.DeleteBeam(b));
            return this;
        }

        public IOSGeometry DeleteAllNodes(int nThreads)
        {
            GetAllNodesList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(n => ComObject.DeleteNode(n));
            return this;
        }

        public IEnumerable<int> GetIdsOfEntitiesInGroups(IEnumerable<string> groupNames, int nThreads)
        {
            return groupNames.AsParallel().WithDegreeOfParallelism(nThreads).SelectMany(gn => GetIdsOfEntitiesInGroup(gn));
        }

        public HashSet<T> GetAllEntities<T>(IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = GetAllEntitiesIds<T>();
            return GetEntities<T>(entitiesIds, allNodes, nThreads).ToHashSet();
        }

        public HashSet<T> GetAllEntities<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = GetAllEntitiesIds<T>();
            return GetEntities<T>(entitiesIds, nThreads).ToHashSet();
        }

        public HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity
        {
            ConcurrentBag<T> entities = new ConcurrentBag<T>();

            if (typeof(T) == typeof(Node))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(nId => entities.Add(GetNode(nId, allNodes) as T));
            }
            else if (typeof(T) == typeof(Beam))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(bId => entities.Add(GetBeam(bId, allNodes) as T));
            }
            else if (typeof(T) == typeof(Plate))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(pId => entities.Add(GetPlate(pId, allNodes) as T));
            }

            return entities.ToHashSet();
        }

        public HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity
        {
            ConcurrentBag<T> entities = new ConcurrentBag<T>();

            if (typeof(T) == typeof(Node))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(nId => entities.Add(GetNode(nId) as T));
            }
            else if (typeof(T) == typeof(Beam))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(bId => entities.Add(GetBeam(bId) as T));
            }
            else if (typeof(T) == typeof(Plate))
            {
                entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(pId => entities.Add(GetPlate(pId) as T));
            }

            return entities.ToHashSet();
        }

        public HashSet<EntityGroup<T>> GetEntityGroups<T>(int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntities = GetAllEntities<T>(nThreads);
            return GetEntityGroups(allEntities, nThreads);
        }

        public HashSet<EntityGroup> GetEntityGroups<T>(string groupNameContaining, int nThreads) where T : class, IEntity
        {
            HashSet<EntityGroup<T>> entityGroups = GetEntityGroups<T>(nThreads).Where(g => g.GroupName.Contains(groupNameContaining)).ToHashSet();
            return EntityGroup<T>.ToNonGeneric(entityGroups);
        }

        public HashSet<EntityGroup<T>> GetEntityGroupsStartingWith<T>(string groupNameStartsWith, int nThreads) where T : class, IEntity
        {
            return GetEntityGroups<T>(nThreads).Where(g => g.GroupName.StartsWith(groupNameStartsWith)).ToHashSet();
        }

        public HashSet<EntityGroup<T>> GetEntityGroups<T>(HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            GroupType groupType = GroupTypeHelpers.GetGroupType(typeof(T));
            ConcurrentBag<EntityGroup<T>> entityGroups = new ConcurrentBag<EntityGroup<T>>();

            int groupsCount = ComObject.GetGroupCount(groupType);
            object groupNames = new string[groupsCount];
            ComObject.GetGroupNames(groupType, ref groupNames);

            ((string[])groupNames).AsParallel().WithDegreeOfParallelism(nThreads).ForAll(gn =>
            {
                EntityGroup<T> group = new EntityGroup<T>
                {
                    GroupName = gn,
                    Entities = GetEntitiesInGroup(gn, allEntities, nThreads)
                };
                entityGroups.Add(group);
            });

            return entityGroups.ToHashSet();
        }

        public HashSet<T> GetEntitiesInGroups<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity
        {
            return groupNames.AsParallel().WithDegreeOfParallelism(nThreads).SelectMany(gn => GetEntitiesInGroup<T>(gn, nThreads)).ToHashSet();
        }

        public HashSet<T> GetEntitiesInGroup<T>(string groupName, int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntitiesInModel = GetAllEntities<T>(nThreads);
            return GetEntitiesInGroup(groupName, allEntitiesInModel, nThreads);
        }

        public HashSet<T> GetEntitiesInGroup<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityIds = new int[entityCount];
            ComObject.GetGroupEntities(groupName, ref entityIds);

            int[] entityIdsArray = (int[])entityIds;
            return allEntities.AsParallel().WithDegreeOfParallelism(nThreads).Where(n => entityIdsArray.Contains(n.Id)).ToHashSet();
        }

        public IEnumerable<Node> GetUniqueNodesFromPlateGroup(string platesGroupName, int nThreads)
        {
            ConcurrentBag<Node> uniqueNodes = new ConcurrentBag<Node>();
            IEnumerable<int> plateNumbers = GetIdsOfEntitiesInGroup(platesGroupName);

            plateNumbers.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(p =>
                GetPlateIncidence(p).ToList().ForEach(n => uniqueNodes.Add(n)));

            return uniqueNodes.ToHashSet();
        }

        public IEnumerable<Beam> GetBeamsWithNoGroupAssignment(int nThreads)
        {
            HashSet<Beam> allBeams = GetAllEntities<Beam>(nThreads);
            HashSet<EntityGroup<Beam>> allBeamGroups = GetEntityGroups<Beam>(nThreads);
            return allBeams.Except(allBeamGroups.SelectMany(g => g.Entities));
        }

        public IEnumerable<Plate> GetPlatesWithNoGroupAssignment(int nThreads)
        {
            HashSet<Plate> allPlates = GetAllEntities<Plate>(nThreads);
            HashSet<EntityGroup<Plate>> platesWithGroups = GetEntityGroups<Plate>(nThreads);
            return allPlates.Except(platesWithGroups.SelectMany(g => g.Entities));
        }

        public IEnumerable<Beam> GetBeamsConnectedAtNode(Node node, int nThreads)
        {
            dynamic connectedCount = ComObject.GetNoOfBeamsConnectedAtNode(node.Id);
            object beamsList = new int[connectedCount];
            ComObject.GetBeamsConnectedAtNode(node.Id, ref beamsList);
            return GetEntities<Beam>((int[])beamsList, nThreads);
        }

        #endregion

        #region Async Methods

        public async Task<List<(int NodeId, double Fx, double Fz)>> DistributeTorqueAsync(string nodeGroupName, double torqueToBeDistributed, int nThreads)
        {
            HashSet<Node> nodes = await GetEntitiesInGroupAsync<Node>(nodeGroupName, nThreads);
            Node center = nodes.GetCenter();
            return nodes.DistributeTorque(center, torqueToBeDistributed);
        }

        public async Task<int[]> GetAllSolidsListAsync()
        {
            dynamic nSolids = await Task.Run(() => ComObject.GetSolidCount());
            object solidsList = new int[nSolids];
            await Task.Run(() => ComObject.GetSolidList(ref solidsList));
            return (int[])solidsList;
        }

        public async Task<int[]> GetAllPlatesListAsync()
        {
            dynamic nPlates = await Task.Run(() => ComObject.GetPlateCount());
            object platesList = new int[nPlates];
            await Task.Run(() => ComObject.GetPlateList(ref platesList));
            return (int[])platesList;
        }

        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId)
        {
            ValidateNodeId(nodeId);
            return Task.Run(() => GetPlatesConnectedAtNode(nodeId));
        }

        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNodeAsync(node.Id);
        }

        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId, IEnumerable<Plate> allPlates)
        {
            ValidateNodeId(nodeId);
            if (allPlates == null) throw new ArgumentNullException(nameof(allPlates));
            return Task.Run(() => GetPlatesConnectedAtNode(nodeId, allPlates));
        }

        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node, IEnumerable<Plate> allPlates)
        {
            ValidateNode(node);
            if (allPlates == null) throw new ArgumentNullException(nameof(allPlates));
            return GetPlatesConnectedAtNodeAsync(node.Id, allPlates);
        }

        public async Task<int[]> GetAllBeamsListAsync()
        {
            dynamic nBeams = await Task.Run(() => ComObject.GetMemberCount());
            object beamsList = new int[nBeams];
            await Task.Run(() => ComObject.GetBeamList(ref beamsList));
            return (int[])beamsList;
        }

        public async Task<int[]> GetAllNodesListAsync()
        {
            dynamic nNodes = await Task.Run(() => ComObject.GetNodeCount());
            object nodesList = new int[nNodes];
            await Task.Run(() => ComObject.GetNodeList(ref nodesList));
            return (int[])nodesList;
        }

        public async Task<int[]> GetSelectedPlatesListAsync()
        {
            dynamic nPlates = await Task.Run(() => ComObject.GetNoOfSelectedPlates());
            object platesList = new int[nPlates];
            await Task.Run(() => ComObject.GetSelectedPlates(ref platesList, false));
            return (int[])platesList;
        }

        public async Task<int[]> GetSelectedBeamsListAsync()
        {
            dynamic nBeams = await Task.Run(() => ComObject.GetNoOfSelectedBeams());
            object beamsList = new int[nBeams];
            await Task.Run(() => ComObject.GetSelectedBeams(ref beamsList, false));
            return (int[])beamsList;
        }

        public async Task<int[]> GetSelectedNodesListAsync()
        {
            dynamic nNodes = await Task.Run(() => ComObject.GetNoOfSelectedNodes());
            object nodesList = new int[nNodes];
            await Task.Run(() => ComObject.GetSelectedNodes(ref nodesList, false));
            return (int[])nodesList;
        }

        public async Task<HashSet<int>> GetSelectedEntitiesIdsAsync<T>() where T : class, IEntity
        {
            IEnumerable<int> ids = new HashSet<int>();
            if (typeof(T) == typeof(Node)) ids = await GetSelectedNodesListAsync();
            else if (typeof(T) == typeof(Beam)) ids = await GetSelectedBeamsListAsync();
            else if (typeof(T) == typeof(Plate)) ids = await GetSelectedPlatesListAsync();
            return ids.ToHashSet();
        }

        public async Task<HashSet<int>> GetAllEntitiesIdsAsync<T>() where T : class, IEntity
        {
            IEnumerable<int> ids = new HashSet<int>();
            if (typeof(T) == typeof(Node)) ids = await GetAllNodesListAsync();
            else if (typeof(T) == typeof(Beam)) ids = await GetAllBeamsListAsync();
            else if (typeof(T) == typeof(Plate)) ids = await GetAllPlatesListAsync();
            return ids.ToHashSet();
        }

        public async Task<HashSet<T>> GetSelectedEntitiesAsync<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = await GetSelectedEntitiesIdsAsync<T>();
            return await GetEntitiesAsync<T>(entitiesIds, nThreads);
        }

        public async Task<HashSet<T>> GetAllEntitiesAsync<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = await GetAllEntitiesIdsAsync<T>();
            return await GetEntitiesAsync<T>(entitiesIds, nThreads);
        }

        public async Task<HashSet<T>> GetEntitiesAsync<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity
        {
            ConcurrentBag<T> entities = new ConcurrentBag<T>();

            if (typeof(T) == typeof(Node))
            {
                await Task.Run(() => entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(nId => entities.Add(GetNode(nId) as T)));
            }
            else if (typeof(T) == typeof(Beam))
            {
                await Task.Run(() => entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(bId => entities.Add(GetBeam(bId) as T)));
            }
            else if (typeof(T) == typeof(Plate))
            {
                await Task.Run(() => entitiesIds.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(pId => entities.Add(GetPlate(pId) as T)));
            }

            return entities.ToHashSet();
        }

        public async Task<HashSet<T>> GetEntitiesInGroupsAsync<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity
        {
            HashSet<T>[] results = await Task.WhenAll(groupNames.Select(gn => GetEntitiesInGroupAsync<T>(gn, nThreads)));
            return results.SelectMany(r => r).ToHashSet();
        }

        public async Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntitiesInModel = await GetAllEntitiesAsync<T>(nThreads);
            return await GetEntitiesInGroupAsync(groupName, allEntitiesInModel, nThreads);
        }

        public async Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityIds = new int[entityCount];
            await Task.Run(() => ComObject.GetGroupEntities(groupName, ref entityIds));

            int[] entityIdsArray = (int[])entityIds;
            return allEntities.AsParallel().WithDegreeOfParallelism(nThreads).Where(n => entityIdsArray.Contains(n.Id)).ToHashSet();
        }

        public async Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntities = await GetAllEntitiesAsync<T>(nThreads);
            return await GetEntityGroupsAsync(allEntities, nThreads);
        }

        public async Task<HashSet<EntityGroup>> GetEntityGroupsAsync<T>(string groupNameContaining, int nThreads) where T : class, IEntity
        {
            HashSet<EntityGroup<T>> entityGroups = await GetEntityGroupsAsync<T>(nThreads);
            return EntityGroup<T>.ToNonGeneric(entityGroups.Where(g => g.GroupName.Contains(groupNameContaining)).ToHashSet());
        }

        public async Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            GroupType groupType = GroupTypeHelpers.GetGroupType(typeof(T));
            ConcurrentBag<EntityGroup<T>> entityGroups = new ConcurrentBag<EntityGroup<T>>();

            int groupsCount = await Task.Run(() => ComObject.GetGroupCount(groupType));
            object groupNames = new string[groupsCount];
            await Task.Run(() => ComObject.GetGroupNames(groupType, ref groupNames));

            await Task.Run(() => ((string[])groupNames).AsParallel().WithDegreeOfParallelism(nThreads).ForAll(gn =>
            {
                EntityGroup<T> group = new EntityGroup<T>
                {
                    GroupName = gn,
                    Entities = GetEntitiesInGroup(gn, allEntities, nThreads)
                };
                entityGroups.Add(group);
            }));

            return entityGroups.ToHashSet();
        }

        #endregion

        #region Helpers

        private List<int> GetAllPlateIdsFromCom()
        {
            int plateCount = ConvertComInteger(ComObject.GetPlateCount(), "plate count");
            if (plateCount < 0) throw new InvalidOperationException("OpenSTAAD returned a negative plate count.");
            if (plateCount == 0) return new List<int>();

            object plateIdsVariant = new int[plateCount];
            ComObject.GetPlateList(ref plateIdsVariant);

            Array plateIds = plateIdsVariant as Array;
            if (plateIds == null || plateIds.Rank != 1 || plateIds.Length != plateCount)
            {
                throw new InvalidOperationException($"OpenSTAAD returned an invalid plate list; expected {plateCount} identifiers.");
            }

            int lowerBound = plateIds.GetLowerBound(0);
            List<int> result = new List<int>(plateCount);

            for (int index = 0; index < plateCount; index++)
            {
                int plateId = ConvertComInteger(plateIds.GetValue(lowerBound + index), $"plate identifier at index {index}");
                if (plateId <= 0) throw new InvalidOperationException($"OpenSTAAD returned invalid plate identifier {plateId}.");
                result.Add(plateId);
            }

            return result;
        }

        private (int A, int B, int C, int D) GetPlateIncidenceIdsFromCom(int plateId)
        {
            object nodeAId = 0, nodeBId = 0, nodeCId = 0, nodeDId = 0;
            ComObject.GetPlateIncidence(plateId, ref nodeAId, ref nodeBId, ref nodeCId, ref nodeDId);

            var incidence = (
                A: ConvertComInteger(nodeAId, $"plate {plateId} node A identifier"),
                B: ConvertComInteger(nodeBId, $"plate {plateId} node B identifier"),
                C: ConvertComInteger(nodeCId, $"plate {plateId} node C identifier"),
                D: ConvertComInteger(nodeDId, $"plate {plateId} node D identifier"));

            if (incidence.A <= 0 || incidence.B <= 0 || incidence.C <= 0 || incidence.D < 0)
            {
                throw new InvalidOperationException($"OpenSTAAD returned invalid incidence identifiers for plate {plateId}.");
            }

            return incidence;
        }

        private Plate CreatePlateFromIncidence(int plateId, (int A, int B, int C, int D) incidence)
        {
            return new Plate(
                plateId,
                GetNode(incidence.A),
                GetNode(incidence.B),
                GetNode(incidence.C),
                incidence.D > 0 ? GetNode(incidence.D) : null);
        }

        private static int ConvertComInteger(object value, string valueDescription)
        {
            try
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
            catch (Exception exception) when (
                exception is FormatException ||
                exception is InvalidCastException ||
                exception is OverflowException)
            {
                throw new InvalidOperationException($"OpenSTAAD returned an invalid {valueDescription}.", exception);
            }
        }

        private static bool ContainsNode((int A, int B, int C, int D) incidence, int nodeId)
        {
            return incidence.A == nodeId || incidence.B == nodeId || incidence.C == nodeId || incidence.D == nodeId;
        }

        private static bool ContainsNode(Plate plate, int nodeId)
        {
            return plate.A?.Id == nodeId || plate.B?.Id == nodeId || plate.C?.Id == nodeId || plate.D?.Id == nodeId;
        }

        private static void ValidateNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            ValidateNodeId(node.Id);
        }

        private static void ValidateNodeId(int nodeId)
        {
            if (nodeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(nodeId), nodeId, "Node ID must be greater than zero.");
            }
        }

        #endregion
    }
}
