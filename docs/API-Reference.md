# StaadPro.Interop API Reference

## `IOSGeometry` Members
- `IsZUp()`: Returns whether the model uses global Z upwards.
- `GetGlobalVerticalAxis()`: Returns `GlobalVerticalAxis.Y` or `GlobalVerticalAxis.Z`.
- `CreateNode(Node)`: Creates a single node.
- `CreateBeam(Beam)`: Creates a single beam.
- `CreatePlate(Plate)`: Creates a 3-node or 4-node plate.
- `CreateAnnularParametricSurface(...)`: Creates an annular parametric surface with boundary generation.
- `CreateSolidCircularParametricSurface(...)`: Creates a circular parametric surface.
- `AddDensityPointToSurface(...)`: Adds mesh refinement density point.
- `AddDensityLineToSurface(...)`: Adds mesh density control line.
- `AddPolygonalRegionToSurface(...)`: Defines polygonal openings or refinements.
- `GetPlatesConnectedAtNode(int nodeId)`: Returns all incident plates for a node.
- `DistributeTorque(...)`: Distributes torsional load as equivalent nodal forces.
