# Output managed workflows

**Updated:** 26 October 2026

This increment adds **25 public overloads** to IOSOutput and the single non-partial OSOutputAdapter: **23 adapted synchronous workflows from the read-only main project and two typed enabling overloads**. Output now exposes **71 managed overloads**, reaching **46 of 60 documented native names**. Fourteen native contracts remain pending. A managed batch, projection or composition does not represent another implemented native name.

## Choosing a workflow

| Requirement | API | Distinction |
| --- | --- | --- |
| Raw single-block steel packet | Existing GetMemberSteelDesignResults(memberId) | No Beam or governing-case hydration. |
| Beam plus governing case | GetSteelDesignResultsForBeam / ForBeams | Resolves a captured case identity from supplied metadata or this wrapper's Load adapter. |
| Ratio observations | GetMemberSteelUtilizationRatios / GetMemberSteelUtilizationRatio(Beam) | Does not update caller Beam objects. |
| Update caller ratio caches | PopulateMemberSteelUtilizationRatios | Stages all reads; an ordinary failed read prevents every managed assignment. |
| Native axes and actions | Numeric GetMemberEndForces with ResultCoordinateSystem | Asks STAAD for Local/Global; no reconstructed beta basis. |
| Actions with case labels | Title-based GetMemberEndForces | Preserves the exact supplied label and explicit axes with the observation. |
| Inspect an offset transfer | GetMemberEndForcesConsideringOffsets | Returns native global face actions, physical-joint transfer, offset and evidence. |
| Physical-joint action only | GetMemberEndForcesAtPhysicalJoint | Projects the audit packet; analytical member ID, not a native physical-member ID. |
| Geometry basis | GetMemberLocalCoordinateSystem | Pure managed geometry convention from supplied nodes/beta, not native orientation read-back. |
| One plate result family | Entity-based plate-center overloads | Existing numeric contracts, selected by case/plate IDs; no geometry hydration. |
| Four families together | GetPlateCenterResults | Four native observations per pair, not the combined eight-slot native query. |
| Ordered equilibrium checks | GetStaticCheckResults | Keeps duplicate requests and individual status; no equality-based result collapse. |
| Opposite support action | GetSupportForces | Negates all six native global reaction components, including moments. |

## Status, ownership and consistency

Inspect **IsSuccessful before Data**. A failed OutputQueryResult does not expose an invented payload. A batch owns an eager read-only Items list; each item retains requested IDs and its own success/failure. Empty validated batches are successful and query nothing. Ordinary native failures do not stop later batch items. Transport, stale binding and malformed-success exceptions abort extraction rather than publishing a partially qualified batch.

The complete beam design workflow reuses MemberSteelDesignResults without fabricating FY/FZ/MX or coercing every unknown design decision to FAIL. BeamSteelDesignResults retains the original mutable caller Beam alongside a captured MemberId and an owned OutputLoadCaseIdentity. A successful native packet with an unresolved governing case returns **MetadataUnavailable**; its successful native observation remains visible in Observations. Conflicting metadata for a case number is rejected before dispatch. Load-based overloads accept only this wrapper's current Load adapter and discover primary/combination metadata once per nonempty selection.

Only PopulateMemberSteelUtilizationRatios updates caller objects. Beam.UtilizationRatio is a nullable, nonserialized cache: null means no cached observation. All native ratio reads are staged before assigning any cache. Native false or ratio sentinel status prevents every assignment. A property setter, PropertyChanged handler or binding-check exception can interrupt the subsequent apply phase after earlier managed changes; no rollback or atomicity is promised. The method never writes a STAAD model.

Composite OutputQueryResult.Observations records contributing native functions, classified status and available native acknowledgement evidence. ManagedOperation names the composition; NativeFunction and native return fields describe the decisive query. Void geometry calls have written-output evidence without fabricated return flags. Legacy simple readers may not expose a raw acknowledgement; a missing native-code/Boolean field is not a synthesized success code.

Selections are materialized once before COM dispatch, limited to **100000** entries and, for plate batches, **100000 Cartesian pairs**. Plate batches use case-major/plate-minor order and preserve duplicate selections. Static batches preserve requested order/duplicates. Beam selections reject duplicate member IDs, since two mutable objects for one identity are ambiguous.

## Axes, units and offsets

