# OpenSTAAD to IDEA StatiCa integration: source coverage, compatibility and porting plan

**Reviewed:** 23 October 2026<br>
**Baseline:** main at 1d8285ce66c95c60da55530d4c721fb7274893da<br>
**Status:** Documentation and architecture only. The existing IDEA StatiCa adapter remains a placeholder.

## 1. Findings and comparison boundary

IDEA StatiCa is a **managed integration across OpenSTAAD domains**, not a separate native OpenSTAAD automation interface. There is no equivalent Design/Commands/Table/View function-group count to compare across 2024, 2025 and 2026. Version compatibility must be inherited from each upstream native contract and separately established for the target IDEA import/mapping format.

The main source contains **24 interface methods and 24 public adapter methods**, an Options property, two constructors and 54 private adapter methods. The current repository contains only a binding-dependent placeholder and its inherited Wrapper property; no extraction, transformation, mapping, verification or export method is implemented.

The source provides substantially more than Excel export: connected-plate ownership, force/coordinate transforms, nodal and support equilibrium, explicit IDEA member mapping, round-trip verification, reusable mapping profiles, topology diagrams, single-joint and batch exports, cancellation/progress and provenance manifests.

These capabilities should be reused deliberately after the required Output/Property/Support operations exist. Porting only the outer adapter now would produce an unusable or misleading API. Non-null placeholder bindings do not establish readiness.

The [complete source API and model inventory](OpenSTAAD-IDEA-StatiCa-API-Coverage-and-Contract-Inventory.md) records all method overloads, parameters, return types, option/model properties, enum members, helper responsibilities and source references. [Machine-readable evidence](data/OpenSTAAD-IDEA-StatiCa-Source-Inventory.json) preserves declarations, call dependencies and read-only source fingerprints.

## 2. Evidence and limitations

The main interface/adapter, seven integration model files and twelve integration service files were read and parsed without modification or execution. Related Design/Commands/View dependencies were also fingerprinted during the remaining-domain review. Counts of helper declarations are not native method counts.

The implementation's force-convention reference was checked against the current official [IDEA coordinate systems and force conventions](https://developer.ideastatica.com/docs/iom/iom_coordinate_systems.html), including its distinction between structural internal forces and Connection load effects. This supports the sign-conversion review; it does not qualify the private implementation against every IDEA release.

No STAAD/IDEA project was opened or modified, no export was produced, and no main-project build was run. No IDEA SDK/API availability, license entitlement, file-schema support or native engineering correctness is certified by source inspection.

## 3. Compatibility and dependency matrix

| Dependency | Required source behavior | Current repository status | Version-selection consequence |
| --- | --- | --- | --- |
| Wrapper / Root | Native STAAD version, model path and units | Approved wrapper and Root surface implemented; caches/fallback provenance documented separately | Select upstream policies from StaadVersion, never ApplicationVersion. |
| Geometry | Node/member lookup, connected beams/plates, node groups and discovery | Reference overloads ported; live behavior still requires qualification | Reuse Geometry services and verified entity/coordinate contracts. |
| Load | Case enumeration, combinations and nodal loads | Reference overloads ported; result-case semantics still need integration fixtures | Preserve case identity, combination factors and cycle protection. |
| Output | Results readiness, member end forces/local basis, plate corner forces and support forces | Binding-only placeholder; comparison/plan completed | Principal blocker; invoke the appropriate qualified Output profile internally. |
| Property | Beta angles and member end offsets | Binding-only placeholder; comparison/plan completed | Principal blocker for correct local/global transforms and eccentric moments. |
| Support | Supported-node membership for optional support-reaction paths | Binding-only placeholder; comparison/plan completed | Required when support-aware equilibrium/closure is requested, not for every extraction. |
| IDEA mapping/import format | Joint/member IDs/names, axes, load positions, active components and source artifacts | No typed integration implementation | Separate format/version profile; STAAD year must not select an IDEA schema. |
| Artifact services | XLSX, HTML/SVG, JSON and fingerprint manifests | Not ported | Managed format contracts; reuse across STAAD years after qualification. |

