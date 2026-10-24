# Property observations and main-project ports

**Updated:** 24 October 2026

## Current increment: 25 management ports

Incorporated 25 further Property overloads for table/composite/built-up section creation, beta-angle assignment and population, material assignment subsets, four-corner plate thickness creation/retrieval/assignment, and member-offset creation/population/assignment. Property now exposes **75 overloads**: 25 typed observations and 50 main-project compatibility ports. Added detailed, matching interface/adapter XML IntelliSense and examples, the required thickness/offset packets, member offset references and preserved table/offset enum codes. Native dispatch covers **54 of 204 active Property candidates**, with **150 remaining**. See [the 25-method guide](Property-Management-Ports.md) for all signatures, units, statuses, mutation behavior and compatibility corrections.

Passed a Release solution build with zero warnings/errors and **3,763 offline tests**, including **189 new Property management cases**. The isolated installed-package verifier compiles and runs **592 selected XML examples**, including all **150 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins exercise the examples without changing a native STAAD model. Live creation/assignment and engineering qualification for this increment remain pending.

The four material subset overloads originate in the main adapter; the other 21 originate in its interface as well. Signature defaults and parameter names are preserved. The expanded supporting packets omit the private unit-converter helpers. Interface expansion requires custom IOSProperty implementations to implement the new methods. Existing 25 typed observations keep their return packets and signatures.

## Previous increment: 25 inventory and assignment ports

Added 25 main-project Property overloads for section/thickness inventories, assignment discovery, material context and assignment, member specifications, and global-Y member-end offsets. Property now exposes **50 overloads**: the existing 25 typed observations and these 25 signature-compatible ports. All new interface and adapter methods include expanded XML IntelliSense with parameters, return values, examples, units, side effects and failure contracts. Shared locking/lifetime checks, bounded native buffers and checked operation-specific statuses are included. Passed **3,531 offline tests**, including **209 new Property cases**, with zero build warnings/errors. The installed-package verifier compiles and runs **542 selected XML examples**, including all **100 Property examples**, against managed stand-ins. Live COM and engineering qualification remain pending. See [all 25 added signatures and usage contracts](Property-Main-Ports.md).

At that preceding checkpoint the adapter exposed **50 public overloads** and adds routes to **19 further native names**, for **44 of 204 active native candidates** with **160 remaining**. Eight of the 212-name union remain excluded. This is dispatch-route coverage, not a claim of complete typed parsing or native qualification: the raw section-buffer route deliberately does not decode a version-specific layout. The archived multi-version review retains its original preimplementation coverage labels.

The compatibility methods preserve main-project signatures and direct return types; the original 25 observation methods retain their status/data contracts. Both sets share one adapter lock and captured-binding checks. Assignment and material-context operations mutate native state without implicit saving; read methods remain read-only.

## Earlier foundation increment

The first increment implemented **25 typed Property queries** through IOSProperty and the single non-partial OSPropertyAdapter: all 13 P2 native names and the first 12 P3 names in the documented allocation. Shared 2024–2026 contracts use one internal reader and strict value decoder. The 212-name union contains eight excluded entries, leaving 204 active candidates; **25 had managed routes and 179 remained pending at that checkpoint**. GetPlateSectionPropertyRefNo is documented but absent from inspected vendor CLR metadata, so its acquired receiver may return Unsupported. Implementation coverage does not certify live COM or engineering behavior.

Passed **3,322 offline tests**, including **422 new Property cases**, and a Release build with zero warnings/errors. All 50 interface/adapter XML examples compiled and ran through isolated installation of the local validation package; all 492 selected package examples passed. This is source and managed-stand-in verification; live native qualification remains pending. That foundation checkpoint used a separate validation archive; the new preview below includes both increments.

Adding these members to IOSProperty requires custom interface implementations to provide the new members. Existing wrapper/provider contracts and target frameworks are unchanged. The legacy ComObject escape hatch remains available; direct calls bypass the typed validation described here.

## Result, units and state rules

Each method returns PropertyQueryResult<T>. Inspect IsSuccessful before reading Data; an unsuccessful result exposes no payload and Data throws InvalidOperationException. NativeFunction identifies the decisive query, including a count/preflight call that prevented retrieval. NativeReturnCode and NativeBooleanReturn retain actual integral/Boolean evidence rather than applying a universal nonzero-success rule. Transport exceptions propagate, while unsupported types, undocumented codes and malformed/unwritten applicable outputs raise InvalidOperationException.

