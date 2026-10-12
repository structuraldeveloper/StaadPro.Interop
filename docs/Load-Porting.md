# Load API porting and new assignments

The reference `IOSLoad` contains 115 method overloads. This repository now exposes 57: 52 matching reference overloads plus five repository-specific convenience overloads. There are 63 reference overloads still pending. Counts treat overloads separately and compare both interface and adapter method signatures.

The repository-specific methods are `CreatePrimaryLoadCases`, `CreateReferenceLoadCases` and three `AddPlateUniformPressure` overloads. They explain why subtracting 57 from 115 does not give the pending reference count.

## Newly ported functions

The following signatures preserve the reference method names, parameter names, types and order:

```csharp
IOSLoad AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs);
IOSLoad AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs);
IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient);
```

- **Reference loads:** snapshots source-case/factor pairs together, activates an existing primary target with `SetLoadActive`, then calls `AddReferenceLoad(int[] ids, double[] factors)`.
- **Repeat loads:** uses the same paired snapshot and activation contract, then calls `AddRepeatLoad(int[] ids, double[] factors)`. It does not enumerate `Keys` and `Values` independently.
- **Thermal loading:** snapshots entity IDs, activates the primary target and calls `AddTemperatureLoad(ids, axialThermalGradient, 0.0, 0.0)`. The supplied value is the raw first native temperature argument. The method does not divide by a length or interpret node coordinates; use the installed STAAD temperature-input convention. The four-parameter threaded overload remains pending.

All target/source/entity IDs must be positive, dictionaries and sequences must be non-null, and factors/thermal values must be finite. Negative and zero factors and thermal values are accepted. Thermal ID order and duplicates are preserved. Validation finishes before native activation or assignment; empty inputs return the same adapter without native access. Native case existence, source-family suitability and entity suitability are handled by OpenSTAAD.

Failed or unrecognized activation throws before assignment. Nonempty calls leave the target primary case active afterward, including when assignment throws. They do not restore previous activation, clear assignments, refresh managed collections, save the model or supply rollback. Native assignment return values are not interpreted; COM and binding exceptions propagate. Serialize session access on the appropriate COM thread.

```csharp
// With a connected wrapper and existing primary target 17, reference source 41,
// suitable repeat source 42 and native temperature-load entities 101/102:
wrapper.Load.AddReferenceLoadsTo(17, new Dictionary<int, double> { { 41, 1.25 } });
wrapper.Load.AddRepeatLoadsTo(17, new Dictionary<int, double> { { 42, -0.75 } });
wrapper.Load.AddAxialThermalGradientTo(17, new[] { 101, 102 }, 15.5);
```

## IntelliSense and validation

Each method has identical expanded XML on `IOSLoad` and `OSLoadAdapter`, including input units, native mapping, empty input, activation side effects, exceptions and executable examples. The XML ships beside the DLL and does not require source access.

Offline tests check paired arrays, double precision, one input snapshot, activation ordering/failure, native exception propagation and empty-input behavior. Installed-package verification compiles and runs the interface and concrete XML examples and checks argument layouts using managed native stand-ins. Live native mutations, engineering units and version-specific behavior remain unverified for these additions.

## Pending reference overloads

The following inventory was compared by method name, generic arity and parameter types. It lists source signatures, including supporting types that may themselves need porting. A name already present here can still appear when a different overload is pending.