Native Output values keep their units and sign conventions. Wrapper input-unit caches do not certify Output units. Plate-center “forces” contain five stresses; plate moments are moments per unit width. Result families remain distinct.

Offset workflows read Property.GetMemberGlobalOffset and native **global** analytical member-end forces. They preserve the main-project joint-to-face convention:

~~~text
face point = incidence joint + global offset
Fjoint = Fface
Mjoint = Mface + global offset × Fface
~~~

GetMemberEndForcesConsideringOffsets returns both observations with their points, offset and Property status. Explicit NoSpecification means an absent offset with recorded provenance; invalid references, missing members and malformed successful buffers never become zero offsets. All coordinate and offset lengths must be consistent with the force-times-length moment units. The ID overload reads incidence and void node-coordinate outputs strictly; the Beam overload uses caller-supplied hydrated geometry, which can be stale. The face/joint engineering convention still requires live qualification for the acquired native build.

GetMemberLocalCoordinateSystem is independent pure geometry math under the adapter's lifetime guard. X follows incidence. Projected global Y defines the nonvertical reference; near vertical, projected global Z defines Z. A finite beta angle rotates Y/Z about positive X. It reuses WPF Point3D/Vector3D and exposes named immutable axes plus ToLocal/ToGlobal. It does not hydrate beta, detect the native up-axis convention or certify agreement with native member orientation. Prefer native Local/Global selection for actual Output extraction.

## Threading, versions and state

All COM calls stay synchronous on the caller's established COM apartment. The degreeOfParallelism overloads accept **1 through 64** as a compatibility planning hint and execute native calls sequentially; they do not create Task.Run/PLINQ work. Main-source asynchronous convenience methods are not enabled by this increment.

Internal OutputWorkflowService composes the existing Output readers and Property/Geometry/Load adapters without adding another adapter class or public service. Binding checks reject disposal/replacement of participating native objects. Shared per-Output locking does not lock out raw calls, other adapters or external model edits. Multiple native reads are sequential observations, not an atomic or fresh solver snapshot.

The reused native contracts are common across the installed 2024–2026 help. No unsupported per-year behavior is guessed. Native version policy must use wrapper.StaadVersion, not host ApplicationVersion. This increment does not enable the fourteen unresolved native Output routes or thirteen supplemental unit candidates.

## Exact public overloads

Namespaces: StaadPro.Interop.Adapters.Interfaces; StaadPro.Interop.Entities; StaadPro.Interop.Enums; StaadPro.Interop.Enums.Output; StaadPro.Interop.Models.Output. No public reference outputs or tuples are introduced.

### 1. GetSteelDesignResultsForBeam

~~~csharp
OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(Beam beam, IEnumerable<ILoadCase> loadCases)
~~~

Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous.

Adapted source contract: `MemberSteelDesignResults GetSteelDesignResultsForBeam(Beam beam, IEnumerable<ILoadCase> allLoadCases)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.GetSteelDesignResultsForBeam(beam, loadCases);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 2. GetSteelDesignResultsForBeam

~~~csharp
OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(IOSLoad load, Beam beam)
~~~

Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Discovers primary and combination cases through the current Load adapter; reference cases are not governing design cases here. Pass this wrapper's Load adapter, not one acquired from a different model. Supplied-metadata overloads avoid discovery calls.

Adapted source contract: `MemberSteelDesignResults GetSteelDesignResultsForBeam(IOSLoad load, Beam beam)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.GetSteelDesignResultsForBeam(wrapper.Load, beam);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 3. GetSteelDesignResultsForBeams

~~~csharp
OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IEnumerable<Beam> beams, IEnumerable<ILoadCase> loadCases)
~~~

Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Selection is enumerated once before dispatch, retains order, and rejects duplicate member IDs. Ordinary native failures remain individual results; transport or malformed-data exceptions abort the operation.

Adapted source contract: `IEnumerable<MemberSteelDesignResults> GetSteelDesignResultsForBeams(IEnumerable<Beam> beams, IEnumerable<ILoadCase> allLoadCases)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.GetSteelDesignResultsForBeams(new[] { beam }, loadCases);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 4. GetSteelDesignResultsForBeams

~~~csharp
OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IOSLoad load, IEnumerable<Beam> beams)
~~~

Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Discovers primary/combination cases once per nonempty selection using this wrapper's current Load adapter. Empty selections perform no native discovery. Duplicate member IDs are rejected.

Adapted source contract: `IEnumerable<MemberSteelDesignResults> GetSteelDesignResultsForBeams(IOSLoad load, IEnumerable<Beam> beams)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.GetSteelDesignResultsForBeams(wrapper.Load, new[] { beam });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 5. GetMemberSteelUtilizationRatios