NoSpecification, NotAssigned, NotFound and NativeFailure have different meanings. A successful zero offset remains valid data; missing offsets never produce a fabricated zero vector. Named native material queries can use integer-zero success, Boolean success or returned-name success depending on the function. Assignment state is native 1=Unassigned / 2=Assigned.

All 25 foundation query calls are read-only, execute on the caller’s established COM apartment, and share a lock on one Property adapter. They do not activate cases, run analysis, save, create/assign properties, retry calls or perform unit conversion. Other adapters, raw calls, wrappers and external edits are not excluded. Reacquire wrapper.Property after RefreshComObjects. Captured receivers are checked before and after retrieval; a same-binding unsaved edit or same-count reorder cannot be ruled out.

Native numerical units are preserved. Weight density means force/volume, not mass/volume. Elastic/shear moduli and strengths retain force/area dimensions; thermal expansion retains inverse-temperature units; damping, Poisson and strength ratios are dimensionless. Member/plate offsets and section thicknesses retain length units, areas length-squared and inertia slots length-to-the-fourth. Wrapper input-unit fallback caches do not establish an observed Property-unit contract.

## Existing model and enum reuse

| Existing item | Reuse or necessary distinction |
| --- | --- |
| MemberEnd | Reused the existing Start=0 / End=1 enum; no duplicate member-end type. |
| Vector3D | Reused the existing WindowsBase value vector for independent local/global offsets. |
| NativeValues | Reused strict integral native decoding instead of Convert.ToInt32 rounding/coercion. |
| Main ElementNodeOffsetAxis | Ported exact Local=0 / Global=1 / Z=2 semantics with XML; did not reuse the oppositely numbered member OffsetAxis. |
| Main MaterialProperty | Reused E/Poisson/weight-density/thermal/damping and strength field meanings. Returned immutable observations; did not copy assumed engineering defaults, computed G/K substitutes, entity-assignment collections or proprietary catalog/unit dependencies. Native G is retained only when returned. |
| Main MemberReleases / Releases / Springs / MpFactors | Reused six-DOF order and MP concepts. Added per-DOF release modes and nullable applicable factors because Boolean Releases loses -1/-2/-3 and the source whole-release classification cannot represent independent mixed modes. Did not copy mutable parsing/event wiring or defaults for unavailable MP outputs. |
| Main OffsetSpecification / ElementNodeOffset | Reused offset, incidence-index and coordinate-selector semantics. Used existing MemberEnd plus Vector3D/status results rather than copying mutable assignment sets or collapsing absent and successful-zero offsets to a shared singleton. |
| Existing result patterns | Followed the existing named status/data pattern without coupling Property absence/status meanings to Output/Load enums or adding public tuples/ref/out. |

The main project was inspected strictly read-only. The earlier 74-file source evidence was rechecked; no source file was modified. New immutable result types make missing versus zero fields explicit and avoid importing the private dependency graph.

## Implemented APIs and choosing between them

### GetBetaAngle

~~~csharp
PropertyQueryResult<double> GetBetaAngle(int memberId);
~~~

Native: `GetBetaAngle`. Workstream: P2.

Use to observe orientation about the analytical member axis; offsets and releases do not represent beta rotation. Returns the finite signed native angle unchanged, including zero. Native -3001 is NotFound. No degrees/radians conversion or managed local-axis reconstruction is performed.

### GetElementOffsetSpecificationCount

~~~csharp
PropertyQueryResult<int> GetElementOffsetSpecificationCount();
~~~

Native: `GetElementOffsetSpecCount`. Workstream: P2.

This counts native offset specifications, not plates, corners, nonzero vectors or assigned members. Zero is a valid count. Use GetPlateOffsetSpecification to inspect one plate corner and its stored convention. Undocumented negative counts are rejected.

### GetMemberReleaseSpecificationExtended

~~~csharp
PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd);
~~~

Native: `GetMemberReleaseSpecEx`. Workstream: P2.

Prefer this query when common MP or directional MPX/MPY/MPZ factors are needed. The basic GetMemberReleaseSpecification returns only six mode/parameter slots and cannot supply the separate extended factors. Native 1/true succeeds and 0/false fails. Modes 0/1/-1/-2/-3 are retained per DOF rather than converted to Boolean release flags. Unused spring/MP slots may remain unwritten and are represented as null; an applicable spring or factor must be written and finite. The native common MP factor is dimensionless; translational and rotational spring stiffness retain their different native dimensions.

### GetMemberReleaseSpecification

~~~csharp
PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd);
~~~