- `IOSLoad AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)`
- `IOSLoad AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)`
- `IOSLoad AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)`
- `IOSLoad AddSelfweightInXYZTo(int loadCaseId, IEnumerable<int> entities, int nThreads, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)`
- `IOSLoad AddSelfweightInXYZTo(int loadCaseId, LoadCaseType lcType, IEnumerable<int> entities, SelfWeightDirection loadDirection = SelfWeightDirection.GlobalY, double factor = -1)`
- `IOSLoad CreateLoadCombinations(IEnumerable<EditorLoadCombination> loadCombinations)`
- `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<EditorLoadCombination> loadCombinations)`
- `IOSLoad CreateLoadEnvelopExt(int envelopId, LoadEnvelopType envelopType, IEnumerable<int> loadCaseList)`
- `HashSet<ILoadEnvelopeDetail> GetAllLoadEnvelopeDetails()`
- `HashSet<int> GetEnvelopeIds()`
- `StaadLoadCombination GetLoadCombination(int id)`
- `ILoadEnvelopeDetail GetLoadEnvelopeDetail(int envId)`
- `Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> GetMemberLoadsGroupedByLoadCase(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> GetNodalLoadsGroupedByLoadCase(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)`
- `HashSet<ILoadCase> GetRepeatLoadCases()`
- `void PopulateMemberConcentratedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberConcentratedMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberLinearVaryingLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `IEnumerable<Beam> PopulateMemberLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberTrapezoidalLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberUniformlyDistributedLoads(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `void PopulateMemberUniformMoments(IEnumerable<Beam> forBeams, IEnumerable<ILoadCase> fromLoadCases)`
- `IEnumerable<Node> PopulateNodalLoads(IEnumerable<Node> forNodes, IEnumerable<ILoadCase> fromLoadCases)`
- `dynamic CreateLoadList(LoadListType loadListType, IEnumerable<int> loadCaseIds)`
- `int AddWindDefinition(int typeNo, string typeName)`
- `int AddWindExposure(int typeNo, double exposureFactor, IEnumerable<int> nodeIds)`
- `int AddWindExposure(int typeNo, double exposureFactor, IEnumerable<Node> nodes)`
- `int AddWindExposure(int typeNo, double exposureFactor, int nodeId)`
- `int AddWindIntensity(int typeNo, double intensity, double height)`
- `int AddWindIntensity(int typeNo, IEnumerable<double> intensities, IEnumerable<double> heights)`
- `int AddWindIntensity(int typeNo, IEnumerable<WindIntensityPair> pairs)`
- `int DeleteWindDefinition(int typeNo = 0)`
- `int DeleteAllWindDefinitions()`
- `int AddWindDefinitionASCE7Parameters(ASCE7WindDefinitionParameters parameters)`
- `int ComputeWallWindPressureProfile(WallWindPressureProfileParameters parameters)`
- `int ComputeWallWindPressureProfileASCE72016(ASCE72016WallWindPressureProfileParameters parameters)`
- `bool AddSeismicDefinition(SeismicCode code, bool considerAccidentalTorsion = false)`
- `bool AddSeismicDefinition(int code, int accidentalTorsion = 0)`
- `int ModifySeismicDefinitionParam(string parameterName, double value)`
- `int ModifySeismicDefinitionParams(IDictionary<string, double> parameters)`
- `int ModifySeismicDefinitionParams(IS1893_2016SeismicParameters parameters)`
- `int ModifySeismicDefinitionParams(IS1893_2002SeismicParameters parameters)`
- `int ModifySeismicDefinitionParams(IBCSeismicParameters parameters)`
- `bool AddSeismicDefSelfWeight(double weightFactor = 1.0)`
- `int AddSeismicDefJointWeight(double weight, IEnumerable<int> nodeIds)`
- `int AddSeismicDefJointWeight(double weight, IEnumerable<Node> nodes)`
- `int AddSeismicDefJointWeight(double weight, int nodeId)`
- `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<int> memberIds)`
- `bool AddSeismicDefMemberWeight(int seismicCode, SeismicMemberLoadType loadType, double weight, double startDistance, double endDistance, IEnumerable<Beam> members)`
- `bool AddSeismicDefMemberUniformWeight(double weight, IEnumerable<int> memberIds, double startDistance = 0, double endDistance = 0, int seismicCode = 0)`
- `bool AddSeismicDefMemberConcentratedWeight(double weight, double distance, IEnumerable<int> memberIds, int seismicCode = 0)`
- `int AddSeismicDefWallArea(int typeNo, string direction, IEnumerable<double> dimensions)`
- `int AddSeismicDefWallArea(int typeNo, SeismicWallDirection direction, IEnumerable<SeismicWallDimension> walls)`
- `bool AddResponseSpectrumLoadEx(ResponseSpectrumLoadExParameters parameters)`
- `bool AddResponseSpectrumLoadEx(int rsaCode, int rsaCombination, string[] set1Names, double[] set1Vals, string[] set2Names = null, double[] set2Vals = null, double[] customDataPairs = null)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType, IEnumerable<int> memberIds, double param = 0.0)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType, IEnumerable<Beam> members, double param = 0.0)`
- `bool AddDirectAnalysisFlexParameter(double flexFactor, IEnumerable<int> memberIds)`
- `bool AddDirectAnalysisAxialParameter(IEnumerable<int> memberIds)`
- `bool AddDirectAnalysisDefinitionParameter(DirectAnalysisParameters parameters)`
- `bool DeleteDirectAnalysisDefinitionParameter(DirectAnalysisParameterType paramType)`
- `bool DeleteDirectAnalysisDefinition()`
- `bool AddRSLoad(ResponseSpectrumLoadParameters parameters)`
