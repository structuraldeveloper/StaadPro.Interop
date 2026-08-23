using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenSTAADUI;
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

        OSGeometryUI ComObject { get; }

        bool IsZUp();

        GlobalVerticalAxis GetGlobalVerticalAxis();

        // -------------------------------------------------------------------------
        // Parametric surface and region creation
        // -------------------------------------------------------------------------

        int GetParametricSurfaceCount();

        bool RemoveParametricSurfaceMesh(int surfaceId);

        IOSGeometry AddAndCommitParametricSurfaceToModel(int surfaceId);

        int AddParametricSurfaceToModel(int surfaceId);

        int CommitParametricSurfaceMesh(int surfaceId);

        (int SurfaceId, IEnumerable<Node> PeripheralNodes, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        (int SurfaceId, IEnumerable<Node> OpeningNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, IEnumerable<Node> peripheralNodes, double rInner, int divInner, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        (int SurfaceId, IEnumerable<Node> PeripheralNodes)
            CreateAnnularParametricSurface(string surfaceName, Node center, double rOuter, int divOuter, IEnumerable<Node> openingNodes, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        int CreateAnnularParametricSurface(string surfaceName, IEnumerable<Node> outerVertices, IEnumerable<Node> innerVertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        int CreateSolidCircularParametricSurface(string surfaceName, Node center, IEnumerable<Node> vertices, AutoGenerate autoGenerate = AutoGenerate.No);

        (int SurfaceId, IEnumerable<Node> GeneratedNodes) CreateSolidCircularParametricSurface(string surfaceName, Node center, double radius, int divisions, int idLastUsedNode = 0, AutoGenerate autoGenerate = AutoGenerate.No);

        int AddDensityPointToSurface(int surfaceId, Node densityPoint, int density);

        int AddDensityPointToSurface(int surfaceId, double xDensityPoint, double yDensityPoint, double zDensityPoint, int density);

        int AddDensityLineToSurface(int surfaceId, Node sNodeDensityLine, Node eNodeDensityLine, int sDensity, int eDensity, int nDivisions);

        int AddDensityLineToSurface(int surfaceId, double xDensityLineSNode, double yDensityLineSNode, double zDensityLineSNode, int sDensity,
                                                   double xDensityLineENode, double yDensityLineENode, double zDensityLineENode, int eDensity, int nDivisions);

        int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, RegionType regionType);

        int AddPolygonalRegionToSurface(int surfaceId, IEnumerable<Node> polygonNodes, int[] densities, int[] edgeDivs, RegionType regionType);

        int AddPolygonalRegionToSurface(int surfaceId, int countVertices, double[] xArrayRegion, double[] yArrayRegion, double[] zArrayRegion, int[] densities, int[] edgeDivs, RegionType regionType);

        (int RegionId, int IdLastUsedNode) AddCircularRegionToSurface(int surfaceId, Node center, double radius, int divisions, RegionType regionType, int idLastUsedNode = 0);

        int AddCircularRegionToSurface(int surfaceId, IEnumerable<Node> regionNodes, RegionType regionType);

        int AddCircularRegionToSurface(int surfaceId, double xCenter, double yCenter, double zCenter, double rRegion, int nDivisions, int density, RegionType regionType);

        int DefineParametricSurface(string surfaceName, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        int DefineParametricSurface(string surfaceName, Node originNode, Node localXVertex, Node localYVertex, IEnumerable<Node> vertices, SurfaceType type = SurfaceType.None, AutoGenerate autoGenerate = AutoGenerate.No);

        int DefineParametricSurface(string surfaceName, SurfaceType type, int idOriginNode, int idLocalXVertex, int idLocalYVertex, int nVertices, int[] idsVertices, int autoGenerate);

        // -------------------------------------------------------------------------
        // Entity Selections & Queries
        // -------------------------------------------------------------------------

        IOSGeometry ClearNodeSelection();
        IOSGeometry ClearMemberSelection();
        IOSGeometry ClearPhysicalMemberSelection();
        IOSGeometry ClearPlateSelection();
        IOSGeometry ClearSolidSelection();
        IOSGeometry ClearSurfaceSelection();
        IOSGeometry SetNodeCoordinate(int id, double x, double y, double z);

        bool SelectMultipleNodes(IEnumerable<Node> nodes);
        bool SelectMultipleBeams(IEnumerable<Beam> beams);
        IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<Member> members);
        bool SelectMultiplePlates(IEnumerable<Plate> plates);

        bool SelectMultipleNodes(IEnumerable<int> nodeIds);
        bool SelectMultipleBeams(IEnumerable<int> beamIds);
        bool SelectMultiplePlates(IEnumerable<int> plateIds);
        int SelectMultipleSolids(IEnumerable<int> solidIds);
        IOSGeometry SelectMultiplePhysicalMembers(IEnumerable<int> memberIds);

        int GetLastNodeNo();
        int GetLastBeamNo();
        int GetLastPlateNo();

        int AddNode(Node node);
        bool AnyItemsSelected();

        IOSGeometry CreateBeam(Beam beam);
        IOSGeometry CreateGroupFrom(EntityGroup entityGroup);
        IOSGeometry CreateGroupFrom(IEnumerable<EntityGroup> entityGroups);

        IOSGeometry CreateMultiplePlates(HashSet<Plate> plates, int nThreads);
        IOSGeometry CreateNode(Node node);
        IOSGeometry CreatePlate(Plate plate);

        IEnumerable<int> GetAllBeamsList();
        IEnumerable<int> GetAllNodesList();
        IEnumerable<int> GetAllPlatesList();
        IEnumerable<int> GetAllSolidsList();

        Beam GetBeam(int beamNumber);
        Beam GetBeam(int bId, IEnumerable<Beam> fromModelNodes);
        Beam GetBeam(int bId, IEnumerable<Node> fromModelNodes);
        IEnumerable<Beam> GetBeamsConnectedAtNode(Node node);

        IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId);
        IEnumerable<Plate> GetPlatesConnectedAtNode(Node node);
        IEnumerable<Plate> GetPlatesConnectedAtNode(int nodeId, IEnumerable<Plate> allPlates);
        IEnumerable<Plate> GetPlatesConnectedAtNode(Node node, IEnumerable<Plate> allPlates);

        HashSet<T> GetAllEntities<T>(int nThreads) where T : class, IEntity;
        HashSet<int> GetAllEntitiesIds<T>() where T : class, IEntity;
        IEnumerable<string> GetAllGroupNames();

        HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity;
        HashSet<T> GetEntitiesInGroup<T>(string groupName, int nThreads) where T : class, IEntity;
        HashSet<T> GetEntitiesInGroups<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity;

        HashSet<EntityGroup<T>> GetEntityGroups<T>(int nThreads) where T : class, IEntity;
        HashSet<EntityGroup> GetEntityGroups<T>(string groupNameContaining, int nThreads) where T : class, IEntity;
        HashSet<EntityGroup<T>> GetEntityGroupsStartingWith<T>(string groupNameStartsWith, int nThreads) where T : class, IEntity;

        IEnumerable<string> GetGroupNames(GroupType groupType);
        IEnumerable<int> GetIdsOfEntitiesInGroup(string groupName);

        Node GetNode(int nId);
        Node GetNode(int nId, IEnumerable<Node> fromModelNodes);

        Plate GetPlate(int pId);
        Plate GetPlate(int pId, IEnumerable<Node> fromModelNodes);
        IEnumerable<Node> GetPlateIncidence(int pId);

        HashSet<Beam> GetSelectedBeamsExt();
        HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Beam> fromModelBeams);
        HashSet<Beam> GetSelectedBeamsExt(IEnumerable<Node> fromModelNodes);
        HashSet<int> GetSelectedBeamsIds();

        HashSet<T> GetSelectedEntities<T>() where T : class, IEntity;
        HashSet<T> GetSelectedEntities<T>(IEnumerable<Node> fromModelNodes) where T : class, IEntity;
        HashSet<int> GetSelectedEntitiesIds<T>() where T : class, IEntity;

        HashSet<Node> GetSelectedNodesExt();
        HashSet<Node> GetSelectedNodesExt(IEnumerable<Node> fromModelNodes);
        HashSet<int> GetSelectedNodesIds();

        HashSet<Plate> GetSelectedPlatesExt();
        HashSet<Plate> GetSelectedPlatesExt(IEnumerable<Node> fromModelNodes);
        HashSet<int> GetSelectedPlatesIds();

        IEnumerable<Node> GetUniqueNodesFromPlateGroup(string platesGroupName, int nThreads);

        // -------------------------------------------------------------------------
        // Parallel (multi-threaded) operations
        // -------------------------------------------------------------------------

        IOSGeometry CreateMultipleBeams(HashSet<Beam> beams, int nThreads);
        IOSGeometry CreateMultipleNodes(HashSet<Node> nodes, int nThreads);
        IOSGeometry DeleteAllBeams(int nThreads);
        IOSGeometry DeleteAllNodes(int nThreads);
        IOSGeometry DeleteAllPlates(int nThreads);
        IOSGeometry DeleteAllSolids(int nThreads);
        IOSGeometry DeleteExistingGeometry(int nThreads = 1);

        List<(int NodeId, double Fx, double Fz)> DistributeTorque(string nodeGroupName, double torqueToBeDistributed, int nThreads);

        HashSet<T> GetAllEntities<T>(IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity;
        HashSet<T> GetEntities<T>(IEnumerable<int> entitiesIds, IEnumerable<Node> allNodes, int nThreads) where T : class, IEntity;
        HashSet<T> GetEntitiesInGroup<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity;
        HashSet<EntityGroup<T>> GetEntityGroups<T>(HashSet<T> allEntities, int nThreads) where T : IEntity;

        IEnumerable<int> GetIdsOfEntitiesInGroups(IEnumerable<string> groupNames, int nThreads);
        IEnumerable<Beam> GetBeamsConnectedAtNode(Node node, int nThreads);
        IEnumerable<Beam> GetBeamsWithNoGroupAssignment(int nThreads);
        IEnumerable<Plate> GetPlatesWithNoGroupAssignment(int nThreads);

        // -------------------------------------------------------------------------
        // Asynchronous operations (Task-returning methods)
        // -------------------------------------------------------------------------

        Task<List<(int NodeId, double Fx, double Fz)>> DistributeTorqueAsync(string nodeGroupName, double torqueToBeDistributed, int nThreads);
        Task<int[]> GetAllBeamsListAsync();
        Task<HashSet<T>> GetAllEntitiesAsync<T>(int nThreads) where T : class, IEntity;
        Task<HashSet<int>> GetAllEntitiesIdsAsync<T>() where T : class, IEntity;
        Task<int[]> GetAllNodesListAsync();
        Task<int[]> GetAllPlatesListAsync();

        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId);
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node);
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(int nodeId, IEnumerable<Plate> allPlates);
        Task<IEnumerable<Plate>> GetPlatesConnectedAtNodeAsync(Node node, IEnumerable<Plate> allPlates);

        Task<int[]> GetAllSolidsListAsync();
        Task<HashSet<T>> GetEntitiesAsync<T>(IEnumerable<int> entitiesIds, int nThreads) where T : class, IEntity;
        Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, HashSet<T> allEntities, int nThreads) where T : IEntity;
        Task<HashSet<T>> GetEntitiesInGroupAsync<T>(string groupName, int nThreads) where T : class, IEntity;
        Task<HashSet<T>> GetEntitiesInGroupsAsync<T>(IEnumerable<string> groupNames, int nThreads) where T : class, IEntity;
        Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(HashSet<T> allEntities, int nThreads) where T : IEntity;
        Task<HashSet<EntityGroup<T>>> GetEntityGroupsAsync<T>(int nThreads) where T : class, IEntity;
        Task<HashSet<EntityGroup>> GetEntityGroupsAsync<T>(string groupNameContaining, int nThreads) where T : class, IEntity;

        Task<int[]> GetSelectedBeamsListAsync();
        Task<HashSet<T>> GetSelectedEntitiesAsync<T>(int nThreads) where T : class, IEntity;
        Task<HashSet<int>> GetSelectedEntitiesIdsAsync<T>() where T : class, IEntity;
        Task<int[]> GetSelectedNodesListAsync();
        Task<int[]> GetSelectedPlatesListAsync();
    }
}