Native: `GetMemberReleaseSpec`. Workstream: P2.

Use for basic release/spring observations. Choose GetMemberReleaseSpecificationExtended for separate common MP and MPX/MPY/MPZ outputs; this basic query never invents zero for those missing outputs. Native 1/true succeeds and 0/false fails. FX/FY/FZ/MX/MY/MZ mode order and 0/1/-1/-2/-3 codes are preserved. Only applicable parameter slots are consumed; a spring or directional partial-factor slot must be finite. These are model specifications, not support reactions or member-end Output forces.

### GetMemberLocalOffset

~~~csharp
PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd);
~~~

Native: `GetMemberLocalOffSet`. Workstream: P2.

Choose this query when a local-coordinate offset is needed; GetMemberGlobalOffset asks STAAD for global components. The vector is an offset in native length units, not a position, displacement result or beta rotation. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector stays successful and is not replaced by a shared no-offset singleton. No coordinate conversion or sign change is applied.

### GetMemberGlobalOffset

~~~csharp
PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd);
~~~

Native: `GetMemberGlobalOffSet`. Workstream: P2.

Choose this query for global components; GetMemberLocalOffset requests member-local components. No managed rotation is substituted. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector differs from an absent specification. The returned Vector3D is an independent value in native length units. These are modeling offsets, not Output displacements.

### GetMemberSectionProperties

~~~csharp
PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId);
~~~

Native: `GetBeamProperty`. Workstream: P2.

Use for width, depth, axial/shear areas and the three named native inertia slots. GetMemberSectionPropertiesExtended additionally returns flange/web thickness; those fields are null here. This does not hydrate a section catalog or select a section shape. Native 1/true succeeds; 0/false cannot distinguish a missing member, unassigned property and general failure, so it returns NativeFailure without fabricated data. Native Ix/Iz descriptions conflict with conventional physical-axis labels; their slot names/order are preserved without assuming a torsion or principal-axis mapping. Length, area and inertia slots retain their respective native length, length-squared and length-to-the-fourth units.

### GetMemberSectionPropertiesExtended

~~~csharp
PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId);
~~~

Native: `GetBeamPropertyAll`. Workstream: P2.

Use instead of GetMemberSectionProperties when the two additional thickness outputs are required. No section shape, catalog identity, material or new property is inferred. Native 1/true succeeds; 0/false is an ambiguous failure and exposes no packet. All returned slots must be finite. Native Ix/Iz prose is inconsistent, so NativeIx/NativeIy/NativeIz retain native slot identity rather than a guessed torsional/principal interpretation. Values retain native length, area and fourth-power length units.

### GetMemberMaterialConstants

~~~csharp
PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId);
~~~

Native: `GetBeamConstants`. Workstream: P2.

Use when the member is known but its material name is not. GetMaterialProperty instead selects a material by exact name; indexed isotropic queries select a material-list index and additionally return G. This native call returns E, Poisson ratio, weight density, thermal expansion and damping, not G or strength fields. ShearModulus is therefore null, never computed from E/Poisson. Native Boolean true succeeds; false does not distinguish a missing member from unavailable constants. WeightDensity is weight per volume, not mass density.

### GetPlateLocalOffset

~~~csharp
PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex);
~~~

Native: `GetElementLocalOffset`. Workstream: P2.

Choose this query for local components, GetPlateGlobalOffset for global components, or GetPlateOffsetSpecification for the stored selector and vector. cornerIndex is native incidence position 1/2/3/4, never a node ID; triangular-corner validity remains STAAD-controlled. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is retained. No incidence lookup, coordinate transform or unit conversion occurs.

### GetPlateGlobalOffset

~~~csharp
PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex);
~~~

Native: `GetElementGlobalOffset`. Workstream: P2.

Use for global components; GetPlateLocalOffset returns local components and GetPlateOffsetSpecification returns the stored convention. The native Global function is called despite its help summary mistakenly saying local. cornerIndex is a one-based incidence position 1-4, not a node number. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is not absence; no conversion or sign change occurs.

### GetPlateOffsetSpecification

~~~csharp
PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex);
~~~

Native: `GetElementOffsetSpec`. Workstream: P2.

Use when the original Local/Global/Z selector is needed; the local/global offset queries instead request components in a particular coordinate frame. Native 1 supplies an offset, native 0 explicitly means NoSpecification, and -4001 means NotFound. This differs from the local/global getters, where 0 is success. Stored selectors are Local=0, Global=1 and Z=2, matching the main ElementNodeOffsetAxis enum; they must not be cast from its opposite member OffsetAxis numbering. No physical meaning or conversion beyond the native Z selector is inferred. A code-1 zero vector remains valid data.

