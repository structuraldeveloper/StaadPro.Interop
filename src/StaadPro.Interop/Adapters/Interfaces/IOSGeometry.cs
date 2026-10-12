using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>
    /// Provides a managed, strongly-typed abstraction over the OpenSTAAD geometry COM API (OSGeometryUI).
    /// </summary>
    public interface IOSGeometry : IOSBase
    {
        // -------------------------------------------------------------------------
        // Core COM binding
        // -------------------------------------------------------------------------

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.P:ComObject']/*" />
        dynamic ComObject { get; }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleBeamsExtended']/*" />
        IOSGeometry CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleNodesExtended']/*" />
        IOSGeometry CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.MergeNodesExtended']/*" />
        IOSGeometry MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregateBeamsByConnectivity`0(IEnumerable&lt;Beam&gt;)']/*" />
        IEnumerable<IEnumerable<Beam>> SegregateBeamsByConnectivity(IEnumerable<Beam> beams);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregateMembersByConnectivity`0(IEnumerable&lt;Member&gt;,MemberConnectivityScope)']/*" />
        IEnumerable<IEnumerable<Member>> SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SegregatePlatesByConnectivity`0(IEnumerable&lt;Plate&gt;,PlateConnectivityScope)']/*" />
        IEnumerable<IEnumerable<Plate>> SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.IsZUp`0()']/*" />
        bool IsZUp();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetGlobalVerticalAxis`0()']/*" />
        GlobalVerticalAxis GetGlobalVerticalAxis();

        // -------------------------------------------------------------------------
        // Parametric surface and region creation
        // -------------------------------------------------------------------------

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetParametricSurfaceCount`0()']/*" />
        int GetParametricSurfaceCount();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.RemoveParametricSurfaceMesh`0(int)']/*" />
        bool RemoveParametricSurfaceMesh(int surfaceId);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddAndCommitParametricSurfaceToModel`0(int)']/*" />
        IOSGeometry AddAndCommitParametricSurfaceToModel(int surfaceId);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddParametricSurfaceToModel`0(int)']/*" />
        int AddParametricSurfaceToModel(int surfaceId);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CommitParametricSurfaceMesh`0(int)']/*" />
        int CommitParametricSurfaceMesh(int surfaceId);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,double,int,double,int,int,AutoGenerate)']/*" />
        (int SurfaceId, IEnumerable<Node> PeripheralNodes, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,IEnumerable&lt;Node&gt;,double,int,int,AutoGenerate)']/*" />
        (int SurfaceId, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, IEnumerable<Node> peripheralNodes, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,Node,double,int,IEnumerable&lt;Node&gt;,int,AutoGenerate)']/*" />
        (int SurfaceId, IEnumerable<Node> PeripheralNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, IEnumerable<Node> openingNodes, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateAnnularParametricSurface`0(string,IEnumerable&lt;Node&gt;,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        int CreateAnnularParametricSurface(string surfaceName, IEnumerable<Node> outerVertices, IEnumerable<Node> innerVertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateSolidCircularParametricSurface`0(string,Node,IEnumerable&lt;Node&gt;,AutoGenerate)']/*" />
        int CreateSolidCircularParametricSurface(string surfaceName, Node center, IEnumerable<Node> vertices, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateSolidCircularParametricSurface`0(string,Node,double,int,int,AutoGenerate)']/*" />
        (int SurfaceId, IEnumerable<Node> GeneratedNodes) CreateSolidCircularParametricSurface(string surfaceName, Node center, double radius, int divisions, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityPointToSurface`0(int,Node,int)']/*" />
        int AddDensityPointToSurface(int surfaceId, Node densityPoint, int density);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityPointToSurface`0(int,double,double,double,int)']/*" />
        int AddDensityPointToSurface(int surfaceId, double xDensityPoint, double yDensityPoint, double zDensityPoint, int density);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityLineToSurface`0(int,Node,Node,int,int,int)']/*" />
        int AddDensityLineToSurface(int surfaceId, Node sNodeDensityLine, Node eNodeDensityLine, int sDensity, int eDensity, int nDivisions);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddDensityLineToSurface`0(int,double,double,double,int,double,double,double,int,int)']/*" />
        int AddDensityLineToSurface(int surfaceId, double xDensityLineSNode, double yDensityLineSNode, double zDensityLineSNode, int sDensity,
                                                   double xDensityLineENode, double yDensityLineENode, double zDensityLineENode, int eDensity, int nDivisions);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,IEnumerable&lt;Node&gt;,RegionType)']/*" />
        int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, RegionType regionType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,IEnumerable&lt;Node&gt;,int[],int[],RegionType)']/*" />
        int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, int[] densities, int[] edgeDivs, RegionType regionType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddPolygonalRegionToSurface`0(int,int,double[],double[],double[],int[],int[],RegionType)']/*" />
        int AddPolygonalRegionToSurface(int surfaceId, int countVertices, double[] xArrayRegion, double[] yArrayRegion, double[] zArrayRegion, int[] densities, int[] edgeDivs, RegionType regionType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,Node,double,int,RegionType,int)']/*" />
        (int RegionId, int IdLastUsedNode) AddCircularRegionToSurface(int surfaceId, Node center, double radius, int divisions, RegionType regionType, int idLastUsedNode = 0);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,IEnumerable&lt;Node&gt;,RegionType)']/*" />
        int AddCircularRegionToSurface(int surfaceId, IEnumerable<Node> regionNodes, RegionType regionType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddCircularRegionToSurface`0(int,double,double,double,double,int,int,RegionType)']/*" />
        int AddCircularRegionToSurface(int surfaceId, double xCenter, double yCenter, double zCenter, double rRegion, int nDivisions, int density, RegionType regionType);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DefineParametricSurface`0(string,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        int DefineParametricSurface(string surfaceName, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DefineParametricSurface`0(string,Node,Node,Node,IEnumerable&lt;Node&gt;,SurfaceType,AutoGenerate)']/*" />
        int DefineParametricSurface(string surfaceName, Node originNode, Node localXVertex, Node localYVertex, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DefineParametricSurface`0(string,SurfaceType,int,int,int,int,int[],int)']/*" />
        int DefineParametricSurface(string surfaceName, SurfaceType type, int idOriginNode, int idLocalXVertex, int idLocalYVertex, int nVertices, int[] idsVertices, int autoGenerate);

        // -------------------------------------------------------------------------
        // Entity Selections & Queries
        // -------------------------------------------------------------------------

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearNodeSelection`0()']/*" />
        IOSGeometry ClearNodeSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearMemberSelection`0()']/*" />
        IOSGeometry ClearMemberSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearPhysicalMemberSelection`0()']/*" />
        IOSGeometry ClearPhysicalMemberSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearPlateSelection`0()']/*" />
        IOSGeometry ClearPlateSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearSolidSelection`0()']/*" />
        IOSGeometry ClearSolidSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.ClearSurfaceSelection`0()']/*" />
        IOSGeometry ClearSurfaceSelection();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SetNodeCoordinate`0(int,double,double,double)']/*" />
        IOSGeometry SetNodeCoordinate(int id, double x, double y, double z);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleNodes`0(IEnumerable&lt;Node&gt;)']/*" />
        bool SelectMultipleNodes(IEnumerable<Node> nodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleBeams`0(IEnumerable&lt;Beam&gt;)']/*" />
        bool SelectMultipleBeams(IEnumerable<Beam> beams);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePhysicalMembers`0(IEnumerable&lt;Member&gt;)']/*" />
        IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<Member> members);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePlates`0(IEnumerable&lt;Plate&gt;)']/*" />
        bool SelectMultiplePlates(IEnumerable<Plate> plates);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleNodes`0(IEnumerable&lt;int&gt;)']/*" />
        bool SelectMultipleNodes(IEnumerable<int> nodeIds);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleBeams`0(IEnumerable&lt;int&gt;)']/*" />
        bool SelectMultipleBeams(IEnumerable<int> beamIds);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePlates`0(IEnumerable&lt;int&gt;)']/*" />
        bool SelectMultiplePlates(IEnumerable<int> plateIds);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultipleSolids`0(IEnumerable&lt;int&gt;)']/*" />
        int SelectMultipleSolids(IEnumerable<int> solidIds);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.SelectMultiplePhysicalMembers`0(IEnumerable&lt;int&gt;)']/*" />
        IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<int> memberIds);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastNodeNo`0()']/*" />
        int GetLastNodeNo();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastBeamNo`0()']/*" />
        int GetLastBeamNo();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetLastPlateNo`0()']/*" />
        int GetLastPlateNo();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AddNode`0(Node)']/*" />
        int AddNode(Node node);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.AnyItemsSelected`0()']/*" />
        bool AnyItemsSelected();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateBeam`0(Beam)']/*" />
        IOSGeometry CreateBeam(Beam beam);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateGroupFrom`0(EntityGroup)']/*" />
        IOSGeometry CreateGroupFrom(EntityGroup entityGroup);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateGroupFrom`0(IEnumerable&lt;EntityGroup&gt;)']/*" />
        IOSGeometry CreateGroupFrom(IEnumerable<EntityGroup> entityGroups);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultiplePlates`0(HashSet&lt;Plate&gt;,int)']/*" />
        IOSGeometry CreateMultiplePlates(HashSet<Plate> plates, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateNode`0(Node)']/*" />
        IOSGeometry CreateNode(Node node);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreatePlate`0(Plate)']/*" />
        IOSGeometry CreatePlate(Plate plate);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllBeamsList`0()']/*" />
        IEnumerable<int> GetAllBeamsList();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllNodesList`0()']/*" />
        IEnumerable<int> GetAllNodesList();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllPlatesList`0()']/*" />
        IEnumerable<int> GetAllPlatesList();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllSolidsList`0()']/*" />
        IEnumerable<int> GetAllSolidsList();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int)']/*" />
        Beam GetBeam(int beamNumber);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int,IEnumerable&lt;Beam&gt;)']/*" />
        Beam GetBeam(int bId, IEnumerable<Beam> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeam`0(int,IEnumerable&lt;Node&gt;)']/*" />
        Beam GetBeam(int bId, IEnumerable<Node> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsConnectedAtNode`0(Node)']/*" />
        IEnumerable<Beam> GetBeamsConnectedAtNode(Node node);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(int)']/*" />
        IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(Node)']/*" />
        IEnumerable<Plate> GetPlatesConnectedAtNode(Node node);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(int,IEnumerable&lt;Plate&gt;)']/*" />
        IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId, IEnumerable<Plate> allPlates);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNode`0(Node,IEnumerable&lt;Plate&gt;)']/*" />
        IEnumerable<Plate> GetPlatesConnectedAtNode(Node node, IEnumerable<Plate> allPlates);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntities`1(int)']/*" />
        HashSet<T> GetAllEntities<T>(int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesIds`1()']/*" />
        HashSet<int> GetAllEntitiesIds<T>() where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllGroupNames`0()']/*" />
        IEnumerable<string> GetAllGroupNames();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntities`1(IEnumerable&lt;int&gt;,int)']/*" />
        HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroup`1(string,int)']/*" />
        HashSet<T> GetEntitiesInGroup<T>(string groupName, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroups`1(IEnumerable&lt;string&gt;,int)']/*" />
        HashSet<T> GetEntitiesInGroups<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(int)']/*" />
        HashSet<EntityGroup<T>> GetEntityGroups<T>(int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(string,int)']/*" />
        HashSet<EntityGroup> GetEntityGroups<T>(string groupNameContaining, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsStartingWith`1(string,int)']/*" />
        HashSet<EntityGroup<T>> GetEntityGroupsStartingWith<T>(string groupNameStartsWith, int nThreads) where T : class, IEntity;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetGroupNames`0(GroupType)']/*" />
        IEnumerable<string> GetGroupNames(GroupType groupType);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetIdsOfEntitiesInGroup`0(string)']/*" />
        IEnumerable<int> GetIdsOfEntitiesInGroup(string groupName);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetNode`0(int)']/*" />
        Node GetNode(int nId);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetNode`0(int,IEnumerable&lt;Node&gt;)']/*" />
        Node GetNode(int nId, IEnumerable<Node> fromModelNodes);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlate`0(int)']/*" />
        Plate GetPlate(int pId);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlate`0(int,IEnumerable&lt;Node&gt;)']/*" />
        Plate GetPlate(int pId, IEnumerable<Node> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlateIncidence`0(int)']/*" />
        IEnumerable<Node> GetPlateIncidence(int pId);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0()']/*" />
        HashSet<Beam> GetSelectedBeamsExt();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0(IEnumerable&lt;Beam&gt;)']/*" />
        HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Beam> fromModelBeams);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsExt`0(IEnumerable&lt;Node&gt;)']/*" />
        HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Node> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsIds`0()']/*" />
        HashSet<int> GetSelectedBeamsIds();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntities`1()']/*" />
        HashSet<T> GetSelectedEntities<T>() where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntities`1(IEnumerable&lt;Node&gt;)']/*" />
        HashSet<T> GetSelectedEntities<T>(IEnumerable<Node> fromModelNodes) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesIds`1()']/*" />
        HashSet<int> GetSelectedEntitiesIds<T>() where T : class, IEntity;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesExt`0()']/*" />
        HashSet<Node> GetSelectedNodesExt();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesExt`0(IEnumerable&lt;Node&gt;)']/*" />
        HashSet<Node> GetSelectedNodesExt(IEnumerable<Node> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesIds`0()']/*" />
        HashSet<int> GetSelectedNodesIds();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesExt`0()']/*" />
        HashSet<Plate> GetSelectedPlatesExt();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesExt`0(IEnumerable&lt;Node&gt;)']/*" />
        HashSet<Plate> GetSelectedPlatesExt(IEnumerable<Node> fromModelNodes);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesIds`0()']/*" />
        HashSet<int> GetSelectedPlatesIds();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetUniqueNodesFromPlateGroup`0(string,int)']/*" />
        IEnumerable<Node> GetUniqueNodesFromPlateGroup(string platesGroupName, int nThreads);

        // -------------------------------------------------------------------------
        // Parallel (multi-threaded) operations
        // -------------------------------------------------------------------------

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleBeams`0(HashSet&lt;Beam&gt;,int)']/*" />
        IOSGeometry CreateMultipleBeams(HashSet<Beam> beams, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.CreateMultipleNodes`0(HashSet&lt;Node&gt;,int)']/*" />
        IOSGeometry CreateMultipleNodes(HashSet<Node> nodes, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllBeams`0(int)']/*" />
        IOSGeometry DeleteAllBeams(int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllNodes`0(int)']/*" />
        IOSGeometry DeleteAllNodes(int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllPlates`0(int)']/*" />
        IOSGeometry DeleteAllPlates(int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteAllSolids`0(int)']/*" />
        IOSGeometry DeleteAllSolids(int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DeleteExistingGeometry`0(int)']/*" />
        IOSGeometry DeleteExistingGeometry(int nThreads = 1);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DistributeTorque`0(string,double,int)']/*" />
        List<(int NodeId, double Fx, double Fz)> DistributeTorque(string nodeGroupName, double torqueToBeDistributed, int nThreads);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntities`1(IEnumerable&lt;Node&gt;,int)']/*" />
        HashSet<T> GetAllEntities<T>(IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntities`1(IEnumerable&lt;int&gt;,IEnumerable&lt;Node&gt;,int)']/*" />
        HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroup`1(string,HashSet&lt;T&gt;,int)']/*" />
        HashSet<T> GetEntitiesInGroup<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroups`1(HashSet&lt;T&gt;,int)']/*" />
        HashSet<EntityGroup<T>> GetEntityGroups<T>(HashSet<T> allEntities, int nThreads) where T : IEntity;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetIdsOfEntitiesInGroups`0(IEnumerable&lt;string&gt;,int)']/*" />
        IEnumerable<int> GetIdsOfEntitiesInGroups(IEnumerable<string> groupNames, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsConnectedAtNode`0(Node,int)']/*" />
        IEnumerable<Beam> GetBeamsConnectedAtNode(Node node, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetBeamsWithNoGroupAssignment`0(int)']/*" />
        IEnumerable<Beam> GetBeamsWithNoGroupAssignment(int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesWithNoGroupAssignment`0(int)']/*" />
        IEnumerable<Plate> GetPlatesWithNoGroupAssignment(int nThreads);

        // -------------------------------------------------------------------------
        // Asynchronous operations (Task-returning methods)
        // -------------------------------------------------------------------------

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.DistributeTorqueAsync`0(string,double,int)']/*" />
        Task<List<(int NodeId, double Fx, double Fz)>> DistributeTorqueAsync(string nodeGroupName, double torqueToBeDistributed, int nThreads);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllBeamsListAsync`0()']/*" />
        Task<int[]> GetAllBeamsListAsync();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesAsync`1(int)']/*" />
        Task<HashSet<T>> GetAllEntitiesAsync<T>(int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllEntitiesIdsAsync`1()']/*" />
        Task<HashSet<int>> GetAllEntitiesIdsAsync<T>() where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllNodesListAsync`0()']/*" />
        Task<int[]> GetAllNodesListAsync();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllPlatesListAsync`0()']/*" />
        Task<int[]> GetAllPlatesListAsync();

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(int)']/*" />
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(Node)']/*" />
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(int,IEnumerable&lt;Plate&gt;)']/*" />
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId, IEnumerable<Plate> allPlates);
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetPlatesConnectedAtNodeAsync`0(Node,IEnumerable&lt;Plate&gt;)']/*" />
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node, IEnumerable<Plate> allPlates);

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetAllSolidsListAsync`0()']/*" />
        Task<int[]> GetAllSolidsListAsync();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesAsync`1(IEnumerable&lt;int&gt;,int)']/*" />
        Task<HashSet<T>> GetEntitiesAsync<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupAsync`1(string,HashSet&lt;T&gt;,int)']/*" />
        Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupAsync`1(string,int)']/*" />
        Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntitiesInGroupsAsync`1(IEnumerable&lt;string&gt;,int)']/*" />
        Task<HashSet<T>> GetEntitiesInGroupsAsync<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(HashSet&lt;T&gt;,int)']/*" />
        Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(HashSet<T> allEntities, int nThreads) where T : IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(int)']/*" />
        Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetEntityGroupsAsync`1(string,int)']/*" />
        Task<HashSet<EntityGroup>> GetEntityGroupsAsync<T>(string groupNameContaining, int nThreads) where T : class, IEntity;

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedBeamsListAsync`0()']/*" />
        Task<int[]> GetSelectedBeamsListAsync();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesAsync`1(int)']/*" />
        Task<HashSet<T>> GetSelectedEntitiesAsync<T>(int nThreads) where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedEntitiesIdsAsync`1()']/*" />
        Task<HashSet<int>> GetSelectedEntitiesIdsAsync<T>() where T : class, IEntity;
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedNodesListAsync`0()']/*" />
        Task<int[]> GetSelectedNodesListAsync();
        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='IOSGeometry.GetSelectedPlatesListAsync`0()']/*" />
        Task<int[]> GetSelectedPlatesListAsync();
    }
}
