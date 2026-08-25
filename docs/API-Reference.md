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

## `IOSLoad` Members
- `ClearPrimaryLoadCase(ILoadCase)`: Clears a single primary load case.
- `ClearPrimaryLoadCase(ILoadCase, bool isReferenceLoad)`: Clears a primary load case with reference load flag.
- `ClearPrimaryLoadCases(IEnumerable<ILoadCase>)`: Clears multiple primary load cases.
- `ClearPrimaryLoadCases(IEnumerable<ILoadCase>, bool isReferenceLoad)`: Clears multiple primary load cases with reference flag.
- `ClearPrimaryLoadCases(IEnumerable<int>)`: Clears primary load cases by IDs.
- `ClearPrimaryLoadCases(IEnumerable<int>, bool isReferenceLoad)`: Clears primary load cases by IDs with reference flag.
- `ClearReferenceLoadCase(ILoadCase)`: Clears a single reference load case.
- `ClearReferenceLoadCases(IEnumerable<ILoadCase>)`: Clears multiple reference load cases.
- `ClearReferenceLoadCases(IEnumerable<int>)`: Clears reference load cases by IDs.