### GetPlateSectionPropertyId

~~~csharp
PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId);
~~~

Native: `GetPlateSectionPropertyRefNo`. Workstream: P2.

Use to identify an assigned property, not to read its thickness values or count property definitions. Positive return values are property IDs; -4001 is NotFound and -6022 NotAssigned. Zero is undocumented and rejected. The method is documented in all three reviewed help sets but absent from inspected 2025/2026 CLR interfaces: a missing acquired dispatch member returns Unsupported. No alternate function name, different entity type or fallback call is attempted; other transport errors propagate.

### GetIsotropicMaterialAssignedMemberCount

~~~csharp
PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName);
~~~

Native: `GetIsotropicMaterialAssignedBeamCount`. Workstream: P3.

Use for sizing member assignments. GetIsotropicMaterialCount counts material definitions; GetIsotropicMaterialAssignedMemberIds returns the member IDs. A material name is not an index or section/property ID. Nonnegative values are treated as counts following the native purpose and read-only main-source usage; the help table incorrectly lists only 0 OK and its example passes a number despite the named input. Zero is a valid empty count; -6023 is NotFound. This count interpretation remains a live qualification requirement.

### GetIsotropicMaterialAssignedMemberIds

~~~csharp
PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults = 100000);
~~~

Native: `GetIsotropicMaterialAssignedBeamList`. Workstream: P3.

Use when native assignment IDs are required without preloading Beam objects. The main source convenience filters a caller-supplied Beam collection; this query returns every native listed ID, preserving order and duplicates. It sizes with GetIsotropicMaterialAssignedBeamCount, validates positive Int32 IDs and exact array shape, and rechecks the count. Native list return 0 succeeds and -6023 is NotFound. Zero count returns an empty read-only snapshot without fetching; a larger count than maximumResults returns LimitExceeded before allocation. Count interpretation follows the native purpose/main usage despite the inconsistent help table. Same-count replacement or concurrent changes through other receivers cannot be proved absent.

### GetMaterialProperty

~~~csharp
PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName);
~~~

Native: `GetMaterialProperty`. Workstream: P3.

Use an exact material name obtained from an entity or native material observation. GetIsotropicMaterialProperty instead uses a zero-based list index and supplies native G; GetExtendedMaterialProperty also supplies strength fields. Native return 0 succeeds and -6023 is NotFound. These are different success semantics from the Boolean extended function. ShearModulus, Strengths and AssignmentState are null because this function does not return them. No default engineering values or computed G are substituted. Density remains weight per volume.

### GetBeamMaterialName

~~~csharp
PropertyQueryResult<string> GetBeamMaterialName(int memberId);
~~~

Native: `GetBeamMaterialName`. Workstream: P3.

Use for name discovery before a named material query. This returns a name, not constants, an index, a property ID or a new assignment. Native spelling/case/whitespace are preserved. Empty or native-null text is EmptyOrUnavailable because the help supplies no way to distinguish no assignment from a missing/unavailable member. Non-string responses are rejected; no guessed material name is returned.

### GetElementMaterialName

~~~csharp
PropertyQueryResult<string> GetElementMaterialName(int plateId);
~~~

Native: `GetElementMaterialName`. Workstream: P3.

This native element query takes a plate ID. Use GetBeamMaterialName for members; do not pass a node, solid ID or material-list index. It returns the exact name, not constants or thickness. Empty/native-null text is EmptyOrUnavailable because the contract does not distinguish unassigned and unavailable entities. No fallback to a similarly named plate/material API is attempted.

### GetIsotropicMaterialCount

~~~csharp
PropertyQueryResult<int> GetIsotropicMaterialCount();
~~~

Native: `GetIsotropicMaterialCount`. Workstream: P3.

Use to bound the zero-based materialIndex accepted by GetIsotropicMaterialProperty and GetExtendedIsotropicMaterialProperty. This is not a member/plate assignment count. Zero is a valid empty definition inventory; undocumented negative or nonintegral counts are rejected. A count is only an observation, not a stable reservation of material identities.

### GetIsotropicMaterialProperty

~~~csharp
PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex);
~~~

Native: `GetIsotropicMaterialProperties`. Workstream: P3.

