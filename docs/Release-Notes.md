# StaadPro.Interop release notes

## 1.0.0-preview.20261028.2 - 28 October 2026 (prepared preview)

Ported ten further existing main-project Table functions: DeleteReport, DeleteTable, RenameTable, ResizeTable, SaveAllReports, SaveTable, SetCellTextBold, SetCellTextItalic, SetCellTextUnderline and SetCellTextSize. Table now exposes 20 of 29 main-source overloads and reaches 20 of 24 documented native names. Nine main-source overloads remain; native-only expansion follows completion of reference porting.

Reused the existing Table result/status types and common internal service. Added matching comparative XML documentation, positive identity/dimension checks, one-based cell coordinates, finite positive native font sizes, exact rename text, scoped save receipts and explicit unacknowledged void completion. Kept native exception and captured-binding behavior, with no implicit save, rollback or success claim. The documented resize contract supports growth; shrink behavior and storage persistence are not inferred.

Retained the verified 400-pixel Markdown banner, original logos, independence/trademark notices, Srinivasa Rao Masanam author and TensorStruct publisher/copyright. The package targets .NET Framework 4.8.1 on Windows. Live COM, native report UI/storage and engineering qualification remain separate acceptance work.

Passed **6,780 offline tests**, including **332 Table cases** (145 additional cases in this increment). Compiled and executed **1,262 installed-package XML examples**, including **forty Table examples**, and exercised the lifecycle/formatting workflow against a managed native-signature fixture.

Prepared for manual upload; public NuGet.org availability is not confirmed.

## 1.0.0-preview.20261028.1 - 28 October 2026 (prepared preview)

Corrected the NuGet README banner to use supported Markdown image syntax and a 400 × 133 PNG rendition. Preserved both original logo assets and the symbol-only package icon. Rebuilt this prepared preview under the same version at the package owner's request; API behavior is unchanged.

Ported ten existing main-project Table functions: CreateReport, AddTable, GetReportCount, GetTableCount, GetCellValue, SetCellValue, SetColumnHeader, SetColumnUnitString, SetRowHeader and SaveReport. Added matching interface/adapter XML documentation, named creation/mutation receipts and managed regression tests. Corrected the reference XML coordinate convention to the documented native one-based rows and columns. Kept counts and cell strings directly usable and distinguished explicit empty native cell text from untouched output.

Added independence and trademark notices to the repository, NuGet README and packaged NOTICE.md. Preserved the TensorStruct publisher identity, Srinivasa Rao Masanam author, original branding assets and compact README banner. The common Table service reuses strict native readers and retains caller-thread, captured-binding and original-exception behavior. Live native COM and report-storage qualification remain pending.

Passed **6,635 offline tests**, including **187 Table cases**. Compiled and executed **1,242 installed-package XML examples**, including all twenty Table examples, plus an isolated Table write/read/save workflow against the exact archive. These checks do not certify live STAAD.Pro behavior or report storage.

Prepared the package for manual upload; NuGet.org publication is not confirmed. Earlier archives are preserved.

## 1.0.0-preview.20261027.2 - 27 October 2026 (prepared preview)

Reduced the logo-with-text banner in the repository and NuGet READMEs to **400 pixels wide**, retaining its original aspect ratio and automatic height (approximately 133 pixels). Preserved both supplied PNG assets and the embedded symbol-only package icon. Updated branding guidance and package verification to retain the compact banner sizing.

Retained the managed APIs and runtime contracts from 1.0.0-preview.20261027.1. Verified the new archive metadata, assets and **1,222 selected XML examples** through an isolated installed-package consumer. Reran and passed all **6,448 offline tests** for this build; live COM and engineering qualification remain pending. Author: Srinivasa Rao Masanam. Company/publisher: TensorStruct. Copyright (c) 2026 TensorStruct.

Prepared the archive for manual NuGet.org upload; public availability has not been confirmed. Earlier archives are preserved.

## 1.0.0-preview.20261027.1 - 27 October 2026 (prepared preview)

This preview adds 35 managed methods: seventeen analysis-command methods, eight Support foundation/assignment operations and ten common Design brief/assignment/query methods. Commands reaches sixteen active native analysis routes, Support covers 27/35 documented native routes, and Design reaches five native names. The package retains 96 managed Output overloads covering 47/60 documented native names, the existing Root/Geometry/Load/Property APIs and the approved wrapper surface.

Added complete comparative XML documentation, source-compatible analysis options/design-code vocabulary, immutable Design requests and explicit native acknowledgements. Preserved stable assignment ordering, identified partial completion, caller-thread dispatch, binding checks and original native exceptions. Copied Design parameter rows from privately owned native holders using explicit indexed-property reads.

Adopted the supplied symbol-only PNG as the embedded NuGet icon and the logo with text as the repository/NuGet README banner. Included both original assets and branding guidance. Author: Srinivasa Rao Masanam. Publisher/company: TensorStruct. Copyright (c) 2026 TensorStruct.

The Release build passed with zero warnings/errors and 6,448 offline tests, including 412 new Commands/Support/Design cases. Exact-archive verification compiled and executed 1,222 selected XML examples through an isolated PackageReference consumer, including all 34 Commands, 54 Support and twenty Design examples. These checks validate managed behavior and packaging; live native COM and engineering qualification remain pending.

See [analysis commands and Support](Commands-and-Support-Porting.md), [Design contracts](Design-Porting.md) and [branding](Branding.md) for the new APIs, native acknowledgement rules, owned data and logo usage. Extended GROUP/Chinese steel, steel-command catalog work, eight Support gates and thirteen Output native routes remain pending. Parked Load work, nine Property gates and fifteen supplemental Property candidates retain their approval requirements. The API-usability experiment remains isolated on its separate branch.

Prepared this archive for manual NuGet.org upload; public availability has not been confirmed. Earlier distribution archives are preserved.

## 1.0.0-preview.20261026.4 - 26 October 2026 (prepared preview)

Added **25 Output overloads** for plate-corner extraction, indexed sparse/Cartesian selections, explicit metadata families and member-local projections: 18 synchronous main-source adaptations plus seven typed enabling overloads. Output now exposes **96 managed overloads reaching 47/60 documented native names**, with **13 documented native routes pending**. All additions use the newly enabled GetPlateCornerForces route. See [the plate-corner guide](Output-Plate-Corners.md) for exact signatures, comparisons, axes evidence and limitations.

Added owned family-aware keys, request snapshots and indexed outcomes, with explicit failure data access and managed member-coordinate results. Ported the source Boolean/out-vector convenience as a named result. Kept global corner forces distinct from plate-center stresses and member-local projections.

Passed a Release build with zero warnings/errors and **6,036 offline tests**, including **248 new corner cases**. Isolated package verification covers **1,152 selected XML examples**, including all 192 Output examples. Read-only native checks support the global force/moment contract on sampled STAAD.Pro 2026.0.0.340 observations; broader native/engineering qualification remains pending. Prepared the 1.0.0-preview.20261026.4 distribution archive for manual NuGet.org upload. Exact-archive verification covers its metadata, documentation and isolated installation/runtime. Earlier distribution archives remain unchanged; public availability of this version is not yet confirmed. Publisher metadata remains TensorStruct, with Srinivasa Rao Masanam as author.

## 1.0.0-preview.20261026.3 - 26 October 2026 (prepared preview)

Prepared the package for Windows/.NET Framework 4.8.1 with the managed Output workflows below, the preceding six steel-design-block queries and nineteen Support operations. The package exposes 71 managed Output overloads covering 46 documented native functions, alongside the existing Geometry, Load, Root, Property and wrapper APIs.

Added **25 managed Output overloads**: 23 synchronous main-project workflow adaptations and two typed enabling overloads for explicit native axes and Beam ratio queries. Output now exposes **71 managed overloads reaching 46/60 documented native names**. The fourteen native contract gates remain; this increment adds compositions, entity conveniences and ordered batches over qualified native routes. See [the managed workflow guide](Output-Managed-Workflows.md) for exact contracts and comparative usage.

Added governing-case resolution, per-item batch outcomes, staged caller utilization caches, native global offset-transfer audit packets, geometry bases, plate-center composites, ordered static checks and opposite support actions. All new APIs have matching XML documentation and examples; no public ref/out, tuples, partial adapters or thread-pool COM calls were introduced.

Validation passed **5,788 offline tests** and an isolated package consumer compiling/executing **1,102 selected XML examples**, including all 142 Output examples. Live native COM and engineering qualification remain pending. Publisher metadata remains TensorStruct, with Srinivasa Rao Masanam as author. The earlier prepared distribution archive remains unchanged.

## 1.0.0-preview.20261026.2 - 26 October 2026 (source preview)

Added **six common documented Output routes and 19 Support functions**, with full matching interface/adapter IntelliSense and named managed results. Output now covers **46/60** documented native names; Support covers **19/35**. The new Output overloads distinguish named parameter blocks from member-only observations, while Support preserves node/definition identity, six-DOF states and operation-specific native acknowledgements.

