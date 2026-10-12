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
using StaadPro.Interop.Services;

namespace StaadPro.Interop.Adapters.Models
{
    public class OSGeometryAdapter : OSBaseAdapter, IOSGeometry, IOSBase
    {
        private readonly object _geometryComSyncRoot = new object();

        #region Properties

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.P:ComObject']/*" />
        public dynamic ComObject { get; }

        #endregion

        #region Constructor

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSGeometryAdapter..ctor(OpenStaadWrapper)']/*" />
        public OSGeometryAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper?.RawGeometry;
        }

        #endregion

        #region Connectivity Groups

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregateBeamsByConnectivity`0(IEnumerable&lt;Beam&gt;)']/*" />
        public IEnumerable<IEnumerable<Beam>> SegregateBeamsByConnectivity(IEnumerable<Beam> beams)
        {
            return ConnectivityService.SegregateBeamsByConnectivity(beams);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregateMembersByConnectivity`0(IEnumerable&lt;Member&gt;,MemberConnectivityScope)']/*" />
        public IEnumerable<IEnumerable<Member>> SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)
        {
            return ConnectivityService.SegregateMembersByConnectivity(members, scope);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregatePlatesByConnectivity`0(IEnumerable&lt;Plate&gt;,PlateConnectivityScope)']/*" />
        public IEnumerable<IEnumerable<Plate>> SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)
        {
            return ConnectivityService.SegregatePlatesByConnectivity(plates, scope);
        }

        #endregion

        #region Coordinate Convention

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.IsZUp`0()']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetGlobalVerticalAxis`0()']/*" />
        public GlobalVerticalAxis GetGlobalVerticalAxis()
        {
            return IsZUp() ? GlobalVerticalAxis.Z : GlobalVerticalAxis.Y;
        }

        #endregion

        #region Fluent Functions

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateNode`0(Node)']/*" />
        public IOSGeometry CreateNode(Node node)
        {
            ComObject.CreateNode(node.Id, node.X, node.Y, node.Z);
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateBeam`0(Beam)']/*" />
        public IOSGeometry CreateBeam(Beam beam)
        {
            ComObject.CreateBeam(beam.Id, beam.StartNode.Id, beam.EndNode.Id);
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreatePlate`0(Plate)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateGroupFrom`0(IEnumerable&lt;EntityGroup&gt;)']/*" />
        public IOSGeometry CreateGroupFrom(IEnumerable<EntityGroup> entityGroups)
        {
            entityGroups.ToList().ForEach(eg => CreateGroupFrom(eg));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateGroupFrom`0(EntityGroup)']/*" />
        public IOSGeometry CreateGroupFrom(EntityGroup entityGroup)
        {
            ComObject.CreateGroupEx(GroupTypeHelpers.GetGroupType(entityGroup.EntityType), entityGroup.GroupName, entityGroup.Entities.Count, entityGroup.Entities.ToArray());
            return this;
        }

        #endregion

        #region Torque Distribution

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DistributeTorque`0(string,double,int)']/*" />
        public List<(int NodeId, double Fx, double Fz)> DistributeTorque(string nodeGroupName, double torqueToBeDistributed, int nThreads)
        {
            HashSet<Node> nodes = GetEntitiesInGroup<Node>(nodeGroupName, nThreads);
            Node center = nodes.GetCenter();

            return nodes.DistributeTorque(center, torqueToBeDistributed);
        }

        #endregion

        #region Parametric Surfaces

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetParametricSurfaceCount`0()']/*" />
        public int GetParametricSurfaceCount()
        {
            return ComObject.GetParametricSurfaceCount();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.RemoveParametricSurfaceMesh`0(int)']/*" />
        public bool RemoveParametricSurfaceMesh(int surfaceId)
        {
            ComObject.RemoveParametricSurfaceMesh(surfaceId);
            return true;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,double,int,IEnumerable&lt;Node&gt;,int,AutoGenerate)']/*" />
        public (int SurfaceId, IEnumerable<Node> PeripheralNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, IEnumerable<Node> openingNodes, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> peripheralNodes = Node.GenerateNodesOnCircularPath(center, rOuter, divOuter, idLastUsedNode);
            CreateMultipleNodes(peripheralNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), peripheralNodes);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,IEnumerable&lt;Node&gt;,double,int,int,AutoGenerate)']/*" />
        public (int SurfaceId, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, IEnumerable<Node> peripheralNodes, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> openingNodes = Node.GenerateNodesOnCircularPath(center, rInner, divInner, idLastUsedNode);
            CreateMultipleNodes(openingNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), openingNodes);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,double,int,double,int,int,AutoGenerate)']/*" />
        public (int SurfaceId, IEnumerable<Node> PeripheralNodes, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> peripheralNodes = Node.GenerateNodesOnCircularPath(center, rOuter, divOuter, idLastUsedNode);
            HashSet<Node> openingNodes = Node.GenerateNodesOnCircularPath(center, rInner, divInner, peripheralNodes.LastId());

            CreateMultipleNodes(peripheralNodes, 1);
            CreateMultipleNodes(openingNodes, 1);

            return (CreateAnnularParametricSurface(surfaceName, peripheralNodes, openingNodes), peripheralNodes, openingNodes);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,IEnumerable&lt;Node&gt;,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        public int CreateAnnularParametricSurface(string surfaceName, IEnumerable<Node> outerVertices, IEnumerable<Node> innerVertices,
                                                  SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            int surfaceId = DefineParametricSurface(surfaceName, outerVertices, type, autoGenerate);
            AddPolygonalRegionToSurface(surfaceId, innerVertices, RegionType.Opening);

            return surfaceId;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateSolidCircularParametricSurface`0(string,Node,double,int,int,AutoGenerate)']/*" />
        public (int SurfaceId, IEnumerable<Node> GeneratedNodes) CreateSolidCircularParametricSurface(string surfaceName, Node center, double radius, int divisions, int idLastUsedNode = 0,
                                                                                                      AutoGenerate autoGenerate = AutoGenerate.No)
        {
            HashSet<Node> nodes = Node.GenerateNodesOnCircularPath(center, radius, divisions, 0.0, idLastUsedNode);
            CreateMultipleNodes(nodes.ToHashSet(), 1);

            return (CreateSolidCircularParametricSurface(surfaceName, center, nodes, autoGenerate), nodes);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateSolidCircularParametricSurface`0(string,Node,IEnumerable&lt;Node&gt;,AutoGenerate)']/*" />
        public int CreateSolidCircularParametricSurface(string surfaceName, Node center, IEnumerable<Node> vertices, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            int surfaceId = DefineParametricSurface(surfaceName, vertices, SurfaceType.None, autoGenerate);

            AddDensityPointToSurface(surfaceId, center, 1);
            AddParametricSurfaceToModel(surfaceId);

            return surfaceId;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityPointToSurface`0(int,Node,int)']/*" />
        public int AddDensityPointToSurface(int surfaceId, Node densityPoint, int density)
        {
            return AddDensityPointToSurface(surfaceId, densityPoint.X, densityPoint.Y, densityPoint.Z, density);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityPointToSurface`0(int,double,double,double,int)']/*" />
        public int AddDensityPointToSurface(int surfaceId, double xDensityPoint, double yDensityPoint, double zDensityPoint, int density)
        {
            return ComObject.AddDensityPointToSurface(surfaceId, xDensityPoint, yDensityPoint, zDensityPoint, density);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityLineToSurface`0(int,Node,Node,int,int,int)']/*" />
        public int AddDensityLineToSurface(int surfaceId, Node sNodeDensityLine, Node eNodeDensityLine, int sDensity, int eDensity, int nDivisions)
        {
            return ComObject.AddDensityLineToSurface(surfaceId, sNodeDensityLine.X, sNodeDensityLine.Y, sNodeDensityLine.Z, sDensity,
                                                                eNodeDensityLine.X, eNodeDensityLine.Y, eNodeDensityLine.Z, eDensity, nDivisions);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityLineToSurface`0(int,double,double,double,int,double,double,double,int,int)']/*" />
        public int AddDensityLineToSurface(int surfaceId, double xDensityLineSNode, double yDensityLineSNode, double zDensityLineSNode, int sDensity,
                                                          double xDensityLineENode, double yDensityLineENode, double zDensityLineENode, int eDensity, int nDivisions)
        {
            return ComObject.AddDensityLineToSurface(surfaceId, xDensityLineSNode, yDensityLineSNode, zDensityLineSNode, sDensity,
                                                                xDensityLineENode, yDensityLineENode, zDensityLineENode, eDensity, nDivisions);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,IEnumerable&lt;Node&gt;,RegionType)']/*" />
        public int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, RegionType regionType)
        {
            int[] densities = Enumerable.Repeat(1, polygonNodes.Count()).ToArray();
            int[] edgeDivs = Enumerable.Repeat(1, polygonNodes.Count()).ToArray();

            return AddPolygonalRegionToSurface(surfaceId, polygonNodes, densities, edgeDivs, regionType);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,IEnumerable&lt;Node&gt;,int[],int[],RegionType)']/*" />
        public int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, int[] densities, int[] edgeDivs, RegionType regionType)
        {
            double[] xArrayRegion = polygonNodes.Select(n => n.X).ToArray();
            double[] yArrayRegion = polygonNodes.Select(n => n.Y).ToArray();
            double[] zArrayRegion = polygonNodes.Select(n => n.Z).ToArray();

            return AddPolygonalRegionToSurface(surfaceId, polygonNodes.Count(), xArrayRegion, yArrayRegion, zArrayRegion, densities, edgeDivs, regionType);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,int,double[],double[],double[],int[],int[],RegionType)']/*" />
        public int AddPolygonalRegionToSurface(int surfaceId, int countVertices, double[] xArrayRegion, double[] yArrayRegion, double[] zArrayRegion,
                                               int[] densities, int[] edgeDivs, RegionType regionType)
        {
            return ComObject.AddPolygonalRegionToSurface(surfaceId, countVertices, xArrayRegion, yArrayRegion, zArrayRegion, densities, edgeDivs, regionType);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,Node,double,int,RegionType,int)']/*" />
        public (int RegionId, int IdLastUsedNode) AddCircularRegionToSurface(int surfaceId, Node center, double radius, int divisions, RegionType regionType, int idLastUsedNode = 0)
        {
            HashSet<Node> nodes = Node.GenerateNodesOnCircularPath(center, radius, divisions, 0.0, idLastUsedNode);
            return (AddCircularRegionToSurface(surfaceId, nodes, regionType), nodes.LastId());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,IEnumerable&lt;Node&gt;,RegionType)']/*" />
        public int AddCircularRegionToSurface(int surfaceId, IEnumerable<Node> regionNodes, RegionType regionType)
        {
            return AddPolygonalRegionToSurface(surfaceId, regionNodes, regionType);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,double,double,double,double,int,int,RegionType)']/*" />
        public int AddCircularRegionToSurface(int surfaceId, double xCenter, double yCenter, double zCenter, double rRegion, int nDivisions, int density, RegionType regionType)
        {
            return ComObject.AddCircularRegionToSurface(surfaceId, xCenter, yCenter, zCenter, rRegion, nDivisions, density, regionType);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DefineParametricSurface`0(string,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        public int DefineParametricSurface(string surfaceName, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            List<Node> verticesList = vertices.ToList();
            Node originNode = verticesList[0];
            Node xVertex = verticesList[1];
            Node yVertex = verticesList[2];

            return DefineParametricSurface(surfaceName, originNode, xVertex, yVertex, vertices, type, autoGenerate);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSGeometryAdapter.DefineParametricSurface`0(string,Node,Node,Node,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        public int DefineParametricSurface(string surfaceName, Node originNode, Node localXNode, Node localYNode, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No)
        {
            return DefineParametricSurface(surfaceName, type, originNode.Id, localXNode.Id, localYNode.Id, vertices.Count(), vertices.Select(v => v.Id).ToArray(), (int)autoGenerate);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSGeometryAdapter.DefineParametricSurface`0(string,SurfaceType,int,int,int,int,int[],int)']/*" />
        public int DefineParametricSurface(string surfaceName, SurfaceType type, int idOriginNode, int idLocalXNode, int idLocalYNode, int countVertices, int[] idsVertices, int autoGenerate)
        {
            return ComObject.DefineParametricSurface(surfaceName, type, idOriginNode, idLocalXNode, idLocalYNode, countVertices, idsVertices, autoGenerate);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddAndCommitParametricSurfaceToModel`0(int)']/*" />
        public IOSGeometry AddAndCommitParametricSurfaceToModel(int surfaceId)
        {
            _ = AddParametricSurfaceToModel(surfaceId);
            _ = CommitParametricSurfaceMesh(surfaceId);
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddParametricSurfaceToModel`0(int)']/*" />
        public int AddParametricSurfaceToModel(int surfaceId) => ComObject.AddParametricSurfaceToModel(surfaceId);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CommitParametricSurfaceMesh`0(int)']/*" />
        public int CommitParametricSurfaceMesh(int surfaceId) => ComObject.CommitParametricSurfaceMesh(surfaceId);

        #endregion

        #region Selections

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearNodeSelection`0()']/*" />
        public IOSGeometry ClearNodeSelection() { ComObject.ClearNodeSelection(); return this; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearMemberSelection`0()']/*" />
        public IOSGeometry ClearMemberSelection() { ComObject.ClearMemberSelection(); return this; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearPhysicalMemberSelection`0()']/*" />
        public IOSGeometry ClearPhysicalMemberSelection() { ComObject.ClearPhysicalMemberSelection(); return this; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearPlateSelection`0()']/*" />
        public IOSGeometry ClearPlateSelection() { ComObject.ClearPlateSelection(); return this; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearSolidSelection`0()']/*" />
        public IOSGeometry ClearSolidSelection() { ComObject.ClearSolidSelection(); return this; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearSurfaceSelection`0()']/*" />
        public IOSGeometry ClearSurfaceSelection() { ComObject.ClearSurfaceSelection(); return this; }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleNodes`0(IEnumerable&lt;Node&gt;)']/*" />
        public bool SelectMultipleNodes(IEnumerable<Node> nodes) => SelectMultipleNodes(nodes.Select(n => n.Id));
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleBeams`0(IEnumerable&lt;Beam&gt;)']/*" />
        public bool SelectMultipleBeams(IEnumerable<Beam> beams) => SelectMultipleBeams(beams.Select(b => b.Id));
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePhysicalMembers`0(IEnumerable&lt;Member&gt;)']/*" />
        public IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<Member> members) => SelectMultiplePhysicalMembers(members.Select(m => m.Id).ToArray());
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePlates`0(IEnumerable&lt;Plate&gt;)']/*" />
        public bool SelectMultiplePlates(IEnumerable<Plate> plates) => SelectMultiplePlates(plates.Select(p => p.Id));

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleNodes`0(IEnumerable&lt;int&gt;)']/*" />
        public bool SelectMultipleNodes(IEnumerable<int> nodeIds) { ComObject.SelectMultipleNodes(nodeIds.ToArray()); return true; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleBeams`0(IEnumerable&lt;int&gt;)']/*" />
        public bool SelectMultipleBeams(IEnumerable<int> beamIds) { ComObject.SelectMultipleBeams(beamIds.ToArray()); return true; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePlates`0(IEnumerable&lt;int&gt;)']/*" />
        public bool SelectMultiplePlates(IEnumerable<int> plateIds) { ComObject.SelectMultiplePlates(plateIds.ToArray()); return true; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleSolids`0(IEnumerable&lt;int&gt;)']/*" />
        public int SelectMultipleSolids(IEnumerable<int> solidIds) { var arr = solidIds.ToArray(); ComObject.SelectMultipleSolids(arr); return arr.Length; }
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePhysicalMembers`0(IEnumerable&lt;int&gt;)']/*" />
        public IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<int> memberIds) { ComObject.SelectMultiplePhysicalMembers(memberIds.ToArray()); return this; }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SetNodeCoordinate`0(int,double,double,double)']/*" />
        public IOSGeometry SetNodeCoordinate(int id, double x, double y, double z) { ComObject.SetNodeCoordinate(id, x, y, z); return this; }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastNodeNo`0()']/*" />
        public int GetLastNodeNo() => ComObject.GetLastNodeNo();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastBeamNo`0()']/*" />
        public int GetLastBeamNo() => ComObject.GetLastBeamNo();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastPlateNo`0()']/*" />
        public int GetLastPlateNo() => ComObject.GetLastPlateNo();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AnyItemsSelected`0()']/*" />
        public bool AnyItemsSelected()
        {
            return ComObject.GetNoOfSelectedNodes() > 0 || ComObject.GetNoOfSelectedBeams() > 0 || ComObject.GetNoOfSelectedPlates() > 0 ||
                   ComObject.GetNoOfSelectedSolids() > 0 || ComObject.GetNoOfSelectedSurfaces() > 0 || ComObject.GetNoOfSelectedPhysicalMembers() > 0;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesIds`0()']/*" />
        public HashSet<int> GetSelectedPlatesIds()
        {
            dynamic count = ComObject.GetNoOfSelectedPlates();
            object plateNumbers = new int[count];
            ComObject.GetSelectedPlates(ref plateNumbers, 0);
            return ((int[])plateNumbers).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesExt`0()']/*" />
        public HashSet<Plate> GetSelectedPlatesExt()
        {
            HashSet<int> plateNumbersList = GetSelectedPlatesIds();
            HashSet<Plate> plates = new HashSet<Plate>();
            foreach (int p in plateNumbersList) { _ = plates.Add(GetPlate(p)); }
            return plates;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesExt`0(IEnumerable&lt;Node&gt;)']/*" />
        public HashSet<Plate> GetSelectedPlatesExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> plateNumbersList = GetSelectedPlatesIds();
            HashSet<Plate> plates = new HashSet<Plate>();
            foreach (int p in plateNumbersList) { _ = plates.Add(GetPlate(p, fromModelNodes)); }
            return plates;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsIds`0()']/*" />
        public HashSet<int> GetSelectedBeamsIds()
        {
            dynamic count = ComObject.GetNoOfSelectedBeams();
            object beamNumbers = new int[count];
            ComObject.GetSelectedBeams(ref beamNumbers, 0);
            return ((int[])beamNumbers).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0()']/*" />
        public HashSet<Beam> GetSelectedBeamsExt()
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b)); }
            return beams;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0(IEnumerable&lt;Beam&gt;)']/*" />
        public HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Beam> fromModelBeams)
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b, fromModelBeams)); }
            return beams;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0(IEnumerable&lt;Node&gt;)']/*" />
        public HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> beamNumbersList = GetSelectedBeamsIds();
            HashSet<Beam> beams = new HashSet<Beam>();
            foreach (int b in beamNumbersList) { _ = beams.Add(GetBeam(b, fromModelNodes)); }
            return beams;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesIds`0()']/*" />
        public HashSet<int> GetSelectedNodesIds()
        {
            dynamic count = ComObject.GetNoOfSelectedNodes();
            object nodeNumbers = new int[count];
            ComObject.GetSelectedNodes(ref nodeNumbers, 0);
            return ((int[])nodeNumbers).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesExt`0()']/*" />
        public HashSet<Node> GetSelectedNodesExt()
        {
            HashSet<int> nodeNumbersList = GetSelectedNodesIds();
            HashSet<Node> nodes = new HashSet<Node>();
            foreach (int n in nodeNumbersList) { _ = nodes.Add(GetNode(n)); }
            return nodes;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesExt`0(IEnumerable&lt;Node&gt;)']/*" />
        public HashSet<Node> GetSelectedNodesExt(IEnumerable<Node> fromModelNodes)
        {
            HashSet<int> nodeNumbersList = GetSelectedNodesIds();
            HashSet<Node> nodes = new HashSet<Node>();
            foreach (int n in nodeNumbersList) { _ = nodes.Add(GetNode(n, fromModelNodes)); }
            return nodes;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesIds`1()']/*" />
        public HashSet<int> GetSelectedEntitiesIds<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesIds();
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsIds();
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesIds();
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntities`1()']/*" />
        public HashSet<T> GetSelectedEntities<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesExt() as HashSet<T>;
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsExt() as HashSet<T>;
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesExt() as HashSet<T>;
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntities`1(IEnumerable&lt;Node&gt;)']/*" />
        public HashSet<T> GetSelectedEntities<T>(IEnumerable<Node> fromModelNodes) where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetSelectedNodesExt(fromModelNodes) as HashSet<T>;
            if (typeof(T) == typeof(Beam)) return GetSelectedBeamsExt(fromModelNodes) as HashSet<T>;
            if (typeof(T) == typeof(Plate)) return GetSelectedPlatesExt(fromModelNodes) as HashSet<T>;
            throw new NotSupportedException($"Entity type {typeof(T).Name} is not supported.");
        }

        #endregion

        #region Entity Retrieval

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddNode`0(Node)']/*" />
        public int AddNode(Node node) => ComObject.AddNode(node.X, node.Y, node.Z);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetGroupNames`0(GroupType)']/*" />
        public IEnumerable<string> GetGroupNames(GroupType groupType)
        {
            int groupCount = ComObject.GetGroupCount(groupType);
            object groupNames = new string[groupCount];
            ComObject.GetGroupNames(groupType, ref groupNames);
            return (string[])groupNames;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllGroupNames`0()']/*" />
        public IEnumerable<string> GetAllGroupNames()
        {
            List<string> groupNames = new List<string>();
            groupNames.AddRange(GetGroupNames(GroupType.Nodes));
            groupNames.AddRange(GetGroupNames(GroupType.Beams));
            groupNames.AddRange(GetGroupNames(GroupType.Plates));
            return groupNames;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetIdsOfEntitiesInGroup`0(string)']/*" />
        public IEnumerable<int> GetIdsOfEntitiesInGroup(string groupName)
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityNumbers = new int[entityCount];
            ComObject.GetGroupEntities(groupName, ref entityNumbers);
            return ((int[])entityNumbers).AsEnumerable();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlateIncidence`0(int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlate`0(int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlate`0(int,IEnumerable&lt;Node&gt;)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int)']/*" />
        public Beam GetBeam(int beamNumber)
        {
            if (beamNumber == 0) return null;
            object sNodeId = 0, eNodeId = 0;
            ComObject.GetMemberIncidence(beamNumber, ref sNodeId, ref eNodeId);
            Node sNode = GetNode((int)sNodeId);
            Node eNode = GetNode((int)eNodeId);
            return new Beam(beamNumber, sNode, eNode);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int,IEnumerable&lt;Node&gt;)']/*" />
        public Beam GetBeam(int bId, IEnumerable<Node> fromModelNodes)
        {
            if (bId == 0) return null;
            object sNodeNumber = 0, eNodeNumber = 0;
            ComObject.GetMemberIncidence(bId, ref sNodeNumber, ref eNodeNumber);
            Node sNode = fromModelNodes.FirstOrDefault(n => n.Id == (int)sNodeNumber);
            Node eNode = fromModelNodes.FirstOrDefault(n => n.Id == (int)eNodeNumber);
            return new Beam(bId, sNode, eNode);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int,IEnumerable&lt;Beam&gt;)']/*" />
        public Beam GetBeam(int bId, IEnumerable<Beam> fromModelNodes)
        {
            return bId != 0 ? fromModelNodes.FirstOrDefault(b => b.Id == bId) : null;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetNode`0(int)']/*" />
        public Node GetNode(int nId)
        {
            if (nId == 0) return null;
            object x = 0.0, y = 0.0, z = 0.0;
            ComObject.GetNodeCoordinates(nId, ref x, ref y, ref z);
            return new Node(nId, (double)x, (double)y, (double)z);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetNode`0(int,IEnumerable&lt;Node&gt;)']/*" />
        public Node GetNode(int nId, IEnumerable<Node> fromModelNodes)
        {
            return fromModelNodes.FirstOrDefault(n => n.Id == nId);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesIds`1()']/*" />
        public HashSet<int> GetAllEntitiesIds<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(Node)) return GetAllNodesList().ToHashSet();
            if (typeof(T) == typeof(Beam)) return GetAllBeamsList().ToHashSet();
            if (typeof(T) == typeof(Plate)) return GetAllPlatesList().ToHashSet();
            return new HashSet<int>();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllSolidsList`0()']/*" />
        public IEnumerable<int> GetAllSolidsList()
        {
            dynamic nSolids = ComObject.GetSolidCount();
            object solidsList = new int[nSolids];
            ComObject.GetSolidList(ref solidsList);
            return (int[])solidsList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllPlatesList`0()']/*" />
        public IEnumerable<int> GetAllPlatesList()
        {
            dynamic nPlates = ComObject.GetPlateCount();
            object platesList = new int[nPlates];
            ComObject.GetPlateList(ref platesList);
            return (int[])platesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllBeamsList`0()']/*" />
        public IEnumerable<int> GetAllBeamsList()
        {
            dynamic nBeams = ComObject.GetMemberCount();
            object beamsList = new int[nBeams];
            ComObject.GetBeamList(ref beamsList);
            return (int[])beamsList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllNodesList`0()']/*" />
        public IEnumerable<int> GetAllNodesList()
        {
            dynamic nNodes = ComObject.GetNodeCount();
            object nodesList = new int[nNodes];
            ComObject.GetNodeList(ref nodesList);
            return (int[])nodesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsConnectedAtNode`0(Node)']/*" />
        public IEnumerable<Beam> GetBeamsConnectedAtNode(Node node) => GetBeamsConnectedAtNode(node, 1);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(Node)']/*" />
        public IEnumerable<Plate> GetPlatesConnectedAtNode(Node node)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNode(node.Id);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(int,IEnumerable&lt;Plate&gt;)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(Node,IEnumerable&lt;Plate&gt;)']/*" />
        public IEnumerable<Plate> GetPlatesConnectedAtNode(Node node, IEnumerable<Plate> allPlates)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNode(node.Id, allPlates);
        }

        #endregion

        #region Parallel

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleNodes`0(HashSet&lt;Node&gt;,int)']/*" />
        public IOSGeometry CreateMultipleNodes(HashSet<Node> nodes, int nThreads)
        {
            nodes.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(n => ComObject.CreateNode(n.Id, n.X, n.Y, n.Z));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleBeams`0(HashSet&lt;Beam&gt;,int)']/*" />
        public IOSGeometry CreateMultipleBeams(HashSet<Beam> beams, int nThreads)
        {
            beams.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(b => ComObject.CreateBeam(b.Id, b.StartNode.Id, b.EndNode.Id));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultiplePlates`0(HashSet&lt;Plate&gt;,int)']/*" />
        public IOSGeometry CreateMultiplePlates(HashSet<Plate> plates, int nThreads)
        {
            plates.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(plate => ComObject.CreatePlate(plate.Id, plate.A.Id, plate.B.Id, plate.C.Id, plate.D.Id));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteExistingGeometry`0(int)']/*" />
        public IOSGeometry DeleteExistingGeometry(int nThreads = 1)
        {
            DeleteAllSolids(nThreads);
            DeleteAllPlates(nThreads);
            DeleteAllBeams(nThreads);
            DeleteAllNodes(nThreads);
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllSolids`0(int)']/*" />
        public IOSGeometry DeleteAllSolids(int nThreads)
        {
            GetAllSolidsList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(s => ComObject.DeleteSolid(s));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllPlates`0(int)']/*" />
        public IOSGeometry DeleteAllPlates(int nThreads)
        {
            GetAllPlatesList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(p => ComObject.DeletePlate(p));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllBeams`0(int)']/*" />
        public IOSGeometry DeleteAllBeams(int nThreads)
        {
            GetAllBeamsList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(b => ComObject.DeleteBeam(b));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllNodes`0(int)']/*" />
        public IOSGeometry DeleteAllNodes(int nThreads)
        {
            GetAllNodesList().AsParallel().WithDegreeOfParallelism(nThreads).ForAll(n => ComObject.DeleteNode(n));
            return this;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetIdsOfEntitiesInGroups`0(IEnumerable&lt;string&gt;,int)']/*" />
        public IEnumerable<int> GetIdsOfEntitiesInGroups(IEnumerable<string> groupNames, int nThreads)
        {
            return groupNames.AsParallel().WithDegreeOfParallelism(nThreads).SelectMany(gn => GetIdsOfEntitiesInGroup(gn));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntities`1(IEnumerable&lt;Node&gt;,int)']/*" />
        public HashSet<T> GetAllEntities<T>(IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = GetAllEntitiesIds<T>();
            return GetEntities<T>(entitiesIds, allNodes, nThreads).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntities`1(int)']/*" />
        public HashSet<T> GetAllEntities<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = GetAllEntitiesIds<T>();
            return GetEntities<T>(entitiesIds, nThreads).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntities`1(IEnumerable&lt;int&gt;,IEnumerable&lt;Node&gt;,int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntities`1(IEnumerable&lt;int&gt;,int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(int)']/*" />
        public HashSet<EntityGroup<T>> GetEntityGroups<T>(int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntities = GetAllEntities<T>(nThreads);
            return GetEntityGroups(allEntities, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(string,int)']/*" />
        public HashSet<EntityGroup> GetEntityGroups<T>(string groupNameContaining, int nThreads) where T : class, IEntity
        {
            HashSet<EntityGroup<T>> entityGroups = GetEntityGroups<T>(nThreads).Where(g => g.GroupName.Contains(groupNameContaining)).ToHashSet();
            return EntityGroup<T>.ToNonGeneric(entityGroups);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsStartingWith`1(string,int)']/*" />
        public HashSet<EntityGroup<T>> GetEntityGroupsStartingWith<T>(string groupNameStartsWith, int nThreads) where T : class, IEntity
        {
            return GetEntityGroups<T>(nThreads).Where(g => g.GroupName.StartsWith(groupNameStartsWith)).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(HashSet&lt;T&gt;,int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroups`1(IEnumerable&lt;string&gt;,int)']/*" />
        public HashSet<T> GetEntitiesInGroups<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity
        {
            return groupNames.AsParallel().WithDegreeOfParallelism(nThreads).SelectMany(gn => GetEntitiesInGroup<T>(gn, nThreads)).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroup`1(string,int)']/*" />
        public HashSet<T> GetEntitiesInGroup<T>(string groupName, int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntitiesInModel = GetAllEntities<T>(nThreads);
            return GetEntitiesInGroup(groupName, allEntitiesInModel, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroup`1(string,HashSet&lt;T&gt;,int)']/*" />
        public HashSet<T> GetEntitiesInGroup<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityIds = new int[entityCount];
            ComObject.GetGroupEntities(groupName, ref entityIds);

            int[] entityIdsArray = (int[])entityIds;
            return allEntities.AsParallel().WithDegreeOfParallelism(nThreads).Where(n => entityIdsArray.Contains(n.Id)).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetUniqueNodesFromPlateGroup`0(string,int)']/*" />
        public IEnumerable<Node> GetUniqueNodesFromPlateGroup(string platesGroupName, int nThreads)
        {
            ConcurrentBag<Node> uniqueNodes = new ConcurrentBag<Node>();
            IEnumerable<int> plateNumbers = GetIdsOfEntitiesInGroup(platesGroupName);

            plateNumbers.AsParallel().WithDegreeOfParallelism(nThreads).ForAll(p =>
                GetPlateIncidence(p).ToList().ForEach(n => uniqueNodes.Add(n)));

            return uniqueNodes.ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsWithNoGroupAssignment`0(int)']/*" />
        public IEnumerable<Beam> GetBeamsWithNoGroupAssignment(int nThreads)
        {
            HashSet<Beam> allBeams = GetAllEntities<Beam>(nThreads);
            HashSet<EntityGroup<Beam>> allBeamGroups = GetEntityGroups<Beam>(nThreads);
            return allBeams.Except(allBeamGroups.SelectMany(g => g.Entities));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesWithNoGroupAssignment`0(int)']/*" />
        public IEnumerable<Plate> GetPlatesWithNoGroupAssignment(int nThreads)
        {
            HashSet<Plate> allPlates = GetAllEntities<Plate>(nThreads);
            HashSet<EntityGroup<Plate>> platesWithGroups = GetEntityGroups<Plate>(nThreads);
            return allPlates.Except(platesWithGroups.SelectMany(g => g.Entities));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsConnectedAtNode`0(Node,int)']/*" />
        public IEnumerable<Beam> GetBeamsConnectedAtNode(Node node, int nThreads)
        {
            dynamic connectedCount = ComObject.GetNoOfBeamsConnectedAtNode(node.Id);
            object beamsList = new int[connectedCount];
            ComObject.GetBeamsConnectedAtNode(node.Id, ref beamsList);
            return GetEntities<Beam>((int[])beamsList, nThreads);
        }

        #endregion

        #region Async Methods

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DistributeTorqueAsync`0(string,double,int)']/*" />
        public async Task<List<(int NodeId, double Fx, double Fz)>> DistributeTorqueAsync(string nodeGroupName, double torqueToBeDistributed, int nThreads)
        {
            HashSet<Node> nodes = await GetEntitiesInGroupAsync<Node>(nodeGroupName, nThreads);
            Node center = nodes.GetCenter();
            return nodes.DistributeTorque(center, torqueToBeDistributed);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllSolidsListAsync`0()']/*" />
        public async Task<int[]> GetAllSolidsListAsync()
        {
            dynamic nSolids = await Task.Run(() => ComObject.GetSolidCount());
            object solidsList = new int[nSolids];
            await Task.Run(() => ComObject.GetSolidList(ref solidsList));
            return (int[])solidsList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllPlatesListAsync`0()']/*" />
        public async Task<int[]> GetAllPlatesListAsync()
        {
            dynamic nPlates = await Task.Run(() => ComObject.GetPlateCount());
            object platesList = new int[nPlates];
            await Task.Run(() => ComObject.GetPlateList(ref platesList));
            return (int[])platesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(int)']/*" />
        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId)
        {
            ValidateNodeId(nodeId);
            return Task.Run(() => GetPlatesConnectedAtNode(nodeId));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(Node)']/*" />
        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node)
        {
            ValidateNode(node);
            return GetPlatesConnectedAtNodeAsync(node.Id);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(int,IEnumerable&lt;Plate&gt;)']/*" />
        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId, IEnumerable<Plate> allPlates)
        {
            ValidateNodeId(nodeId);
            if (allPlates == null) throw new ArgumentNullException(nameof(allPlates));
            return Task.Run(() => GetPlatesConnectedAtNode(nodeId, allPlates));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(Node,IEnumerable&lt;Plate&gt;)']/*" />
        public Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node, IEnumerable<Plate> allPlates)
        {
            ValidateNode(node);
            if (allPlates == null) throw new ArgumentNullException(nameof(allPlates));
            return GetPlatesConnectedAtNodeAsync(node.Id, allPlates);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllBeamsListAsync`0()']/*" />
        public async Task<int[]> GetAllBeamsListAsync()
        {
            dynamic nBeams = await Task.Run(() => ComObject.GetMemberCount());
            object beamsList = new int[nBeams];
            await Task.Run(() => ComObject.GetBeamList(ref beamsList));
            return (int[])beamsList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllNodesListAsync`0()']/*" />
        public async Task<int[]> GetAllNodesListAsync()
        {
            dynamic nNodes = await Task.Run(() => ComObject.GetNodeCount());
            object nodesList = new int[nNodes];
            await Task.Run(() => ComObject.GetNodeList(ref nodesList));
            return (int[])nodesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesListAsync`0()']/*" />
        public async Task<int[]> GetSelectedPlatesListAsync()
        {
            dynamic nPlates = await Task.Run(() => ComObject.GetNoOfSelectedPlates());
            object platesList = new int[nPlates];
            await Task.Run(() => ComObject.GetSelectedPlates(ref platesList, false));
            return (int[])platesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsListAsync`0()']/*" />
        public async Task<int[]> GetSelectedBeamsListAsync()
        {
            dynamic nBeams = await Task.Run(() => ComObject.GetNoOfSelectedBeams());
            object beamsList = new int[nBeams];
            await Task.Run(() => ComObject.GetSelectedBeams(ref beamsList, false));
            return (int[])beamsList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesListAsync`0()']/*" />
        public async Task<int[]> GetSelectedNodesListAsync()
        {
            dynamic nNodes = await Task.Run(() => ComObject.GetNoOfSelectedNodes());
            object nodesList = new int[nNodes];
            await Task.Run(() => ComObject.GetSelectedNodes(ref nodesList, false));
            return (int[])nodesList;
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesIdsAsync`1()']/*" />
        public async Task<HashSet<int>> GetSelectedEntitiesIdsAsync<T>() where T : class, IEntity
        {
            IEnumerable<int> ids = new HashSet<int>();
            if (typeof(T) == typeof(Node)) ids = await GetSelectedNodesListAsync();
            else if (typeof(T) == typeof(Beam)) ids = await GetSelectedBeamsListAsync();
            else if (typeof(T) == typeof(Plate)) ids = await GetSelectedPlatesListAsync();
            return ids.ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesIdsAsync`1()']/*" />
        public async Task<HashSet<int>> GetAllEntitiesIdsAsync<T>() where T : class, IEntity
        {
            IEnumerable<int> ids = new HashSet<int>();
            if (typeof(T) == typeof(Node)) ids = await GetAllNodesListAsync();
            else if (typeof(T) == typeof(Beam)) ids = await GetAllBeamsListAsync();
            else if (typeof(T) == typeof(Plate)) ids = await GetAllPlatesListAsync();
            return ids.ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesAsync`1(int)']/*" />
        public async Task<HashSet<T>> GetSelectedEntitiesAsync<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = await GetSelectedEntitiesIdsAsync<T>();
            return await GetEntitiesAsync<T>(entitiesIds, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesAsync`1(int)']/*" />
        public async Task<HashSet<T>> GetAllEntitiesAsync<T>(int nThreads) where T : class, IEntity
        {
            IEnumerable<int> entitiesIds = await GetAllEntitiesIdsAsync<T>();
            return await GetEntitiesAsync<T>(entitiesIds, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesAsync`1(IEnumerable&lt;int&gt;,int)']/*" />
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

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupsAsync`1(IEnumerable&lt;string&gt;,int)']/*" />
        public async Task<HashSet<T>> GetEntitiesInGroupsAsync<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity
        {
            HashSet<T>[] results = await Task.WhenAll(groupNames.Select(gn => GetEntitiesInGroupAsync<T>(gn, nThreads)));
            return results.SelectMany(r => r).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupAsync`1(string,int)']/*" />
        public async Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntitiesInModel = await GetAllEntitiesAsync<T>(nThreads);
            return await GetEntitiesInGroupAsync(groupName, allEntitiesInModel, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupAsync`1(string,HashSet&lt;T&gt;,int)']/*" />
        public async Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity
        {
            int entityCount = ComObject.GetGroupEntityCount(groupName);
            object entityIds = new int[entityCount];
            await Task.Run(() => ComObject.GetGroupEntities(groupName, ref entityIds));

            int[] entityIdsArray = (int[])entityIds;
            return allEntities.AsParallel().WithDegreeOfParallelism(nThreads).Where(n => entityIdsArray.Contains(n.Id)).ToHashSet();
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(int)']/*" />
        public async Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(int nThreads) where T : class, IEntity
        {
            HashSet<T> allEntities = await GetAllEntitiesAsync<T>(nThreads);
            return await GetEntityGroupsAsync(allEntities, nThreads);
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(string,int)']/*" />
        public async Task<HashSet<EntityGroup>> GetEntityGroupsAsync<T>(string groupNameContaining, int nThreads) where T : class, IEntity
        {
            HashSet<EntityGroup<T>> entityGroups = await GetEntityGroupsAsync<T>(nThreads);
            return EntityGroup<T>.ToNonGeneric(entityGroups.Where(g => g.GroupName.Contains(groupNameContaining)).ToHashSet());
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(HashSet&lt;T&gt;,int)']/*" />
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
