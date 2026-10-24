using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Common.Units;
using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Property;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Implements typed Property observations and main-project compatibility operations over the captured native receiver.</summary>
    /// <remarks>A single adapter shares binding checks and serialization across typed observations and compatibility ports. Assignment and material-context methods mutate native state; observation methods remain read-only.</remarks>
    public sealed class OSPropertyAdapter : OSBaseAdapter, IOSProperty
    {
        private readonly PropertyQueryReader queries;
        private readonly PropertyPortReader ports;
        private readonly PropertyManagementReader management;
        private readonly PropertyWorkflowReader workflows;
        private readonly PropertySectionReader sections;
        /// <summary>Captures the wrapper's Property interface without invoking Property domain functions.</summary>
        /// <param name="wrapper">Connected wrapper with a non-null RawProperty receiver.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawProperty binding is null.</exception>
        public OSPropertyAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawProperty ?? throw new ArgumentNullException(nameof(wrapper.RawProperty));
            queries = new PropertyQueryReader(wrapper, (object)ComObject);
            ports = new PropertyPortReader(queries);
            management = new PropertyManagementReader(queries, ports);
            workflows = new PropertyWorkflowReader(queries, management, ports);
            sections = new PropertySectionReader(wrapper, queries, ports);
        }
        /// <summary>Gets the captured native Property binding; direct calls bypass typed validation and coordination.</summary>
        /// <value>The acquired automation receiver; no readiness or compatibility is implied.</value>
        public dynamic ComObject { get; }

        /// <summary>Reads the native beta angle of one member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <returns>The signed native beta angle, or NotFound for native -3001. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use to observe orientation about the analytical member axis; offsets and releases do not represent beta rotation. Returns the finite signed native angle unchanged, including zero. Native -3001 is NotFound. No degrees/radians conversion or managed local-axis reconstruction is performed.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetBetaAngle(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<double> GetBetaAngle(int memberId) =>
            queries.GetBetaAngle(memberId);

        /// <summary>Counts element-offset specifications in the current model.</summary>
        /// <returns>The nonnegative native specification count; zero is a successful count. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: This counts native offset specifications, not plates, corners, nonzero vectors or assigned members. Zero is a valid count. Use GetPlateOffsetSpecification to inspect one plate corner and its stored convention. Undocumented negative counts are rejected.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetElementOffsetSpecificationCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<int> GetElementOffsetSpecificationCount() =>
            queries.GetElementOffsetSpecificationCount();

        /// <summary>Reads member-end release modes and applicable extended partial-moment outputs.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <param name="memberEnd">MemberEnd.Start (native 0) or MemberEnd.End (native 1).</param>
        /// <returns>A six-DOF release snapshot including applicable common and directional partial-moment factors, or NativeFailure. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Prefer this query when common MP or directional MPX/MPY/MPZ factors are needed. The basic GetMemberReleaseSpecification returns only six mode/parameter slots and cannot supply the separate extended factors. Native 1/true succeeds and 0/false fails. Modes 0/1/-1/-2/-3 are retained per DOF rather than converted to Boolean release flags. Unused spring/MP slots may remain unwritten and are represented as null; an applicable spring or factor must be written and finite. The native common MP factor is dimensionless; translational and rotational spring stiffness retain their different native dimensions.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive. memberEnd is not Start or End.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberReleaseSpecificationExtended(17, MemberEnd.End);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd) =>
            queries.GetMemberReleaseSpecificationExtended(memberId, memberEnd);

        /// <summary>Reads the six native release modes and applicable spring/partial-moment slots at a member end.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <param name="memberEnd">MemberEnd.Start (native 0) or MemberEnd.End (native 1).</param>
        /// <returns>A six-DOF release snapshot with applicable native parameter slots; separate extended factors remain unavailable. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for basic release/spring observations. Choose GetMemberReleaseSpecificationExtended for separate common MP and MPX/MPY/MPZ outputs; this basic query never invents zero for those missing outputs. Native 1/true succeeds and 0/false fails. FX/FY/FZ/MX/MY/MZ mode order and 0/1/-1/-2/-3 codes are preserved. Only applicable parameter slots are consumed; a spring or directional partial-factor slot must be finite. These are model specifications, not support reactions or member-end Output forces.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive. memberEnd is not Start or End.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberReleaseSpecification(17, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd) =>
            queries.GetMemberReleaseSpecification(memberId, memberEnd);

        /// <summary>Reads a member-end offset expressed in member-local coordinates.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <param name="memberEnd">MemberEnd.Start (native 0) or MemberEnd.End (native 1).</param>
        /// <returns>An independent member-local offset vector, or the documented absent, missing-entity or invalid-reference outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Choose this query when a local-coordinate offset is needed; GetMemberGlobalOffset asks STAAD for global components. The vector is an offset in native length units, not a position, displacement result or beta rotation. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector stays successful and is not replaced by a shared no-offset singleton. No coordinate conversion or sign change is applied.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive. memberEnd is not Start or End.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberLocalOffset(17, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd) =>
            queries.GetMemberLocalOffset(memberId, memberEnd);

        /// <summary>Reads a member-end offset expressed in global coordinates.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <param name="memberEnd">MemberEnd.Start (native 0) or MemberEnd.End (native 1).</param>
        /// <returns>An independent global offset vector, or the documented absent, missing-entity or invalid-reference outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Choose this query for global components; GetMemberLocalOffset requests member-local components. No managed rotation is substituted. Native 0 succeeds; -6027 is NoSpecification, -3001 NotFound and -6026 InvalidReference. A successful zero vector differs from an absent specification. The returned Vector3D is an independent value in native length units. These are modeling offsets, not Output displacements.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive. memberEnd is not Start or End.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberGlobalOffset(17, MemberEnd.End);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd) =>
            queries.GetMemberGlobalOffset(memberId, memberEnd);

        /// <summary>Reads the short native section-property packet assigned to a member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <returns>A section snapshot with width, depth, areas and native inertia slots; thickness fields are unavailable. Native zero reports NativeFailure. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for width, depth, axial/shear areas and the three named native inertia slots. GetMemberSectionPropertiesExtended additionally returns flange/web thickness; those fields are null here. This does not hydrate a section catalog or select a section shape. Native 1/true succeeds; 0/false cannot distinguish a missing member, unassigned property and general failure, so it returns NativeFailure without fabricated data. Native Ix/Iz descriptions conflict with conventional physical-axis labels; their slot names/order are preserved without assuming a torsion or principal-axis mapping. Length, area and inertia slots retain their respective native length, length-squared and length-to-the-fourth units.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberSectionProperties(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId) =>
            queries.GetMemberSectionProperties(memberId);

        /// <summary>Reads the native member-section packet including top-flange and web thickness.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <returns>A section snapshot with width, depth, areas, native inertia slots and flange/web thicknesses; native zero reports NativeFailure. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use instead of GetMemberSectionProperties when the two additional thickness outputs are required. No section shape, catalog identity, material or new property is inferred. Native 1/true succeeds; 0/false is an ambiguous failure and exposes no packet. All returned slots must be finite. Native Ix/Iz prose is inconsistent, so NativeIx/NativeIy/NativeIz retain native slot identity rather than a guessed torsional/principal interpretation. Values retain native length, area and fourth-power length units.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberSectionPropertiesExtended(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId) =>
            queries.GetMemberSectionPropertiesExtended(memberId);

        /// <summary>Reads material constants associated with a specific member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <returns>The five member-assigned material constants; the unreturned shear modulus remains unavailable. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use when the member is known but its material name is not. GetMaterialProperty instead selects a material by exact name; indexed isotropic queries select a material-list index and additionally return G. This native call returns E, Poisson ratio, weight density, thermal expansion and damping, not G or strength fields. ShearModulus is therefore null, never computed from E/Poisson. Native Boolean true succeeds; false does not distinguish a missing member from unavailable constants. WeightDensity is weight per volume, not mass density.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMemberMaterialConstants(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId) =>
            queries.GetMemberMaterialConstants(memberId);

        /// <summary>Reads a plate-corner offset in plate-local coordinates.</summary>
        /// <param name="plateId">Positive native plate ID, not a node or section-property ID.</param>
        /// <param name="cornerIndex">One-based native incidence position 1-4, not a node ID. The acquired model determines valid triangular-plate corners.</param>
        /// <returns>An independent plate-local offset vector, or the documented absent/missing-plate outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Choose this query for local components, GetPlateGlobalOffset for global components, or GetPlateOffsetSpecification for the stored selector and vector. cornerIndex is native incidence position 1/2/3/4, never a node ID; triangular-corner validity remains STAAD-controlled. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is retained. No incidence lookup, coordinate transform or unit conversion occurs.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">plateId is not positive. cornerIndex is outside 1 through 4.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetPlateLocalOffset(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex) =>
            queries.GetPlateLocalOffset(plateId, cornerIndex);

        /// <summary>Reads a plate-corner offset in global coordinates.</summary>
        /// <param name="plateId">Positive native plate ID, not a node or section-property ID.</param>
        /// <param name="cornerIndex">One-based native incidence position 1-4, not a node ID. The acquired model determines valid triangular-plate corners.</param>
        /// <returns>An independent global offset vector, or the documented absent/missing-plate outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for global components; GetPlateLocalOffset returns local components and GetPlateOffsetSpecification returns the stored convention. The native Global function is called despite its help summary mistakenly saying local. cornerIndex is a one-based incidence position 1-4, not a node number. Native 0 succeeds, -4001 is NotFound and -6027 NoSpecification. A successful zero vector is not absence; no conversion or sign change occurs.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">plateId is not positive. cornerIndex is outside 1 through 4.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetPlateGlobalOffset(17, 2);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex) =>
            queries.GetPlateGlobalOffset(plateId, cornerIndex);

        /// <summary>Reads a plate-corner offset in its stored native convention.</summary>
        /// <param name="plateId">Positive native plate ID, not a node or section-property ID.</param>
        /// <param name="cornerIndex">One-based native incidence position 1-4, not a node ID. The acquired model determines valid triangular-plate corners.</param>
        /// <returns>The stored coordinate convention and offset, or NoSpecification for native zero and NotFound for native -4001. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use when the original Local/Global/Z selector is needed; the local/global offset queries instead request components in a particular coordinate frame. Native 1 supplies an offset, native 0 explicitly means NoSpecification, and -4001 means NotFound. This differs from the local/global getters, where 0 is success. Stored selectors are Local=0, Global=1 and Z=2, matching the main ElementNodeOffsetAxis enum; they must not be cast from its opposite member OffsetAxis numbering. No physical meaning or conversion beyond the native Z selector is inferred. A code-1 zero vector remains valid data.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">plateId is not positive. cornerIndex is outside 1 through 4.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetPlateOffsetSpecification(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex) =>
            queries.GetPlateOffsetSpecification(plateId, cornerIndex);

        /// <summary>Reads the native section-property reference assigned to a plate.</summary>
        /// <param name="plateId">Positive native plate ID, not a node or section-property ID.</param>
        /// <returns>The positive assigned property reference, NotFound, NotAssigned, or Unsupported when the acquired receiver lacks this documented member. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use to identify an assigned property, not to read its thickness values or count property definitions. Positive return values are property IDs; -4001 is NotFound and -6022 NotAssigned. Zero is undocumented and rejected. The method is documented in all three reviewed help sets but absent from inspected 2025/2026 CLR interfaces: a missing acquired dispatch member returns Unsupported. No alternate function name, different entity type or fallback call is attempted; other transport errors propagate.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">plateId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed for a reason other than a missing member; the original error propagates.</exception>
        /// <example><code>var result = property.GetPlateSectionPropertyId(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId) =>
            queries.GetPlateSectionPropertyId(plateId);

        /// <summary>Counts members assigned to an exact isotropic material name.</summary>
        /// <param name="materialName">Exact nonblank native material name, preserved without trimming or case normalization; null and NUL-containing text are rejected.</param>
        /// <returns>The nonnegative assigned-member count, or NotFound for native -6023. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for sizing member assignments. GetIsotropicMaterialCount counts material definitions; GetIsotropicMaterialAssignedMemberIds returns the member IDs. A material name is not an index or section/property ID. Nonnegative values are treated as counts following the native purpose and read-only main-source usage; the help table incorrectly lists only 0 OK and its example passes a number despite the named input. Zero is a valid empty count; -6023 is NotFound. This count interpretation remains a live qualification requirement.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">materialName is null.</exception>
        /// <exception cref="ArgumentException">materialName is blank or contains a NUL character.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialAssignedMemberCount("STEEL");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName) =>
            queries.GetIsotropicMaterialAssignedMemberCount(materialName);

        /// <summary>Reads an owned bounded snapshot of members assigned to an isotropic material.</summary>
        /// <param name="materialName">Exact nonblank native material name, preserved without trimming or case normalization; null and NUL-containing text are rejected.</param>
        /// <param name="maximumResults">Positive retrieval/allocation limit, default 100000. No truncation or unbounded allocation occurs.</param>
        /// <returns>A materialized read-only member-ID sequence preserving native order and duplicates, or an absence, retrieval-limit or changed-count outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use when native assignment IDs are required without preloading Beam objects. The main source convenience filters a caller-supplied Beam collection; this query returns every native listed ID, preserving order and duplicates. It sizes with GetIsotropicMaterialAssignedBeamCount, validates positive Int32 IDs and exact array shape, and rechecks the count. Native list return 0 succeeds and -6023 is NotFound. Zero count returns an empty read-only snapshot without fetching; a larger count than maximumResults returns LimitExceeded before allocation. Count interpretation follows the native purpose/main usage despite the inconsistent help table. Same-count replacement or concurrent changes through other receivers cannot be proved absent.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">materialName is null.</exception>
        /// <exception cref="ArgumentException">materialName is blank or contains a NUL character.</exception>
        /// <exception cref="ArgumentOutOfRangeException">maximumResults is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialAssignedMemberIds("STEEL");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults = 100000) =>
            queries.GetIsotropicMaterialAssignedMemberIds(materialName, maximumResults);

        /// <summary>Reads basic material constants selected by exact name.</summary>
        /// <param name="materialName">Exact nonblank native material name, preserved without trimming or case normalization; null and NUL-containing text are rejected.</param>
        /// <returns>Five named material constants without an inferred shear modulus or strength packet, or NotFound. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use an exact material name obtained from an entity or native material observation. GetIsotropicMaterialProperty instead uses a zero-based list index and supplies native G; GetExtendedMaterialProperty also supplies strength fields. Native return 0 succeeds and -6023 is NotFound. These are different success semantics from the Boolean extended function. ShearModulus, Strengths and AssignmentState are null because this function does not return them. No default engineering values or computed G are substituted. Density remains weight per volume.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">materialName is null.</exception>
        /// <exception cref="ArgumentException">materialName is blank or contains a NUL character.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetMaterialProperty("STEEL");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName) =>
            queries.GetMaterialProperty(materialName);

        /// <summary>Reads the exact material name associated with a member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a collection index or physical-member ID.</param>
        /// <returns>The exact native member material name, or EmptyOrUnavailable when the response cannot distinguish absence from unavailable entity data. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for name discovery before a named material query. This returns a name, not constants, an index, a property ID or a new assignment. Native spelling/case/whitespace are preserved. Empty or native-null text is EmptyOrUnavailable because the help supplies no way to distinguish no assignment from a missing/unavailable member. Non-string responses are rejected; no guessed material name is returned.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetBeamMaterialName(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<string> GetBeamMaterialName(int memberId) =>
            queries.GetBeamMaterialName(memberId);

        /// <summary>Reads the exact material name associated with a plate element.</summary>
        /// <param name="plateId">Positive native plate ID, not a node or section-property ID.</param>
        /// <returns>The exact native plate material name, or EmptyOrUnavailable when the response cannot distinguish absence from unavailable entity data. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: This native element query takes a plate ID. Use GetBeamMaterialName for members; do not pass a node, solid ID or material-list index. It returns the exact name, not constants or thickness. Empty/native-null text is EmptyOrUnavailable because the contract does not distinguish unassigned and unavailable entities. No fallback to a similarly named plate/material API is attempted.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">plateId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetElementMaterialName(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<string> GetElementMaterialName(int plateId) =>
            queries.GetElementMaterialName(plateId);

        /// <summary>Counts isotropic material definitions in the current model.</summary>
        /// <returns>The nonnegative native isotropic-material count; zero is successful. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use to bound the zero-based materialIndex accepted by GetIsotropicMaterialProperty and GetExtendedIsotropicMaterialProperty. This is not a member/plate assignment count. Zero is a valid empty definition inventory; undocumented negative or nonintegral counts are rejected. A count is only an observation, not a stable reservation of material identities.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<int> GetIsotropicMaterialCount() =>
            queries.GetIsotropicMaterialCount();

        /// <summary>Reads isotropic constants and name by zero-based material-list index.</summary>
        /// <param name="materialIndex">Nonnegative zero-based native material-list index, checked against the observed isotropic count.</param>
        /// <returns>The indexed material name and constants including native shear modulus, or an absent/changed-count outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for material enumeration, unlike GetMaterialProperty which selects by name. materialIndex is zero-based, not a native property ID or member ID. This function returns G directly; that native value is retained instead of the main MaterialProperty computed E/(2*(1+Poisson)) substitute. The native return is the material name, not a success flag. Null/empty name or an index outside the observed count is NotFound. Before/after material counts detect observed changes but not same-count reorderings. Strengths and AssignmentState are null.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">materialIndex is negative.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialProperty(0);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex) =>
            queries.GetIsotropicMaterialProperty(materialIndex);

        /// <summary>Reads named material constants and extended strength fields.</summary>
        /// <param name="materialName">Exact nonblank native material name, preserved without trimming or case normalization; null and NUL-containing text are rejected.</param>
        /// <returns>Named material constants and all five strength values; the unreturned shear modulus remains unavailable. Native false reports NativeFailure. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Choose over GetMaterialProperty when Fy/Fu/Ry/Rt/Fcu are needed. GetExtendedIsotropicMaterialProperty selects a zero-based index and additionally returns native G; this name-based function omits G, so ShearModulus is null. Unlike basic named GetMaterialProperty, native Boolean true means success and false means failure. Strength stresses retain native force/area units; Ry/Rt are dimensionless. No material classification, design pass/fail, default strength or computed shear modulus is inferred.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">materialName is null.</exception>
        /// <exception cref="ArgumentException">materialName is blank or contains a NUL character.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetExtendedMaterialProperty("STEEL");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName) =>
            queries.GetExtendedMaterialProperty(materialName);

        /// <summary>Reads isotropic constants and extended strengths by zero-based material-list index.</summary>
        /// <param name="materialIndex">Nonnegative zero-based native material-list index, checked against the observed isotropic count.</param>
        /// <returns>The indexed material name, constants including native shear modulus and all five strength values, or an absent/changed-count outcome. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for enumeration with native G and Fy/Fu/Ry/Rt/Fcu. GetExtendedMaterialProperty selects by name and lacks G; GetIsotropicMaterialProperty omits strengths. The native return is the material name, not a Boolean or numeric status. Null/empty name or an out-of-range observed index is NotFound. Coupled counts are checked before/after; no atomic snapshot or stable same-count ordering is promised. All supplied constants and strength fields must be written and finite; optional AssignmentState remains null.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">materialIndex is negative.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetExtendedIsotropicMaterialProperty(0);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex) =>
            queries.GetExtendedIsotropicMaterialProperty(materialIndex);

        /// <summary>Reads native isotropic constants together with assignment state.</summary>
        /// <param name="nativeMaterialNumber">Nonnegative native material selector, passed unchanged. The reviewed contract does not establish its index base; no automatic rebasing is performed.</param>
        /// <returns>The returned material name and constants with explicit native assignment state 1=Unassigned or 2=Assigned, or NotFound. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use when the native assignment observation is required; the ordinary and extended indexed functions do not return it. nativeMaterialNumber is forwarded unchanged: this help says material number ID, unlike the explicitly zero-based indexed methods. Nonnegative input is accepted without guessed rebasing or a count-based ID/index conversion; qualify the selector on the acquired build. Native string name supplies success; null/empty means NotFound. Assignment output 1 means Unassigned and 2 Assigned, not Boolean 0/1; documented Double or integral forms are decoded exactly. Strengths remain null.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">nativeMaterialNumber is negative.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialPropertiesAssigned(2);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber) =>
            queries.GetIsotropicMaterialPropertiesAssigned(nativeMaterialNumber);

        /// <summary>Counts native 2D orthotropic material definitions.</summary>
        /// <returns>The nonnegative native two-dimensional orthotropic-material count; zero is successful. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for the 2D orthotropic inventory, not isotropic or 3D material counts and not element assignments. Zero is valid. The associated property getter documents a native material number rather than an explicit zero-based list index; this count does not establish a selector rebasing rule. Undocumented negative/nonintegral counts are rejected.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetOrthotropic2DMaterialCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<int> GetOrthotropic2DMaterialCount() =>
            queries.GetOrthotropic2DMaterialCount();

        /// <summary>Reads the native name and thirteen values of a 2D orthotropic material.</summary>
        /// <param name="nativeMaterialNumber">Nonnegative native material selector, passed unchanged. The reviewed contract does not establish its index base; no automatic rebasing is performed.</param>
        /// <returns>An immutable named material snapshot preserving all 13 returned scalar slots and their native ordering, or NotFound. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for independent directional material constants; isotropic queries cannot substitute for them. nativeMaterialNumber is forwarded unchanged and must be qualified on the acquired build because the help does not establish its base. Native string name indicates success; null/empty is NotFound. Buffer sizes are E=2, Poisson=2, G=3, weight density=2, thermal expansion=2 and damping=2; each output must be finite and owned. Named first/second/third fields preserve native order because the help does not identify physical axis/plane ordering. Do not assume global X/Y or XY/YZ/ZX, derive G, or replace missing values with isotropic defaults.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">nativeMaterialNumber is negative.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetOrthotropic2DMaterialProperties(2);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        public PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber) =>
            queries.GetOrthotropic2DMaterialProperties(nativeMaterialNumber);


        // Main-project signature-compatible Property ports.

        /// <summary>Counts distinct section property definitions with no assigned analytical members.</summary>
        /// <returns>The number of distinct section IDs whose native assigned-member count is exactly zero.</returns>
        /// <remarks><para>When to use: Choose this for an unused-definition count and GetUnusedSectionsIds when IDs are needed. Enumerates the section inventory and queries each assigned-member count; it is not the native total definition count. Negative counts throw instead of being classified as unused. No properties are deleted.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetUnusedSectionsCount());</code></example>
        public int GetUnusedSectionsCount() =>
            ports.GetUnusedSectionsCount();

        /// <summary>Finds section property definitions with no assigned analytical members.</summary>
        /// <returns>A new mutable set of positive native section IDs having exactly zero assigned members; an empty set is valid.</returns>
        /// <remarks><para>When to use: Use to inspect unused definitions before deciding whether to remove them. Enumerates the section inventory and queries each assigned-member count; duplicate property IDs collapse in the result. A native failure is never treated as an unused definition. Does not delete or assign properties.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetUnusedSectionsIds()) System.Console.WriteLine(id);</code></example>
        public HashSet<int> GetUnusedSectionsIds() =>
            ports.GetUnusedSectionsIds();

        /// <summary>Finds the first section property assigned to an analytical member.</summary>
        /// <param name="memberId">Positive analytical member number; not a property ID, list index or physical-member number.</param>
        /// <returns>The first matching positive property ID in native property-list order, or zero when no enumerated assignment contains the member.</returns>
        /// <remarks><para>When to use: Use to discover a section ID before GetSectionPropertyName or GetSectionPropertyValues. This managed search enumerates sections and their member assignments, matching the main-project implementation; it does not call the similarly named native dimensional GetBeamProperty API used by GetMemberSectionProperties. Zero does not distinguish an absent member from an unassigned member. Failures throw instead of returning zero.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>int propertyId = property.GetBeamProperty(17);
        /// System.Console.WriteLine(propertyId);</code></example>
        public int GetBeamProperty(int memberId) =>
            ports.GetBeamProperty(memberId);

        /// <summary>Reads the material name assigned to a plate using the native plate-specific query.</summary>
        /// <param name="pId">Positive analytical plate number; not a material index, property ID, node or solid number.</param>
        /// <returns>The exact native string, preserving case and whitespace; null or empty text is retained when returned by STAAD.</returns>
        /// <remarks><para>When to use: Use the main-project plate-specific compatibility method when the plate number is known. GetElementMaterialName is the existing status/data observation over a different native entry point; this port calls GetPlateMaterialName without alias fallback. Null or empty text cannot distinguish a missing plate from no material or an unavailable response. Does not retrieve constants or assign material.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetPlateMaterialName(17));</code></example>
        public string GetPlateMaterialName(int pId) =>
            ports.GetPlateMaterialName(pId);

        /// <summary>Lists the native section property reference IDs.</summary>
        /// <returns>A new owned array of positive IDs in native order, retaining duplicates; an empty array when the count is zero.</returns>
        /// <remarks><para>When to use: Use to enumerate section definitions, not assigned members or thickness properties. Sizes from GetSectionPropertyCount and validates GetSectionPropertyList success, array shape and IDs. A positive-count retrieval rechecks the count and rejects observed changes. Zero count skips fetching. Counts above 100000 are rejected before allocation.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetSectionPropertyList()) System.Console.WriteLine(id);</code></example>
        public int[] GetSectionPropertyList() =>
            ports.GetSectionPropertyList();

        /// <summary>Reads the native identification name of a section property.</summary>
        /// <param name="propId">Positive native section property reference ID, typically from GetSectionPropertyList; not a member number.</param>
        /// <returns>The explicitly written native name, preserving empty text, whitespace and case.</returns>
        /// <remarks><para>When to use: Use for a section label after locating its property ID. GetBeamProperty first resolves a member to a property ID. The native GetSectionPropertyName code must be zero; general error -1, no-definition -6025 and any other nonzero code throw. Null, non-string and unwritten string outputs throw. No section catalog or material identity is inferred.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetSectionPropertyName(37));</code></example>
        public string GetSectionPropertyName(int propId) =>
            ports.GetSectionPropertyName(propId);

        /// <summary>Reads the main-project raw, zero-padded native section-definition value buffer.</summary>
        /// <param name="propId">Positive native section property reference ID; not an analytical member number.</param>
        /// <returns>A new owned array sized by GetCountofSectionPropertyValuesEx, in native section-specific order and units, including zero padding.</returns>
        /// <remarks><para>When to use: Use only when the native section type and acquired STAAD version are already known. This compatibility port calls GetSectionPropertyValuesEx, not native GetSectionPropertyValues or the member-dimensional queries. Preserves the main-project zero-initialized full buffer and discards the returned section type after validating it. Unused padding and genuinely zero fields cannot be distinguished from unwritten zero slots. Layouts differ by section kind and some change between 2024 and 2025/2026; no universal Ax/inertia index, section parser, unit conversion or inferred physical axis is supplied. The native call must succeed; capacity must be 1-100000 and remain unchanged. Nonfinite or malformed outputs throw.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>double[] values = property.GetSectionPropertyValues(37);
        /// System.Console.WriteLine(values.Length);</code></example>
        public double[] GetSectionPropertyValues(int propId) =>
            ports.GetSectionPropertyValues(propId);

        /// <summary>Filters supplied beam objects by one section property assignment.</summary>
        /// <param name="allBeams">Non-null candidate set containing non-null Beam objects with positive analytical IDs. Objects are not cloned or hydrated.</param>
        /// <param name="propId">Positive section property reference ID; not a member number or profile type code.</param>
        /// <returns>A new mutable set containing the original candidate objects whose IDs appear in the native assignment list.</returns>
        /// <remarks><para>When to use: Use when beam objects are already available and only their assignment subset is needed. Unlisted candidates are excluded and native IDs absent from allBeams are not synthesized. Sizes the native list with GetSectionPropertyAssignedBeamCount and rechecks the count; rejects invalid IDs and failures. Vendor section-assignment return tables are inconsistent: counts follow the native purpose/main usage, and a nonnegative integral list return or Boolean true is accepted with a fully validated buffer. Negative/false responses fail. This interpretation requires qualification on the acquired build.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentNullException">A required candidate collection or property-ID sequence is null.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>var beams = new System.Collections.Generic.HashSet&lt;Beam&gt; { new Beam { Id = 17 } };
        /// System.Console.WriteLine(property.GetSectionPropertyAssignedBeams(beams, 37).Count);</code></example>
        public HashSet<Beam> GetSectionPropertyAssignedBeams(HashSet<Beam> allBeams, int propId) =>
            ports.GetSectionPropertyAssignedBeams(allBeams, propId);

        /// <summary>Lists native plate thickness property reference IDs.</summary>
        /// <returns>A new owned array of positive thickness IDs in native order, retaining duplicates; an empty array for zero definitions.</returns>
        /// <remarks><para>When to use: Choose this inventory for thickness definitions; GetSectionPropertyList enumerates member sections. Sizes from GetThicknessPropertyCount, requires successful GetThicknessPropertyList, validates shape/IDs and rechecks positive counts. Counts above 100000 fail before allocation and zero skips fetching. Does not return thickness magnitudes or assigned plate IDs.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetThicknessPropertyList()) System.Console.WriteLine(id);</code></example>
        public int[] GetThicknessPropertyList() =>
            ports.GetThicknessPropertyList();

        /// <summary>Filters supplied plate objects by one thickness property assignment.</summary>
        /// <param name="allPlates">Non-null candidate set of non-null plates with positive analytical IDs. Returned objects are these original instances.</param>
        /// <param name="propId">Positive native thickness property reference ID; not a plate number or member section ID.</param>
        /// <returns>A new mutable set of original candidate plates listed by the native assignment query.</returns>
        /// <remarks><para>When to use: Use for assignment membership when plate objects are already available; GetAveragePlateThickness reads a magnitude instead. Sizes with GetThicknessPropertyAssignedPlateCount, requires successful GetThicknessPropertyAssignedPlateList, validates IDs and rechecks the count. Native IDs absent from the candidates are not constructed. Zero count produces an empty set without a list call. No managed plate properties are changed.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentNullException">A required candidate collection or property-ID sequence is null.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>var plates = new System.Collections.Generic.HashSet&lt;Plate&gt; { new Plate { Id = 17 } };
        /// System.Console.WriteLine(property.GetThicknessPropertyAssignedPlates(plates, 45).Count);</code></example>
        public HashSet<Plate> GetThicknessPropertyAssignedPlates(HashSet<Plate> allPlates, int propId) =>
            ports.GetThicknessPropertyAssignedPlates(allPlates, propId);

        /// <summary>Finds supplied beams absent from the assignments of the supplied property IDs.</summary>
        /// <param name="allBeams">Non-null candidate set of non-null Beam objects with positive analytical IDs; original instances are retained.</param>
        /// <param name="propIds">Non-null sequence of positive section property reference IDs. Enumerated once and validated completely before native access.</param>
        /// <returns>A new mutable set of the candidate objects not listed by any supplied property; an empty property sequence returns all candidates.</returns>
        /// <remarks><para>When to use: Use as a missing-assignment check only when propIds includes every relevant section definition. With a subset, the result means unassigned to that subset and can include entities assigned elsewhere. Combines the same checked native count/list queries as GetSectionPropertyAssignedBeams; each list is bounded and count-rechecked. Does not validate member/plate existence, change objects, or assume a global unassigned status. Input collections are never modified.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentNullException">A required candidate collection or property-ID sequence is null.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>var candidates = new System.Collections.Generic.HashSet&lt;Beam&gt; { new Beam { Id = 17 } };
        /// System.Console.WriteLine(property.GetBeamsWithMissingSectionProperty(candidates, new[] { 37 }).Count);</code></example>
        public HashSet<Beam> GetBeamsWithMissingSectionProperty(HashSet<Beam> allBeams, IEnumerable<int> propIds) =>
            ports.GetBeamsWithMissingSectionProperty(allBeams, propIds);

        /// <summary>Finds supplied plates absent from the assignments of the supplied property IDs.</summary>
        /// <param name="allPlates">Non-null candidate set of non-null Plate objects with positive analytical IDs; original instances are retained.</param>
        /// <param name="propIds">Non-null sequence of positive thickness property reference IDs. Enumerated once and validated completely before native access.</param>
        /// <returns>A new mutable set of the candidate objects not listed by any supplied property; an empty property sequence returns all candidates.</returns>
        /// <remarks><para>When to use: Use as a missing-assignment check only when propIds includes every relevant thickness definition. With a subset, the result means unassigned to that subset and can include entities assigned elsewhere. Combines the same checked native count/list queries as GetThicknessPropertyAssignedPlates; each list is bounded and count-rechecked. Does not validate member/plate existence, change objects, or assume a global unassigned status. Input collections are never modified.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentNullException">A required candidate collection or property-ID sequence is null.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>var candidates = new System.Collections.Generic.HashSet&lt;Plate&gt; { new Plate { Id = 17 } };
        /// System.Console.WriteLine(property.GetPlatesWithMissingThicknessProperty(candidates, new[] { 45 }).Count);</code></example>
        public HashSet<Plate> GetPlatesWithMissingThicknessProperty(HashSet<Plate> allPlates, IEnumerable<int> propIds) =>
            ports.GetPlatesWithMissingThicknessProperty(allPlates, propIds);

        /// <summary>Computes the rounded arithmetic mean of four native plate-corner thickness slots.</summary>
        /// <param name="plateId">Positive analytical plate number, not a thickness property ID.</param>
        /// <returns>The sum of four native thickness values divided by four, rounded to five decimal places with midpoint-to-even rounding, in unchanged native length units.</returns>
        /// <remarks><para>When to use: Use for the main-project four-slot arithmetic average, not an area-weighted or integration-point thickness. Calls GetPlateThickness once and requires native code zero and four explicitly written finite values. No interpolation, triangular-corner substitution, sign adjustment or unit conversion is performed; an unwritten fourth slot throws. Native missing-property and invalid-plate errors throw rather than yielding a zero thickness. The intermediate sum must remain finite.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetAveragePlateThickness(17));</code></example>
        public double GetAveragePlateThickness(int plateId) =>
            ports.GetAveragePlateThickness(plateId);

        /// <summary>Reads the native axial behavior specification of an analytical member.</summary>
        /// <param name="msId">Positive analytical member number forwarded to GetMemberSpecCode. The preserved main-project parameter name does not mean a specification record ID.</param>
        /// <returns>The defined native specification: Other=-1, Truss=0, TensionOnly=1, CompressionOnly=2, CableOnly=3 or Joist=4.</returns>
        /// <remarks><para>When to use: Use for member behavior, not section, material, release or offset classification. Requires Boolean/integral native success and an explicitly written known code. Unknown or unwritten codes throw rather than being cast silently. No member attributes, model state, units or results are changed.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetMemberSpecification(17));</code></example>
        public MemberSpecificationType GetMemberSpecification(int msId) =>
            ports.GetMemberSpecification(msId);

        /// <summary>Assigns an existing section property to one analytical member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a property reference ID.</param>
        /// <param name="propertyId">Positive existing section property reference ID; the method does not create a section.</param>
        /// <remarks><para>When to use: Use to assign an existing property; section inventory/read methods do not mutate assignments. Calls native AssignBeamProperty once with the scalar or copied array. Native zero is success and any nonzero code throws, including invalid member -3006, invalid property -6001 and assignment error -6002. This changes the live model without saving or rollback; errors may follow partial changes. No managed Beam object is updated.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.AssignBeamProperty(17, 37);</code></example>
        public void AssignBeamProperty(int memberId, int propertyId) =>
            ports.AssignBeamProperty(memberId, propertyId);

        /// <summary>Assigns an existing section property to a batch of analytical members.</summary>
        /// <param name="memberIds">Analytical member IDs. Null or empty arrays are a no-op; otherwise copied and all entries must be positive. Order and duplicates are forwarded.</param>
        /// <param name="propertyId">Positive existing section property reference ID; the method does not create a section.</param>
        /// <remarks><para>When to use: Use to assign an existing property; section inventory/read methods do not mutate assignments. Calls native AssignBeamProperty once with the scalar or copied array. Native zero is success and any nonzero code throws, including invalid member -3006, invalid property -6001 and assignment error -6002. This changes the live model without saving or rollback; errors may follow partial changes. Null/empty arrays return before binding or property-ID validation.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.AssignBeamProperty(new[] { 17, 41 }, 37);</code></example>
        public void AssignBeamProperty(int[] memberIds, int propertyId) =>
            ports.AssignBeamProperty(memberIds, propertyId);

        /// <summary>Assigns an existing named material to one analytical member.</summary>
        /// <param name="materialName">Exact native material name; case and surrounding whitespace are preserved. Null/empty/whitespace-only names are a no-op. Nonblank names must not contain NUL.</param>
        /// <param name="memberId">Positive analytical member number, validated only for a nonblank material name.</param>
        /// <remarks><para>When to use: Use to change the actual member material assignment. SetMaterialName changes only the Property creation context and cannot substitute for assignment. Calls native AssignMaterialToMember once with the exact name and scalar or copied array; false/zero fails with InvalidOperationException. No trimming, material creation, conversion, save, rollback or managed entity update occurs. A failure can have partial native effects. No-op inputs return before binding/native access.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.AssignMaterialToMember(&quot;STEEL&quot;, 17);</code></example>
        public void AssignMaterialToMember(string materialName, int memberId) =>
            ports.AssignMaterialToMember(materialName, memberId);

        /// <summary>Assigns an existing named material to a batch of analytical members.</summary>
        /// <param name="materialName">Exact native material name; case and surrounding whitespace are preserved. Null/empty/whitespace-only names are a no-op. Nonblank names must not contain NUL.</param>
        /// <param name="memberIds">Member IDs copied before dispatch; positive entries required. Null or empty arrays are a no-op.</param>
        /// <remarks><para>When to use: Use to change the actual member material assignment. SetMaterialName changes only the Property creation context and cannot substitute for assignment. Calls native AssignMaterialToMember once with the exact name and scalar or copied array; false/zero fails with InvalidOperationException. No trimming, material creation, conversion, save, rollback or managed entity update occurs. A failure can have partial native effects. No-op inputs return before binding/native access.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.AssignMaterialToMember(&quot;STEEL&quot;, new[] { 17, 41 });</code></example>
        public void AssignMaterialToMember(string materialName, int[] memberIds) =>
            ports.AssignMaterialToMember(materialName, memberIds);

        /// <summary>Sets the native Property material context for subsequent creation operations.</summary>
        /// <param name="materialName">Exact existing native material name, preserving case and whitespace. Null/empty/whitespace-only names are a no-op; nonblank names must not contain NUL.</param>
        /// <remarks><para>When to use: Use before a native property creation API that consumes the current material context. AssignMaterialToMember instead changes member assignments. Calls SetMaterialName once and requires native Boolean/integral success; the documented integer success code is one. Does not create material, assign members, restore the previous context or save the model. The context is shared native state: serialization covers this call, not a later separate creation transaction. No-op names return before binding checks.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.SetMaterialName(&quot;STEEL&quot;);</code></example>
        public void SetMaterialName(string materialName) =>
            ports.SetMaterialName(materialName);

        /// <summary>Creates and assigns global-Y offsets at both analytical member ends.</summary>
        /// <param name="memberId">Positive analytical member number, not a specification ID.</param>
        /// <param name="startY">Finite signed global-Y start offset in the current native length units. Zero is a valid explicit offset.</param>
        /// <param name="endY">Finite signed global-Y end offset in the same native length units. Zero is valid.</param>
        /// <returns>True only when both positive specification IDs are created and both native assignment calls return zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Use to create and assign both end specifications; AssignMemberEndOffset changes one end. Creates the start then end specification with native global selector zero and X=Z=0, then assigns start followed by end. This mutates the live model; failure can leave orphan definitions or partial assignments. No rollback, cleanup, save or managed entity update occurs. Validation, stale/disposed bindings and malformed nonintegral native responses throw; the Boolean result does not certify physical placement or engineering suitability.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffsets(17, 0.15, -0.20));</code></example>
        public bool AssignMemberEndOffsets(int memberId, double startY, double endY) =>
            ports.AssignMemberEndOffsets(memberId, startY, endY);

        /// <summary>Creates and assigns global-Y offsets at both analytical member ends.</summary>
        /// <param name="memberIds">Positive analytical member IDs, copied before dispatch; null/empty arrays return false without native calls.</param>
        /// <param name="startY">Finite signed global-Y start offset in the current native length units. Zero is a valid explicit offset.</param>
        /// <param name="endY">Finite signed global-Y end offset in the same native length units. Zero is valid.</param>
        /// <returns>True only when both positive specification IDs are created and both native assignment calls return zero; false for native error codes, COM/binding failures or null/empty member arrays.</returns>
        /// <remarks><para>When to use: Use to create and assign both end specifications; AssignMemberEndOffset changes one end. Creates the start then end specification with native global selector zero and X=Z=0, then assigns start followed by end. Each specification is created once for the whole copied member array, not once per member. This mutates the live model; failure can leave orphan definitions or partial assignments. No rollback, cleanup, save or managed entity update occurs. Validation, stale/disposed bindings and malformed nonintegral native responses throw; the Boolean result does not certify physical placement or engineering suitability.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffsets(new[] { 17, 41 }, 0.15, -0.20));</code></example>
        public bool AssignMemberEndOffsets(int[] memberIds, double startY, double endY) =>
            ports.AssignMemberEndOffsets(memberIds, startY, endY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberId">Positive analytical member number.</param>
        /// <param name="location">Native member-end selector: zero for start, one for end. Other values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(17, 1, -0.20));</code></example>
        public bool AssignMemberEndOffset(int memberId, int location, double offsetY) =>
            ports.AssignMemberEndOffset(memberId, location, offsetY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberId">Positive analytical member number.</param>
        /// <param name="location">OffsetLocation.Start (native zero) or OffsetLocation.End (native one); undefined enum values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. The typed overload forwards the exact enum code to the integer overload. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(17, OffsetLocation.End, -0.20));</code></example>
        public bool AssignMemberEndOffset(int memberId, OffsetLocation location, double offsetY) =>
            ports.AssignMemberEndOffset(memberId, (int)location, offsetY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberIds">Positive analytical member IDs copied before dispatch. Null or empty arrays return false without native calls.</param>
        /// <param name="location">Native member-end selector: zero for start, one for end. Other values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures or null/empty arrays.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(new[] { 17, 41 }, 1, -0.20));</code></example>
        public bool AssignMemberEndOffset(int[] memberIds, int location, double offsetY) =>
            ports.AssignMemberEndOffset(memberIds, location, offsetY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberIds">Positive analytical member IDs copied before dispatch. Null or empty arrays return false without native calls.</param>
        /// <param name="location">OffsetLocation.Start (native zero) or OffsetLocation.End (native one); undefined enum values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures or null/empty arrays.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. The typed overload forwards the exact enum code to the integer overload. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(new[] { 17, 41 }, OffsetLocation.End, -0.20));</code></example>
        public bool AssignMemberEndOffset(int[] memberIds, OffsetLocation location, double offsetY) =>
            ports.AssignMemberEndOffset(memberIds, (int)location, offsetY);

        #region Property management ports

        /// <summary>Deletes a native material definition by its exact name.</summary>
        /// <param name="mName">Nonblank existing native material name; case and whitespace are passed unchanged.</param>
        /// <returns>The native success flag: true for deletion and false for native refusal or failure; no cause is inferred from false.</returns>
        /// <remarks><para>When to use: Use only when a material should be removed from the model. This mutates native state. Unlike material queries this does not retrieve or reassign entities, and it does not save. Boolean or documented integral 0/1 evidence is decoded without treating arbitrary nonzero codes as success.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.DeleteMaterial(&quot;UNUSED_STEEL&quot;));
        /// </code></example>
        public bool DeleteMaterial(string mName) => management.DeleteMaterial(mName);

        /// <summary>Creates a beam section definition from an installed native section table.</summary>
        /// <param name="country">Named table selector with the preserved main-project integer code; database availability is native-controlled.</param>
        /// <param name="sectionName">Exact nonblank native database section designation.</param>
        /// <param name="typeSpec">Preserved native section modifier. The reviewed function documents ST, D and T; other named codes require acquired-build support.</param>
        /// <param name="additionalSpec1">Finite extra specification in the native creation length convention; clear spacing for a double profile, otherwise as defined by STAAD.</param>
        /// <param name="additionalSpec2">Finite second extra specification; the reviewed contract requests 0.0.</param>
        /// <returns>The native signed integral result unchanged. Only a positive result is a created property ID; zero is a documented creation failure. Never assign a nonpositive result.</returns>
        /// <remarks><para>When to use: Use for a table-defined section and call SetMaterialName with the intended existing material immediately beforehand. Does not choose or create material, assign the section, convert dimensions or save. The enum is a catalog of codes, not a per-build capability check. Prefer the composite helper for TC/BC/TB cover plates.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// int id = property.CreateBeamPropertyFromTable(Country.Indian, &quot;ISMB300&quot;, TypeSpecification.ST, 0.0, 0.0);
        /// System.Console.WriteLine(id);
        /// </code></example>
        public int CreateBeamPropertyFromTable(Country country, string sectionName, TypeSpecification typeSpec, double additionalSpec1, double additionalSpec2) => management.CreateBeamPropertyFromTable(country, sectionName, typeSpec, additionalSpec1, additionalSpec2);

        /// <summary>Creates a rolled-table section with top, bottom or both cover plates.</summary>
        /// <param name="country">Preserved native table country selector.</param>
        /// <param name="baseStaadName">Exact nonblank rolled base section name in that table.</param>
        /// <param name="typeSpecification">TC for top, BC for bottom, or TB for both; all other values are rejected.</param>
        /// <param name="plateValuesMetres">Owned-copy input: exactly two positive finite values [width, thickness] for TC/BC, or four [top width, top thickness, bottom width, bottom thickness] for TB. The source parameter name is retained; values pass unchanged and must match the acquired creation-unit convention.</param>
        /// <returns>The signed native result unchanged. Positive values are created property IDs; 0, -6004, -6005 and -6006 represent documented creation/database/type failures.</returns>
        /// <remarks><para>When to use: Use after SetMaterialName for an existing intended material. Calls CreateBeamPropertyFromTableWithCoverPlates with the supplied TC/BC/TB code; BC is never substituted with TC. The wrapper does not set units, infer plate geometry, assign members or save. Do not infer the native unit convention solely from the Metres parameter suffix.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// int id = property.CreateCompositeBeamPropertyFromTable(Country.Indian, &quot;ISMB300&quot;, TypeSpecification.BC, new[] { 0.2, 0.01 });
        /// System.Console.WriteLine(id);
        /// </code></example>
        public int CreateCompositeBeamPropertyFromTable(Country country, string baseStaadName, TypeSpecification typeSpecification, double[] plateValuesMetres) => management.CreateCompositeBeamPropertyFromTable(country, baseStaadName, typeSpecification, plateValuesMetres);

        /// <summary>Creates a fabricated or tapered I-section from seven explicit plate dimensions.</summary>
        /// <param name="designation">Source-compatible display designation parameter. The main tapered-I path derives its designation; this argument is not sent to STAAD or used to rename the definition.</param>
        /// <param name="propertyValuesMetres">Exactly seven finite positive values: start depth, web thickness, end depth, top flange width, top flange thickness, bottom flange width, bottom flange thickness. Copied and passed unchanged; verify the acquired native creation units despite the retained Metres suffix.</param>
        /// <returns>The native signed integral result unchanged: a positive property ID, zero for creation failure, or a native error such as -106/-108. Nonpositive results must not be assigned.</returns>
        /// <remarks><para>When to use: Use when no rolled table lookup is needed. Dispatches the same CreateTaperedIProperty route as the main TaperISection without importing its private catalog graph. Equal start/end depths describe a prismatic fabricated I-section. Set the intended existing material beforehand. No engineering adequacy check, unit conversion, member assignment or save is performed.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// System.Console.WriteLine(property.CreateBuiltUpBeamProperty(&quot;Fabricated I&quot;, new[] { 0.6, 0.01, 0.6, 0.25, 0.02, 0.25, 0.02 }));
        /// </code></example>
        public int CreateBuiltUpBeamProperty(string designation, double[] propertyValuesMetres) => management.CreateBuiltUpBeamProperty(designation, propertyValuesMetres);

        /// <summary>Assigns each supplied beam its managed beta angle in degrees.</summary>
        /// <param name="beams">Non-null set of non-null beams with positive analytical IDs and finite BetaAngle values in degrees. The set and angles are snapshotted before dispatch.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use to write Beam.BetaAngle values into STAAD; PopulateBetaAngles reads in the opposite direction. Calls AssignBetaAngle once per beam and requires native true/1; false/0 fails. Empty sets are valid no-ops. Stops at the first failure and does not roll back earlier assignments. The degree argument is retained without copying the unsafe worker-thread COM dispatch of the reference implementation.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var beams = new System.Collections.Generic.HashSet&lt;Beam&gt; { new Beam { Id = 17, BetaAngle = 25.0 } };
        /// property.AssignBetaAnglesToBeams(beams);
        /// </code></example>
        public IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams) { management.AssignBetaAnglesToBeams(beams, 1); return this; }

        /// <summary>Assigns each supplied beam its managed beta angle in degrees.</summary>
        /// <param name="beams">Non-null set of non-null beams with positive analytical IDs and finite BetaAngle values in degrees. The set and angles are snapshotted before dispatch.</param>
        /// <param name="nThreads">Source-compatible degree argument, 1 through 512. Native calls remain synchronous and serialized on the caller COM apartment; no worker COM calls are created.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use to write Beam.BetaAngle values into STAAD; PopulateBetaAngles reads in the opposite direction. Calls AssignBetaAngle once per beam and requires native true/1; false/0 fails. Empty sets are valid no-ops. Stops at the first failure and does not roll back earlier assignments. The degree argument is retained without copying the unsafe worker-thread COM dispatch of the reference implementation.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var beams = new System.Collections.Generic.HashSet&lt;Beam&gt; { new Beam { Id = 17, BetaAngle = 25.0 } };
        /// property.AssignBetaAnglesToBeams(beams, 2);
        /// </code></example>
        public IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams, int nThreads) { management.AssignBetaAnglesToBeams(beams, nThreads); return this; }

        /// <summary>Reads native beta angles into the supplied managed beam objects.</summary>
        /// <param name="forBeams">Non-null finite sequence of non-null positive-ID beams. Equal beams are queried once; original instances are retained.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before inspecting Beam.BetaAngle. Stages all successful finite observations before updating the supplied objects, so a native read failure leaves their angles unchanged. Changes managed beam angles/local-axis caches only; no STAAD assignments, save or analysis. Native -3001 is a failure to populate, not an angle.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var beams = new[] { new Beam { Id = 17 } };
        /// property.PopulateBetaAngles(beams);
        /// System.Console.WriteLine(beams[0].BetaAngle);
        /// </code></example>
        public IOSProperty PopulateBetaAngles(IEnumerable<Beam> forBeams) { management.PopulateBetaAngles(forBeams); return this; }

        /// <summary>Finds supplied or hydrated beams assigned to one isotropic material.</summary>
        /// <param name="mName">Exact nonblank material name; passed unchanged, not a list index.</param>
        /// <param name="allBeams">Non-null sequence of non-null positive-ID candidates. Results reference the first supplied object matching each native ID.</param>
        /// <returns>A new mutable set of matching Beam references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.</returns>
        /// <remarks><para>When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The member count follows native purpose and main usage despite an inconsistent help return table; the member list requires integral zero success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetIsotropicMaterialAssignedBeams(&quot;STEEL&quot;, new[] { new Beam { Id = 17 } }).Count);
        /// </code></example>
        public HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IEnumerable<Beam> allBeams) => management.GetIsotropicMaterialAssignedBeams(mName, allBeams);

        /// <summary>Finds supplied or hydrated beams assigned to one isotropic material.</summary>
        /// <param name="mName">Exact nonblank material name; passed unchanged, not a list index.</param>
        /// <param name="geometry">Non-null Geometry adapter for the same model/binding. Retrieves all candidate entities with GetAllEntities&lt;T&gt;(1); node/incidence hydration may be expensive.</param>
        /// <returns>A new mutable set of matching Beam references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.</returns>
        /// <remarks><para>When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The member count follows native purpose and main usage despite an inconsistent help return table; the member list requires integral zero success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetIsotropicMaterialAssignedBeams(&quot;STEEL&quot;, wrapper.Geometry).Count);
        /// </code></example>
        public HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IOSGeometry geometry) => management.GetIsotropicMaterialAssignedBeams(mName, geometry);

        /// <summary>Finds supplied or hydrated plates assigned to one isotropic material.</summary>
        /// <param name="mName">Exact nonblank material name; passed unchanged, not a list index.</param>
        /// <param name="allPlates">Non-null sequence of non-null positive-ID candidates. Results reference the first supplied object matching each native ID.</param>
        /// <returns>A new mutable set of matching Plate references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.</returns>
        /// <remarks><para>When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The plate count must be nonnegative; its list requires true/1 success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetIsotropicMaterialAssignedPlates(&quot;STEEL&quot;, new[] { new Plate { Id = 17 } }).Count);
        /// </code></example>
        public HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IEnumerable<Plate> allPlates) => management.GetIsotropicMaterialAssignedPlates(mName, allPlates);

        /// <summary>Finds supplied or hydrated plates assigned to one isotropic material.</summary>
        /// <param name="mName">Exact nonblank material name; passed unchanged, not a list index.</param>
        /// <param name="geometry">Non-null Geometry adapter for the same model/binding. Retrieves all candidate entities with GetAllEntities&lt;T&gt;(1); node/incidence hydration may be expensive.</param>
        /// <returns>A new mutable set of matching Plate references; empty when native assignment count is zero. Native IDs absent from the candidates are not fabricated.</returns>
        /// <remarks><para>When to use: Use the candidate overload when geometry is already loaded, or the Geometry overload for full hydration. Bounds native counts to 100000, validates positive Int32 IDs and exact array shape, then rechecks positive counts. Zero skips fetching. The plate count must be nonnegative; its list requires true/1 success. No entity assignments or native edits occur. Geometry must refer to the same model; the wrapper cannot prove the provenance of arbitrary candidates. Same-count external changes remain possible.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetIsotropicMaterialAssignedPlates(&quot;STEEL&quot;, wrapper.Geometry).Count);
        /// </code></example>
        public HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IOSGeometry geometry) => management.GetIsotropicMaterialAssignedPlates(mName, geometry);

        /// <summary>Creates native plate thickness definitions and stores their IDs in the supplied packets.</summary>
        /// <param name="pet">Non-null thickness packet. A/B/C must be finite positive lengths; D may be zero for the native triangular fourth slot. No corner substitution occurs.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before AssignPlateElementThickness. Copies A/B/C/D in incidence order into CreatePlateThicknessProperty and stores only a positive native ID. Zero or a negative creation error throws without replacing that packet&#x27;s prior ID. Repeated calls create definitions again. Earlier successful creations survive a later failure; no rollback, assignment, save or unit conversion occurs.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = new PlateElementThickness(0.2);
        /// property.CreatePlateElementThickness(pet);
        /// System.Console.WriteLine(pet.Id);
        /// </code></example>
        public IOSProperty CreatePlateElementThickness(PlateElementThickness pet) { management.CreatePlateElementThickness(pet); return this; }

        /// <summary>Creates native plate thickness definitions and stores their IDs in the supplied packets.</summary>
        /// <param name="petSet">Non-null, nonempty set without null entries. All packets are validated before the first native call.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before AssignPlateElementThickness. Copies A/B/C/D in incidence order into CreatePlateThicknessProperty and stores only a positive native ID. Zero or a negative creation error throws without replacing that packet&#x27;s prior ID. Repeated calls create definitions again. Earlier successful creations survive a later failure; no rollback, assignment, save or unit conversion occurs.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = new PlateElementThickness(0.2);
        /// property.CreatePlateElementThicknesses(new System.Collections.Generic.HashSet&lt;PlateElementThickness&gt; { pet });
        /// System.Console.WriteLine(pet.Id);
        /// </code></example>
        public IOSProperty CreatePlateElementThicknesses(HashSet<PlateElementThickness> petSet) { management.CreatePlateElementThicknesses(petSet); return this; }

        /// <summary>Assigns existing native thickness definitions to their candidate plate sets.</summary>
        /// <param name="pet">Null or no AssignedPlates is a no-op. Otherwise Id must identify an existing positive native thickness definition and candidates must be non-null positive-ID plates.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use after creating or resolving thickness IDs. Snapshots assignment IDs before dispatch, calls AssignPlateThickness and requires integral zero success. Native -4008 can report partial assignment and is surfaced as an exception; already applied changes are not undone. Does not create definitions, alter managed geometry, save or retry.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = new PlateElementThickness(0.2) { Id = 45 };
        /// pet.AssignedPlates.Add(new Plate { Id = 17 });
        /// property.AssignPlateElementThickness(pet);
        /// </code></example>
        public IOSProperty AssignPlateElementThickness(PlateElementThickness pet) { management.AssignPlateElementThickness(pet); return this; }

        /// <summary>Assigns existing native thickness definitions to their candidate plate sets.</summary>
        /// <param name="petSet">Null or empty is a no-op. Null entries and packets with no assignments are skipped; nonempty assignments require positive packet and plate IDs.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use after creating or resolving thickness IDs. Snapshots assignment IDs before dispatch, calls AssignPlateThickness and requires integral zero success. Native -4008 can report partial assignment and is surfaced as an exception; already applied changes are not undone. Does not create definitions, alter managed geometry, save or retry.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = new PlateElementThickness(0.2) { Id = 45 };
        /// pet.AssignedPlates.Add(new Plate { Id = 17 });
        /// property.AssignPlateElementThicknesses(new System.Collections.Generic.HashSet&lt;PlateElementThickness&gt; { pet });
        /// </code></example>
        public IOSProperty AssignPlateElementThicknesses(HashSet<PlateElementThickness> petSet) { management.AssignPlateElementThicknesses(petSet); return this; }

        /// <summary>Reads four native thickness slots by analytical plate ID.</summary>
        /// <param name="plateId">Positive analytical plate ID; not a thickness definition ID.</param>
        /// <returns>A new independent packet with four finite nonnegative native length values, no assigned plates and Id zero because the query does not resolve a definition ID.</returns>
        /// <remarks><para>When to use: Use the property-ID method for definitions or the plate-ID method for effective plate values. GetPlateThickness must return integral zero success. All four output slots must be written and finite. No uniformity assumption, triangular fourth-slot replacement, averaging, conversion or assignment occurs.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = property.GetPlateThicknessByPlateId(17);
        /// System.Console.WriteLine(pet.D);
        /// </code></example>
        public PlateElementThickness GetPlateThicknessByPlateId(int plateId) => management.GetPlateThicknessByPlateId(plateId);

        /// <summary>Reads four native thickness slots by definition ID.</summary>
        /// <param name="petId">Positive native thickness definition ID; not a plate ID.</param>
        /// <returns>A new independent packet with four finite nonnegative native length values, no assigned plates and Id set to petId.</returns>
        /// <remarks><para>When to use: Use the property-ID method for definitions or the plate-ID method for effective plate values. GetThicknessPropertyValues must return exactly integral 4; zero is failure. All four output slots must be written and finite. No uniformity assumption, triangular fourth-slot replacement, averaging, conversion or assignment occurs.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var pet = property.GetPlateThicknessByThicknessPropertyId(45);
        /// System.Console.WriteLine(pet.D);
        /// </code></example>
        public PlateElementThickness GetPlateThicknessByThicknessPropertyId(int petId) => management.GetPlateThicknessByThicknessPropertyId(petId);

        /// <summary>Reads all native thickness definitions and their four corner values.</summary>
        /// <returns>A new mutable set of independent packets with native definition IDs and empty AssignedPlates sets; empty for an empty inventory.</returns>
        /// <remarks><para>When to use: Use to inspect definitions without hydrating plates. Uses the bounded thickness inventory and four-value read contract, then rereads inventory IDs to reject an observed reorder/replacement. Duplicate inventory IDs retain separate packets as in the reference type&#x27;s identity equality. Other receivers can still make same-ID edits. No model changes, defaults or unit conversion occur.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var pet in property.GetAllPlateElementThicknesses()) System.Console.WriteLine(pet.Id);
        /// </code></example>
        public HashSet<PlateElementThickness> GetAllPlateElementThicknesses() => management.GetAllPlateElementThicknesses();

        /// <summary>Reads thickness definitions and populates their candidate plate references.</summary>
        /// <param name="allPlates">Non-null sequence of non-null positive-ID candidate plates. Matching references are reused; no unknown plates are synthesized.</param>
        /// <returns>A new set of thickness packets whose AssignedPlates sets contain the matching original or hydrated candidate instances.</returns>
        /// <remarks><para>When to use: Use when thickness assignments are needed alongside values. The candidate overload avoids geometry hydration. Uses checked native thickness inventories, four-value reads and assignment count/list contracts; positive list counts are rechecked. No thickness or native assignment is created. External edits across separate observations can still prevent a model-wide atomic snapshot.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetAllPlateElementThicknessesWithPopulatedAssignments(new[] { new Plate { Id = 17 } }).Count);
        /// </code></example>
        public HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IEnumerable<Plate> allPlates) => management.GetAllPlateElementThicknessesWithPopulatedAssignments(allPlates);

        /// <summary>Reads thickness definitions and populates their candidate plate references.</summary>
        /// <param name="geometry">Non-null Geometry adapter for the same model. Hydrates plates using GetAllEntities&lt;Plate&gt;(1).</param>
        /// <returns>A new set of thickness packets whose AssignedPlates sets contain the matching original or hydrated candidate instances.</returns>
        /// <remarks><para>When to use: Use when thickness assignments are needed alongside values. The candidate overload avoids geometry hydration. Uses checked native thickness inventories, four-value reads and assignment count/list contracts; positive list counts are rechecked. No thickness or native assignment is created. External edits across separate observations can still prevent a model-wide atomic snapshot.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetAllPlateElementThicknessesWithPopulatedAssignments(wrapper.Geometry).Count);
        /// </code></example>
        public HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IOSGeometry geometry) => management.GetAllPlateElementThicknessesWithPopulatedAssignments(geometry);

        /// <summary>Reads the components of one member-end offset in the requested frame.</summary>
        /// <param name="bId">Positive analytical beam/member ID.</param>
        /// <param name="location">Start=0 or End=1; not an incidence node ID.</param>
        /// <param name="offsetAxis">Global=0 or Local=1. No managed coordinate transform is substituted.</param>
        /// <returns>An independent unnumbered packet preserving the requested end/frame and native components. Native NoSpecification is represented by zero components for compatibility; other failures throw.</returns>
        /// <remarks><para>When to use: Use for a direct mutable compatibility packet; use GetMemberGlobalOffset/GetMemberLocalOffset when absence must remain distinct from successful zero. Checks the existing typed native contract (0 success, -6027 no specification). Unlike the shared None shortcut in the main implementation, zero packets remain independent and retain end/frame. No native ID, candidate assignments, unit conversion or model edits are inferred.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var offset = property.GetBeamOffsetSpecification(17, OffsetLocation.End, OffsetAxis.Local);
        /// System.Console.WriteLine(offset.Y);
        /// </code></example>
        public OffsetSpecification GetBeamOffsetSpecification(int bId, OffsetLocation location, OffsetAxis offsetAxis) => management.GetBeamOffsetSpecification(bId, location, offsetAxis);

        /// <summary>Populates the supplied beams with independent start/end offset packets.</summary>
        /// <param name="forBeams">Non-null finite sequence of non-null positive-ID beams. Equal beams are read once; original references are updated.</param>
        /// <param name="offsetAxis">Requested component frame, Global by default; only Global or Local is accepted.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before reading Beam.StartOffset/EndOffset. Stages every native read before updating the supplied objects. NoSpecification yields a zero compatibility packet; use the typed observations to retain absence status. Failure leaves managed offset references unchanged. Does not create native specifications, assign members, save or convert units.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var beams = new[] { new Beam { Id = 17 } };
        /// property.PopulateMemberOffsets(beams);
        /// System.Console.WriteLine(beams[0].EndOffset.Y);
        /// </code></example>
        public IOSProperty PopulateMemberOffsets(IEnumerable<Beam> forBeams, OffsetAxis offsetAxis = OffsetAxis.Global) { management.PopulateMemberOffsets(forBeams, offsetAxis); return this; }

        /// <summary>Creates nonzero member-end offset definitions and stores their native IDs.</summary>
        /// <param name="oSpec">Non-null definition with Start/End location, Global/Local frame and finite signed components in native length units.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before AssignOffsetSpecifications. A packet is zero when every component magnitude is below 1e-9 native units; zero packets skip creation and retain their prior IDs. Nonzero CreateMemberOffsetSpec must return a positive ID before that packet is updated. Repeated creation may add new native definitions. Earlier successes survive later failure; no rollback, assignment, conversion or save. The bulk null guard is corrected from the reference&#x27;s unsafe null dereference.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var offset = OffsetSpecification.GlobalYEnd(-0.15);
        /// property.CreateOffsetSpecification(offset);
        /// System.Console.WriteLine(offset.Id);
        /// </code></example>
        public IOSProperty CreateOffsetSpecification(OffsetSpecification oSpec) { management.CreateOffsetSpecification(oSpec); return this; }

        /// <summary>Creates nonzero member-end offset definitions and stores their native IDs.</summary>
        /// <param name="osSet">Non-null nonempty set of non-null definitions. Location/frame and finite components are validated before native dispatch; do not change definition values while in the set.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use before AssignOffsetSpecifications. A packet is zero when every component magnitude is below 1e-9 native units; zero packets skip creation and retain their prior IDs. Nonzero CreateMemberOffsetSpec must return a positive ID before that packet is updated. Repeated creation may add new native definitions. Earlier successes survive later failure; no rollback, assignment, conversion or save. The bulk null guard is corrected from the reference&#x27;s unsafe null dereference.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var offset = OffsetSpecification.GlobalYEnd(-0.15);
        /// property.CreateOffsetSpecifications(new System.Collections.Generic.HashSet&lt;OffsetSpecification&gt; { offset });
        /// System.Console.WriteLine(offset.Id);
        /// </code></example>
        public IOSProperty CreateOffsetSpecifications(HashSet<OffsetSpecification> osSet) { management.CreateOffsetSpecifications(osSet); return this; }

        /// <summary>Assigns nonzero existing offset specifications to their candidate members.</summary>
        /// <param name="osSet">Null or empty is a no-op; otherwise non-null valid definitions. Zero definitions or empty Beams sets are skipped. Other definitions require positive existing specification/member IDs.</param>
        /// <returns>The same Property adapter for fluent chaining, after all requested operations complete.</returns>
        /// <remarks><para>When to use: Use after CreateOffsetSpecifications or resolving native IDs. Snapshots each member ID set before dispatch and requires integral zero from AssignMemberSpecToBeam. Stops at the first failure; earlier native assignments remain applied. Does not create or delete specifications, save, retry or repair stale native IDs.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// var offset = OffsetSpecification.GlobalYEnd(-0.15);
        /// offset.Id = 72;
        /// offset.Beams.Add(new Beam { Id = 17 });
        /// property.AssignOffsetSpecifications(new System.Collections.Generic.HashSet&lt;OffsetSpecification&gt; { offset });
        /// </code></example>
        public IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> osSet) { management.AssignOffsetSpecifications(osSet); return this; }

        #endregion

        #region Material, thickness and release workflows

        /// <summary>Assigns each named material to its managed beam and plate candidates.</summary>
        /// <param name="mpSet">Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// material.Beams.Add(new Beam { Id = 17 });
        /// material.Plates.Add(new Plate { Id = 17 });
        /// property.AssignMaterialProperties(new System.Collections.Generic.HashSet&lt;MaterialProperty&gt; { material });
        /// </code></example>
        public IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> mpSet) { workflows.AssignMaterialProperties(mpSet, 1); return this; }

        /// <summary>Assigns each named material to its managed beam and plate candidates.</summary>
        /// <param name="materialPropertyCollection">Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// material.Beams.Add(new Beam { Id = 17 });
        /// material.Plates.Add(new Plate { Id = 17 });
        /// property.AssignMaterialProperties(new System.Collections.Generic.HashSet&lt;MaterialProperty&gt; { material }, 2);
        /// </code></example>
        public IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> materialPropertyCollection, int nThreads) { workflows.AssignMaterialProperties(materialPropertyCollection, nThreads); return this; }

        /// <summary>Assigns each named material to its managed beam and plate candidates.</summary>
        /// <param name="mp">Material packet, or set of packets. Null/empty is a no-op; null packets in sets are rejected. Nonempty candidate sets require non-null positive-ID entities and an exact nonblank native material name.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after the named materials already exist. Snapshots names and candidate IDs before native access, then assigns members before plates for each packet. AssignMaterialToMember requires true/1; AssignMaterialToPlate requires integral 0. A -4008 plate result can mean partial assignment and throws. Does not create definitions, assign solids, save or roll back prior writes. Null candidate sets are treated as empty. The degree argument retains the source signature without copying its worker-thread COM dispatch.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// material.Beams.Add(new Beam { Id = 17 });
        /// material.Plates.Add(new Plate { Id = 17 });
        /// property.AssignMaterialProperty(material, 2);
        /// </code></example>
        public IOSProperty AssignMaterialProperty(MaterialProperty mp, int nThreads) { workflows.AssignMaterialProperty(mp, nThreads); return this; }

        /// <summary>Creates or updates named isotropic material definitions using the source legacy E/G scale.</summary>
        /// <param name="mpSet">Non-null, nonempty set without null packets. All input values are snapshotted and validated before the first native call.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: This legacy compatibility overload multiplies E and the derived G by exactly 1000 and leaves density, thermal expansion and damping unchanged, matching the main source. This is a fixed legacy scale, not a detected unit conversion. Prefer the NoConversion overload after preparing inputs in the acquired native creation convention. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet&#x27;s prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// property.CreateMaterialProperties(new System.Collections.Generic.HashSet&lt;MaterialProperty&gt; { material });
        /// System.Console.WriteLine(material.LastCreationStatus);
        /// </code></example>
        public IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet) { workflows.CreateMaterialProperties(mpSet, true); return this; }

        /// <summary>Creates or updates named isotropic material definitions using unconverted constants.</summary>
        /// <param name="mpSet">Non-null, nonempty set without null packets. All input values are snapshotted and validated before the first native call.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use when E and derived G are already in the acquired native creation force/area convention. Density remains weight per volume; alpha and damping pass unchanged. No conversion or native input-unit changes occur. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet&#x27;s prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// property.CreateMaterialPropertiesNoConversion(new System.Collections.Generic.HashSet&lt;MaterialProperty&gt; { material });
        /// System.Console.WriteLine(material.LastCreationStatus);
        /// </code></example>
        public IOSProperty CreateMaterialPropertiesNoConversion(HashSet<MaterialProperty> mpSet) { workflows.CreateMaterialProperties(mpSet, false); return this; }

        /// <summary>Creates or updates named isotropic material definitions using the source legacy E/G scale.</summary>
        /// <param name="mp">Non-null named material with finite constants. E/G must be positive, -1 &lt; Poisson &lt; 0.5, and weight density/damping nonnegative. Basic creation does not send strength fields.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: This legacy compatibility overload multiplies E and the derived G by exactly 1000 and leaves density, thermal expansion and damping unchanged, matching the main source. This is a fixed legacy scale, not a detected unit conversion. Prefer the NoConversion overload after preparing inputs in the acquired native creation convention. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet&#x27;s prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// property.CreateMaterialProperty(material);
        /// System.Console.WriteLine(material.LastCreationStatus);
        /// </code></example>
        public IOSProperty CreateMaterialProperty(MaterialProperty mp) { workflows.CreateMaterialProperty(mp, true); return this; }

        /// <summary>Creates or updates named isotropic material definitions using unconverted constants.</summary>
        /// <param name="mp">Non-null named material with finite constants. E/G must be positive, -1 &lt; Poisson &lt; 0.5, and weight density/damping nonnegative. Basic creation does not send strength fields.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use when E and derived G are already in the acquired native creation force/area convention. Density remains weight per volume; alpha and damping pass unchanged. No conversion or native input-unit changes occur. Calls CreateIsotropicMaterialProperties: native 0 means Created and 1 Updated, not a material ID. Records LastCreationStatus after success and leaves Id unchanged, correcting the source status-as-ID assignment. General error -1 and other undocumented responses throw without changing that packet&#x27;s prior status/Id. Earlier successful definitions survive a later failure. G is computed from E/Poisson as in the source packet, not read from native storage. Does not populate assignment sets, assign entities, send Fy/Fu/Ry/Rt/Fcu, save or roll back.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000.0, Poisson = 0.3, Density = 77.0, Alpha = 0.000012, Damping = 0.02 };
        /// property.CreateMaterialPropertyNoConversion(material);
        /// System.Console.WriteLine(material.LastCreationStatus);
        /// </code></example>
        public IOSProperty CreateMaterialPropertyNoConversion(MaterialProperty mp) { workflows.CreateMaterialProperty(mp, false); return this; }

        /// <summary>Reads all extended isotropic material definitions by native zero-based index.</summary>
        /// <returns>A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.</returns>
        /// <remarks><para>When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Retains native strength values. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var material in property.GetAllExtendedIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialProperties() => workflows.GetAllExtendedIsotropicMaterialProperties();

        /// <summary>Reads all isotropic material definitions by native zero-based index.</summary>
        /// <returns>A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.</returns>
        /// <remarks><para>When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Basic packets have zero strength placeholders; these are not observations of absent strength fields. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var material in property.GetAllIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllIsotropicMaterialProperties() => workflows.GetAllIsotropicMaterialProperties();

        /// <summary>Reads all indexed isotropic definitions and populates candidate assignments.</summary>
        /// <param name="forBeams">Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.</param>
        /// <param name="forPlates">Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.</param>
        /// <returns>New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use to include unused definitions as well as assigned materials. Retrieves all indexed isotropic materials first. Uses extended strength reads. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var materials = property.GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
        /// System.Console.WriteLine(materials.Count);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => workflows.GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates);

        /// <summary>Reads all indexed isotropic definitions and populates candidate assignments.</summary>
        /// <param name="forBeams">Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.</param>
        /// <param name="forPlates">Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.</param>
        /// <returns>New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use to include unused definitions as well as assigned materials. Retrieves all indexed isotropic materials first. Uses basic reads with zero placeholders for unreturned strengths. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var materials = property.GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
        /// System.Console.WriteLine(materials.Count);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => workflows.GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates);

        /// <summary>Reads material definitions used by the supplied entities and populates candidate assignments.</summary>
        /// <param name="forBeams">Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.</param>
        /// <param name="forPlates">Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.</param>
        /// <returns>New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use to query only material names actually used by the supplied beams/plates; both empty inputs return an empty set. Names are discovered with the beam/plate-specific APIs and resolved once per exact distinct name. Uses extended strength reads. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var materials = property.GetAllExtendedMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
        /// System.Console.WriteLine(materials.Count);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllExtendedMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => workflows.GetAllExtendedMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates);

        /// <summary>Reads material definitions used by the supplied entities and populates candidate assignments.</summary>
        /// <param name="forBeams">Optional finite sequence of non-null positive-ID beam candidates; null/empty skips replacing beam assignments.</param>
        /// <param name="forPlates">Optional finite sequence of non-null positive-ID plate candidates; null/empty skips replacing plate assignments.</param>
        /// <returns>New material packets with matching original candidate references in Beams/Plates. Definitions are name-equal using the current culture, ignoring case; unknown native entity IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use to query only material names actually used by the supplied beams/plates; both empty inputs return an empty set. Names are discovered with the beam/plate-specific APIs and resolved once per exact distinct name. Uses basic reads with zero placeholders for unreturned strengths. Snapshots each input sequence once, validates candidate IDs, and stages all native assignment reads before updating packet sets. Native material subset count/list contracts and the 100000-entry limits apply. Null/empty candidate sides leave that side unchanged. The typed single-material APIs retain their qualified return packets; compatibility collection methods map those observations into mutable MaterialProperty packets. No geometry hydration, native assignment, conversion, save or model-wide atomic snapshot is implied.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var materials = property.GetAllMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } });
        /// System.Console.WriteLine(materials.Count);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates) => workflows.GetAllMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates);

        /// <summary>Resolves distinct entity material names with a caller delegate and populates their candidate assignments.</summary>
        /// <param name="forBeams">Optional finite sequence of non-null positive-ID beam candidates; enumerated once.</param>
        /// <param name="forPlates">Optional finite sequence of non-null positive-ID plate candidates; enumerated once.</param>
        /// <param name="materialResolver">Non-null synchronous callback, invoked once per exact distinct discovered native name on the caller apartment under this adapter&#x27;s reentrant lock. Must return a non-null packet with a nonblank Name. Its returned Name selects native assignment lookups; it may deliberately resolve an alias.</param>
        /// <returns>A mutable set of the resolver-returned original material packets, deduplicated by their current-culture case-insensitive names, with staged assignment subsets populated.</returns>
        /// <remarks><para>When to use: Use for custom material resolution/caching without recreating packets. Does not itself query constants; the callback decides how they are obtained. Native name discovery must succeed before resolution. A callback exception propagates. All assignment reads complete before any returned packet assignment set is changed; nonempty candidate sides clear/repopulate the existing sets (or initialize null sets), while null/empty sides preserve them. Native and callback side effects cannot be rolled back. Do not rename packets already stored in sets. No save, unit conversion or entity assignment occurs in this helper.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var materials = property.GetAllMaterialPropertiesWithPopulatedAssignments(new[] { new Beam { Id = 17 } }, new[] { new Plate { Id = 17 } }, name =&gt; new MaterialProperty(name));
        /// System.Console.WriteLine(materials.Count);
        /// </code></example>
        public HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, Func<string, MaterialProperty> materialResolver) => workflows.GetAllMaterialPropertiesWithPopulatedAssignments(forBeams, forPlates, materialResolver);

        /// <summary>Creates uniform native thickness definitions from scalar thickness packets.</summary>
        /// <param name="ptSet">Non-null nonempty set of non-null packets with finite positive Thickness values in the acquired native creation length convention. All values are copied before native access.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use for one uniform scalar thickness per definition; use PlateElementThickness creation for explicit four-corner values. Preserves the source scalar CreatePlateThicknessProperty route. Stores only a positive returned native definition ID; invalid/error responses throw and retain that packet&#x27;s prior ID. Does not assign plates, convert units or save. Earlier successful creations survive later failure.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var thickness = new PlateThickness { Thickness = 0.2 };
        /// property.CreatePlateThicknessProperties(new System.Collections.Generic.HashSet&lt;PlateThickness&gt; { thickness });
        /// System.Console.WriteLine(thickness.Id);
        /// </code></example>
        public IOSProperty CreatePlateThicknessProperties(HashSet<PlateThickness> ptSet) { workflows.CreatePlateThicknessProperties(ptSet); return this; }

        /// <summary>Assigns existing uniform thickness IDs to their managed plate candidates.</summary>
        /// <param name="tpSet">Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns each packet&#x27;s plate-ID array in one native call. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
        /// thickness.Plates.Add(new Plate { Id = 17 });
        /// property.AssignThicknessProperties(new System.Collections.Generic.HashSet&lt;PlateThickness&gt; { thickness });
        /// </code></example>
        public IOSProperty AssignThicknessProperties(HashSet<PlateThickness> tpSet) { workflows.AssignThicknessProperties(tpSet, 1, false); return this; }

        /// <summary>Assigns existing uniform thickness IDs to their managed plate candidates.</summary>
        /// <param name="thicknessPropertyCollection">Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns plates individually with the source scalar route. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
        /// thickness.Plates.Add(new Plate { Id = 17 });
        /// property.AssignThicknessProperties(new System.Collections.Generic.HashSet&lt;PlateThickness&gt; { thickness }, 2);
        /// </code></example>
        public IOSProperty AssignThicknessProperties(HashSet<PlateThickness> thicknessPropertyCollection, int nThreads) { workflows.AssignThicknessProperties(thicknessPropertyCollection, nThreads, true); return this; }

        /// <summary>Assigns existing uniform thickness IDs to their managed plate candidates.</summary>
        /// <param name="thicknessProperty">Null/empty input or no plate candidates is a no-op. Nonempty assignments require a positive existing thickness Id and non-null positive-ID plates. Null packets in sets are rejected.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use after native thickness definitions exist. Snapshots target IDs before dispatch; assigns plates individually with the source scalar route. Requires integral zero from AssignPlateThickness; -4008 can indicate a partial assignment and throws. Corrects the source bulk null guard; no definition creation, rollback, save or unit conversion. An assignment can skip invalid positive plate IDs inside native STAAD before returning an error; there is no implicit existence preflight.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var thickness = new PlateThickness { Id = 45, Thickness = 0.2 };
        /// thickness.Plates.Add(new Plate { Id = 17 });
        /// property.AssignThicknessProperty(thickness, 2);
        /// </code></example>
        public IOSProperty AssignThicknessProperty(PlateThickness thicknessProperty, int nThreads) { workflows.AssignThicknessProperty(thicknessProperty, nThreads); return this; }

        /// <summary>Assigns existing offset specification IDs to individual candidate members.</summary>
        /// <param name="os">Null/empty input or no member candidates is a no-op. Nonempty targets require positive existing specification and member IDs. Assignment uses Id; these overloads do not inspect component values or skip IsZero packets.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use the source-compatible degree overload after resolving native IDs. Snapshots target IDs and calls AssignMemberSpecToBeam per member, requiring integral zero success. The non-degree bulk overload filters zero definitions; these ID-based overloads deliberately preserve the source distinction. No definition creation, unit conversion, retry, save or rollback. All native calls stay on the caller apartment.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var offset = OffsetSpecification.GlobalYEnd(-0.15);
        /// offset.Id = 72;
        /// offset.Beams.Add(new Beam { Id = 17 });
        /// property.AssignOffsetSpecification(offset, 2);
        /// </code></example>
        public IOSProperty AssignOffsetSpecification(OffsetSpecification os, int nThreads) { workflows.AssignOffsetSpecification(os, nThreads); return this; }

        /// <summary>Assigns existing offset specification IDs to individual candidate members.</summary>
        /// <param name="offsetSpecificationCollection">Null/empty input or no member candidates is a no-op. Nonempty targets require positive existing specification and member IDs. Assignment uses Id; these overloads do not inspect component values or skip IsZero packets.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use the source-compatible degree overload after resolving native IDs. Snapshots target IDs and calls AssignMemberSpecToBeam per member, requiring integral zero success. The non-degree bulk overload filters zero definitions; these ID-based overloads deliberately preserve the source distinction. No definition creation, unit conversion, retry, save or rollback. All native calls stay on the caller apartment.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var offset = OffsetSpecification.GlobalYEnd(-0.15);
        /// offset.Id = 72;
        /// offset.Beams.Add(new Beam { Id = 17 });
        /// property.AssignOffsetSpecifications(new System.Collections.Generic.HashSet&lt;OffsetSpecification&gt; { offset }, 2);
        /// </code></example>
        public IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> offsetSpecificationCollection, int nThreads) { workflows.AssignOffsetSpecifications(offsetSpecificationCollection, nThreads); return this; }

        /// <summary>Groups managed member-end offsets, creates native definitions and assigns their target members.</summary>
        /// <param name="beams">Non-null set of positive-ID beams. Null StartOffset/EndOffset is ignored. Definition location/frame and components must be valid and finite; packet Location is used as supplied rather than inferred from the Beam property.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use to apply populated or manually supplied nonzero offset packets. Groups by exact Location/Axis/X/Y/Z equality, preserving the first packet&#x27;s existing Beams and adding the supplied beams using equal offsets. Creates all definitions before starting assignments. Copies each positive created ID into all equal source packets and adds candidate references to the canonical packet. Definitions below the 1e-9 per-component zero threshold are skipped in both overloads, avoiding source degree-pipeline assignments of uncreated ID zero. Assigns each grouped ID array. Requires positive creation IDs and integral zero assignment success. Empty beams/no offsets is a no-op. Earlier creations/assignments remain if a later step fails; no cleanup, unit conversion, save or analysis occurs. Do not mutate definition values while held in hash sets.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var beam = new Beam { Id = 17, EndOffset = OffsetSpecification.GlobalYEnd(-0.15) };
        /// property.CreateAndAssignOffsetSpecifications(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam });
        /// </code></example>
        public IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams) { workflows.CreateAndAssignOffsetSpecifications(beams, 1, false); return this; }

        /// <summary>Groups managed member-end offsets, creates native definitions and assigns their target members.</summary>
        /// <param name="beams">Non-null set of positive-ID beams. Null StartOffset/EndOffset is ignored. Definition location/frame and components must be valid and finite; packet Location is used as supplied rather than inferred from the Beam property.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use to apply populated or manually supplied nonzero offset packets. Groups by exact Location/Axis/X/Y/Z equality, preserving the first packet&#x27;s existing Beams and adding the supplied beams using equal offsets. Creates all definitions before starting assignments. Copies each positive created ID into all equal source packets and adds candidate references to the canonical packet. Definitions below the 1e-9 per-component zero threshold are skipped in both overloads, avoiding source degree-pipeline assignments of uncreated ID zero. Assigns individual members. Requires positive creation IDs and integral zero assignment success. Empty beams/no offsets is a no-op. Earlier creations/assignments remain if a later step fails; no cleanup, unit conversion, save or analysis occurs. Do not mutate definition values while held in hash sets.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var beam = new Beam { Id = 17, EndOffset = OffsetSpecification.GlobalYEnd(-0.15) };
        /// property.CreateAndAssignOffsetSpecifications(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam }, 2);
        /// </code></example>
        public IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams, int nThreads) { workflows.CreateAndAssignOffsetSpecifications(beams, nThreads, true); return this; }

        /// <summary>Reads both member-end release specifications into the supplied managed beams.</summary>
        /// <param name="forBeams">Non-null finite sequence of non-null positive-ID beams. Equal candidates are queried once; original beam references are updated only after all reads succeed.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use before inspecting StartRelease/EndRelease. Calls the checked basic GetMemberReleaseSpec route for each end, preserving the source query choice. Projects Boolean release/spring fields while retaining every native 0/1/-1/-2/-3 mode in NativeSpecification, including mixed modes. Basic reads do not supply separate common/directional extended MP outputs; compatibility zero placeholders are not evidence of native zero. Use the existing extended typed query when those outputs are needed. Does not resolve native specification IDs, create/assign releases, save or convert units. Any read failure leaves all supplied beam release references unchanged.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// var beams = new[] { new Beam { Id = 17 } };
        /// property.PopulateMemberReleases(beams);
        /// System.Console.WriteLine(beams[0].EndRelease.ReleaseType);
        /// </code></example>
        public IOSProperty PopulateMemberReleases(IEnumerable<Beam> forBeams) { workflows.PopulateMemberReleases(forBeams); return this; }

        /// <summary>Creates and assigns basic full-release/spring specifications at supplied member ends.</summary>
        /// <param name="beams">Non-null set of non-null positive-ID beams. Null/no-release ends are skipped. Use independent release packets for start and end; applicable spring inputs must be finite and nonnegative.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use for the source basic 0/1 release-flag and six-spring workflow. Snapshots all end requests before the first native write and rejects common/directional partial-moment packets with NotSupportedException rather than silently dropping -2/-3 modes or MP factors. Each end is created with CreateMemberReleaseSpec, its positive ID is stored, then it is assigned to the member using integral-zero AssignMemberSpecToBeam success. Starts precede ends for each supplied beam. Translational springs retain force/length units; rotational springs retain moment/angle units. Native interpretation of combined flags/springs requires live qualification. Does not save, convert units, run analysis, roll back earlier operations or delete orphaned definitions after assignment failure.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <exception cref="NotSupportedException">A partial-moment mode/factor cannot be represented by this basic release-creation workflow.</exception>
        /// <example><code>
        /// var releases = new MemberReleases();
        /// releases.Releases.My = true;
        /// releases.Releases.Mz = true;
        /// var beam = new Beam { Id = 17, EndRelease = releases };
        /// property.CreateAndAssignMemberEndReleases(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam });
        /// </code></example>
        public IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams) { workflows.CreateAndAssignMemberEndReleases(beams, 1); return this; }

        /// <summary>Creates and assigns basic full-release/spring specifications at supplied member ends.</summary>
        /// <param name="beams">Non-null set of non-null positive-ID beams. Null/no-release ends are skipped. Use independent release packets for start and end; applicable spring inputs must be finite and nonnegative.</param>
        /// <param name="nThreads">Source-compatible degree value from 1 through 512. COM calls remain synchronous on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested workflow completes.</returns>
        /// <remarks><para>When to use: Use for the source basic 0/1 release-flag and six-spring workflow. Snapshots all end requests before the first native write and rejects common/directional partial-moment packets with NotSupportedException rather than silently dropping -2/-3 modes or MP factors. Each end is created with CreateMemberReleaseSpec, its positive ID is stored, then it is assigned to the member using integral-zero AssignMemberSpecToBeam success. Starts precede ends for each supplied beam. Translational springs retain force/length units; rotational springs retain moment/angle units. Native interpretation of combined flags/springs requires live qualification. Does not save, convert units, run analysis, roll back earlier operations or delete orphaned definitions after assignment failure.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <exception cref="NotSupportedException">A partial-moment mode/factor cannot be represented by this basic release-creation workflow.</exception>
        /// <example><code>
        /// var releases = new MemberReleases();
        /// releases.Releases.My = true;
        /// releases.Releases.Mz = true;
        /// var beam = new Beam { Id = 17, EndRelease = releases };
        /// property.CreateAndAssignMemberEndReleases(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam }, 2);
        /// </code></example>
        public IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams, int nThreads) { workflows.CreateAndAssignMemberEndReleases(beams, nThreads); return this; }

        #endregion

        #region Section completion and native observations

        /// <summary>Assigns native section IDs from retrieved section packets to their candidate beams.</summary>
        /// <param name="gsSet">Null/empty is a no-op. Non-null packets with nonempty AssignedBeams require positive existing section/member IDs; empty assignments are skipped.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use with GetAllSections/GetSectionBy results when section definitions already exist. Copies candidate IDs before dispatch and calls AssignBeamProperty once per section with an Int32 array, requiring integral zero success. Does not create sections, hydrate geometry, change managed beam section references, save or roll back earlier writes.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var section = property.GetSectionBy(37);
        /// section.AssignedBeams.Add(new Beam { Id = 17 });
        /// property.AssignSectionProperties(new System.Collections.Generic.HashSet&lt;StaadPro.Interop.Interfaces.IGenericSection&gt; { section });
        /// </code></example>
        public IOSProperty AssignSectionProperties(HashSet<IGenericSection> gsSet) { sections.AssignSectionProperties(gsSet); return this; }

        /// <summary>Assigns existing section definition IDs to their managed member candidates.</summary>
        /// <param name="spSet">Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty with each packet&#x27;s member-ID array. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.AssignSectionProperties(new System.Collections.Generic.HashSet&lt;SectionProperty&gt; { section });
        /// </code></example>
        public IOSProperty AssignSectionProperties(HashSet<SectionProperty> spSet) { sections.AssignSectionProperties(spSet, 1, false); return this; }

        /// <summary>Assigns existing section definition IDs to their managed member candidates.</summary>
        /// <param name="sectionProperties">Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.</param>
        /// <param name="nThreads">Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty for each individual member, preserving the source scalar route. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.AssignSectionProperties(new System.Collections.Generic.HashSet&lt;SectionProperty&gt; { section }, 2);
        /// </code></example>
        public IOSProperty AssignSectionProperties(HashSet<SectionProperty> sectionProperties, int nThreads) { sections.AssignSectionProperties(sectionProperties, nThreads, true); return this; }

        /// <summary>Assigns existing section definition IDs to their managed member candidates.</summary>
        /// <param name="sp">Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty with each packet&#x27;s member-ID array. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.AssignSectionProperty(section);
        /// </code></example>
        public IOSProperty AssignSectionProperty(SectionProperty sp) { sections.AssignSectionProperty(sp, 1, false); return this; }

        /// <summary>Assigns existing section definition IDs to their managed member candidates.</summary>
        /// <param name="sectionProperty">Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.</param>
        /// <param name="nThreads">Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty for each individual member, preserving the source scalar route. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.AssignSectionProperty(section, 2);
        /// </code></example>
        public IOSProperty AssignSectionProperty(SectionProperty sectionProperty, int nThreads) { sections.AssignSectionProperty(sectionProperty, nThreads, true); return this; }

        /// <summary>Groups explicit beam section definitions, creates them and assigns their members.</summary>
        /// <param name="beams">Non-null set of non-null positive-ID beams. Null SectionProperty references are ignored. Complete H/C/L/O/Box selections and valid additional values are required for non-null definitions.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use to apply managed SectionProperty definitions in one ordered workflow. Groups equal selected profile identities, modifiers and additional values; retains canonical existing Beams and adds supplied beam references. Validates/snapshots all requests and target IDs first, creates every grouped definition, propagates each positive native ID to all equal source packets, then assigns grouped member-ID arrays. Set the intended existing current material beforehand. A later creation/assignment failure leaves earlier native changes and successful IDs applied; no transaction, cleanup, save, implicit material selection or conversion occurs. Empty input/no definitions is a no-op. Private catalog/UI services are not imported.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// var beam = new Beam { Id = 17, SectionProperty = section };
        /// property.CreateAndAssignSectionProperties(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam });
        /// </code></example>
        public IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams) { sections.CreateAndAssignSectionProperties(beams, 1, false); return this; }

        /// <summary>Groups explicit beam section definitions, creates them and assigns their members.</summary>
        /// <param name="beams">Non-null set of non-null positive-ID beams. Null SectionProperty references are ignored. Complete H/C/L/O/Box selections and valid additional values are required for non-null definitions.</param>
        /// <param name="nThreads">Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use to apply managed SectionProperty definitions in one ordered workflow. Groups equal selected profile identities, modifiers and additional values; retains canonical existing Beams and adds supplied beam references. Validates/snapshots all requests and target IDs first, creates every grouped definition, propagates each positive native ID to all equal source packets, then assigns individual member IDs. Set the intended existing current material beforehand. A later creation/assignment failure leaves earlier native changes and successful IDs applied; no transaction, cleanup, save, implicit material selection or conversion occurs. Empty input/no definitions is a no-op. Private catalog/UI services are not imported.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// var beam = new Beam { Id = 17, SectionProperty = section };
        /// property.CreateAndAssignSectionProperties(new System.Collections.Generic.HashSet&lt;Beam&gt; { beam }, 2);
        /// </code></example>
        public IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams, int nThreads) { sections.CreateAndAssignSectionProperties(beams, nThreads, true); return this; }

        /// <summary>Creates or updates isotropic materials using explicit dimensional conversion factors.</summary>
        /// <param name="mpSet">Non-null material packet, or non-null nonempty set without null packets. Finite named creation inputs are required; all packets are validated before native writes.</param>
        /// <param name="uc">Non-null repository UnitsConverter. Valid source/target length/force selectors are copied once; E/G use force/length-squared and weight density uses force/length-cubed.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use when caller constants need explicit conversion to the acquired native creation convention. Multiplies E, derived G and weight density by the copied corresponding factors, matching the source overload&#x27;s dimensions; Poisson, alpha and damping pass unchanged. Does not apply the legacy E/G x1000 scale, alter the source packet or change native input units. Uses repository double-precision unit constants rather than the private decimal units library. Native 0=Created and 1=Updated are recorded in LastCreationStatus; Id remains unchanged. Strength fields are not sent. Earlier successes survive later failure; no assignment, save or rollback occurs.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000000.0, Poisson = 0.3, Density = 77.0 };
        /// var converter = new StaadPro.Interop.Common.Units.UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton);
        /// property.CreateMaterialProperties(new System.Collections.Generic.HashSet&lt;MaterialProperty&gt; { material }, converter);
        /// </code></example>
        public IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet, UnitsConverter uc) { workflows.CreateMaterialProperties(mpSet, uc); return this; }

        /// <summary>Creates or updates isotropic materials using explicit dimensional conversion factors.</summary>
        /// <param name="mp">Non-null material packet, or non-null nonempty set without null packets. Finite named creation inputs are required; all packets are validated before native writes.</param>
        /// <param name="uc">Non-null repository UnitsConverter. Valid source/target length/force selectors are copied once; E/G use force/length-squared and weight density uses force/length-cubed.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use when caller constants need explicit conversion to the acquired native creation convention. Multiplies E, derived G and weight density by the copied corresponding factors, matching the source overload&#x27;s dimensions; Poisson, alpha and damping pass unchanged. Does not apply the legacy E/G x1000 scale, alter the source packet or change native input units. Uses repository double-precision unit constants rather than the private decimal units library. Native 0=Created and 1=Updated are recorded in LastCreationStatus; Id remains unchanged. Strength fields are not sent. Earlier successes survive later failure; no assignment, save or rollback occurs.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var material = new MaterialProperty(&quot;STEEL&quot;) { E = 200000000.0, Poisson = 0.3, Density = 77.0 };
        /// var converter = new StaadPro.Interop.Common.Units.UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton);
        /// property.CreateMaterialProperty(material, converter);
        /// </code></example>
        public IOSProperty CreateMaterialProperty(MaterialProperty mp, UnitsConverter uc) { workflows.CreateMaterialProperty(mp, uc); return this; }

        /// <summary>Creates a uniform thickness definition through the explicitly supplied captured receiver.</summary>
        /// <param name="property">This adapter&#x27;s ComObject instance. The proprietary OSPropertyUI parameter is adapted to dynamic (System.Object) to avoid a vendor assembly dependency; null and foreign receivers are rejected before native access.</param>
        /// <param name="pt">Non-null packet with finite positive scalar Thickness in the acquired native length convention. Creation does not use its candidate set.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use for source compatibility with the explicit-receiver overload. Requires reference identity with this adapter&#x27;s captured Property binding, so a foreign model cannot be modified through the wrapper accidentally. Sends the scalar thickness unchanged and stores only a positive created definition ID; failure preserves the prior ID. Does not assign plates, convert dimensions, save or roll back native creation.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var thickness = new PlateThickness { Thickness = 0.2 };
        /// property.CreatePlateThicknessProperty(property.ComObject, thickness);
        /// System.Console.WriteLine(thickness.Id);
        /// </code></example>
        public IOSProperty CreatePlateThicknessProperty(dynamic property, PlateThickness pt) { sections.CreatePlateThicknessProperty((object)property, pt); return this; }

        /// <summary>Creates native section definitions from explicit H/C/L/O/Box profile selections.</summary>
        /// <param name="spSet">Non-null complete section definition, or non-null nonempty set without null definitions. All selected names/country/modifier codes and applicable additional values are validated/copied before dispatch.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use for the main table-selection creation graph. H supports ST/T/D/TC/BC/TB/CM, C supports ST/D/FR/BA, L supports ST/RA/SA/LD/SD, and pipe/tube support ST/UserDefined. Uses the corresponding native table routine and actual BC code with Bottom dimensions. Composite creation forwards source CT/FC/CW/CD values unchanged; acquired-build support requires qualification. Unsupported families/modifiers throw before native writes instead of silently doing nothing. Set the intended existing material first. Only positive native results replace packet IDs; earlier successful creations remain on later failure. No assignment, private catalog lookup, conversion, implicit save or rollback.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// property.CreateSectionProperties(new System.Collections.Generic.HashSet&lt;SectionProperty&gt; { section });
        /// System.Console.WriteLine(section.Id);
        /// </code></example>
        public IOSProperty CreateSectionProperties(HashSet<SectionProperty> spSet) { sections.CreateSectionProperties(spSet); return this; }

        /// <summary>Creates native section definitions from explicit H/C/L/O/Box profile selections.</summary>
        /// <param name="sp">Non-null complete section definition, or non-null nonempty set without null definitions. All selected names/country/modifier codes and applicable additional values are validated/copied before dispatch.</param>
        /// <returns>The same Property adapter for fluent chaining after the requested operations complete.</returns>
        /// <remarks><para>When to use: Use for the main table-selection creation graph. H supports ST/T/D/TC/BC/TB/CM, C supports ST/D/FR/BA, L supports ST/RA/SA/LD/SD, and pipe/tube support ST/UserDefined. Uses the corresponding native table routine and actual BC code with Bottom dimensions. Composite creation forwards source CT/FC/CW/CD values unchanged; acquired-build support requires qualification. Unsupported families/modifiers throw before native writes instead of silently doing nothing. Set the intended existing material first. Only positive native results replace packet IDs; earlier successful creations remain on later failure. No assignment, private catalog lookup, conversion, implicit save or rollback.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
        /// definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = &quot;ISMB300&quot;, Designation = &quot;ISMB300&quot; };
        /// var section = new SectionProperty { Id = 37, SectionDefinition = definition };
        /// section.Beams.Add(new Beam { Id = 17 });
        /// property.SetMaterialName(&quot;STEEL&quot;);
        /// property.CreateSectionProperty(section);
        /// System.Console.WriteLine(section.Id);
        /// </code></example>
        public IOSProperty CreateSectionProperty(SectionProperty sp) { sections.CreateSectionProperty(sp); return this; }

        /// <summary>Reads native section definitions into owned version-decoded section packets.</summary>
        /// <returns>A new mutable set of section packets with native IDs, identity, raw-capacity values, applicable field labels, area availability and empty AssignedBeams sets. Empty inventory returns an empty set.</returns>
        /// <remarks><para>When to use: Use to enumerate definitions without loading geometry. Uses bounded section inventory/count checks, reads each version-specific packet, then rechecks inventory IDs/order. Supports all 47 reviewed top-level layouts for acquired STAAD 24/25/26; unknown versions/types fail explicitly. Native identity is retained without private catalog naming/area substitutions. Count/order checks cannot rule out same-ID external edits. No native mutation, inferred units or hidden member lookup occurs.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// foreach (var section in property.GetAllSections()) System.Console.WriteLine(section.StaadName);
        /// </code></example>
        public HashSet<IGenericSection> GetAllSections() => sections.GetAllSections();

        /// <summary>Reads section packets and populates their candidate member references.</summary>
        /// <param name="allBeams">Non-null finite sequence of non-null positive-ID candidate beams. Enumerated once and matching original references are reused.</param>
        /// <returns>A new mutable set of section packets whose AssignedBeams contain matching candidate/hydrated references; unknown native IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use the candidate overload when beams are already loaded; use Geometry for full hydration. Reads checked version-specific definitions and each native assignment subset using bounded count/list contracts. Does not assign members or alter supplied beam objects. Geometry/candidate provenance must match the model; separate observations are not a model-wide atomic snapshot. No private catalog graph, save or unit conversion is used.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var sections = property.GetAllSectionsWithAssignedBeamsList(new[] { new Beam { Id = 17 } });
        /// System.Console.WriteLine(sections.Count);
        /// </code></example>
        public HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IEnumerable<Beam> allBeams) => sections.GetAllSectionsWithAssignedBeamsList(allBeams);

        /// <summary>Reads section packets and populates their candidate member references.</summary>
        /// <param name="geometry">Non-null Geometry adapter for the same model; hydrates all beams using GetAllEntities&lt;Beam&gt;(1).</param>
        /// <returns>A new mutable set of section packets whose AssignedBeams contain matching candidate/hydrated references; unknown native IDs are not synthesized.</returns>
        /// <remarks><para>When to use: Use the candidate overload when beams are already loaded; use Geometry for full hydration. Reads checked version-specific definitions and each native assignment subset using bounded count/list contracts. Does not assign members or alter supplied beam objects. Geometry/candidate provenance must match the model; separate observations are not a model-wide atomic snapshot. No private catalog graph, save or unit conversion is used.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var sections = property.GetAllSectionsWithAssignedBeamsList(wrapper.Geometry);
        /// System.Console.WriteLine(sections.Count);
        /// </code></example>
        public HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IOSGeometry geometry) => sections.GetAllSectionsWithAssignedBeamsList(geometry);

        /// <summary>Reads an available native section area, or the source mean tapered-I plate area.</summary>
        /// <param name="propId">Positive native section property reference ID; not a member ID or section type code.</param>
        /// <returns>Ax in native length-squared units where supplied, or the source mean start/end tapered-I area. Returns null for BeamD and UPT ISECTION, whose native layouts omit Ax; zero is retained only when explicitly returned/calculated.</returns>
        /// <remarks><para>When to use: Use when a nullable area is needed without touching an assigned member. The source already returns null for BeamD; this port also avoids its fabricated default-zero UPT ISECTION area. Does not fetch a private catalog area or infer an exact double-beam area from rectangular dimensions. Version/type/required-output failures throw. No unit conversion, native mutation or engineering adequacy check occurs.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// double? area = property.GetSectionalArea(37);
        /// System.Console.WriteLine(area);
        /// </code></example>
        public double? GetSectionalArea(int propId) => sections.GetSectionalArea(propId);

        /// <summary>Reads one native section definition with the acquired version-specific value layout.</summary>
        /// <param name="propId">Positive native section property reference ID, not an analytical member number.</param>
        /// <returns>A new owned GenericSection implementing IGenericSection. Id/native name/country/type/record and raw-capacity values are retained; AssignedBeams is empty. Native record -1 means unavailable/not applicable. Ax throws for missing area layouts; inspect GenericSection.HasSectionalArea or GetSectionalArea first.</returns>
        /// <remarks><para>When to use: Use before reading a section&#x27;s identity/field values or populating its assignments. Selects one of 47 native layouts from wrapper.StaadVersion.Major 24, 25 or 26, never the host ApplicationVersion. All required slots must be explicitly written and finite; unused unwritten padding becomes zero. Capacity is bounded to 100000 and rechecked with type/country. Preserves native labels and inertia slot identities without axis reinterpretation. Non-table records are -1; table lookup -1 is retained without failing an otherwise valid packet. Native Ax is used where present; tapered-I mean area follows source plate geometry; BeamD/UPT ISECTION have no substituted area. Unknown profiles fail explicitly. No private catalog, raw COM methods on models, model writes or unit conversion.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var section = property.GetSectionBy(37);
        /// System.Console.WriteLine(section.StaadName);
        /// </code></example>
        public IGenericSection GetSectionBy(int propId) => sections.GetSectionBy(propId);

        /// <summary>Reads one native section and fills its actual candidate member assignment subset.</summary>
        /// <param name="propId">Positive native section property reference ID.</param>
        /// <param name="allBeams">Non-null finite sequence of non-null positive-ID candidate beams; enumerated once. Matching original references are reused.</param>
        /// <returns>A new section packet with AssignedBeams populated from the actual checked native assignment list; unmatched native IDs do not create new candidate objects.</returns>
        /// <remarks><para>When to use: Use to obtain identity/values and assignments for one section. Corrects the main candidate overload filtering an allocated but unfilled zero array: both overloads now use the actual returned assignment IDs. Reads the reviewed version-specific section packet and bounded assignment list, rechecking positive counts. Does not change supplied beams, assign sections, save or convert units; external edits across observations remain possible.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var section = property.GetSectionByPropertyIdWithAssignedBeamsList(37, new[] { new Beam { Id = 17 } });
        /// System.Console.WriteLine(section.AssignedBeams.Count);
        /// </code></example>
        public IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IEnumerable<Beam> allBeams) => sections.GetSectionByPropertyIdWithAssignedBeamsList(propId, allBeams);

        /// <summary>Reads one native section and fills its actual candidate member assignment subset.</summary>
        /// <param name="propId">Positive native section property reference ID.</param>
        /// <param name="geometry">Non-null Geometry adapter for the same model; hydrates all beams with degree one.</param>
        /// <returns>A new section packet with AssignedBeams populated from the actual checked native assignment list; unmatched native IDs do not create new candidate objects.</returns>
        /// <remarks><para>When to use: Use to obtain identity/values and assignments for one section. Corrects the main candidate overload filtering an allocated but unfilled zero array: both overloads now use the actual returned assignment IDs. Reads the reviewed version-specific section packet and bounded assignment list, rechecking positive counts. Does not change supplied beams, assign sections, save or convert units; external edits across observations remain possible.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// var section = property.GetSectionByPropertyIdWithAssignedBeamsList(37, wrapper.Geometry);
        /// System.Console.WriteLine(section.AssignedBeams.Count);
        /// </code></example>
        public IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IOSGeometry geometry) => sections.GetSectionByPropertyIdWithAssignedBeamsList(propId, geometry);

        /// <summary>Counts native section definitions.</summary>
        /// <returns>The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.</returns>
        /// <remarks><para>When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetSectionPropertyCount());
        /// </code></example>
        public int GetSectionPropertyCount() => sections.GetSectionPropertyCount();

        /// <summary>Counts native thickness definitions.</summary>
        /// <returns>The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.</returns>
        /// <remarks><para>When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetThicknessPropertyCount());
        /// </code></example>
        public int GetThicknessPropertyCount() => sections.GetThicknessPropertyCount();

        /// <summary>Reads the native section-value buffer capacity.</summary>
        /// <returns>The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.</returns>
        /// <remarks><para>When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetCountofSectionPropertyValuesEx());
        /// </code></example>
        public int GetCountofSectionPropertyValuesEx() => sections.GetCountofSectionPropertyValuesEx();

        /// <summary>Reads one native section type code by section definition ID.</summary>
        /// <param name="varSecRefNo">Positive native section property reference ID, preserving the documented native parameter name.</param>
        /// <returns>The native integral section type code unchanged. Only positive type codes succeed; unfamiliar positive codes remain observable. Native negative errors such as -6025 throw.</returns>
        /// <remarks><para>When to use: Use for direct metadata discovery without parsing a buffer. Does not identify an analytical member, resolve a catalog profile, convert codes into a guessed shape, create definitions or save. Boolean/floating/text responses are rejected. GetSectionBy separately requires a reviewed native version/type layout.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetSectionPropertyType(37));
        /// </code></example>
        public int GetSectionPropertyType(int varSecRefNo) => sections.GetSectionPropertyType(varSecRefNo);

        /// <summary>Reads one native country code by section definition ID.</summary>
        /// <param name="varSecRefNo">Positive native section property reference ID, preserving the documented native parameter name.</param>
        /// <returns>The native integral country code unchanged. Nonnegative country codes succeed; zero/non-enum codes are retained for non-table/future definitions. Native negative errors such as -6025 throw.</returns>
        /// <remarks><para>When to use: Use for direct metadata discovery without parsing a buffer. Does not identify an analytical member, resolve a catalog profile, convert codes into a guessed shape, create definitions or save. Boolean/floating/text responses are rejected. GetSectionBy separately requires a reviewed native version/type layout.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetSectionPropertyCountry(37));
        /// </code></example>
        public int GetSectionPropertyCountry(int varSecRefNo) => sections.GetSectionPropertyCountry(varSecRefNo);

        /// <summary>Looks up a native section database record for an exact table designation.</summary>
        /// <param name="Country">Positive native table country code; integer parameter name/type preserves the documented native input and allows future country codes.</param>
        /// <param name="SectionName">Exact nonblank native table designation without NUL; case and whitespace pass unchanged.</param>
        /// <returns>The native nonnegative database record, including zero, or documented -1 when lookup fails. A record is not a model section property ID.</returns>
        /// <remarks><para>When to use: Use for table record identity, not to create or assign a model property. Preserves -1 without throwing or substituting a guessed record. Other negative codes and malformed nonintegral responses throw. Database/build record conventions are native-controlled; no assumed one-based adjustment, private catalog fallback, model mutation or save.</para><para>Shares this Property adapter&#x27;s serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.</para></remarks>
        /// <exception cref="ArgumentException">A required entity, name, ID, selector, converter, definition, receiver or creation value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the captured binding is stale/disposed, or a native status/count/buffer/metadata response violates this operation's contract.</exception>
        /// <exception cref="NotSupportedException">A requested section layout/version, profile family or modifier has no reviewed implementation. No guessed geometry/catalog result is substituted.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; earlier successful native writes may remain.</exception>
        /// <example><code>
        /// System.Console.WriteLine(property.GetRecordForSection((int)Country.Indian, &quot;ISMB300&quot;));
        /// </code></example>
        public int GetRecordForSection(int Country, string SectionName) => sections.GetRecordForSection(Country, SectionName);

        #endregion
    }
}