For **2024, 2025 and 2026**, the integration is currently unimplemented. A matching upstream method name does not certify its output units, local basis, plate result convention or result-case readiness. Each target combination needs a fixture: STAAD build, architecture, upstream contract profile, IDEA/import-format build and applicable options.

### Constructor and availability mismatch

The main adapter requires Geometry, Load, Output **and Property**. The current placeholder requires Geometry, Load and Output only. Preserve the existing wrapper and placeholder contract during planning; an eventual integration-capability factory must assess the required operations for the requested feature, not simply reproduce either null check.

The Options property currently returns the source's mutable options object. Future implementation should validate and snapshot options and dictionaries at an operation boundary; concurrent mutation must not change the meaning of half an export.

## 4. Complete feature workstreams

| Workstream | Source functionality | Managed output / key contract | Prerequisites |
| --- | --- | --- | --- |
| I1 | Options, force/result DTOs and capabilities | Owned snapshots, explicit units and feature-readiness diagnostics | Wrapper, approved result/model reuse |
| I2 | Plate connectivity and member/joint forces | Read-only force/contribution rows with IDs, ends, bases and provenance | Geometry, Load, Output, Property |
| I3 | Joint equilibrium and closure policy | Separate raw, applied correction and corrected residuals | I2; Support only for reaction paths |
| I4 | Explicit mapping and topology | Validated joint/member mapping, active components, round-trip residuals, diagrams | I2/I3 plus mapping-format fixtures |
| I5 | Single-joint XLSX/HTML exports and provenance | Named artifact result, paths/hashes, validation state and overwrite policy | Qualified I2–I4 and artifact services |
| I6 | Batch discovery/preview/export, mapping profiles | Per-joint status, cancellation/progress, manifest and recoverable artifact delivery | I1–I5 |
| I7 | Cross-product acceptance and NuGet integration | Documented qualified profiles, package installation and XML examples | All implemented scope and native fixtures |

All 24 source overloads are individually mapped to these workstreams in the annex. ID/entity overloads and path/provenance conveniences are separate managed signatures, not additional native functions.

## 5. Reuse of the existing managed implementation

| Source component | Reuse and port boundary |
| --- | --- |
| OpenStaadIdeaStatiCaOptions and force/result models | Retain explicit units, tolerances, rotations, contribution kinds and audit fields; make snapshots defensively owned. |
| StaadToIdeaStatiCaForceTransformer | Reuse pure forward/inverse sign and basis conversions after independent analytical tests. |
| IdeaEquilibriumClosurePolicyService | Reuse named policies and explicit allocation records; preserve raw data and correction provenance. |
| IdeaConnectionMapping models/serializer | Reuse validated IDs, coordinates, local bases, load positions and component masks; no raw COM escape. |
| IdeaCheckbotJointMappingReader | Reuse streaming read-only XML/mapping input and path resolution; establish supported source-format versions and ambiguity handling. |
| IdeaConnectionMappingProfileService | Reuse schema validation, geometric/identity diff and model fingerprint; changed/renamed mapping requires review. |
| IdeaStatiCaJointTopologyBuilder/renderer | Reuse owned topology and SVG projection/labels; adapt WPF vector/point dependencies coherently. |
| Forces/Batch Excel exporters | Reuse Office Open XML writers built with System.IO.Packaging and XML; no Excel automation or ClosedXML dependency is used by these source files. |
| Single/batch HTML exporters | Reuse encoded data/report structure and numerical audit content; verify all dynamic text and embedded data. |
| IdeaExportArtifactPathService | Reuse deterministic naming and same-directory pending/backup paths; harden exclusive reservation and collision handling. |
| IdeaExportProvenanceService | Reuse hashes, selected cases/options, model/mapping context and path-disclosure policy. |
| Adapter's extraction and artifact orchestration | Separate internal extraction, mapping, verification and delivery responsibilities without creating partial adapter classes. |

The source uses Newtonsoft.Json, System.IO.Packaging, WPF Point3D/Vector3D and private ReInvented.Units models/enums. These dependencies require explicit adaptation/reuse against this repository; they are not evidence that a vendor SDK is required. No source service in this reviewed integration loads an IDEA SDK to read Checkbot mappings.