~~~csharp
OutputBatchResult<double> GetMemberSteelUtilizationRatios(HashSet<Beam> beams)
~~~

Unlike PopulateMemberSteelUtilizationRatios, this only reads. Snapshots the set once in its enumeration order; distinct beam objects with the same member ID are rejected. Retains each query's status and member identity, including failures. Ratios are dimensionless.

Adapted source contract: `Dictionary<Beam, double> GetMemberSteelUtilizationRatios(HashSet<Beam> beams)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.GetMemberSteelUtilizationRatios(new HashSet<Beam> { beam });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 6. PopulateMemberSteelUtilizationRatios

~~~csharp
OutputPopulationResult PopulateMemberSteelUtilizationRatios(HashSet<Beam> beams)
~~~

Unlike GetMemberSteelUtilizationRatios, this updates the caller-owned Beam.UtilizationRatio caches, never the STAAD model. Any ordinary failed query prevents all managed assignments. Empty selections apply nothing. Successful native reads are staged before assignment; an exception from a property setter, notification handler or lifetime check can interrupt assignment after earlier beams were updated. This is not an atomic rollback transaction. Prior cache values do not certify current design results. Ratios are dimensionless.

Adapted source contract: `void PopulateMemberSteelUtilizationRatios(HashSet<Beam> forBeams)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
var result = wrapper.Output.PopulateMemberSteelUtilizationRatios(new HashSet<Beam> { beam });
System.Console.WriteLine(result.IsSuccessful);
System.Console.WriteLine(result.WasApplied);
~~~

### 7. GetMemberEndForces

~~~csharp
OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd)
~~~

Unlike the three-argument numeric overload, this returns an identified packet containing the exact caller-supplied case title and coordinate system. The title is a label, not a native lookup or proof of case identity. Values keep native member-end signs; no support-reaction sign reversal is applied. Local X follows native member incidence; beta and native local conventions are handled by STAAD.

Adapted source contract: `MemberForces GetMemberEndForces(string lcTitle, int beamId, int loadCaseId, MemberEnd memberEnd)`.

~~~csharp
var result = wrapper.Output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 8. GetMemberEndForces

~~~csharp
OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd, ResultCoordinateSystem coordinateSystem)
~~~

Unlike the three-argument numeric overload, this returns an identified packet containing the exact caller-supplied case title and coordinate system. The title is a label, not a native lookup or proof of case identity. Values keep native member-end signs; no support-reaction sign reversal is applied. Local or Global is sent directly to the native call. This performs no managed axis reconstruction.

Adapted source contract: `MemberForces GetMemberEndForces(string lcTitle, int beamId, int loadCaseId, MemberEnd memberEnd, LocalOrGlobal coordinateSystem)`.

~~~csharp
var result = wrapper.Output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start, ResultCoordinateSystem.Global);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 9. GetMemberEndForcesConsideringOffsets

~~~csharp
OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)
~~~

Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Uses supplied hydrated incidence/coordinates; stale caller geometry cannot be certified against STAAD.

Adapted source contract: `OffsetAwareMemberEndForces GetMemberEndForcesConsideringOffsets(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var result = wrapper.Output.GetMemberEndForcesConsideringOffsets("Dead", beam, 1, MemberEnd.Start);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 10. GetMemberEndForcesAtPhysicalJoint

~~~csharp
OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)
~~~

Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Returns only the physical-joint projection; choose GetMemberEndForcesConsideringOffsets to inspect both face and joint results with the offset. This uses analytical members; it is not GetPhysicalMemberEndForces for a native physical-member ID. Supply hydrated incidence/coordinates.

Adapted source contract: `MemberForces GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var result = wrapper.Output.GetMemberEndForcesAtPhysicalJoint("Dead", beam, 1, MemberEnd.Start);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 11. GetMemberEndForcesConsideringOffsets

~~~csharp
OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd)
~~~

Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Reads incidence and node coordinates from the current Geometry binding with strict status/written-output validation.