Passed a Release build with zero warnings/errors and **5,523 offline tests**, including 170 new cases. A separate local CI archive verifies **1,052 selected XML examples** using managed native-signature fixtures. Live 2024–2026 COM and engineering qualification remain pending. The existing prepared distribution archive is unchanged.

See [Output parameter blocks](Output-Steel-Design-Blocks.md) and [Support foundations](Support-Foundations.md) for exact signatures, examples, native conventions and limitations.

## 1.0.0-preview.20261026.1 - 26 October 2026 (prepared preview)

Added four documented Property operations for model-wide element-node release cleanup, member fireproofing cleanup and country-qualified profile-name translation. Property now provides **249 common overloads and five optional catalog queries**, covering **195 of 204** active documented native routes. Nine documented contract gates and fifteen supplemental discovery candidates remain.

Added **25 typed Output queries**, bringing coverage to **40 of 60** documented native names. The new APIs cover member and physical-member results, solid stresses, mat influence areas and base pressures, modal and buckling observations, nonlinear load steps, steel-design observations and time-history results. Named immutable payloads preserve native status evidence, units and component meanings, with bounded collections and result-state checks.

All new APIs include matching interface/adapter XML documentation and usage examples explaining their differences from similar operations. Validation passed **5,353 offline tests** and **1,002 selected packaged XML examples** using managed native-signature fixtures. Live COM and engineering qualification remain pending. The target remains Windows/.NET Framework 4.8.1.

See the [Property completion guide](Property-Profile-Names-and-Final-Cleanup.md) and [extended Output guide](Output-Extended-Queries.md) for contracts, examples and limitations.

## 1.0.0-preview.20261024.12 - 24 October 2026 (prepared preview)

Includes the Property catalog and cleanup APIs from the preceding preview: 245 common overloads and five separately acquired catalog queries. See [the Property guide](Property-Catalog-Capabilities-and-Cleanup.md) for operation-specific results and native-version requirements.

Public UPT methods use PascalCase, including `AddUptPropertyChannel`. Consumers upgrading from earlier previews should use [the API rename table](Property-API-Naming.md).

## 1.0.0-preview.20261024.11 - 24 October 2026 (prepared preview)

Incorporated **25 Property operations**: all five remaining P9 catalog queries and twenty P10 removal/deletion functions. The current surface has **245 common overloads plus five separately acquired catalog queries**, covering **191 of 204 active native routes**, with **13 remaining**. P9 is complete; P10 has twenty of twenty-four routes. The nine earlier P7/P8 contract gaps remain pending. See [all 25 catalog/cleanup contracts](Property-Catalog-Capabilities-and-Cleanup.md).

Passed a Release build with zero warnings/errors and **5,156 offline tests**, including **197 new cases**. The isolated PackageReference-only consumer verifies **944 selected XML examples**: 490 common Property examples plus twelve optional catalog/acquisition examples, alongside the existing groups. Separate managed checks exercise all twenty new mutations and five catalog queries through both public surfaces. Live native mutation and engineering qualification remain pending.

Added a passive version/signature acquisition boundary for the newer catalog group on exact native profiles **25.00.01.424** and **26.00.00.340**. Retained operation-specific zero-success, Boolean-success and explicit no-specification removal outcomes, owned bounded bulk targets, and separate member-end, cable-definition and analytical-node selectors. No native model is modified by verification.

Renamed **17 public UPT methods** to PascalCase, including **AddUptPropertyChannel**, **AddUptPropertyWideFlange** and **CreateUptTable**. This preview requires callers to update those method names. Parameter names, types, ordering, defaults and return types are preserved. Bentley native dispatch names and recorded contract evidence retain their original spelling.

See [all 17 API renames](Property-API-Naming.md).

- Incorporated `RemovePropertyFromPlate`: Remove Thickness Property From the specific Plate.
- Incorporated `RemoveMaterialFromPlate`: Remove Material From the specific Plates.
- Incorporated `RemoveMaterialFromSolid`: Remove Material From the specific Solids.
- Incorporated `DeleteMemberReleaseSpec`: Delete MEMBER RELEASE specification.
- Incorporated `DeleteAllControlDependentRelations`: Delete all control/dependent joint specifications from model.
- Incorporated `RemoveAllElementOffsetSpec`: Remove all element node offset specification from the model.
- Incorporated `RemovePropertyFromBeam`: Remove property from beam.
- Incorporated `DeleteProperty`: Delete property based the on the property ID passed.
- Incorporated `DeleteMemberSpec`: Delete specification based on the specification number passed.
- Incorporated `RemoveMemberReleaseSpecFromBeam`: Removes the member specification from a particular member at the provided location (Start or End)..
- Incorporated `RemoveMemberOffsetSpecFromBeam`: Removes the member offset specification from a particular member at the provided location (Start or End).
- Incorporated `RemoveMemberTrussSpecFromBeam`: Remove member truss specification from beam.
- Incorporated `RemoveMemberInactiveSpecFromBeam`: Remove member inactive specification from beam.
- Incorporated `RemoveMemberTensionSpecFromBeam`: Remove member tension specification from beam.
- Incorporated `RemoveMemberCompressionSpecFromBeam`: Remove member compression specification from beam.
- Incorporated `RemoveMemberIgnoreStiffSpecFromBeam`: Remove member ignore stiff specification from beam.
- Incorporated `RemoveMemberCableSpecFromBeam`: Removes the member cable specification from a particular member at the provided location type (Tension or Length).
- Incorporated `RemoveElementPlaneStressSpecFromPlate`: Remove element plane stress specification from plate.
- Incorporated `RemoveElementIgnoreInplaneRotnSpecFromPlate`: Remove element ignore in plane rotation specification from plate.
- Incorporated `RemoveElementNodeReleaseSpecFromPlate`: Remove element node release specification from plate.
- Incorporated `GetDefaultStandardProfileDBFolder`: Gets standard profile default database folder path.
- Incorporated `IsStandardDatabaseSection`: Checks if the specified section property reference number is from standard section database source or not.
- Incorporated `GetStandardSectionDatabaseName`: Gets standard section database name for the specified section property reference number.
- Incorporated `GetStandardSectionTableName`: Get the table name from the standard section database for the specified standard section property reference number.
- Incorporated `GetStandardSectionName`: Get the section name from the standard section database and table for the specified standard section property reference number.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.11.nupkg` for manual upload.

## 1.0.0-preview.20261024.10 - 24 October 2026 (prepared preview)

Incorporated **25 Property functions**: 22 reviewed P8 user-provided-table operations and the next three P9 catalog/database-folder operations. Property now exposes **225 overloads**, covering **166 of 204 active native routes**, with **38 remaining**. Five P8 transport contracts and four earlier P7 contracts remain pending. P8 has 25 of 30 routes; P9 has four of nine. See [all 25 UPT/catalog contracts](Property-UPT-Profiles-and-Catalogs.md).

Passed a Release build with zero warnings/errors and **4,959 offline tests**, including **213 new cases**. The isolated PackageReference-only consumer verifies **892 selected XML examples**, including all **450 Property interface/adapter examples**. All sixteen new mutation examples are guarded; separate managed checks execute all 25 routes through both public surfaces. Live native mutation and engineering qualification remain pending.

Added immutable explicit UPT profile inputs, owned table inventories and named section observations. Kept operation-specific creation, insertion and removal outcomes separate. Qualified ten UPT read-back layouts and the database-folder setter against exact acquired native profiles 24.00.02.354, 25.00.01.424 and 26.00.00.340, retaining their different folder return conventions. Unreviewed builds cannot select those policies. Table numbers, native indices and property references retain separate identities.

- Incorporated `AddUPTPropertyCHANNEL`: Adds explicit channel data to an existing native UPT table.
- Incorporated `AddUPTPropertyANGLE`: Adds explicit angle data to an existing native UPT table.
- Incorporated `AddUPTPropertyDOUBLEANGLE`: Adds explicit double angle data to an existing native UPT table.
- Incorporated `AddUPTPropertyTEE`: Adds explicit tee data to an existing native UPT table.
- Incorporated `AddUPTPropertyPIPE`: Adds explicit pipe data to an existing native UPT table.
- Incorporated `AddUPTPropertyTUBE`: Adds explicit tube data to an existing native UPT table.
- Incorporated `AddUPTPropertyGENERAL`: Adds explicit general profile data to an existing native UPT table.
- Incorporated `AddUPTPropertyISECTION`: Adds explicit I-section data to an existing native UPT table.
- Incorporated `AddUPTPropertyPRISMATIC`: Adds explicit prismatic profile data to an existing native UPT table.
- Incorporated `AddUPTPropertyWIDEFLANGECOMPOSITE`: Adds an explicit 12-, 16- or 19-slot composite wide-flange UPT section.
- Incorporated `CreatePropertyFromUPTTable`: Creates an assigned section-property reference from an existing named UPT entry.
- Incorporated `CreatePropertyFromUserTable`: Obtains a positive section-property reference through the native section-name-first user-table route.
- Incorporated `CreateUPTTableEx`: Creates a UPT table using an explicit requested table number and type.
- Incorporated `RemoveUPTTable`: Requests native removal of an explicitly identified UPT table.
- Incorporated `RemovePropertyFromUPTTable`: Requests deletion of an exact named section from a UPT table.
- Incorporated `GetUserProvidedTableCount`: Reads the native user-provided table count.
- Incorporated `GetUserProvidedTableList`: Reads a bounded owned snapshot of native UPT table IDs.
- Incorporated `GetUserProvidedTableSectionCount`: Reads a native table's section count with explicit table identity.
- Incorporated `GetUserProvidedTableSectionType`: Reads the written scalar native UPT section type with count consistency checks.
- Incorporated `GetUserProvidedTableSectionPropertyCount`: Retains the native named-section count with exact requested UPT identity.
- Incorporated `GetUserProvidedTableSectionProperties`: Reads an owned version-qualified native UPT section-value layout.
- Incorporated `GetUserProvidedTableNo`: Reads a table number for an explicitly supplied native UPT inventory index.
- Incorporated `GetShapeCode`: Reads a raw country-specific section shape code with exact catalog identity.
- Incorporated `SetStandardProfileDBFolder`: Requests a standard-profile database folder change under an exact native return-policy profile.
- Incorporated `GetStandardProfileDBFolder`: Reads the exact native standard-profile database folder path.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.10.nupkg` for manual upload.