Do not automatically expose the main project's public static helper services. Keep implementation helpers internal in the open-source port, while requests/results/enums needed by consumers remain public and documented.

## 6. Numerical and engineering behavior to preserve and qualify

### 6.1. Action conventions and coordinate transforms

Keep these stages distinct in both data and tests:

1. Obtain STAAD member-end actions and the actual member local basis, including beta/orientation and end offsets.
2. Identify whether the target joint is the member start or end. The source changes the end-action sign for its structural-internal-force representation.
3. Transfer actions to a common global point using a defined displacement vector and the corresponding force-induced moment.
4. Rotate to the mapped IDEA local basis and apply the explicit structural-result convention.
5. Convert internal forces to Connection load effects using the actual mapped load position.
6. Apply the active-component mask, retain omitted components, then reconstruct actions to the STAAD joint for an independent round-trip residual.

IDEA documents different sign factors for structural internal forces at start/end load positions. Those factors must not be confused with the earlier STAAD end-action conversion. The official [force-convention reference](https://developer.ideastatica.com/docs/iom/iom_coordinate_systems.html#conversion-between-the-structural-model-results-and-connection-app-load-effects) supports that distinction. The source additionally supports separate force rotation and moment-component conventions.

Use independently defined test vectors for axial force, both shears, torsion and both bending moments at each end, rotated and eccentric members, inverse conversions and unit-system changes. Round-tripping through the same erroneous transform can cancel an error; analytical and native comparisons must also be independent.

### 6.2. Plate and load ownership

The source selects shared-edge plates and assigns other plate-corner contributions deterministically to incident members, with explicit overrides. Each corner contribution must be owned exactly once. Preserve the provenance of member-only, shared-edge, proximity and plate-only rows; do not aggregate away evidence needed to detect duplication.

Mapping requires exact incident-member coverage and a matching joint coordinate. Plate-only joints deliberately use a global-force export path rather than inventing an IDEA analytical member. Reversed incidence, merged IDEA member names, different origins and disconnected or zero-length members require explicit checks.

Primary and combined nodal loads need correct factors, case identities and cycle detection. Envelope/modal/nonlinear result semantics must not be treated as linearly additive merely because a load-case ID exists. Include support reactions only when explicitly requested and supported by the required operations.

### 6.3. Equilibrium closure is a policy, not proof of correctness

The source default is DiagnosticSingleOwner. It can allocate the raw residual as a correction; the configured large-closure ratios are diagnostic and do not inherently block the corrected export. A numerically balanced corrected result therefore does not prove the raw extraction is physically complete.

Retain RawResidual, ClosureAllocation, CorrectedResidual, policy and tolerance evidence separately. Recommend **StrictNoClosure as the initial public default**, subject to engineering/API approval; diagnostic or stiffness/reaction-aware correction should require explicit selection. This is a proposed policy change, not a completed source-equivalent port.

Preserve the explicit allowUnsafeExport=false guard on mapped workbook methods. Do not silently enable it, discard disabled-component residuals, or describe engineering-review diagnostics as a successful design check.

Unit overrides must be explicit and traceable. Wrapper input-unit caches may contain fallbacks; they are not sufficient proof of the actual units of every native result function. Confirm those units in the Output profile/native fixture before conversion to the source's kN/metre convention.

## 7. Execution flow and internal architecture

~~~mermaid
flowchart TD
    A["Capture wrapper identity, native profile and option snapshot"] --> B["Resolve feature-specific upstream capabilities"]
    B --> C{"Required operations and results ready?"}
    C -->|No| X["Return diagnostic without partial export"]
    C -->|Yes| D["Extract topology, cases, forces, bases and offsets"]
    D --> E["Assign plate contributions once and retain raw actions"]
    E --> F["Transform, verify raw equilibrium and apply explicit policy"]
    F --> G{"Mapped export requested?"}
    G -->|Yes| H["Validate mapping, component masks and round trip"]
    G -->|No| I["Prepare generic force and audit package"]
    H --> J{"Export checks satisfied or explicit override?"}
    J -->|No| X
    J -->|Yes| I
    I --> K["Write and validate staged artifacts"]
    K --> L["Deliver with rollback evidence and provenance manifest"]
    L --> M["Return named artifact and per-joint results"]
~~~

IOpenStaadIdeaStatiCaAdapter and one non-partial adapter remain the public entry point. Put public contracts under Models/IdeaStatiCa and applicable enums nearby; internal Services/IdeaStatiCa can have Extraction, Mapping, Verification and Export subfolders where the existing source responsibilities warrant them. Reuse foundational force/unit/entity services instead of cloning them.

Upstream adapters select their own verified native policies polymorphically. The integration orchestrator should not duplicate a switch on STAAD year for every call. A separate small format-profile service handles verified IDEA mapping/import differences.

Expose named query/export result classes with status/data, per-joint outcomes, validation diagnostics and artifact evidence. Preserve programmer argument errors and transport failures distinctly. Replace source string-only export returns with structured artifact results while retaining useful path/provenance overloads. No new public ref/out or tuples are planned.

Options, results and mapping data must be deep snapshots; a read-only outer list is insufficient if its records/dictionaries remain mutable. Keep native calls serialized on the acquired COM apartment. Pure transformation and file generation may operate on owned snapshots independently after extraction; do not use Task.Run to move RCWs arbitrarily.

## 8. Artifact delivery, recovery and privacy

The source stages files and moves existing outputs to backups before finalizing deliveries. That provides useful best-effort rollback, but a set of file moves is not a crash-atomic multi-file transaction. Rollback itself can fail; cancellation and process termination need explicit recovery evidence.

Improve the port with exclusive pending-file creation, collision-resistant names, validated output-root containment, explicit overwrite policy, a delivery journal/manifest, bounded cleanup and error reporting that preserves both the original and rollback errors. Do not delete unrelated output files. Same-directory staging helps keep final moves on the same volume but does not guarantee storage durability.

Fingerprint the model, mapping, options, selected cases and produced artifacts. A file hash does not prove the open model's unsaved state matches the file; record that distinction. Recheck model/results identity around extraction and either reject a changed snapshot or return a clear stale-context diagnostic.

Honor path-disclosure policy in HTML/JSON/manifests and errors. Encode dynamic HTML/SVG text, constrain XML loading and validate JSON schema/versions. Preserve progress/cancellation and Atomic-versus-partial batch semantics; do not label a partially delivered batch successful.

## 9. Tests, XML documentation and package strategy

Pure managed tests should cover all component/end signs, basis validation, offsets and moment transfer, unit conversion, plate-corner ownership, primary/combined loads, cycle detection, disabled components, exact/changed/renamed mappings, raw versus corrected equilibrium and every closure policy.

Artifact tests should reopen the produced XLSX package and inspect XML relationships, sheet dimensions, row/case ordering and numbers; parse HTML/SVG/JSON and verify encoding and hashes. Inject failures/cancellation at every delivery step, rollback error, long path, Unicode filename, locked output and parallel collision.

Integration fixtures must cover each qualified STAAD build and separately qualified IDEA format/import workflow, including supported joints, plate-only joints, merged/reversed members, result-unit changes, stale results and changed mappings. Neither source parity nor a generated workbook proves a correct IDEA import.

Every public method/model/property/enum requires XML documentation for units, coordinate frames, end/load-position conventions, status meaning, tolerances/defaults, mapping/closure choices, side effects, exceptions, ownership and examples. Explain raw/corrected data and override consequences in IntelliSense.

Initially implement pure models/transforms and upstream prerequisites. Decide whether the complete integration belongs in the base package or an optional companion package before adding export/JSON dependencies. A companion can keep artifact dependencies separate, but must not be introduced without an API/packaging decision. No change to OpenStaadWrapper is proposed by this review.

The current library targets .NET Framework 4.8.1. Modern .NET support requires explicit target/dependency and COM/runtime testing; source portability cannot be inferred from managed-only algorithms. This review creates no NuGet package and makes no new compatibility or release claim.