Adapted source contract: `OffsetAwareMemberEndForces GetMemberEndForcesConsideringOffsets(string loadCaseTitle, int beamId, int loadCaseId, MemberEnd memberEnd)`.

~~~csharp
var result = wrapper.Output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.Start);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 12. GetMemberEndForcesAtPhysicalJoint

~~~csharp
OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd)
~~~

Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Returns only the physical-joint projection; choose GetMemberEndForcesConsideringOffsets to inspect both face and joint results with the offset. This uses analytical members; it is not GetPhysicalMemberEndForces for a native physical-member ID. Resolves native incidence/coordinates.

Adapted source contract: `MemberForces GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, int beamId, int loadCaseId, MemberEnd memberEnd)`.

~~~csharp
var result = wrapper.Output.GetMemberEndForcesAtPhysicalJoint("Dead", 17, 1, MemberEnd.Start);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 13. GetMemberLocalCoordinateSystem

~~~csharp
MemberCoordinateSystem GetMemberLocalCoordinateSystem(Beam member)
~~~

Uses no native result query. Unlike requesting native Local/Global axes, this is a geometry convention, not native orientation read-back. Supply hydrated distinct nodes and the intended finite beta angle. X follows start to end; projected global Y defines the reference except near vertical, where projected global Z defines Z. Beta rotates Y/Z about positive X. Native orientation rules or stale caller geometry can differ; prefer native axis selection for Output extraction. The returned pure transforms change axes without converting units.

Adapted source contract: `LocalCS GetMemberLocalCoordinateSystem(Beam member)`.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var result = wrapper.Output.GetMemberLocalCoordinateSystem(beam);
System.Console.WriteLine(result.XAxis);
~~~

### 14. GetPlateCenterForces

~~~csharp
OutputQueryResult<PlateCenterForces> GetPlateCenterForces(ILoadCase loadCase, Plate plate)
~~~

Returns the five native center force/stress components, not the three center moments or combined eight-value stress/moment packet. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation.

Adapted source contract: `PlateCenterForces GetPlateCenterForces(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterForces(loadCase, plate);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 15. GetPlateCenterMoments

~~~csharp
OutputQueryResult<PlateCenterMoments> GetPlateCenterMoments(ILoadCase loadCase, Plate plate)
~~~

Returns the three native center bending/twisting moments per unit width, not the five center force/stress components. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation.

Adapted source contract: `PlateCenterMoments GetPlateCenterMoments(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterMoments(loadCase, plate);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 16. GetPlateCenterVonMises

~~~csharp
OutputQueryResult<PlateCenterVonMises> GetPlateCenterVonMises(ILoadCase loadCase, Plate plate)
~~~

Returns top/bottom center von Mises stresses, not normal principal stresses or principal directions. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation.

Adapted source contract: `PlateCenterVonMises GetPlateCenterVonMises(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterVonMises(loadCase, plate);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 17. GetPlatePrincipalStresses

~~~csharp
OutputQueryResult<PlatePrincipalStresses> GetPlatePrincipalStresses(ILoadCase loadCase, Plate plate)
~~~

Returns top/bottom center normal principal stresses, not von Mises stresses or principal directions. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation.

Adapted source contract: `PlatePrincipalStresses GetPlatePrincipalStresses(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlatePrincipalStresses(loadCase, plate);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 18. GetPlateCenterResults

~~~csharp
OutputQueryResult<PlateCenterResults> GetPlateCenterResults(ILoadCase loadCase, Plate plate)
~~~

Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved.

Adapted source contract: `PlateCenterResults GetPlateCenterResults(ILoadCase loadCase, Plate plate)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterResults(loadCase, plate);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 19. GetPlateCenterResults

~~~csharp
OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)
~~~

Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved. Snapshots both sequences once, then queries case-major/plate-minor Cartesian order. Duplicate selections are retained; conflicting metadata for the same case number is rejected. Ordinary per-pair failures remain in Items. Empty products perform no native queries.

Adapted source contract: `IEnumerable<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterResults(new[] { loadCase }, new[] { plate });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 20. GetPlateCenterResults

~~~csharp
OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates, int degreeOfParallelism)
~~~

Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved. Snapshots both sequences once, then queries case-major/plate-minor Cartesian order. Duplicate selections are retained; conflicting metadata for the same case number is rejected. Ordinary per-pair failures remain in Items. Empty products perform no native queries. Unlike the main-source parallel implementation, degreeOfParallelism (1-64) is retained as a compatibility planning hint; every COM call remains serialized on the caller thread. It does not request native worker threads or promise parallel speedup.

Adapted source contract: `IEnumerable<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates, int nThreads)`.

~~~csharp
ILoadCase loadCase = new LoadCase(1, "Dead");
var plate = new Plate { Id = 17 };
var result = wrapper.Output.GetPlateCenterResults(new[] { loadCase }, new[] { plate }, 2);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 21. GetStaticCheckResults

~~~csharp
OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds)
~~~

