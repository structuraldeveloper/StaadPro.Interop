using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Property;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Implements typed Property observations over the wrapper's captured native receiver.</summary>
    /// <remarks>A single adapter delegates common native contracts to an internal shared reader. No Property query mutates the model or derives missing material values.</remarks>
    public sealed class OSPropertyAdapter : OSBaseAdapter, IOSProperty
    {
        private readonly PropertyQueryReader queries;
        /// <summary>Captures the wrapper's Property interface without invoking Property domain functions.</summary>
        /// <param name="wrapper">Connected wrapper with a non-null RawProperty receiver.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawProperty binding is null.</exception>
        public OSPropertyAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawProperty ?? throw new ArgumentNullException(nameof(wrapper.RawProperty));
            queries = new PropertyQueryReader(wrapper, (object)ComObject);
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

    }
}