## 1.0.0-preview.20261024.8 - 24 October 2026 (prepared preview)

Incorporated **25 Property functions**: 22 reviewed P7 inactive/fireproofing/attribute operations and the first three P8 UPT operations. Property now exposes **200 overloads** and reaches **141 of 204 active native routes**, with **63 remaining**. P7 has 42 of 46 routes; four contract gaps remain explicitly pending. P8 has three of thirty routes. See [the 25 fireproofing/attribute/UPT contracts](Property-Fireproofing-Attributes-and-UPT.md).

Passed a Release build with zero warnings/errors and **4,746 offline tests**, including **195 new cases**. The fresh PackageReference-only consumer verifies **842 selected XML examples**, including all **400 Property interface/adapter examples**. All ten new mutation examples are guarded; dedicated managed checks execute every new route through both public surfaces. Verification does not change a live STAAD model.

Added immutable exact attribute pairs, pair/domain/ID snapshots, fireproofing correspondence and explicit UPT value packets. Bounded count/list/count reads validate written outputs and preserve order/duplicates. By-reference native attribute-value rewriting returns StateChanged rather than data for the wrong pair. UPT table IDs, insertion acknowledgements and duplicate outcomes remain separate; scalar and unequal wide-flange routes retain their different return conventions.

- Incorporated inactive-member count and seven fireproofing count/list/detail operations.
- Incorporated fourteen member/element attribute definition, assignment, removal, count and paired-inventory operations.
- Incorporated UPT table creation and scalar/unequal wide-flange insertion with explicit input packets and separate native outcomes.
- Kept four P7 contracts pending: inactive list, alpha angle, centroid and assigned-attribute indexed reads. No guessed success or native index context was exposed.
- Updated coverage, native evidence mappings, shipped guides and installed XML verification; retained exact author metadata.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.8.nupkg` for manual upload; no NuGet.org upload or registry ownership change is performed.

## 1.0.0-preview.20261024.7 - 24 October 2026 (prepared preview)

Incorporated **25 Property functions**: the eight remaining P6 section creators and the next seventeen P7 specification/property operations. Property now exposes **175 overloads** and reaches **116 of 204 active native routes**, with **88 remaining**. All P2/P3/P4/P5/P6 allocations have managed routes; P7 has 20 of 46. Route coverage does not establish live native or engineering qualification. See [all 25 creation/specification contracts](Property-Section-and-Specification-Creation.md).

Passed a Release build with zero warnings/errors and **4,551 offline tests**, including **205 new cases**. The isolated PackageReference-only consumer verifies **792 selected XML examples**, including all **350 Property interface/adapter examples**. All 24 new mutation examples are guarded; dedicated managed checks exercise every new route through both the interface and concrete adapter. Verification does not change a live STAAD model.

Added explicit immutable wide-flange, general-prismatic, six-DOF and partial-factor inputs. Retained separate created section IDs, created specification IDs, zero-based fireproofing indices and zero-success status-only results. The void unique-ID setter returns InvokedWithoutAcknowledgement with no fabricated native code and IsSuccessful false; its exact requested property/text identity is retained. Existing signatures, current-material context and native units remain explicit.

- Incorporated wide-flange/solid-shape table, four simple prismatic, general-prismatic and tapered-tube creators.
- Incorporated partial/member/element specification creators, both cable routes, control/dependent relations, plate offsets and fireproofing creation.
- Incorporated exact property unique-ID reads and explicit void-setter invocation evidence.
- Added immutable named input packets/selectors, separate result identities and complete interface/adapter XML with guarded mutations.
- Updated coverage, roadmap, shipped guides and package metadata safeguards.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.7.nupkg` for manual upload; no NuGet.org upload or registry owner change is performed.

## 1.0.0-preview.20261024.6 - 24 October 2026 (prepared preview)

Incorporated **25 Property functions**: the eight remaining P3 material operations, eight remaining P4 member-section queries and nine remaining P5 mutations. Property now exposes **150 overloads** and reaches **91 of 204 active native routes**, with **113 remaining**. All P2/P3/P4/P5 allocations have managed routes; this is implementation coverage, not live native or engineering qualification. See [the 25 material/member-section contracts](Property-Materials-and-Member-Sections.md).

Passed a Release build with zero warnings/errors and **4,346 offline tests**, including **190 new cases**. The isolated PackageReference-only consumer verifies **742 selected XML examples**, including all **300 Property interface/adapter examples**. New mutation examples are guarded; separate installed-consumer checks invoke all eleven mutations against managed stand-ins. No live STAAD model is changed by verification.

Added immutable explicit material definitions/strengths, orthotropic constants, member-section snapshots and named mutation acknowledgements retaining actual native evidence and requested identity. Kept Steel Fu/Fy/Rt/Ry order distinct from extended Fy/Fu/Ry/Rt/Fcu order. Added bounded owned solid-ID snapshots, strict written-output checks, operation-specific absence/error handling and native-version section layouts. A successful mutation acknowledgement does not prove design-strength persistence, save the model or provide rollback.

- Incorporated orthotropic 3D count/properties, member material removal, solid material name/count/list and material classification get/set.
- Incorporated assigned-profile values and member property reference/value/type/table/name/display/country queries.
- Incorporated assigned-profile creation, plate specification assignment, design-section updates, five explicit material creators and solid material assignment.
- Added immutable named inputs/results, guarded examples, owned bounded snapshots and shared binding checks; preserved all earlier signatures.
- Updated the native inventory/roadmap, shipped usage guide, installation/publishing instructions and installed XML verification.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.6.nupkg` for manual upload.

## 1.0.0-preview.20261024.5 - 24 October 2026 (prepared preview)

Incorporated the remaining **19 active, nonconflicting main-interface Property signatures** plus **six documented native observations**, bringing Property to **125 overloads**. Added section creation/assignment/read-back, explicit material conversion and captured-receiver uniform-thickness creation with complete matching XML IntelliSense. Native dispatch now covers **66 of 204 active candidates**, with **138 remaining**. See [all 25 section-completion contracts](Property-Sections.md).

Passed a Release build with zero warnings/errors and **4,156 offline tests**, including **225 new section cases**. Isolated installed-package verification compiles and runs **692 selected XML examples**, including all **250 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins perform every example; live native mutation and engineering qualification remain pending.

Selected 47 reviewed section layouts per acquired STAAD 24/25/26 profile, reading native version metadata after wrapper initialization and rechecking it before returning packets. Corrected the source assignment query filtering an unfilled array. Preserved native identity/field labels, explicit missing-area results, operation-specific statuses and partial-write behavior. Added small section selection/data models without importing private catalog/UI services; reused the repository UnitsConverter for explicit modulus/weight-density conversion. Existing typed-return collisions (eight) and obsolete source overloads (five) remain documented exceptions.

- Incorporated five section assignment, two create-and-assign, two explicit material conversion, one captured-receiver thickness creation, two section creation and seven section read-back/area/assignment overloads.
- Added the six standalone native observations listed in the section guide; retained raw country/type codes and database record zero/-1 semantics.
- Added reviewed section layout profiles, small table-definition carriers, owned section packets, Beam.SectionProperty and dimensional converter aliases. Kept unsupported layouts/families and missing areas explicit.
- Strengthened inventory dispatch lifetime checks and preserved caller-apartment writes, prevalidated input snapshots and nontransactional failure behavior.
- Updated shipped guides, coverage, package metadata and all interface/adapter XML examples.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.5.nupkg` for manual upload; no NuGet.org upload or registry ownership change is performed.