Unlike GetStaticCheckResult(int), this returns an ordered list of independent case results, including duplicate case requests and failed observations. It does not collapse equal force records in a HashSet. Preserves native global total loads/reactions and their original signs; does not negate reactions, compute an equilibrium tolerance or assert that analysis is current.

Adapted source contract: `HashSet<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds)`.

~~~csharp
var result = wrapper.Output.GetStaticCheckResults(new[] { 1, 2 });
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 22. GetStaticCheckResults

~~~csharp
OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds, int degreeOfParallelism)
~~~

Unlike GetStaticCheckResult(int), this returns an ordered list of independent case results, including duplicate case requests and failed observations. It does not collapse equal force records in a HashSet. Preserves native global total loads/reactions and their original signs; does not negate reactions, compute an equilibrium tolerance or assert that analysis is current. degreeOfParallelism (1-64) is a compatibility planning hint; COM dispatch stays serialized on the caller thread and creates no tasks.

Adapted source contract: `HashSet<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds, int nThreads)`.

~~~csharp
var result = wrapper.Output.GetStaticCheckResults(new[] { 1, 2 }, 2);
foreach (var item in result.Items)
    if (item.IsSuccessful) System.Console.WriteLine(item.Data);
    else System.Console.WriteLine(item.Status);
~~~

### 23. GetSupportForces

~~~csharp
OutputQueryResult<ForceComponents> GetSupportForces(int nodeId, int loadCaseId)
~~~

Unlike GetSupportReactions, this negates all six global components, including moments, to preserve the main-project support-action convention. Native failure remains a failure without Data. No force or moment unit conversion, support assignment or model update is performed. Choose GetSupportReactions when the original STAAD reaction sign is required.

Adapted source contract: `LoadCaseForces GetSupportForces(int supportId, int loadCaseId)`.

~~~csharp
var result = wrapper.Output.GetSupportForces(17, 1);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 24. GetMemberEndForces

~~~csharp
OutputQueryResult<ForceComponents> GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem)
~~~

Unlike the existing three-argument overload, this sends the requested Local or Global selector to STAAD. Unlike the title overloads, it returns only the six components with envelope identities. It does not reconstruct beta/local axes or transfer offsets. Native member-end signs are unchanged.

New typed enabling overload.

~~~csharp
var result = wrapper.Output.GetMemberEndForces(17, MemberEnd.Start, 1, ResultCoordinateSystem.Global);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

### 25. GetMemberSteelUtilizationRatio

~~~csharp
OutputQueryResult<double> GetMemberSteelUtilizationRatio(Beam beam)
~~~

Equivalent to the numeric-ID query, capturing the positive beam ID and checking it after dispatch. It does not populate Beam.UtilizationRatio; choose PopulateMemberSteelUtilizationRatios for staged cache updates. The ratio is dimensionless and this does not run or certify current steel design.

New typed enabling overload.

~~~csharp
var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
var result = wrapper.Output.GetMemberSteelUtilizationRatio(beam);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
else System.Console.WriteLine(result.Status);
~~~

## Verification and remaining work

The Release build and offline regression suite validate status/data isolation, dispatch, version fixtures, binding lifetime, metadata ambiguity, staged cache updates, geometry transforms, offset cross-product signs, selection bounds, duplicate ordering and malformed/unwritten outputs. Matching interface/adapter XML examples are compiled and executed from an isolated PackageReference consumer. Managed fixtures do not certify live COM marshaling or engineering accuracy.

The fourteen unresolved native routes and thirteen supplemental unit candidates remain in the [Output plan](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md). Main async, unresolved corner/point/cut and remaining convenience workflows retain separate implementation/qualification work. [Machine-readable contracts](contracts/OutputManagedWorkflowContractIndex.json) record the exact 23 source adaptations and two new enabling overloads.