Use for material enumeration, unlike GetMaterialProperty which selects by name. materialIndex is zero-based, not a native property ID or member ID. This function returns G directly; that native value is retained instead of the main MaterialProperty computed E/(2*(1+Poisson)) substitute. The native return is the material name, not a success flag. Null/empty name or an index outside the observed count is NotFound. Before/after material counts detect observed changes but not same-count reorderings. Strengths and AssignmentState are null.

### GetExtendedMaterialProperty

~~~csharp
PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName);
~~~

Native: `GetMaterialPropertyEx`. Workstream: P3.

Choose over GetMaterialProperty when Fy/Fu/Ry/Rt/Fcu are needed. GetExtendedIsotropicMaterialProperty selects a zero-based index and additionally returns native G; this name-based function omits G, so ShearModulus is null. Unlike basic named GetMaterialProperty, native Boolean true means success and false means failure. Strength stresses retain native force/area units; Ry/Rt are dimensionless. No material classification, design pass/fail, default strength or computed shear modulus is inferred.

### GetExtendedIsotropicMaterialProperty

~~~csharp
PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex);
~~~

Native: `GetIsotropicMaterialPropertiesEx`. Workstream: P3.

Use for enumeration with native G and Fy/Fu/Ry/Rt/Fcu. GetExtendedMaterialProperty selects by name and lacks G; GetIsotropicMaterialProperty omits strengths. The native return is the material name, not a Boolean or numeric status. Null/empty name or an out-of-range observed index is NotFound. Coupled counts are checked before/after; no atomic snapshot or stable same-count ordering is promised. All supplied constants and strength fields must be written and finite; optional AssignmentState remains null.

### GetIsotropicMaterialPropertiesAssigned

~~~csharp
PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber);
~~~

Native: `GetIsotropicMaterialPropertiesAssigned`. Workstream: P3.

Use when the native assignment observation is required; the ordinary and extended indexed functions do not return it. nativeMaterialNumber is forwarded unchanged: this help says material number ID, unlike the explicitly zero-based indexed methods. Nonnegative input is accepted without guessed rebasing or a count-based ID/index conversion; qualify the selector on the acquired build. Native string name supplies success; null/empty means NotFound. Assignment output 1 means Unassigned and 2 Assigned, not Boolean 0/1; documented Double or integral forms are decoded exactly. Strengths remain null.

### GetOrthotropic2DMaterialCount

~~~csharp
PropertyQueryResult<int> GetOrthotropic2DMaterialCount();
~~~

Native: `GetOrthotropic2DMaterialCount`. Workstream: P3.

Use for the 2D orthotropic inventory, not isotropic or 3D material counts and not element assignments. Zero is valid. The associated property getter documents a native material number rather than an explicit zero-based list index; this count does not establish a selector rebasing rule. Undocumented negative/nonintegral counts are rejected.

### GetOrthotropic2DMaterialProperties

~~~csharp
PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber);
~~~

Native: `GetOrthotropic2DMaterialProperties`. Workstream: P3.

Use for independent directional material constants; isotropic queries cannot substitute for them. nativeMaterialNumber is forwarded unchanged and must be qualified on the acquired build because the help does not establish its base. Native string name indicates success; null/empty is NotFound. Buffer sizes are E=2, Poisson=2, G=3, weight density=2, thermal expansion=2 and damping=2; each output must be finite and owned. Named first/second/third fields preserve native order because the help does not identify physical axis/plane ordering. Do not assume global X/Y or XY/YZ/ZX, derive G, or replace missing values with isotropic defaults.

## Native qualification still required

- Confirm actual dispatch, scalar/buffer marshaling and engineering values on each advertised STAAD build using disposable models.
- Confirm the assigned-member count return interpretation: the native purpose and main source use a count, but the help table says only zero OK.
- Establish physical interpretations of native Ix/Iz slots before using them as named torsional/principal properties.
- Confirm native material-number selectors for assignment-aware isotropic and 2D orthotropic queries; no undocumented base is inferred.
- Establish physical axis/plane order for orthotropic buffers; first/second/third values intentionally retain native order.
- Confirm acquired GetPlateSectionPropertyRefNo presence; missing-member observations are Unsupported without a fallback API.

The remaining Property work includes the pending P3 operations and complete typed/version-specific P4 section/thickness observations. The [25 compatibility ports](Property-Main-Ports.md) add selected main-project inventory/assignment capabilities but do not complete those typed workstreams. Reserved, private and deprecated entries remain excluded. The parked Load items require separate explicit user approval; this Property increment does not resume them.