## 1.0.0-preview.20261024.4 - 24 October 2026 (prepared preview)

Incorporated 25 further main-interface Property overloads for material collections, material creation/assignment, uniform plate thicknesses, grouped offset pipelines and member releases. Property now exposes **100 overloads**: 25 qualified observations and 75 main-project compatibility ports. Added detailed matching interface/adapter XML IntelliSense and examples, mutable material/thickness/release packets and member-end release references. Native dispatch reaches **57 of 204 active Property candidates**, with **147 remaining**. See [all 25 workflow signatures](Property-Workflows.md) for parameters, units, native statuses, side effects and deliberate source corrections.

Passed a Release build with zero warnings/errors and **3,931 offline tests**, including **168 new workflow cases**. Isolated installed-package verification compiles and runs **642 selected XML examples**, including all **200 Property interface/adapter examples**, through a fresh PackageReference-only consumer and package cache. Examples run against managed native stand-ins; live mutation and engineering qualification remain pending.

Material creation records native Created/Updated outcomes separately from list IDs; legacy creation retains its explicitly documented E/G x1000 scale. Indexed material collections retain native names and consistent name equality/hash behavior. Release read-back preserves mixed native modes and partial-mode provenance; basic creation refuses unsupported partial-moment packets. Populations stage native reads before changing caller packets, degree overloads retain caller-apartment dispatch, and multi-step writes recheck binding lifetime around each native write.

- Incorporated seven material read/population overloads, four material creation overloads and three material assignment overloads.
- Incorporated four uniform thickness overloads, four offset assignment/pipeline overloads and three member-release population/creation overloads.
- Added MaterialProperty, PlateThickness, MemberReleases, Releases, Springs, MpFactors, ReleaseType and MaterialCreationStatus with XML documentation. Preserved WPF change notifications and documented private catalog/unit-converter omissions.
- Updated package metadata/version, shipped workflow/installation/publishing guides, coverage and installed XML verification. Retained prior Boolean load enumeration fixes and existing qualified observation signatures.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.4.nupkg` for manual upload. No public upload, registry owner change or live model mutation is performed by this task.

## 1.0.0-preview.20261024.3 - 24 October 2026 (prepared preview)

Incorporated 25 further Property overloads for table/composite/built-up section creation, beta-angle assignment and population, material assignment subsets, four-corner plate thickness creation/retrieval/assignment, and member-offset creation/population/assignment. Property now exposes **75 overloads**: 25 typed observations and 50 main-project compatibility ports. Added detailed, matching interface/adapter XML IntelliSense and examples, the required thickness/offset packets, member offset references and preserved table/offset enum codes. Native dispatch covers **54 of 204 active Property candidates**, with **150 remaining**. See [the 25-method guide](Property-Management-Ports.md) for all signatures, units, statuses, mutation behavior and compatibility corrections.

Passed a Release solution build with zero warnings/errors and **3,763 offline tests**, including **189 new Property management cases**. The isolated installed-package verifier compiles and runs **592 selected XML examples**, including all **150 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins exercise the examples without changing a native STAAD model. Live creation/assignment and engineering qualification for this increment remain pending.

- Preserved 25 main-project signatures, including the Global default for PopulateMemberOffsets and the nThreads beta-assignment overload. Retained caller-apartment dispatch and a shared Property binding/serialization gate.
- Decoded GetThicknessPropertyValues success as 4, AssignPlateThickness/AssignMemberSpecToBeam as 0 and AssignBetaAngle as true/1. Retained direct signed section-creation results, checked positive packet IDs before updating them and documented partial native writes without rollback.
- Added bounded material count/list retrieval, four-slot buffer validation, staged managed population and independent zero-offset packets retaining their end/frame. Corrected the bulk offset null guard.
- Added the packaged Property-Management-Ports.md guide and updated coverage, installation, publishing and expanded XML verification.

Prepared `artifacts/release/StaadPro.Interop.1.0.0-preview.20261024.3.nupkg` for manual upload; this task does not publish it to NuGet.org or change registry owners.

## 1.0.0-preview.20261024 - 24 October 2026 (prepared preview)

Fixed primary and combination discovery rejecting the Boolean `true` returned by `GetPrimaryLoadCaseNumbers` and `GetLoadCombinationCaseNumbers` on the observed STAAD.Pro 25.00.00.539 build. The shared readers now handle Boolean success separately from integral case counts, validate the complete ID array, and recheck the separate count before reading metadata. Boolean false, malformed arrays and inconsistent counts still fail. Reference discovery retains its integer-count contract; matching documented integral primary/combination results remain supported.

- Corrected the readers used by `GetLoadCasesFromIds`, `GetAllPrimaryLoadCases`, family/range filters, primary population queries and combination discovery/lookups. Kept the general integer decoder strict and public signatures unchanged.
- Added Boolean-success/failure, 74-primary/230-combination, in-place buffer, malformed-array, changed-count, strict reference-return and integral compatibility regressions. Removed the test expectation that Boolean true is invalid for primary/combination number retrieval.
- Updated matching interface/adapter XML IntelliSense and usage guides. Retained all 50 Property overloads, 171 Load overloads, 143 reference Geometry overloads, 15 typed Output queries and reviewed Root/wrapper APIs.

Passed a Release build with zero warnings/errors and **3,574 offline tests**, including **43 new case-number return regressions**. Isolated PackageReference verification compiles and executes **542 selected XML examples**; the packaged primary and combination stand-ins now return Boolean success. Native return shapes were observed read-only during diagnosis; wider native/version and engineering qualification remains pending.

The net481 archive is prepared for manual upload and has not been published to NuGet.org by this task. No model-opening, case-activation, geometry-editing or save operation is added by the correction.

Post-package validation on STAAD.Pro **25.00.00.539** passed using the DLL extracted from this exact archive: the reported ID filter returned cases 1, 2 and 3, and primary/reference/combination discovery matched the native inventories. The model path, active case, node count and case counts remained unchanged. This additional source documentation records the live result without replacing the prepared archive. See [the validation record](NuGet-Readiness.md#post-package-live-verification) for scope and limits.

## 1.0.0-preview.20261023.1 - 23 October 2026 (prepared preview)

Added 25 main-project Property overloads for section/thickness inventories, assignment discovery, material context and assignment, member specifications, and global-Y member-end offsets. Property now exposes **50 overloads**: the existing 25 typed observations and these 25 signature-compatible ports. All new interface and adapter methods include expanded XML IntelliSense with parameters, return values, examples, units, side effects and failure contracts. Shared locking/lifetime checks, bounded native buffers and checked operation-specific statuses are included. Passed **3,531 offline tests**, including **209 new Property cases**, with zero build warnings/errors. The installed-package verifier compiles and runs **542 selected XML examples**, including all **100 Property examples**, against managed stand-ins. Live COM and engineering qualification remain pending.

- Ported section/thickness inventory and assignment lookups: `GetUnusedSectionsCount`, `GetUnusedSectionsIds`, `GetBeamProperty`, `GetSectionPropertyList`, `GetSectionPropertyName`, `GetSectionPropertyValues`, `GetSectionPropertyAssignedBeams`, `GetBeamsWithMissingSectionProperty`, `GetThicknessPropertyList`, `GetThicknessPropertyAssignedPlates`, `GetPlatesWithMissingThicknessProperty` and `GetAveragePlateThickness`.
- Ported plate material and member behavior reads: `GetPlateMaterialName` and `GetMemberSpecification`, with exact main-project enum codes.
- Ported both scalar/array `AssignBeamProperty` and `AssignMaterialToMember` overloads, `SetMaterialName`, both `AssignMemberEndOffsets` overloads and all four `AssignMemberEndOffset` overloads. Preserved main no-op inputs and documented global-Y units, native status conventions and partial-mutation effects.
- Retained the existing 25 typed Property observations, 171 Load overloads, all 143 reference Geometry overloads, 15 typed Output queries and reviewed Root/wrapper APIs. Added matching interface/adapter XML, two supporting enums, shared-gate/lifetime validation and the packaged [25-method guide](Property-Main-Ports.md).
- Rejected failed native responses, negative/oversized/changing counts and malformed buffers. Preserved raw zero-padded section values without a universal version-dependent parser. Kept obsolete inactive-member methods excluded.

The net481 archive includes the earlier source-only Property foundation, Load inspection and Load expansion increments recorded below. Verification uses managed stand-ins and an isolated PackageReference consumer restoring the exact local archive; it performs no customer-model mutations. Native COM marshaling, section-layout interpretation and engineering qualification remain pending. This archive is prepared for manual upload and has not been published to NuGet.org.

### Earlier source validation — Property foundations

Implemented the first 25 typed Property queries for member and plate offsets, releases, section properties and material observations. Results use named status/data classes, existing MemberEnd and Vector3D types, owned collections and comparative XML IntelliSense. Shared 2024–2026 contracts use one internal reader. Native naming, selector and documentation ambiguities are recorded rather than replaced with inferred values. Passed 3,322 offline tests, including 422 new Property cases, and a Release build with zero warnings/errors. Native COM and engineering qualification remain pending; 179 active Property mappings remain to be implemented. The parked Load work and prepared distribution archive are unchanged. Verified a separate local CI archive through isolated installation and compilation/execution of all 492 selected XML examples, including the 50 new Property examples. See the [Property guide](Property-Porting.md) for all 25 signatures, source reuse decisions and native qualification limits.

### Earlier source validation — Load expansion implementation

Implemented all 36 approved Load expansion methods for case/list/envelope management, automatic combinations and repeat generation, whole-model self-weight, additional assignments, original-command and plate/floor readback, reference contributions and beam load transfer. Source now exposes 171 Load overloads and dispatches 126 of 129 documented native names, plus the metadata-corroborated merge body. Retained managed alternatives for the three unused entry points. Added matching comparative XML IntelliSense on interface and adapter, typed results, validated snapshots, explicit family activation, bounded initialized outputs, changed-state checks and balanced merge cleanup. GetLoadAttribute exposes documented status only; MergeBeamLoads reports invocation completion without certifying success. Passed 2,900 offline tests, including 424 new expansion cases, and a Release build with zero warnings/errors. A separate local CI archive passed isolated restore/build/runtime verification for 442 selected XML examples, including all 72 expansion examples with mutation guards disabled. Native COM and engineering qualification remain pending. The prepared distribution archive is unchanged.

### Earlier source validation — Load contract preparation

Prepared the original 56-entry contract catalog and 36 proposed managed additions, with consistent named results, typed requests and descriptive parameters. Reused existing case, force and inspection models, ported the semantic GlobalAxis enum, and defined required supporting classes/enums with XML documentation and managed validation. Added a public validated constructor to the existing notional contribution type for future assignment input. Kept pending signatures in a separate compile-checked review file rather than exposing throwing runtime methods. Documented internal service boundaries and withheld attribute/merge contracts pending native evidence. Passed 2,476 offline tests, including 28 contract-focused cases, and a Release build with zero warnings/errors. This preparation adds no native dispatch and does not rebuild the prepared package.

### Earlier source validation — Load inspection queries

Added 15 Load inspection operations for active-case number, native lists, command/item counts and kinds, dynamic inclusion, indexed repeat/notional contributions and reference-command counts. Added distinct command/item/direction enums, named status/data results, immutable contribution snapshots, bounded internal buffers, checked primary activation, binding-lifetime validation and complete interface/adapter/model XML documentation. Preserved order, duplicate contributions, native status meanings and signed notional case-family identity.

Source now exposes 135 Load overloads and dispatches 88 of 129 documented native names. Of 41 undispatched names, one has an existing alternative, one is derivable, four are partially covered and 35 have no managed equivalent. Passed 2,448 offline tests, including 189 new cases, and compiled/executed all 30 new XML examples. Verified a validation-only CI archive through isolated PackageReference installation and all 370 selected XML examples. This increment is not included in the prepared preview archive below; no new distribution archive was built or published, and live native qualification remains pending.

## 1.0.0-preview.20261023 - 23 October 2026 (prepared preview)

This preview includes the completed main-interface Geometry and Load ports and the first 15 typed Output queries, alongside the reviewed Root APIs and approved wrapper surface.

- Completed all 143 reference Geometry overloads, including physical-member identification, plan-angle grouping and bounded polygon meshing.
- Completed all 115 main-interface Load signatures, exposed through 120 managed overloads including five conveniences. Included wind and seismic configuration, response-spectrum requests, direct-analysis definitions, population readers and related typed parameter models.
- Added 15 Output observations for analysis/buckling availability, node displacements, support reactions, local member-end actions, static checks, seven plate-center result operations and member design section/utilization. Results use named status/data classes and immutable payloads, with strict native-response validation and expanded interface/adapter/model IntelliSense.
- Corrected Load coverage terminology: 56 documented native entry points lack direct/delegated calls, but one capability has an existing alternative and one is derivable. Four have partial coverage and 50 have no managed equivalent found. This release does not add those broader native operations.
- Updated packaged usage, installation and publishing guidance. Retained 53 common Root operations, three optional Root operations and the approved wrapper bindings. Seven remaining domain adapters retain placeholder APIs.

Release verification covers a Release solution build, **2,259 offline tests**, and isolated PackageReference installation with compilation/execution of **340 selected XML examples** against managed stand-ins, including all 30 new Output examples. Package verification checks metadata, target assets, XML documentation and shipped guides. These checks do not invoke a native STAAD model or certify live marshaling, result units or engineering accuracy.

Targets Windows and .NET Framework 4.8.1 (net481). The archive contains no .NET Core/.NET 8 library asset. Live native compatibility and engineering qualification remain pending; 45 documented Output mappings and additional Load scopes remain separate work. This entry describes a locally prepared preview, not a new public NuGet.org publication.

### Unreleased source development — 23 October 2026

Implemented 15 typed Output queries for analysis/buckling availability, nodal displacement, support reactions, local member-end forces, static checks, seven plate-center result families and member design section/ratio observations. Added named status/data results, immutable owned payloads, strict native flags and buffer validation, documented design sentinel outcomes, binding-lifetime guards and complete interface/adapter/model IntelliSense. Reused source component conventions and existing force models without modifying the private source project or wrapper. Passed 2,259 offline tests, including 74 new Output cases, and a Release build with zero warnings/errors. Compiled and executed all 30 new Output XML examples against managed native stand-ins.

Reconfirmed complete Load reference parity: 115 main-interface overloads are present in both current types, each exposing 120 overloads including five conveniences. No reference Load port was pending. The broader vendor-only Load inventory remains separate work. Forty-five documented Output native names and live marshaling/engineering qualification remain pending. No NuGet archive was built or published for this increment.

### Unreleased source development — 22 October 2026

Completed the final 15 main-interface Load reference overloads, including remaining seismic member/wall operations, extended and legacy response-spectrum loads and direct-analysis definition APIs. Reused source request models and enum domains; added independent native packets, strict validation, checked Single conversion, guarded examples and complete interface/adapter/model IntelliSense. Source now exposes 120 Load overloads (115 reference plus five conveniences), with no reference interface overload pending. Passed 2,185 offline tests, including 161 new cases, and a Release build with zero warnings/errors. Compiled and executed all 310 selected source-reference XML examples with native mutations guarded; updated reusable package verification for the same inventory. Native numerical/marshaling validation and the broader managed capability/result architecture remain separate work; no NuGet archive was built or published.

Ported the next 15 reference Load overloads for ASCE wind definitions/profile calculations and seismic definition, parameter and weight operations. Reused the main source model hierarchy without proprietary binary dependencies. Added acquired-profile seismic guards, owned array packets, strict operation-specific result validation and complete interface/adapter/model IntelliSense. Preserved caller-thread execution and documented partial updates, native code distinctions and calculation-layout discrepancies. Current source exposes 105 Load overloads (100 reference plus five conveniences), leaving 15 reference overloads pending. Validated 2,024 offline tests, including 263 new cases. Compiled and executed all 280 selected source-reference XML examples with native mutations guarded; updated the reusable package verifier accordingly. No NuGet archive was built or published for this increment; live native numerical and cross-version marshaling validation remains pending.

Added 12 reference Load overloads for uniform member-load population, nodal refresh and basic wind definitions, exposure, intensity and deletion. Reused validated population readers and strict native integer handling. Added the immutable WindIntensityPair value model, descriptive wind parameters, full XML documentation, guarded examples and regression tests. Source Load coverage is now 90 overloads with 30 reference overloads pending; the next package verifier expects 250 selected XML examples. Validated 1,761 offline tests, including 157 new Load cases, and compiled/executed all 250 selected source XML examples with guarded native mutation samples. This work is not included in the published preview.

Completed the remaining three Geometry functions: IdentifyPhysicalMembers, GroupPhysicalMembersByPlanAngle and GeneratePolygonMesh. Added source-compatible result models, the Beam.Member managed back-reference, validated member reconstruction, XZ plan-bearing groups and bounded planar polygon meshing with explicit node/plate IDs and honest quality reporting. Corrected source mesh mapping, topology and generated-ID defects. Added complete interface/adapter/service XML contracts, regression tests and wrapper integration checks. Reconfirmed wrapper member parity; kept the eight placeholder domain APIs separate. Updated future package verification for 143 Geometry methods and 226 selected XML examples. These additions are not in the published 1.0.0-preview.20261015 package.

## 1.0.0-preview.20261015 - 15 October 2026 (prepared preview)

Completed the reviewed Root API port and expanded the wrapper surface. This preview includes all 39 reviewed native Root function mappings and all 23 private-source Root method mappings, exposed through 53 common managed operations and three separately acquired optional operations.

- Added the complete approved wrapper surface, raw native object bindings, application metadata and adapter placeholders for domains whose operations will be ported separately.
- Added wrapper-backed metadata queries, analysis state and status results, model information, input-unit selection, job metadata, CONNECTED project information and explicit analysis requests.
- Added guarded model creation, named-view creation, saving, removal and modification, opaque process-handle values, explicit application shutdown and qualified member-end force results.
- Added optional ADINA export and analysis error/warning retrieval through capability interfaces. Availability requires an exact candidate-build policy and compatible native member metadata. Candidate profiles cover export on 25.0.1.424 and export/messages on 26.0.0.340; these declarations do not certify live native behavior.
- Added descriptive managed parameters, named status/data results, typed options, bounded message retrieval and complete XML IntelliSense for the new operations. Expanded the packaged Root and wrapper usage guides.

Release validation covers 1,547 offline tests, a Release solution build, and isolated installation of the archive with compilation and execution of 217 selected XML examples against managed stand-ins. Mutating examples remain guarded; these checks do not run native STAAD operations. See the [Root completion guide](Root-Completion-and-Capabilities.md) for contracts and remaining validation gates.

Targets Windows and .NET Framework 4.8.1 (net481). This package does not contain a .NET Core library asset. New interface members require updates to custom interface implementations. Existing geometry and load APIs remain included. Live STAAD compatibility, native flag meanings, result-unit conventions and file side effects require validation before a stable release. This entry describes a prepared archive; public availability requires a separate NuGet.org upload and verification.

## 1.0.0-preview.20261014.7 - 14 October 2026 (preview)

This maintenance release improves the package metadata and documentation. The runtime API is unchanged from `1.0.0-preview.20261014.6`.

- Set the package description to **A strongly-typed .NET Framework wrapper for Bentley STAAD.Pro OpenSTAAD**.
- Reorganized the package README into a current product overview, installation instructions, API reference links and operating guidance. Removed internal implementation-history headings and corrected the documented preview version.
- Synchronized the package release notes, README and publishing instructions. Added version checks during packaging so distribution archives require a matching, reviewed documentation entry; automated CI previews receive their actual version in the packaged documents.

The package includes geometry and load operations, model identity and lifecycle functions, application-window controls, and typed native analysis requests. Expanded XML IntelliSense accompanies the library. The existing implementation passed **914 offline tests**; installed-package verification checks **149 XML example blocks**, including managed runtime checks and guarded window examples.

Targets Windows and .NET Framework 4.8.1 (`net481`). Native save/open/close, analysis-engine behavior and compatibility across STAAD releases require further validation before a stable release. This entry describes the prepared archive; public availability depends on NuGet.org acceptance and validation.

## 1.0.0-preview.20261014.6 - 14 October 2026 (prepared)

Added RestoreStaadMainWindow and AnalyzeEx with matching signatures and complete IntelliSense. Restore validates the reported window/PID before posting command 9. Analysis validates three native flags before one call, preserves documented error/in-progress/completion codes and unknown Int32 codes, and rejects unsupported status types or ranges. Added HiddenMode, WaitAnalysisMode and AnalysisResult; no pre-save, extra silent-mode mutation, result-file retrieval, retry, rollback or managed analysis timeout occurs.

Root coverage is **22 of 23 methods**, with **1 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **914 offline tests** and **149 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.5 - 14 October 2026 (prepared)

Added MinimizeStaadMainWindow and MaximizeStaadMainWindow with matching void signatures and complete IntelliSense. Read supported integral native handle/process values, reject malformed or foreign windows and post the correct Windows state request. Accepted posting does not prove the final state or focus; identity checks are snapshots and handle-recycling races remain possible. No model mutation, process launch, retry or rollback occurs.

Root coverage is **20 of 23 methods**, with **3 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **868 offline tests** and **145 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.4 - 14 October 2026 (prepared)

Added CloseCurrentStaadModel and CloseWriteReopenStaadFile with matching signatures and detailed IntelliSense. Conditional close skips explicit blank filenames and propagates errors. Rewrite validates/snapshots inputs, requires the current target, waits for blank identity and released locks, replaces complete UTF-8 contents atomically and avoids reopening over an intervening model. No save, backup, silent-state restoration, rollback or hard native deadline is supplied; false can follow native/file side effects.

Root coverage is **18 of 23 methods**, with **5 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **841 offline tests** and **141 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.3 - 14 October 2026 (prepared)

Added AwaitModelHandle and OpenStaadModel with matching signatures, explicit duration validation and complete IntelliSense. Poll full normalized paths using a monotonic budget, distinguish matching models from similarly named files, check immediately and cap waits to remaining time. Open requests occur once and count toward the documented seconds limit. Native errors propagate; logical timeouts cannot interrupt synchronous COM calls, and a path match does not certify Geometry/Load readiness.

Root coverage is **16 of 23 methods**, with **7 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **808 offline tests** and **137 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.2 - 14 October 2026 (prepared)

Added OpenSTAADFile and ShowApplication with matching main-project signatures and expanded IntelliSense. Open validates and resolves the requested path before one native model-switch request; show dispatches once without opening a model. Both return the original root after native dispatch and propagate exceptions, without readiness/visibility verification, automatic saves, retries or rollback.

Root coverage is **14 of 23 methods**, with **9 pending**. Custom `IOSRoot` implementations must implement the new members. Validated **774 offline tests** and **133 installed-package XML examples**. Live native mutation/version validation remains pending; no public upload is claimed.

## 1.0.0-preview.20261014.1 - 14 October 2026 (root save/close batch, prepared)

- Added main-project `SetSilentMode(SilentMode)`, `SaveModel(SilentMode)` and `CloseSTAADFile()` in the existing non-partial `IOSRoot`/`OSRootAdapter`, with matching complete XML IntelliSense and executable examples. Added documented `SilentMode.Disable=0` and `Enable=1`; reject undefined values before dispatch and pass native integer codes.
- Preserve `SetSilentMode`'s Boolean call-completion contract: ignore the native previous-state VARIANT, return true on completion and false on dispatch exceptions. Save/close remain one-call void native mutations with fluent adapter returns and unchanged exception propagation; no read-back, automatic save-on-close, rollback, application termination or unattended-operation guarantee.
- Root coverage is **12 of 23 methods**, with **11 pending**. Geometry remains 140 overloads and load remains 78, with 42 pending reference load overloads. Custom `IOSRoot` implementations must implement the three new members and consumers must rebuild.
- Passed **760 offline tests**, including 27 new cases covering enum codes, input validation, previous-state semantics, errors/partial effects, call isolation, fluent identity and matching IntelliSense. Expanded installed-package verification to **129 executable XML examples**, including all 24 root examples. Live native save/close and broader-version validation remain pending; no customer model is saved or closed by offline verification. This preview is prepared locally, not publicly published.

## 1.0.0-preview.20261013.10 - 13 October 2026 (load batch 4, prepared)

Added PopulateMemberLinearVaryingLoads, PopulateMemberLoads and PopulateMemberTrapezoidalLoads with matching signatures and detailed IntelliSense. Individual-family calls append; full population replaces managed lists with all six supported families in reference order and returns an eager snapshot of original beam objects. Preserved raw fields and supplied case identity with input validation before updates and documented partial failure behavior.

Completed twelve pending overloads in four batches. Validated 733 offline tests and 123 installed-package XML examples. Load coverage is 78 overloads, with 42 reference overloads pending. Live native mutation/unit/version checks remain pending.

## 1.0.0-preview.20261013.9 - 13 October 2026 (load batch 3, prepared)

Added GetRepeatLoadCases by scanning checked primary activation and native repeat-command counts, replacing the reference stub. Returned metadata classifies underlying primary IDs as RepeatLoad; native zero-count ambiguity is documented. Added append-only PopulateMemberConcentratedLoads and PopulateMemberConcentratedMoments with original case identity, single input snapshots and validated native readers.

Validated 728 offline tests and 117 installed-package XML examples. Load coverage is 75 overloads, with 45 reference overloads pending. Live native mutation/unit/version checks remain pending.

## 1.0.0-preview.20261013.8 - 13 October 2026 (load batch 2, prepared)

Added GetLoadCombination, GetMemberLoadsGroupedByLoadCase and GetNodalLoadsGroupedByLoadCase with detailed matching IntelliSense. Validated combination Boolean status, source arrays and primary membership; rejected unrepresentable SRSS overall factors. Preserved managed refresh, reference case identity, exact definition comparison and duplicate-assignment buckets.

Validated 720 offline tests and 111 installed-package XML examples. Load coverage is 72 overloads, with 48 reference overloads pending. Live native unit/version validation remains pending.

## 1.0.0-preview.20261013.7 - 13 October 2026 (load batch 1, prepared)

Added the thread-count AddSelfweightInXYZTo overload, CreateLoadCombinations and editor-definition CreateLoadEnvelopExt with matching signatures. Preserved the existing typed self-weight and integer-envelope implementations. Added invariant-culture editor combinations, eager filtering/validation and caller-thread entity-specific self-weight. Shared XML documentation is now an explicit compiler input for reliable incremental IntelliSense updates.

Validated 708 offline tests and 105 installed-package XML examples. Load coverage is 69 overloads, with 51 reference overloads pending. Live native mutation/unit/version verification remains pending.

## 1.0.0-preview.20261013.4 - 13 October 2026 (prepared)

- Added root `GetShortJobInfo()`, `GetFullJobInfo()` and `SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit)` with reference signatures and complete matching interface/concrete XML IntelliSense. Implementations stay in the existing non-partial root adapter.
- Added standalone property-notifying `ShortJobInfo` and `FullJobInfo` models with reference property/factory signatures. Native metadata reads use per-call BSTR sentinels, validate every output before model/date mapping and return independent snapshots. Empty dates map to DateTime.MinValue; nonempty dates preserve CurrentCulture parsing and invalid dates throw.
- Validate unit enums before a single native void SetInputUnits call, preserving length/force codes and fluent return semantics. Document input-state mutation, no automatic conversion/save/rollback and the unnamed length-code alias excluded by the current enum.
- Root coverage is **9 of 23 methods**, with **14 pending**. Load coverage remains 66 overloads, with 54 reference overloads pending. Custom IOSRoot implementations must implement the new members.
- Passed **694 offline tests**, including 90 new cases for all field mappings, malformed/unwritten output, date behavior, model ownership/notifications, all 56 defined unit pairs and compiled IntelliSense. Release build passed with zero warnings/errors; isolated installed-package verification compiled/executed **99 XML examples**.
- Read-only native job queries on STAAD.Pro Advanced 2026's unnamed session left outputs unwritten and were rejected as documented. Native named-model metadata/date conventions and live unit switching remain pending. This increment has not published a new version publicly.

## 1.0.0-preview.20261013.3 - 13 October 2026 (prepared)

- Added root `GetOpenedStaadFilename()`, `IsTarget(string staadFileFullPath)` and `ModelIsOpen(string fileFullPath)` with the main project's signatures, complete matching interface/concrete XML IntelliSense and executable examples. Implementations remain in the existing non-partial root adapter.
- Normalize complete model paths and compare them case-insensitively. Validate requested paths before COM access, accept relative paths using the process working directory, return false for explicitly unnamed sessions and propagate native/path failures. Each function reads the native model filename once.
- Corrected the reference's `ModelIsOpen` full-path-flag/basename behavior and empty-filename normalization. Corrected shared root output initialization to a string sentinel so native `GetSTAADFile` receives BSTR rather than null/VT_EMPTY, while unwritten output is still detected.
- Root coverage is now **6 of 23 methods**, with **17 pending**. Custom `IOSRoot` implementations must implement the added members. Load coverage remains 66 overloads with 54 pending reference overloads.
- Passed **604 offline tests**, including 21 new cases, and a Release build with zero warnings/errors. Isolated package verification compiles/runs **93 XML examples**, including all twelve root examples, and checks normalized identities, distinct-directory basenames and unnamed sessions.
- Read-only live checks on STAAD.Pro Advanced 2026's unnamed session passed for all six root methods with the corrected string output buffer. Native named-model and broader-version validation remain pending. This increment does not change provider acquisition behavior or publish a package publicly.

## 1.0.0-preview.20261013.2 - 13 October 2026 (prepared)

- Added OSLoad `AddSelfweightInXYZTo(int, LoadCaseType, IEnumerable<int>, SelfWeightDirection, double)`, `CreateLoadEnvelopExt(int, LoadEnvelopType, IEnumerable<int>)` and `CreateLoadList(LoadListType, IEnumerable<int>)` with matching main-project signatures. Added documented native-code enums and checked activation/status handling, input snapshots and independent ID arrays.
- Added OSRoot `GetStaadFileFullPath()`, `GetStaadFileNameOnly()` and `GetAnlFileFullPath()` to the existing interface/adapter binding exposed by `wrapper.Root`. Each uses one native model-path read. Analysis paths use a single snapshot and do not imply existing/current results.
- Included the prior envelope-query batch in the committed source, with standalone models, full validation and independent metadata. No adapter partial classes or private application/Bentley binary dependencies were introduced.
- Shipped matching expanded XML for every new interface/concrete function, executable examples, parameter/default/return/exception/state-change contracts, load/native mapping documentation and a root implementation/pending-method guide.
- Load coverage is now **66 overloads** (61 reference matches plus five conveniences), with **54 reference overloads pending**. Root coverage is **3 of 23 methods**, with **20 pending**. Custom implementations of `IOSLoad`/`IOSRoot` must implement the added interface members.
- Passed **583 offline tests**, with 62 added for this batch and 113 since the last committed grouped-assignment baseline. Release build has zero warnings/errors. Isolated package verification compiles/runs **87 XML examples** and exercises all new load/root functions without ProjectReferences.
- Native self-weight/envelope mutation, load-list semantics, new root reads and envelope marshaling still require live STAAD/version validation. The preview is built for NuGet distribution; public upload is a separate step and was not performed by this increment.

## 1.0.0-preview.20261013.1 - 13 October 2026 (prepared)

- Added `GetEnvelopeIds()`, `GetLoadEnvelopeDetail(int envId)` and `GetAllLoadEnvelopeDetails()` with matching main-project public signatures in `IOSLoad` and implementations/helpers in the existing non-partial `OSLoadAdapter`.
- Added standalone `ILoadEnvelopeDetail`, `LoadEnvelopeDetail` and `LoadEnvelopType` with all native classification codes 0–6, property notification and independent constructor/query membership sets.
- Checked envelope fetch success **1** and rejected failure **-1**, malformed counts, invalid classifications, inconsistent arrays, duplicate/nonpositive IDs and unwritten outputs. Supported replaced/in-place arrays and nonzero SAFEARRAY lower bounds. Valid zero counts return non-null empty sets; all-envelope reads are eager and return no partial result on failure.
- Preserved active-case and active-load-list state. Envelope results contain metadata/membership, with no computed extrema, native writes or member-case resolution. Documented mutable-model behavior and the intentional validation/empty-result differences from the main reference.
- Added full expanded IntelliSense on interface and concrete calls, supporting-type documentation, an envelope guide and updated API/inventory references. Load coverage is now **63 overloads**, comprising 58 reference matches plus five repository-specific conveniences; **57 reference overloads remain pending**.
- Verification covers **521 passing offline tests**, a Release build with no warnings/errors, and isolated package installation with **75 compiled/executed XML examples**, including all six new envelope examples. Live envelope queries remain pending validation against native STAAD. This prepared version has not been published by this increment.

## 1.0.0-preview.20261013 - 13 October 2026 (prepared)

- Added `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient, int nThreads)` with the reference signature and one scalar temperature assignment per entity. Validated all inputs before checked primary-case activation. Kept COM calls on the caller's thread instead of the reference worker dispatch; positive `nThreads` is retained for compatibility and does not control concurrency.
- Added `AddMemberLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(MemberLoad Load, List<Beam> Beams)>> grouped)`, preserving member-tuple skipping and delegation to the existing member assignment helper.
- Added `AddNodalLoadsFromGroupsByLoadCase(Dictionary<ILoadCase, List<(NodalLoad Load, List<Node> Nodes)>> grouped)`, preserving delegation to the existing six-component nodal helper and its null/empty behavior.
- Preserved grouped case-key routing, concrete member-load mapping, raw values and managed-object ownership. Documented legacy activation-status handling, native exception propagation and partial changes without rollback.
- Added matching expanded interface/concrete IntelliSense and executable examples. Updated load coverage to 60 overloads, including 55 reference matches and five repository-specific conveniences; 60 reference load overloads remain pending.
- Passed 470 offline tests and a Release build with no warnings/errors. Extended installed-package verification to restore a fresh consumer and compile/run all 69 XML examples, including the three new APIs. Native live mutation, engineering-unit and STAAD-version validation remain pending. This package is prepared for manual upload and has not been published to NuGet.org.

## 1.0.0-preview.20261012.3 - 12 October 2026 (prepared)

- Added `CreateMultipleBeamsExtended(IOSRoot openStaad, IEnumerable<Beam> beamCollection)` with the original signature and ordered native `CreateBeam` calls. Replaced the reference's non-working bulk implementation; validated all incidences before mutation and documented partial failure without rollback.
- Added `CreateMultipleNodesExtended(IOSRoot openStaad, IEnumerable<Node> nodeCollection)` with explicit IDs and a row-aligned XYZ coordinate matrix in model input units.
- Added `MergeNodesExtended(IOSRoot openStaad, Node retain, IEnumerable<Node> nodesToMerge)` preserving retained-ID inclusion in the native merge array.
- Added the minimal `IOSRoot`/`OSRootAdapter` binding and `OpenStaadWrapper.Root`, populated on construction and cleared on disposal. The supplied root selects the model for extended mutations.
- Added `AddReferenceLoadsTo(int loadCaseId, Dictionary<int, double> loadCaseNumberFactorPairs)`, `AddRepeatLoadsTo(int loadCaseId, IDictionary<int, double> loadCaseNumberFactorPairs)` and the synchronous `AddAxialThermalGradientTo(int loadCaseId, IEnumerable<int> entities, double axialThermalGradient)` overload. Added input prevalidation, paired factor snapshots, primary activation checks and empty-input no-ops. Nonempty calls leave the target active.
- Added matching detailed interface/concrete XML contracts and executable examples. Updated coverage to 140 geometry and 57 load overloads; three geometry and 63 reference load overloads remain pending. See [geometry](Geometry-Porting.md) and [load](Load-Porting.md) inventories.
- Extended installed-package verification to compile/run 63 XML examples and check all six native argument contracts. Offline validation passed 441 tests. These checks use managed native stand-ins; live native mutation and unit/version validation remain pending. This version has not been published to NuGet.org.

## 1.0.0-preview.20261012.2 - 12 October 2026 (prepared)

- Added `SegregateBeamsByConnectivity(IEnumerable<Beam> beams)` to group connected analytical beams and order simple chains.
- Added `SegregateMembersByConnectivity(IEnumerable<Member> members, MemberConnectivityScope scope = MemberConnectivityScope.EndpointsOnly)` for endpoint or all-node member grouping.
- Added `SegregatePlatesByConnectivity(IEnumerable<Plate> plates, PlateConnectivityScope scope = PlateConnectivityScope.SharedEdge)` for candidate-edge or any-corner plate grouping, preserving the reference predicate.
- Added the standalone `ConnectivityService` and matching connectivity-scope enums. The algorithms operate on managed entities without native STAAD calls. Beam input is explicitly deduplicated by ID to accommodate the target entity's endpoint-aware equality.
- Restored and expanded XML documentation for every geometry/load overload on both interfaces and concrete adapters, plus previously undocumented exported managed functions. Shared XML source comments expand into the NuGet XML file; documentation warnings now fail builds.
- Added a complete [geometry comparison](Geometry-Porting.md): nine methods were pending before this increment, three are now ported, and six remain.
- Extended package checks to validate complete adapter documentation and compile/run 51 XML examples, including the three new APIs. Added offline connectivity and compiled-XML regression coverage. This version has not been published to NuGet.org.

## 1.0.0-preview.20261012.1 - 12 October 2026

- Fixes `Get(fullPath)` reopening a matching running model: `GetSTAADFile` now uses Bentley's output-VARIANT signature (`ref object filename, true`) rather than treating its return value as the filename.
- Recognizes OpenSTAAD's registered class-ID ROT monikers as well as model paths and ProgID names. Always includes the active ProgID candidate, skips unrelated solution registrations, and releases enumeration COM objects after each lookup.
- Replaces fixed three/four-second sleeps with polling every 250 ms, for up to two minutes. A path-specific acquisition returns only the requested model, with Geometry/Load interfaces and read-only count calls available. An identified loading model is waited for without reopening it. Connection failures/timeouts still return null, with Trace diagnostics.
- Clarifies legacy WPF installation: use Visual Studio's `Install-Package` with the project selected. The solution directory may contain no project, and an unconfigured legacy project can also reject `dotnet add package`.
- Validation: 361 passing offline tests, including 19 attachment/ROT regression cases; restore/build of a copy of the user's legacy WPF project against public `1.0.0-preview.20261012`; isolated corrected-package verification of 42 executable XML examples and all 21 load queries.
- Live validation on installed STAAD.Pro 2025: the supplied model opened on the first call in approximately 41.8 seconds, returning Geometry/Load with 6,444 nodes and 74 primary load cases. The second call took approximately 57 ms and used the same STAAD process with no additional session. This is one model/version check; engineering units and broader version compatibility still require validation.

This corrected version supersedes `1.0.0-preview.20261012`. Already-published `1.0.0-preview.20261012` remains unchanged. Public availability is recorded in the [release assessment](NuGet-Readiness.md); see [Publishing.md](Publishing.md) for public-feed verification.

## 1.0.0-preview.20261012 — 12 October 2026

This preview targets Windows and **.NET Framework 4.8.1 (`net481`)**. It contains the existing geometry/load wrapper and twenty-one load-query functions ported from the main application's interop project. The package ID is `StaadPro.Interop`; its version is `1.0.0-preview.20261012`. Public availability requires NuGet.org submission, acceptance and public-feed verification, tracked in the [readiness assessment](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/NuGet-Readiness.md).

### Latest additions

- `GetLoadCases(int fromId, int toId, LoadCaseType loadCaseType)` returns fresh metadata for an inclusive positive ID range. Invalid bounds are rejected before COM access; sparse ranges do not require generating every integer.
- `GetLoadCasesFromIds(IEnumerable<int> loadCasesIds, LoadCaseType loadCaseType)` snapshots input once, validates IDs before COM access, collapses duplicate requests and omits missing IDs. Empty input returns a fresh empty list without native reads after validating the selected family.
- `GetLoadCasesOfType(LoadCaseType loadCaseType)` returns a fresh concrete `List<ILoadCase>` for the selected primary, reference or combination family.

These methods read the complete selected family eagerly and preserve the active load case. Metadata failures propagate even for cases excluded by a filter. Output ordering is unspecified. Repeat and undefined families are unsupported. Results contain metadata; they do not load assignments, combination factors or analysis results.

### Other load queries included in this preview

Seven assignment readers return nodal forces/moments and member distributed forces, point forces, point moments, linear varying loads, trapezoidal loads and uniform moments:

- `GetNodalLoads`
- `GetMemberUniformlyDistributedLoads`
- `GetMemberConcentratedLoads`
- `GetMemberConcentratedMoments`
- `GetMemberLinearVaryingLoads`
- `GetMemberTrapezoidalLoads`
- `GetMemberUniformMoments`

Eleven other metadata queries provide family discovery, direct lookup, batch lookup and generic family selection:

- `GetAllPrimaryLoadCases`, `GetAllReferenceLoadCases`, `GetAllLoadCombinationCases`
- `GetPrimaryLoadCaseFromId`, `GetReferenceLoadCaseFromId`, `GetLoadCombinationCaseFromId`
- `GetPrimaryLoadCasesFromIds`, `GetReferenceLoadCasesFromIds`, `GetLoadCombinationCasesFromIds`
- `GetAllLoadCasesOfType`, `GetLoadCase`

Primary/reference batches preserve requested order and duplicates as separate objects; combination batches collapse duplicates and validate all memberships before reading titles. Generic lookup returns null for a missing positive ID after successfully reading the selected family. Assignment readers activate the supplied case and preserve native signs, order and raw units. Family metadata readers leave active-case state unchanged. See [Load-Queries.md](Load-Queries.md) and the [API reference](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/docs/API-Reference.md) for complete signatures, contracts and examples.

### Validation and documentation

- Inputs, native statuses, counts, arrays, directions and metadata are checked; malformed responses throw rather than returning partial results. SAFEARRAYs with nonzero lower bounds are supported by the assignment readers.
- Uniform moment model conversion uses moment divided by length for intensity and treats eccentricity as a length. This corrects the main model's dimensional conversion and omitted eccentricity conversion.
- All twenty-one ported functions have expanded XML IntelliSense on both `IOSLoad` and `OSLoadAdapter`, including parameters, return values, exceptions, side effects and executable examples. Adapter implementations remain in a single non-partial class.
- The package ships XML beside the DLL, a NuGet README, load-query documentation, release notes, and [installation](Installation.md)/[publishing](Publishing.md) guides.
- Release verification covers **342 passing offline tests**, **42 compiled and executed XML examples**, and a compiled installation example in an isolated PackageReference consumer using a fresh cache. The managed stand-in exercises mapping and error contracts; it does not certify native STAAD behavior.

### Compatibility and remaining validation

A licensed STAAD.Pro installation exposing the required OpenSTAAD APIs is needed for actual model access. Bentley binaries and the private main application are not bundled. Newtonsoft.Json 13.0.3 is a NuGet dependency. Shared-session COM coordination remains the application's responsibility.

Live COM dispatch/marshaling, engineering unit read-back and STAAD version/architecture compatibility remain pending. Native combination-number return-count documentation also requires validation with differing family counts. The readiness assessment records these and inherited implementation gates before a stable release. This preview makes no claim of verified support across STAAD versions.

### Installation

After this exact version becomes publicly available:

```powershell
dotnet add ./YourApp.csproj package StaadPro.Interop --version 1.0.0-preview.20261012 --source https://api.nuget.org/v3/index.json
```

Enable prerelease packages in Visual Studio's NuGet UI. The verified archive can also be installed through a local feed before publication; see [Installation.md](Installation.md).
