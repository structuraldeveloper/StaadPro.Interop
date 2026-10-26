using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Common.Units;
using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Property;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides typed Property observations and main-project assignment and inventory operations.</summary>
    /// <remarks>Typed observations return qualified status/data snapshots. Compatibility ports retain direct return types, sentinels and documented no-op inputs. Assignment and material-context operations mutate the native model without saving; read operations preserve native units. No method exposes native reference outputs.</remarks>
    public interface IOSProperty : IOSBase
    {
        /// <summary>Gets the captured native Property binding; direct calls bypass typed validation and coordination.</summary>
        /// <value>The acquired automation receiver; no readiness or compatibility is implied.</value>
        dynamic ComObject { get; }

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
        PropertyQueryResult<double> GetBetaAngle(int memberId);

        /// <summary>Counts element-offset specifications in the current model.</summary>
        /// <returns>The nonnegative native specification count; zero is a successful count. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: This counts native offset specifications, not plates, corners, nonzero vectors or assigned members. Zero is a valid count. Use GetPlateOffsetSpecification to inspect one plate corner and its stored convention. Undocumented negative counts are rejected.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetElementOffsetSpecificationCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        PropertyQueryResult<int> GetElementOffsetSpecificationCount();

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
        PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecificationExtended(int memberId, MemberEnd memberEnd);

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
        PropertyQueryResult<MemberReleaseSpecification> GetMemberReleaseSpecification(int memberId, MemberEnd memberEnd);

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
        PropertyQueryResult<Vector3D> GetMemberLocalOffset(int memberId, MemberEnd memberEnd);

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
        PropertyQueryResult<Vector3D> GetMemberGlobalOffset(int memberId, MemberEnd memberEnd);

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
        PropertyQueryResult<BeamSectionProperties> GetMemberSectionProperties(int memberId);

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
        PropertyQueryResult<BeamSectionProperties> GetMemberSectionPropertiesExtended(int memberId);

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
        PropertyQueryResult<MaterialConstants> GetMemberMaterialConstants(int memberId);

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
        PropertyQueryResult<Vector3D> GetPlateLocalOffset(int plateId, int cornerIndex);

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
        PropertyQueryResult<Vector3D> GetPlateGlobalOffset(int plateId, int cornerIndex);

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
        PropertyQueryResult<ElementOffsetSpecification> GetPlateOffsetSpecification(int plateId, int cornerIndex);

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
        PropertyQueryResult<int> GetPlateSectionPropertyId(int plateId);

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
        PropertyQueryResult<int> GetIsotropicMaterialAssignedMemberCount(string materialName);

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
        PropertyQueryResult<IEnumerable<int>> GetIsotropicMaterialAssignedMemberIds(string materialName, int maximumResults = 100000);

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
        PropertyQueryResult<MaterialProperties> GetMaterialProperty(string materialName);

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
        PropertyQueryResult<string> GetBeamMaterialName(int memberId);

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
        PropertyQueryResult<string> GetElementMaterialName(int plateId);

        /// <summary>Counts isotropic material definitions in the current model.</summary>
        /// <returns>The nonnegative native isotropic-material count; zero is successful. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use to bound the zero-based materialIndex accepted by GetIsotropicMaterialProperty and GetExtendedIsotropicMaterialProperty. This is not a member/plate assignment count. Zero is a valid empty definition inventory; undocumented negative or nonintegral counts are rejected. A count is only an observation, not a stable reservation of material identities.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetIsotropicMaterialCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        PropertyQueryResult<int> GetIsotropicMaterialCount();

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
        PropertyQueryResult<MaterialProperties> GetIsotropicMaterialProperty(int materialIndex);

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
        PropertyQueryResult<MaterialProperties> GetExtendedMaterialProperty(string materialName);

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
        PropertyQueryResult<MaterialProperties> GetExtendedIsotropicMaterialProperty(int materialIndex);

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
        PropertyQueryResult<MaterialProperties> GetIsotropicMaterialPropertiesAssigned(int nativeMaterialNumber);

        /// <summary>Counts native 2D orthotropic material definitions.</summary>
        /// <returns>The nonnegative native two-dimensional orthotropic-material count; zero is successful. Inspect IsSuccessful before Data; unsuccessful observations expose no payload. Native evidence is retained when returned.</returns>
        /// <remarks><para>When to use: Use for the 2D orthotropic inventory, not isotropic or 3D material counts and not element assignments. Zero is valid. The associated property getter documents a native material number rather than an explicit zero-based list index; this count does not establish a selector rebasing rule. Undocumented negative/nonintegral counts are rejected.</para><para>Uses the common installed 2024–2026 native contract; the acquired build must provide the underlying member. Executes synchronously on the caller's established COM apartment. Typed queries on one Property adapter are serialized; raw calls, other adapters/wrappers and external STAAD edits are not locked out. Reacquire wrapper.Property after refresh; disposed/replaced bindings are rejected before native access and checked again after retrieval. No model mutation, save, analysis, retry, unit conversion or assumed engineering defaults occur. Native values and their documented units are preserved; wrapper input-unit fallback values are not proof of observed Property units. Live COM/engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or a response has an undocumented type/code, invalid buffer shape, unknown mode or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original error propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible member.</exception>
        /// <example><code>var result = property.GetOrthotropic2DMaterialCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status + ": " + result.Diagnostic);</code></example>
        PropertyQueryResult<int> GetOrthotropic2DMaterialCount();

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
        PropertyQueryResult<Orthotropic2DMaterialProperties> GetOrthotropic2DMaterialProperties(int nativeMaterialNumber);


        // Main-project signature-compatible Property ports.

        /// <summary>Counts distinct section property definitions with no assigned analytical members.</summary>
        /// <returns>The number of distinct section IDs whose native assigned-member count is exactly zero.</returns>
        /// <remarks><para>When to use: Choose this for an unused-definition count and GetUnusedSectionsIds when IDs are needed. Enumerates the section inventory and queries each assigned-member count; it is not the native total definition count. Negative counts throw instead of being classified as unused. No properties are deleted.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetUnusedSectionsCount());</code></example>
        int GetUnusedSectionsCount();

        /// <summary>Finds section property definitions with no assigned analytical members.</summary>
        /// <returns>A new mutable set of positive native section IDs having exactly zero assigned members; an empty set is valid.</returns>
        /// <remarks><para>When to use: Use to inspect unused definitions before deciding whether to remove them. Enumerates the section inventory and queries each assigned-member count; duplicate property IDs collapse in the result. A native failure is never treated as an unused definition. Does not delete or assign properties.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetUnusedSectionsIds()) System.Console.WriteLine(id);</code></example>
        HashSet<int> GetUnusedSectionsIds();

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
        int GetBeamProperty(int memberId);

        /// <summary>Reads the material name assigned to a plate using the native plate-specific query.</summary>
        /// <param name="pId">Positive analytical plate number; not a material index, property ID, node or solid number.</param>
        /// <returns>The exact native string, preserving case and whitespace; null or empty text is retained when returned by STAAD.</returns>
        /// <remarks><para>When to use: Use the main-project plate-specific compatibility method when the plate number is known. GetElementMaterialName is the existing status/data observation over a different native entry point; this port calls GetPlateMaterialName without alias fallback. Null or empty text cannot distinguish a missing plate from no material or an unavailable response. Does not retrieve constants or assign material.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetPlateMaterialName(17));</code></example>
        string GetPlateMaterialName(int pId);

        /// <summary>Lists the native section property reference IDs.</summary>
        /// <returns>A new owned array of positive IDs in native order, retaining duplicates; an empty array when the count is zero.</returns>
        /// <remarks><para>When to use: Use to enumerate section definitions, not assigned members or thickness properties. Sizes from GetSectionPropertyCount and validates GetSectionPropertyList success, array shape and IDs. A positive-count retrieval rechecks the count and rejects observed changes. Zero count skips fetching. Counts above 100000 are rejected before allocation.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetSectionPropertyList()) System.Console.WriteLine(id);</code></example>
        int[] GetSectionPropertyList();

        /// <summary>Reads the native identification name of a section property.</summary>
        /// <param name="propId">Positive native section property reference ID, typically from GetSectionPropertyList; not a member number.</param>
        /// <returns>The explicitly written native name, preserving empty text, whitespace and case.</returns>
        /// <remarks><para>When to use: Use for a section label after locating its property ID. GetBeamProperty first resolves a member to a property ID. The native GetSectionPropertyName code must be zero; general error -1, no-definition -6025 and any other nonzero code throw. Null, non-string and unwritten string outputs throw. No section catalog or material identity is inferred.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetSectionPropertyName(37));</code></example>
        string GetSectionPropertyName(int propId);

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
        double[] GetSectionPropertyValues(int propId);

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
        HashSet<Beam> GetSectionPropertyAssignedBeams(HashSet<Beam> allBeams, int propId);

        /// <summary>Lists native plate thickness property reference IDs.</summary>
        /// <returns>A new owned array of positive thickness IDs in native order, retaining duplicates; an empty array for zero definitions.</returns>
        /// <remarks><para>When to use: Choose this inventory for thickness definitions; GetSectionPropertyList enumerates member sections. Sizes from GetThicknessPropertyCount, requires successful GetThicknessPropertyList, validates shape/IDs and rechecks positive counts. Counts above 100000 fail before allocation and zero skips fetching. Does not return thickness magnitudes or assigned plate IDs.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>foreach (int id in property.GetThicknessPropertyList()) System.Console.WriteLine(id);</code></example>
        int[] GetThicknessPropertyList();

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
        HashSet<Plate> GetThicknessPropertyAssignedPlates(HashSet<Plate> allPlates, int propId);

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
        HashSet<Beam> GetBeamsWithMissingSectionProperty(HashSet<Beam> allBeams, IEnumerable<int> propIds);

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
        HashSet<Plate> GetPlatesWithMissingThicknessProperty(HashSet<Plate> allPlates, IEnumerable<int> propIds);

        /// <summary>Computes the rounded arithmetic mean of four native plate-corner thickness slots.</summary>
        /// <param name="plateId">Positive analytical plate number, not a thickness property ID.</param>
        /// <returns>The sum of four native thickness values divided by four, rounded to five decimal places with midpoint-to-even rounding, in unchanged native length units.</returns>
        /// <remarks><para>When to use: Use for the main-project four-slot arithmetic average, not an area-weighted or integration-point thickness. Calls GetPlateThickness once and requires native code zero and four explicitly written finite values. No interpolation, triangular-corner substitution, sign adjustment or unit conversion is performed; an unwritten fourth slot throws. Native missing-property and invalid-plate errors throw rather than yielding a zero thickness. The intermediate sum must remain finite.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetAveragePlateThickness(17));</code></example>
        double GetAveragePlateThickness(int plateId);

        /// <summary>Reads the native axial behavior specification of an analytical member.</summary>
        /// <param name="msId">Positive analytical member number forwarded to GetMemberSpecCode. The preserved main-project parameter name does not mean a specification record ID.</param>
        /// <returns>The defined native specification: Other=-1, Truss=0, TensionOnly=1, CompressionOnly=2, CableOnly=3 or Joist=4.</returns>
        /// <remarks><para>When to use: Use for member behavior, not section, material, release or offset classification. Requires Boolean/integral native success and an explicitly written known code. Unknown or unwritten codes throw rather than being cast silently. No member attributes, model state, units or results are changed.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>System.Console.WriteLine(property.GetMemberSpecification(17));</code></example>
        MemberSpecificationType GetMemberSpecification(int msId);

        /// <summary>Assigns an existing section property to one analytical member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a property reference ID.</param>
        /// <param name="propertyId">Positive existing section property reference ID; the method does not create a section.</param>
        /// <remarks><para>When to use: Use to assign an existing property; section inventory/read methods do not mutate assignments. Calls native AssignBeamProperty once with the scalar or copied array. Native zero is success and any nonzero code throws, including invalid member -3006, invalid property -6001 and assignment error -6002. This changes the live model without saving or rollback; errors may follow partial changes. No managed Beam object is updated.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.AssignBeamProperty(17, 37);</code></example>
        void AssignBeamProperty(int memberId, int propertyId);

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
        void AssignBeamProperty(int[] memberIds, int propertyId);

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
        void AssignMaterialToMember(string materialName, int memberId);

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
        void AssignMaterialToMember(string materialName, int[] memberIds);

        /// <summary>Sets the native Property material context for subsequent creation operations.</summary>
        /// <param name="materialName">Exact existing native material name, preserving case and whitespace. Null/empty/whitespace-only names are a no-op; nonblank names must not contain NUL.</param>
        /// <remarks><para>When to use: Use before a native property creation API that consumes the current material context. AssignMaterialToMember instead changes member assignments. Calls SetMaterialName once and requires native Boolean/integral success; the documented integer success code is one. Does not create material, assign members, restore the previous context or save the model. The context is shared native state: serialization covers this call, not a later separate creation transaction. No-op names return before binding checks.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentException">A validated collection has null entities or nonpositive IDs, or a nonblank material name contains NUL.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates and prior native changes are not rolled back.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired native receiver lacks a compatible member; no alias fallback is attempted.</exception>
        /// <example><code>property.SetMaterialName(&quot;STEEL&quot;);</code></example>
        void SetMaterialName(string materialName);

        /// <summary>Creates and assigns global-Y offsets at both analytical member ends.</summary>
        /// <param name="memberId">Positive analytical member number, not a specification ID.</param>
        /// <param name="startY">Finite signed global-Y start offset in the current native length units. Zero is a valid explicit offset.</param>
        /// <param name="endY">Finite signed global-Y end offset in the same native length units. Zero is valid.</param>
        /// <returns>True only when both positive specification IDs are created and both native assignment calls return zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Use to create and assign both end specifications; AssignMemberEndOffset changes one end. Creates the start then end specification with native global selector zero and X=Z=0, then assigns start followed by end. This mutates the live model; failure can leave orphan definitions or partial assignments. No rollback, cleanup, save or managed entity update occurs. Validation, stale/disposed bindings and malformed nonintegral native responses throw; the Boolean result does not certify physical placement or engineering suitability.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffsets(17, 0.15, -0.20));</code></example>
        bool AssignMemberEndOffsets(int memberId, double startY, double endY);

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
        bool AssignMemberEndOffsets(int[] memberIds, double startY, double endY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberId">Positive analytical member number.</param>
        /// <param name="location">Native member-end selector: zero for start, one for end. Other values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(17, 1, -0.20));</code></example>
        bool AssignMemberEndOffset(int memberId, int location, double offsetY);

        /// <summary>Creates and assigns a global-Y offset at one analytical member end.</summary>
        /// <param name="memberId">Positive analytical member number.</param>
        /// <param name="location">OffsetLocation.Start (native zero) or OffsetLocation.End (native one); undefined enum values are rejected.</param>
        /// <param name="offsetY">Finite signed global-Y offset in current native length units. Zero creates an explicit zero offset.</param>
        /// <returns>True when a positive specification ID is created and its native assignment returns zero; false for native error codes, COM/binding failures.</returns>
        /// <remarks><para>When to use: Choose this for one end and AssignMemberEndOffsets for both ends. The typed overload forwards the exact enum code to the integer overload. Calls CreateMemberOffsetSpec once using global selector zero and X=Z=0, then AssignMemberSpecToBeam once with the scalar or copied array. Existing opposite-end state is not explicitly changed by this method. Native state is mutated without rollback, cleanup, save or managed entity synchronization; partial assignment or an orphan definition can remain on failure. Validation/stale-binding/malformed-response errors throw. The offset uses analytical member incidence, not a physical-member endpoint.</para><para>Calls execute synchronously on the caller&#x27;s established COM apartment and share one Property-adapter lock with the existing typed queries. Raw calls, other adapters/wrappers and external STAAD edits are not excluded. Captured bindings are checked before and after native work; reacquire wrapper.Property after refresh. A sequence of observations is not an atomic model snapshot, and count checks cannot detect same-count replacement. No native unit conversion, automatic retry, analysis or implicit save occurs. Live COM and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">A validated scalar ID is not positive, a location is undefined, or an offset is not finite.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced, a native operation fails its documented contract, a count changes/exceeds its bound, or applicable output is malformed, unknown or unwritten.</exception>
        /// <example><code>System.Console.WriteLine(property.AssignMemberEndOffset(17, OffsetLocation.End, -0.20));</code></example>
        bool AssignMemberEndOffset(int memberId, OffsetLocation location, double offsetY);

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
        bool AssignMemberEndOffset(int[] memberIds, int location, double offsetY);

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
        bool AssignMemberEndOffset(int[] memberIds, OffsetLocation location, double offsetY);

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
        bool DeleteMaterial(string mName);

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
        int CreateBeamPropertyFromTable(Country country, string sectionName, TypeSpecification typeSpec, double additionalSpec1, double additionalSpec2);

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
        int CreateCompositeBeamPropertyFromTable(Country country, string baseStaadName, TypeSpecification typeSpecification, double[] plateValuesMetres);

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
        int CreateBuiltUpBeamProperty(string designation, double[] propertyValuesMetres);

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
        IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams);

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
        IOSProperty AssignBetaAnglesToBeams(HashSet<Beam> beams, int nThreads);

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
        IOSProperty PopulateBetaAngles(IEnumerable<Beam> forBeams);

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
        HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IEnumerable<Beam> allBeams);

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
        HashSet<Beam> GetIsotropicMaterialAssignedBeams(string mName, IOSGeometry geometry);

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
        HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IEnumerable<Plate> allPlates);

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
        HashSet<Plate> GetIsotropicMaterialAssignedPlates(string mName, IOSGeometry geometry);

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
        IOSProperty CreatePlateElementThickness(PlateElementThickness pet);

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
        IOSProperty CreatePlateElementThicknesses(HashSet<PlateElementThickness> petSet);

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
        IOSProperty AssignPlateElementThickness(PlateElementThickness pet);

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
        IOSProperty AssignPlateElementThicknesses(HashSet<PlateElementThickness> petSet);

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
        PlateElementThickness GetPlateThicknessByPlateId(int plateId);

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
        PlateElementThickness GetPlateThicknessByThicknessPropertyId(int petId);

        /// <summary>Reads all native thickness definitions and their four corner values.</summary>
        /// <returns>A new mutable set of independent packets with native definition IDs and empty AssignedPlates sets; empty for an empty inventory.</returns>
        /// <remarks><para>When to use: Use to inspect definitions without hydrating plates. Uses the bounded thickness inventory and four-value read contract, then rereads inventory IDs to reject an observed reorder/replacement. Duplicate inventory IDs retain separate packets as in the reference type&#x27;s identity equality. Other receivers can still make same-ID edits. No model changes, defaults or unit conversion occur.</para><para>Executes synchronously on the caller&#x27;s established COM apartment and shares this adapter&#x27;s serialization and captured-binding checks with typed observations. Reacquire wrapper.Property after refresh. Raw calls, other adapters/wrappers and external edits are not excluded. No automatic analysis, save, retry or rollback occurs. Inputs and native dimensions are not converted. Source compatibility does not certify live native or engineering behavior.</para></remarks>
        /// <exception cref="ArgumentException">An input collection, name, selector, dimension or entity ID is invalid. Validation occurs before the corresponding native operation.</exception>
        /// <exception cref="InvalidOperationException">A required definition is null/empty, the captured binding is stale/disposed, or native status, count, shape or output values violate this method's contract. Direct section-creation methods retain their signed native failure results instead.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier successful writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var pet in property.GetAllPlateElementThicknesses()) System.Console.WriteLine(pet.Id);
        /// </code></example>
        HashSet<PlateElementThickness> GetAllPlateElementThicknesses();

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
        HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IEnumerable<Plate> allPlates);

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
        HashSet<PlateElementThickness> GetAllPlateElementThicknessesWithPopulatedAssignments(IOSGeometry geometry);

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
        OffsetSpecification GetBeamOffsetSpecification(int bId, OffsetLocation location, OffsetAxis offsetAxis);

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
        IOSProperty PopulateMemberOffsets(IEnumerable<Beam> forBeams, OffsetAxis offsetAxis = OffsetAxis.Global);

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
        IOSProperty CreateOffsetSpecification(OffsetSpecification oSpec);

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
        IOSProperty CreateOffsetSpecifications(HashSet<OffsetSpecification> osSet);

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
        IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> osSet);

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
        IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> mpSet);

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
        IOSProperty AssignMaterialProperties(HashSet<MaterialProperty> materialPropertyCollection, int nThreads);

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
        IOSProperty AssignMaterialProperty(MaterialProperty mp, int nThreads);

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
        IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet);

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
        IOSProperty CreateMaterialPropertiesNoConversion(HashSet<MaterialProperty> mpSet);

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
        IOSProperty CreateMaterialProperty(MaterialProperty mp);

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
        IOSProperty CreateMaterialPropertyNoConversion(MaterialProperty mp);

        /// <summary>Reads all extended isotropic material definitions by native zero-based index.</summary>
        /// <returns>A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.</returns>
        /// <remarks><para>When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Retains native strength values. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var material in property.GetAllExtendedIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
        /// </code></example>
        HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialProperties();

        /// <summary>Reads all isotropic material definitions by native zero-based index.</summary>
        /// <returns>A new mutable name-equal set of material packets with actual native names and zero-based Id values; empty for a zero inventory. Case-insensitive duplicate names collapse, retaining the first packet.</returns>
        /// <remarks><para>When to use: Use to enumerate definitions regardless of their use by supplied entities. Bounds the count to 100000 and uses the existing qualified indexed query for every item, checking its native name, constants and count consistency. Rechecks the total count afterward. Correctly retains returned names instead of leaving every indexed packet unnamed as in the source. Basic packets have zero strength placeholders; these are not observations of absent strength fields. Compatibility G/K remain derived; use the qualified indexed query to inspect directly observed G. Assignment sets are initially empty. Same-count external edits cannot be ruled out. No native mutation, inferred units or defaults replace failed reads.</para><para>Executes synchronously on the established caller COM apartment and shares this Property adapter&#x27;s captured-binding/lifetime checks and lock with earlier queries and ports. Reacquire wrapper.Property after refresh. Other receivers, raw calls, callbacks and external STAAD edits are not excluded. Only the explicitly documented legacy material E/G scale is applied; no unit discovery, input-unit change, implicit analysis/save/retry or transactional rollback occurs. Validate behavior and units on the acquired native build before engineering use.</para></remarks>
        /// <exception cref="ArgumentException">An input name, entity, degree, constant, stiffness, selector or required sequence violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">A required creation packet/set is null/empty, the binding is stale/disposed, a resolver returns no packet, or a native status/count/output violates this operation's contract.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original error propagates and earlier writes may remain applied.</exception>
        /// <example><code>
        /// foreach (var material in property.GetAllIsotropicMaterialProperties()) System.Console.WriteLine(material.Name);
        /// </code></example>
        HashSet<MaterialProperty> GetAllIsotropicMaterialProperties();

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
        HashSet<MaterialProperty> GetAllExtendedIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates);

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
        HashSet<MaterialProperty> GetAllIsotropicMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates);

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
        HashSet<MaterialProperty> GetAllExtendedMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates);

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
        HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates);

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
        HashSet<MaterialProperty> GetAllMaterialPropertiesWithPopulatedAssignments(IEnumerable<Beam> forBeams, IEnumerable<Plate> forPlates, Func<string, MaterialProperty> materialResolver);

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
        IOSProperty CreatePlateThicknessProperties(HashSet<PlateThickness> ptSet);

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
        IOSProperty AssignThicknessProperties(HashSet<PlateThickness> tpSet);

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
        IOSProperty AssignThicknessProperties(HashSet<PlateThickness> thicknessPropertyCollection, int nThreads);

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
        IOSProperty AssignThicknessProperty(PlateThickness thicknessProperty, int nThreads);

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
        IOSProperty AssignOffsetSpecification(OffsetSpecification os, int nThreads);

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
        IOSProperty AssignOffsetSpecifications(HashSet<OffsetSpecification> offsetSpecificationCollection, int nThreads);

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
        IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams);

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
        IOSProperty CreateAndAssignOffsetSpecifications(HashSet<Beam> beams, int nThreads);

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
        IOSProperty PopulateMemberReleases(IEnumerable<Beam> forBeams);

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
        IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams);

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
        IOSProperty CreateAndAssignMemberEndReleases(HashSet<Beam> beams, int nThreads);

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
        IOSProperty AssignSectionProperties(HashSet<IGenericSection> gsSet);

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
        IOSProperty AssignSectionProperties(HashSet<SectionProperty> spSet);

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
        IOSProperty AssignSectionProperties(HashSet<SectionProperty> sectionProperties, int nThreads);

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
        IOSProperty AssignSectionProperty(SectionProperty sp);

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
        IOSProperty AssignSectionProperty(SectionProperty sectionProperty, int nThreads);

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
        IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams);

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
        IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams, int nThreads);

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
        IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet, UnitsConverter uc);

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
        IOSProperty CreateMaterialProperty(MaterialProperty mp, UnitsConverter uc);

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
        IOSProperty CreatePlateThicknessProperty(dynamic property, PlateThickness pt);

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
        IOSProperty CreateSectionProperties(HashSet<SectionProperty> spSet);

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
        IOSProperty CreateSectionProperty(SectionProperty sp);

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
        HashSet<IGenericSection> GetAllSections();

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
        HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IEnumerable<Beam> allBeams);

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
        HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IOSGeometry geometry);

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
        double? GetSectionalArea(int propId);

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
        IGenericSection GetSectionBy(int propId);

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
        IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IEnumerable<Beam> allBeams);

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
        IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IOSGeometry geometry);

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
        int GetSectionPropertyCount();

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
        int GetThicknessPropertyCount();

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
        int GetCountofSectionPropertyValuesEx();

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
        int GetSectionPropertyType(int varSecRefNo);

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
        int GetSectionPropertyCountry(int varSecRefNo);

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
        int GetRecordForSection(int Country, string SectionName);

        #endregion
        #region Material and member-section native routes

        /// <summary>Reads the count of orthotropic 3D material definitions.</summary>
        /// <returns>Success with the actual nonnegative count, including zero; the integral response is retained as evidence.</returns>
        /// <remarks>When to use: size or inspect the orthotropic 3D inventory. A count is not an upper bound on native material number IDs and does not establish their indexing. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetOrthotropic3DMaterialCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetOrthotropic3DMaterialCount();

        /// <summary>Reads all eighteen constants of one native orthotropic 3D material.</summary>
        /// <param name="nativeMaterialNumber">Nonnegative native material number ID established by the caller. It is not inferred from the 3D count or a managed zero/one-based list index.</param>
        /// <returns>Success with the exact native name and eighteen owned immutable constants; native null/DBNull/empty name yields NotFound with no Data.</returns>
        /// <remarks>When to use: obtain anisotropic constants without an isotropic E/Poisson-to-G relation. Sends six sentinel double arrays of exactly three slots each, including density, alpha and damping. Every returned array must be rank one, exactly length three, floating-point and finite. Native first/second/third order is retained without guessing physical axis/plane labels. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetOrthotropic3DMaterialProperties(0);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Name);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<Orthotropic3DMaterialProperties> GetOrthotropic3DMaterialProperties(int nativeMaterialNumber);

        /// <summary>Removes a material assignment from one analytical member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -1; retains the requested member ID and actual return code.</returns>
        /// <remarks>When to use: deliberately clear a member material assignment while retaining the material definition. Does not delete a definition or replace the assignment with a default material. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMaterialFromBeam(17);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMaterialFromBeam(int memberId);

        /// <summary>Reads the exact material name assigned to an analytical solid.</summary>
        /// <param name="solidId">Positive analytical solid ID in the acquired model; not a plate/member ID or material number.</param>
        /// <returns>Success with the exact native string. Null/DBNull/empty names yield EmptyOrUnavailable because no-assignment and missing-entity states cannot be distinguished.</returns>
        /// <remarks>When to use: inspect solid material assignment without hydrating solid geometry. Retains whitespace and does not reinterpret the material name as its classification. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetSolidMaterialName(71);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<string> GetSolidMaterialName(int solidId);

        /// <summary>Reads the count of solids assigned to a named isotropic material.</summary>
        /// <param name="materialName">Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.</param>
        /// <returns>Success with the nonnegative native count; zero does not prove the named material exists. Negative/Boolean/fractional counts are malformed.</returns>
        /// <remarks>When to use: inspect assignment size before requesting IDs. The help documents a count, without a separate missing-material code. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetIsotropicMaterialAssignedSolidCount(&quot;STEEL&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetIsotropicMaterialAssignedSolidCount(string materialName);

        /// <summary>Reads a bounded owned snapshot of solid IDs assigned to an isotropic material.</summary>
        /// <param name="materialName">Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.</param>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. A larger native count returns LimitExceeded before allocating/retrieving a list.</param>
        /// <returns>Success with a read-only owned ID list retaining native order and duplicates; zero count gives an empty list without retrieval. False retrieval gives NativeFailure; changed before/after counts give StateChanged.</returns>
        /// <remarks>When to use: enumerate assignment IDs without loading solids. Uses GetIsotropicMaterialAssignedSolidCount before and after GetIsotropicMaterialAssignedSolidList; checks rank-one Int32 shape, exact length and positive written IDs. Every native dispatch rechecks captured receiver binding. Count equality cannot rule out same-count external edits; no model-wide atomic snapshot is promised. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetIsotropicMaterialAssignedSolidIds(&quot;STEEL&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IReadOnlyList<int>> GetIsotropicMaterialAssignedSolidIds(string materialName, int maximumResults = 100000);

        /// <summary>Sets the classification of an existing named isotropic material.</summary>
        /// <param name="materialName">Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.</param>
        /// <param name="materialType">Defined classification: NotSpecified=0, Steel=1, Concrete=2, Aluminum=3 or Timber=4. Unknown enum values are rejected before native access.</param>
        /// <returns>Succeeded for true/integral one, NativeFailure for false/integral zero, with exact name and actual return representation.</returns>
        /// <remarks>When to use: change material classification without creating a material or supplying elastic/design constants. Acknowledgement does not mean that E, G, density or strengths were changed. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.SetTypeToIsotropicMaterial(&quot;STEEL&quot;, StaadPro.Interop.Enums.Property.IsotropicMaterialType.Steel);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult SetTypeToIsotropicMaterial(string materialName, IsotropicMaterialType materialType);

        /// <summary>Reads the native classification of an isotropic material.</summary>
        /// <param name="materialName">Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.</param>
        /// <returns>Success with a defined classification 0 through 4; false/zero gives NotFound. An unwritten or unknown type on success throws.</returns>
        /// <remarks>When to use: inspect classification independently from material constants. Native NotSpecified=0 is successful data, distinct from a false return indicating no such material. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetTypeForIsotropicMaterial(&quot;STEEL&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IsotropicMaterialType> GetTypeForIsotropicMaterial(string materialName);

        /// <summary>Reads the ten native section values for an ASSIGN profile selector.</summary>
        /// <param name="profileType">Defined ASSIGN profile selector: Angle=0, DoubleAngle=1, Beam=2, Column=3 or Channel=4. Not a property reference ID.</param>
        /// <returns>Success for native zero with width/depth, Ax/Ay/Az, native Ix/Iy/Iz and flange/web thickness. -3001 gives NotFound and -6022 NotAssigned; failures have no Data.</returns>
        /// <remarks>When to use: query GetSectionPropertyValues using its documented profile selector, rather than a member or property ID. The older GetSectionPropertyValues(int propId) compatibility method calls the different native GetSectionPropertyValuesEx property-ID route and remains unchanged. All ten scalar outputs must be explicitly written finite floating-point values. Length, area and inertia slots retain native units and names; no physical-axis reinterpretation is inferred. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetSectionPropertiesForProfile(StaadPro.Interop.Enums.Property.AssignedSectionProfile.Beam);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.AxialArea);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<BeamSectionProperties> GetSectionPropertiesForProfile(AssignedSectionProfile profileType);

        /// <summary>Reads the section property reference assigned to a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with a positive native property reference. Native zero gives EmptyOrUnavailable, without distinguishing missing member from missing assignment.</returns>
        /// <remarks>When to use: bridge a member identity to existing property-ID readers. Negative results are undocumented and throw. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetBeamSectionPropertyRefNo(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetBeamSectionPropertyRefNo(int memberId);

        /// <summary>Reads the version-decoded section-value packet directly from a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with an immutable member identity, native type/version, applicable field labels and matching read-only values. False/zero gives NativeFailure with no Data.</returns>
        /// <remarks>When to use: inspect assigned member section parameters without synthesizing a property reference, country, catalog identity or area. Allocates the documented fixed 24-double sentinel buffer, validates its exact rank/type/length, and validates only applicable slots using the reviewed native STAAD 24/25/26 layout selected at execution from wrapper.StaadVersion. Unknown versions/types fail explicitly. Unused padding is excluded. Required nonfinite/unwritten values throw. No extra entity lookup or atomic external-edit guarantee is implied. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetBeamSectionPropertyValuesEx(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativePropertyType);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<MemberSectionValues> GetBeamSectionPropertyValuesEx(int memberId);

        /// <summary>Reads the native section table number for a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with a nonnegative native table number (including zero); -3001 is NotFound, -6004 InvalidReference (profile database section absent), and -6022 NotAssigned.</returns>
        /// <remarks>When to use: inspect table identity for an assigned member. This output is not a country code, property reference ID or section type code. Unknown nonnegative codes are retained. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetSectionTableNo(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetSectionTableNo(int memberId);

        /// <summary>Reads the native section property type code for a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with a positive raw native type code, including unknown positive codes. Zero gives NativeFailure; undocumented negatives throw.</returns>
        /// <remarks>When to use: inspect section type without decoding its value layout. A retained unknown type does not imply layout support in GetBeamSectionPropertyValuesEx. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetBeamSectionPropertyTypeNo(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetBeamSectionPropertyTypeNo(int memberId);

        /// <summary>Reads the exact section name assigned to a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with the exact nonempty native string; null/DBNull/empty yields EmptyOrUnavailable, without guessing whether the member or assignment is missing.</returns>
        /// <remarks>When to use: obtain the native base name, which can be shared by differently modified assignments. Does not construct a catalog designation or parse dimensions from the string. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetBeamSectionName(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<string> GetBeamSectionName(int memberId);

        /// <summary>Reads the exact display section name assigned to a member.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with the exact nonempty native string; null/DBNull/empty yields EmptyOrUnavailable, without guessing whether the member or assignment is missing.</returns>
        /// <remarks>When to use: show the native assignment description, which may include modifiers and spacing. Does not construct a catalog designation or parse dimensions from the string. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetBeamSectionDisplayName(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<string> GetBeamSectionDisplayName(int memberId);

        /// <summary>Reads the native country code associated with a member section.</summary>
        /// <param name="memberId">Positive analytical member ID in the acquired model; not a section property ID, table number or section type.</param>
        /// <returns>Success with a nonnegative raw country code, retaining zero and unknown positive codes. -3001 gives NotFound; -6022 gives NotAssigned.</returns>
        /// <remarks>When to use: inspect the country of an assigned member section. The input is a member ID; the output is a country code, not a section table number. No forced conversion to the repository Country enum occurs. Inspect IsSuccessful and Status before reading Data. Known absence/failure states do not expose default data; malformed values, undocumented status codes and transport failures throw. No conversion, hydration, model write, save or catalog lookup occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// var result = property.GetCountryTableNo(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetCountryTableNo(int memberId);

        /// <summary>Creates an ASSIGN profile section definition.</summary>
        /// <param name="profileType">Defined ASSIGN profile selector: Angle=0, DoubleAngle=1, Beam=2, Column=3 or Channel=4. Not a property reference ID.</param>
        /// <returns>Created with a separate positive CreatedPropertyId on success; zero is NativeFailure and -6008 InvalidInput. The actual native code and requested profile are retained.</returns>
        /// <remarks>When to use: create one of the five ASSIGN profile definitions. Does not assign members or fabricate section dimensions. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateAssignProfileProperty(StaadPro.Interop.Enums.Property.AssignedSectionProfile.Beam);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateAssignProfileProperty(AssignedSectionProfile profileType);

        /// <summary>Assigns an existing element specification to explicit plate IDs.</summary>
        /// <param name="plateIds">Finite sequence of 1 through 100000 positive analytical plate IDs. Enumerated once with a hard bound; order/duplicates retained. Empty/null/invalid sequences are rejected before native writes.</param>
        /// <param name="specificationId">Positive existing native element specification ID; no specification is created or inferred.</param>
        /// <returns>Succeeded for integral zero, InvalidInput for -106 (native array representation rejected), NativeFailure for -6017. Retains requested IDs/specification and actual code.</returns>
        /// <remarks>When to use: deliberately apply an already-created element specification. Sends an owned Int32 array even for one plate; native help requires the array route. There is no per-plate success result, so partial native assignment is possible. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.AssignElementSpecToPlate(new[] { 31, 47 }, 9);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AssignElementSpecToPlate(IEnumerable<int> plateIds, int specificationId);

        /// <summary>Updates model section assignments to their design sections.</summary>
        /// <returns>Succeeded for true/integral one and NativeFailure for false/integral zero, retaining the actual representation and model-wide scope.</returns>
        /// <remarks>When to use: deliberately apply existing design section selections to the acquired model. This is a whole-model mutation; does not run design/analysis or produce a per-member change report. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.UpdatePropertiesToDesignSection();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult UpdatePropertiesToDesignSection();

        /// <summary>Creates or updates an isotropic material with explicit extended strengths.</summary>
        /// <param name="material">Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.</param>
        /// <param name="strengths">Non-null immutable explicit Fy, Fu, Ry, Rt and Fcu packet. All strengths/ratios are finite nonnegative and sent in this native order without conversion.</param>
        /// <returns>Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name and actual code; no material ID is inferred.</returns>
        /// <remarks>When to use: explicitly request all five extended strength fields through the native route. Native acknowledgement does not prove strength persistence; the older obsolete compatibility creator was excluded because persistence was not verified. This method exposes only the native acknowledgement, without a read-back guarantee. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition(&quot;EXPLICIT_MATERIAL&quot;, 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
        ///     var result = property.CreateIsotropicMaterialPropertiesEx(material, new StaadPro.Interop.Models.Property.IsotropicMaterialStrengths(250000.0, 400000.0, 1.1, 1.2, 30000.0));
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateIsotropicMaterialPropertiesEx(IsotropicMaterialDefinition material, IsotropicMaterialStrengths strengths);

        /// <summary>Creates or updates a Steel material with explicit strengths and physical flag.</summary>
        /// <param name="material">Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.</param>
        /// <param name="strengths">Non-null immutable explicit Fu, Fy, Rt and Ry packet. Sent in this native order, which differs from CreateIsotropicMaterialPropertiesEx.</param>
        /// <param name="isPhysical">Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.</param>
        /// <returns>Created for integral zero, Updated for one, NativeFailure for -1, with exact name and actual code; statuses are not material IDs.</returns>
        /// <remarks>When to use: explicitly request Steel creation/classification and strength fields. Does not derive strengths, convert units or certify that all design fields persisted. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition(&quot;EXPLICIT_MATERIAL&quot;, 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
        ///     var result = property.CreateIsotropicMaterialSteel(material, new StaadPro.Interop.Models.Property.SteelMaterialStrengths(400000.0, 250000.0, 1.2, 1.1), false);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateIsotropicMaterialSteel(IsotropicMaterialDefinition material, SteelMaterialStrengths strengths, bool isPhysical);

        /// <summary>Creates or updates a Concrete material with explicit Fcu and physical flag.</summary>
        /// <param name="material">Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.</param>
        /// <param name="compressiveStrength">Finite nonnegative Fcu in the acquired native stress convention. Sent unchanged; no cylinder/cube conversion is inferred.</param>
        /// <param name="isPhysical">Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.</param>
        /// <returns>Created for integral zero, Updated for one, NativeFailure for -1; exact name and native code retained without a fabricated material ID.</returns>
        /// <remarks>When to use: explicitly request Concrete creation and its compressive strength field. No default strength, conversion or strength-persistence guarantee is supplied. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition(&quot;EXPLICIT_MATERIAL&quot;, 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
        ///     var result = property.CreateIsotropicMaterialConcrete(material, 30000.0, false);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateIsotropicMaterialConcrete(IsotropicMaterialDefinition material, double compressiveStrength, bool isPhysical);

        /// <summary>Creates or updates a Aluminum material from explicit common constants.</summary>
        /// <param name="material">Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.</param>
        /// <param name="isPhysical">Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.</param>
        /// <returns>Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name/native code; no material ID is fabricated.</returns>
        /// <remarks>When to use: explicitly request the native Aluminum creator without assuming built-in material defaults or deriving shear modulus. No design strength packet is accepted by this native function. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition(&quot;EXPLICIT_MATERIAL&quot;, 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
        ///     var result = property.CreateIsotropicMaterialAluminum(material, false);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateIsotropicMaterialAluminum(IsotropicMaterialDefinition material, bool isPhysical);

        /// <summary>Creates or updates a Timber material from explicit common constants.</summary>
        /// <param name="material">Non-null immutable definition with explicit name, E, Poisson, G, weight density, thermal expansion and damping in the acquired native creation convention. No defaults, derived G or conversion are applied.</param>
        /// <param name="isPhysical">Explicit native physical-material flag; forwarded unchanged. Does not create physical members or assign entities.</param>
        /// <returns>Created for integral zero, Updated for one, NativeFailure for -1. Retains exact material name/native code; no material ID is fabricated.</returns>
        /// <remarks>When to use: explicitly request the native Timber creator without assuming built-in material defaults or deriving shear modulus. No design strength packet is accepted by this native function. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var material = new StaadPro.Interop.Models.Property.IsotropicMaterialDefinition(&quot;EXPLICIT_MATERIAL&quot;, 200000000.0, 0.3, 76923076.9, 77.0, 0.000012, 0.02);
        ///     var result = property.CreateIsotropicMaterialTimber(material, false);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateIsotropicMaterialTimber(IsotropicMaterialDefinition material, bool isPhysical);

        /// <summary>Assigns an existing named material to explicit analytical solids.</summary>
        /// <param name="materialName">Exact nonblank native material name without NUL characters. Case and whitespace are preserved; no material is created by this input.</param>
        /// <param name="solidIds">Finite sequence of 1 through 100000 positive analytical solid IDs. Enumerated once with a hard bound and copied; empty/null/invalid input is rejected. Order and duplicates are retained.</param>
        /// <returns>Succeeded for true/integral one, NativeFailure for false/integral zero. Retains exact name, owned read-only requested solid IDs and actual return evidence.</returns>
        /// <remarks>When to use: assign solid material directly without entity hydration. Uses the documented native Int32-array route even for a single solid. Does not create the material, change current material context or report per-solid completion; partial assignment can remain. Native success is an acknowledgement only, not persistence read-back or engineering certification. Changes may remain after a native failure, stale-binding exception or transport error. No analysis, implicit save, retry, transaction or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required name, packet, ID, enum, allocation limit or numeric value violates the documented preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is stale/disposed, or native data/status violates the operation-specific contract. Mutations may already have occurred.</exception>
        /// <exception cref="NotSupportedException">A requested native section version/type has no reviewed layout.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no automatic retry, rollback or save is performed.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Set true only for the intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.AssignMaterialToSolid(&quot;STEEL&quot;, new[] { 71, 89 });
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AssignMaterialToSolid(string materialName, IEnumerable<int> solidIds);

        #endregion

        #region Section and specification creation

        /// <summary>Creates a wide-flange table definition using explicit applicable native specifications.</summary>
        /// <param name="definition">Non-null immutable selection created with a named factory: Standard, Double, Tee, Composite, WithTopPlate, WithBottomPlate or WithTopAndBottomPlates. Country codes 1..16, exact native name and applicable numeric inputs are validated before dispatch.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Native -1 gives NativeFailure; zero and undocumented negatives throw.</returns>
        /// <remarks>When to use: create through the four-slot wide-flange native route while retaining ST=0, D=2, T=5, CM=6, TC=7, BC=8 and TB=9. Copies SP/CT/WP, FC/TH, CW/BW, CD/BT in exact order; unused slots are zero buffer padding. BC sends bottom dimensions in slots zero/one with selector eight. Caller must establish the intended current material context beforehand; no material is selected implicitly. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = StaadPro.Interop.Models.Property.WideFlangeTableDefinition.WithBottomPlate(Country.Indian, &quot;ISMB300&quot;, 0.2, 0.01);
        ///     var result = property.CreateWideFlangePropertyFromTable(definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateWideFlangePropertyFromTable(WideFlangeTableDefinition definition);

        /// <summary>Creates a table property with the native solid-shape selector.</summary>
        /// <param name="country">Reviewed native country code 1..37. The source-only Country.Chilean=38 and undefined selectors are rejected; catalog availability is not inferred.</param>
        /// <param name="sectionName">Exact nonblank native table section name without NUL characters. Case/whitespace preserved; no database lookup.</param>
        /// <param name="shape">Defined native selector: PlateStrip=1, SolidRectangle=2, SolidRound=3, Round=4 or Cable=5. Not a TypeSpecification modifier or property ID.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Native zero gives NativeFailure; negative/Boolean/fractional responses throw.</returns>
        /// <remarks>When to use: use the native solid-shape table route, whose TypeSpec declaration is documented as TypeSolidShape in the parameter table. It is different from the existing CreateBeamPropertyFromTable modifier/value route. Caller must establish intended current material context beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateBeamPropertyFromTableEx(Country.Indian, &quot;ISMB300&quot;, StaadPro.Interop.Enums.Property.TableSolidShape.SolidRectangle);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateBeamPropertyFromTableEx(Country country, string sectionName, TableSolidShape shape);

        /// <summary>Creates a rectangular prismatic section from local-axis dimensions.</summary>
        /// <param name="depthY">YD depth along local Y. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="widthZ">ZD width along local Z. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; other nonpositive responses are undocumented and throw.</returns>
        /// <remarks>When to use: create an explicit local-Y/local-Z rectangle without a table or private section catalog. Caller establishes the intended existing current material before creation. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePrismaticRectangleProperty(0.4, 0.2);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePrismaticRectangleProperty(double depthY, double widthZ);

        /// <summary>Creates a circular prismatic section from its explicit diameter.</summary>
        /// <param name="diameter">Circle diameter YD, not radius. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; undocumented responses throw.</returns>
        /// <remarks>When to use: create a circular section without converting diameter to radius or synthesizing section constants. Caller establishes current material beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePrismaticCircleProperty(0.3);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePrismaticCircleProperty(double diameter);

        /// <summary>Creates a tee prismatic section from explicit flange/stem dimensions.</summary>
        /// <param name="totalDepth">YD total depth from top of flange to bottom of stem. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="flangeWidth">ZD flange width. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="stemDepth">YB stem depth; must be smaller than totalDepth. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="stemWidth">ZB stem width; must not exceed flangeWidth. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; undocumented responses throw.</returns>
        /// <remarks>When to use: create a tee using total depth, flange width, stem depth and stem width in exact native order. No flange-thickness or area/inertia input is fabricated. Caller establishes current material beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePrismaticTeeProperty(0.5, 0.3, 0.45, 0.02);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePrismaticTeeProperty(double totalDepth, double flangeWidth, double stemDepth, double stemWidth);

        /// <summary>Creates a trapezoidal prismatic section from explicit depth/top/bottom widths.</summary>
        /// <param name="depth">YD total depth. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="topWidth">ZD width at the top fiber. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="bottomWidth">ZB width at the bottom fiber. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; undocumented responses throw.</returns>
        /// <remarks>When to use: create a trapezoid without interchanging top and bottom widths or deriving an angle from them. Caller establishes current material beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePrismaticTrapezoidalProperty(0.5, 0.2, 0.4);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePrismaticTrapezoidalProperty(double depth, double topWidth, double bottomWidth);

        /// <summary>Creates a general prismatic section from all ten named explicit constants.</summary>
        /// <param name="definition">Non-null immutable AX, AY, AZ, IX, IY, IZ, YD, ZD, YB, ZB packet in this native order. AX is finite positive; other inputs finite nonnegative. AY/AZ zero retains native ignore-shear-deformation semantics. Areas use length squared, inertia/torsion length to fourth power, dimensions length.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; -106/-108 give InvalidInput (native buffer representation/size rejected). Other undocumented responses throw.</returns>
        /// <remarks>When to use: supply complete general-section constants independently from any catalog. Sends a fresh rank-one ten-double buffer. Here native IX is documented as torsional constant; IY/IZ are the local bending inertias. YB/ZB retain the native tee/trapezoid meanings. No area/inertia consistency or engineering-adequacy check is inferred. Caller establishes current material beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.GeneralPrismaticSectionDefinition(0.01, 0.002, 0.003, 0.000001, 0.00001, 0.0001, 0.3, 0.2, 0.25, 0.02);
        ///     var result = property.CreatePrismaticGeneralProperty(definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePrismaticGeneralProperty(GeneralPrismaticSectionDefinition definition);

        /// <summary>Creates a tapered tube with constant explicit wall thickness.</summary>
        /// <param name="shape">Defined native shape: Round=0, Hexadecagonal=1, Dodecagonal=2, Octagonal=3, Hexagonal=4 or Square=5.</param>
        /// <param name="startDepth">Section depth at member start. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="endDepth">Section depth at member end. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="thickness">Constant section thickness; twice this value must be smaller than both endpoint depths. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <returns>Created with a separate positive CreatedPropertyId when STAAD returns a positive reference; the actual integral response and SectionDefinition scope are retained. Zero gives NativeFailure; undocumented responses throw.</returns>
        /// <remarks>When to use: create a tapered tube while preserving native start/end ordering and shape code. Does not reinterpret polygon dimensions as radii or infer endpoint connectivity. Caller establishes current material beforehand. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateTaperedTubeProperty(StaadPro.Interop.Enums.Property.TaperedTubeShape.Round, 0.4, 0.2, 0.01);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateTaperedTubeProperty(TaperedTubeShape shape, double startDepth, double endDepth, double thickness);

        /// <summary>Requests a native partial member release specification with explicit six-DOF inputs.</summary>
        /// <param name="memberEnd">Defined native START=0 or END=1 selector; no member ID or geometry is implied.</param>
        /// <param name="releasedDofs">Non-null immutable FX/FY/FZ/MX/MY/MZ selection. Sent as an owned six-Int32 array: false=0 (no release), true=1 (release); no basic/partial release-mode inference.</param>
        /// <param name="factors">Non-null immutable finite dimensionless six-factor packet in FX/FY/FZ/MX/MY/MZ order. Managed range is zero through one. Every explicit factor is forwarded, including entries for unselected flags; no MP/MPX/MPY/MPZ substitution.</param>
        /// <returns>Succeeded for integral zero; -106/-108 give InvalidInput and -6020 NativeFailure. This reviewed contract returns status only, so CreatedSpecificationId and CreatedSpecificationIndex are always null. Positive returns are undocumented and throw.</returns>
        /// <remarks>When to use: request this distinct native partial-release route. Nearby specification creators return IDs, but this function documents zero OK; do not assign a fabricated zero or discover a last-created ID by count arithmetic. This method creates no analytical-member assignment. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var dofs = new StaadPro.Interop.Models.Property.DegreeOfFreedomSelection(false, false, false, true, true, true);
        ///     var factors = new StaadPro.Interop.Models.Property.PartialReleaseFactors(0, 0, 0, 0.1, 0.2, 0.3);
        ///     var result = property.CreateMemberPartialReleaseSpec(MemberEnd.End, dofs, factors);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberPartialReleaseSpec(MemberEnd memberEnd, DegreeOfFreedomSelection releasedDofs, PartialReleaseFactors factors);

        /// <summary>Creates a native MEMBER TRUSS specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6010 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the MEMBER TRUSS definition independently of assigning any member. An existing specification may be reused by STAAD; no new-unique allocation or analytical behavior is certified. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberTrussSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberTrussSpec();

        /// <summary>Creates a native MEMBER INACTIVE specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6011 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the MEMBER INACTIVE definition independently of assigning any member. An existing specification may be reused by STAAD; no new-unique allocation or analytical behavior is certified. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberInactiveSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberInactiveSpec();

        /// <summary>Creates a native MEMBER TENSION specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6012 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the MEMBER TENSION definition independently of assigning any member. An existing specification may be reused by STAAD; no new-unique allocation or analytical behavior is certified. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberTensionSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberTensionSpec();

        /// <summary>Creates a native MEMBER COMPRESSION specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6013 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the MEMBER COMPRESSION definition independently of assigning any member. An existing specification may be reused by STAAD; no new-unique allocation or analytical behavior is certified. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberCompressionSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberCompressionSpec();

        /// <summary>Creates a native IGNORE STIFFNESS specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6014 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the IGNORE STIFFNESS definition independently of assigning any member. An existing specification may be reused by STAAD; no new-unique allocation or analytical behavior is certified. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberIgnoreStiffSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberIgnoreStiffSpec();

        /// <summary>Creates a cable specification from an explicit tension-or-length value.</summary>
        /// <param name="definitionKind">InitialTension=0 or UnstressedLength=1. This selector changes the dimension of value; no Boolean inversion or unit conversion.</param>
        /// <param name="value">Finite nonnegative initial tension in native force units, or finite positive unstressed length in native length units, according to definitionKind.</param>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6015 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the basic cable definition without advanced tension-end/self-weight options. Does not activate cable analysis or assign a member. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberCableSpec(StaadPro.Interop.Enums.Property.CableDefinitionKind.InitialTension, 25.0);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberCableSpec(CableDefinitionKind definitionKind, double value);

        /// <summary>Creates an advanced cable specification with explicit tension end and global self-weight factors.</summary>
        /// <param name="definitionKind">InitialTension=0 or UnstressedLength=1 as specified by all three reviewed native parameter tables; despite the declaration spelling varbTension, this is sent as an integral selector.</param>
        /// <param name="value">Finite nonnegative tension in native force units, or finite positive unstressed length in native length units.</param>
        /// <param name="tensionEnd">Unspecified=0, Start=1 or End=2 for initial tension. Must be Unspecified for UnstressedLength; not the MemberEnd 0/1 domain.</param>
        /// <param name="selfWeightFactorX">Finite signed dimensionless multiplier for the global-X self-weight component, forwarded unchanged.</param>
        /// <param name="selfWeightFactorY">Finite signed dimensionless multiplier for the global-Y self-weight component, forwarded unchanged.</param>
        /// <param name="selfWeightFactorZ">Finite signed dimensionless multiplier for the global-Z self-weight component, forwarded unchanged.</param>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6015 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the advanced cable definition without changing global vertical axis, enabling advanced analysis or assuming gravity direction. Explicit self-weight multipliers are not forces and are not normalized. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberCableSpecEx(StaadPro.Interop.Enums.Property.CableDefinitionKind.InitialTension, 25.0, StaadPro.Interop.Enums.Property.CableTensionEnd.Start, 0.0, -1.0, 0.0);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberCableSpecEx(CableDefinitionKind definitionKind, double value, CableTensionEnd tensionEnd, double selfWeightFactorX, double selfWeightFactorY, double selfWeightFactorZ);

        /// <summary>Requests a native element-node release using explicit six-DOF flags.</summary>
        /// <param name="nodeId">Positive analytical node number as documented by this native function; not a plate corner index or member-end selector.</param>
        /// <param name="releasedDofs">Non-null immutable FX/FY/FZ/MX/MY/MZ selection. Sent as an owned six-Int32 array: false=0 (no release), true=1 (release); no basic/partial release-mode inference.</param>
        /// <returns>Succeeded for integral zero; -106/-108 give InvalidInput and -6021 NativeFailure. TargetIds retains the requested node. No created specification ID/index is supplied; positive returns are undocumented and throw.</returns>
        /// <remarks>When to use: request the native node release definition without borrowing another creator&#x27;s ID-return convention. No implicit plate assignment or corner/node remapping occurs. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var dofs = new StaadPro.Interop.Models.Property.DegreeOfFreedomSelection(false, false, false, true, true, true);
        ///     var result = property.CreateElementNodeReleaseSpec(11, dofs);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementNodeReleaseSpec(int nodeId, DegreeOfFreedomSelection releasedDofs);

        /// <summary>Creates a native ELEMENT PLANE STRESS specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6018 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the ELEMENT PLANE STRESS definition without assigning analytical elements. Use a separately reviewed assignment route and the returned positive specification ID when applying it. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateElementPlaneStressSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementPlaneStressSpec();

        /// <summary>Creates a native ELEMENT IGNORE INPLANE ROTATION specification definition.</summary>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6019 gives NativeFailure. Zero is undocumented for this ID-returning function and throws rather than being exposed as an ID.</returns>
        /// <remarks>When to use: create the ELEMENT IGNORE INPLANE ROTATION definition without assigning analytical elements. Use a separately reviewed assignment route and the returned positive specification ID when applying it. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateElementIgnoreInplaneRotnSpec();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementIgnoreInplaneRotnSpec();

        /// <summary>Adds a control/dependent-node relation with explicit rigidity selection.</summary>
        /// <param name="controlNodeId">Positive analytical control-node ID. Must not appear in dependentNodeIds.</param>
        /// <param name="mode">AllDirections=0, RigidXY=1, RigidYZ=2, RigidZX=3 or SpecifiedDegreesOfFreedom=4. Four selects the documented other-values branch; no arbitrary native selector is guessed.</param>
        /// <param name="rigidDofs">Non-null immutable explicit FX/FY/FZ/MX/MY/MZ rigidity flags. Sent as six integral 0/1 scalars in that order for every mode; only the explicit-DOF native mode uses them as its selection.</param>
        /// <param name="dependentNodeIds">Finite sequence of 1..100000 positive analytical dependent-node IDs excluding controlNodeId. Enumerated once with a hard bound, copied and sent as a rank-one Int32 array. Input order/duplicates retained; no geometry lookup or deduplication.</param>
        /// <returns>Succeeded for integral zero, InvalidInput for -106 and NativeFailure for -6029. Retains separate control-node identity and owned dependent TargetIds. No specification ID/index or per-node completion result is inferred.</returns>
        /// <remarks>When to use: deliberately add the native relation to the acquired model, rather than creating a free-standing specification for later assignment. A native failure can leave part of the relation applied. The rigidity coordinate meanings follow the native selected mode; no transforms occur. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var dofs = new StaadPro.Interop.Models.Property.DegreeOfFreedomSelection(true, true, true, false, false, false);
        ///     var result = property.AddControlDependentRelation(11, StaadPro.Interop.Enums.Property.ControlDependencyMode.SpecifiedDegreesOfFreedom, dofs, new[] { 12, 13 });
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddControlDependentRelation(int controlNodeId, ControlDependencyMode mode, DegreeOfFreedomSelection rigidDofs, IEnumerable<int> dependentNodeIds);

        /// <summary>Creates a local or global offset specification for one plate corner.</summary>
        /// <param name="coordinateSystem">ElementNodeOffsetAxis.Local=0 or Global=1 only. Z=2 is rejected; this numbering differs from member OffsetAxis.</param>
        /// <param name="plateNodeIndex">Native plate corner index 1..4, not an analytical node ID. The result retains this corner identity.</param>
        /// <param name="offsetX">Finite signed X offset in native length units and the explicit coordinateSystem.</param>
        /// <param name="offsetY">Finite signed Y offset in native length units and the explicit coordinateSystem.</param>
        /// <param name="offsetZ">Finite signed Z offset in native length units and the explicit coordinateSystem.</param>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6016 gives NativeFailure; zero and other undocumented values throw.</returns>
        /// <remarks>When to use: create the per-corner local/global offset definition without transforming coordinates, inventing a plate incidence list or applying the definition to a plate. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateElementOffsetSpec(StaadPro.Interop.Enums.Property.ElementNodeOffsetAxis.Local, 2, 0.0, -0.1, 0.2);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementOffsetSpec(ElementNodeOffsetAxis coordinateSystem, int plateNodeIndex, double offsetX, double offsetY, double offsetZ);

        /// <summary>Creates a four-corner local-Z plate offset specification.</summary>
        /// <param name="node1Offset">Finite signed local-Z offset at native plate corner 1, in native length units. Corner order is retained; no analytical node ID is supplied.</param>
        /// <param name="node2Offset">Finite signed local-Z offset at native plate corner 2, in native length units. Corner order is retained; no analytical node ID is supplied.</param>
        /// <param name="node3Offset">Finite signed local-Z offset at native plate corner 3, in native length units. Corner order is retained; no analytical node ID is supplied.</param>
        /// <param name="node4Offset">Finite signed local-Z offset at native plate corner 4, in native length units. Corner order is retained; no analytical node ID is supplied.</param>
        /// <returns>Created with a separate positive CreatedSpecificationId when STAAD returns a positive ID; the actual integral response and SpecificationDefinition scope are retained. Native -6016 gives NativeFailure; zero and undocumented values throw.</returns>
        /// <remarks>When to use: create an explicit offset for all four native plate corner slots. All four inputs are mandatory; no triangle-corner duplication, uniform-thickness assumption or global/local transformation is inferred. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateElementLocalZOffsetSpec(0.1, 0.2, -0.3, 0.4);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementLocalZOffsetSpec(double node1Offset, double node2Offset, double node3Offset, double node4Offset);

        /// <summary>Creates a native block/contour fireproofing definition and retains its zero-based index.</summary>
        /// <param name="fireProofingType">Defined native Block/BFP=1 or Contour/CFP=2.</param>
        /// <param name="thickness">Fireproofing thickness. Finite positive value in the acquired native length convention; no conversion.</param>
        /// <param name="density">Finite nonnegative fireproofing density in the acquired native density convention; no mass/weight or force/length conversion is inferred.</param>
        /// <returns>Created for any nonnegative integral return, including zero, with CreatedSpecificationIndex set to that exact zero-based index. -1 gives NativeFailure. CreatedSpecificationId is always null; undocumented negatives/Boolean/fractional responses throw.</returns>
        /// <remarks>When to use: create a fireproofing definition without assuming its creation index is the ID or one-based index used by other native getters. Never add one or pass it as an assignment reference without separately qualified mapping. No member assignment or fire-analysis activation occurs. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateMemberFireProofingSpec(StaadPro.Interop.Enums.Property.FireProofingType.Block, 0.03, 12.0);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberFireProofingSpec(FireProofingType fireProofingType, double thickness, double density);

        /// <summary>Reads the exact native unique-ID metadata for a property reference.</summary>
        /// <param name="propertyId">Positive native property reference number, not a member ID or specification index.</param>
        /// <returns>Success with an immutable requested property ID and exact nonempty string. Null/DBNull/empty gives EmptyOrUnavailable because missing metadata and missing property cannot be distinguished. Non-string values throw.</returns>
        /// <remarks>When to use: inspect property metadata without assuming GUID syntax or global uniqueness. Case and whitespace are retained; the string is not parsed or converted into a numerical entity identity. Inspect IsSuccessful/Status before Data. Uses the captured binding/gate; no native mutation, conversion, save or hydration occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// var result = property.GetPropertyUniqueID(37);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Value);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<PropertyUniqueIdentifier> GetPropertyUniqueID(int propertyId);

        /// <summary>Invokes the native void unique-ID metadata setter without fabricating success.</summary>
        /// <param name="propertyId">Positive existing native property reference number, not a member ID or created specification index.</param>
        /// <param name="uniqueId">Exact non-null string without embedded NUL characters. Empty and whitespace strings are intentionally forwarded unchanged; no GUID format, generation, collision check or normalization is imposed.</param>
        /// <returns>InvokedWithoutAcknowledgement after the native void call returns. IsSuccessful remains false because no success acknowledgement was supplied. NativeReturnCode/NativeBooleanReturn and all created identities are null. Requested PropertyId and UniqueId are retained; no persistence read-back is implied.</returns>
        /// <remarks>When to use: deliberately write the exact property metadata string through the reviewed void setter. A completed invocation is not a reported failure, but cannot certify persistence. Call GetPropertyUniqueID separately when read-back is required; this method does not automatically read or overwrite more state. Every call uses the captured Property binding and shared serialization gate, checked before and after access. Native mutations may be partial after a failure or transport/lifetime exception. No retry, implicit save, analysis, unit conversion, assignment, read-back or rollback occurs unless explicitly stated. Native acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, name, ID, bounded sequence or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or the native response violates this operation's reviewed contract; earlier native changes can remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, rollback or save occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for a disposable/intended model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.SetPropertyUniqueID(37, &quot;reviewed-property-identifier&quot;);
        ///     System.Console.WriteLine(result.Status);
        ///     System.Console.WriteLine(result.PropertyId); // Void invocation supplies no success acknowledgement.
        /// }
        /// </code></example>
        PropertyMutationResult SetPropertyUniqueID(int propertyId, string uniqueId);

        #endregion

        #region Fireproofing attributes and user-provided tables

        /// <summary>Reads the native inactive-member count without relying on the gated list transport.</summary>
        /// <returns>Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.</returns>
        /// <remarks>When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetInactiveMemberCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetInactiveMemberCount();

        /// <summary>Reads the native count of fireproofed members.</summary>
        /// <returns>Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.</returns>
        /// <remarks>When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofedBeamCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetFireProofedBeamCount();

        /// <summary>Reads the native count of fireproofing specifications.</summary>
        /// <returns>Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative counts throw.</returns>
        /// <remarks>When to use: inspect this inventory size without deriving IDs or inferring model engineering behavior. A count is not a maximum entity ID or a guarantee of stable contents. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofingSpecCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetFireProofingSpecCount();

        /// <summary>Reads an owned bounded snapshot of native fireproofed member IDs.</summary>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.</param>
        /// <returns>Success with a read-only ID list in native order, retaining duplicates. Zero count gives an empty list without list dispatch. False/zero list flag gives NativeFailure; changed counts give StateChanged; failures have no Data.</returns>
        /// <remarks>When to use: enumerate fireproofed member IDs without loading beam objects. Uses GetFireProofedBeamCount before and after the flag-returning list call, validates exact rank-one Int32 shape/length and positive written IDs. Equal counts cannot exclude same-count external edits. No assumption about specification indices is made. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofedBeamList();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IReadOnlyList<int>> GetFireProofedBeamList(int maximumResults = 100000);

        /// <summary>Reads a member&#x27;s native fireproofing type, thickness and density.</summary>
        /// <param name="memberId">Positive analytical member ID; not a property reference or fireproofing specification index.</param>
        /// <returns>Success for true/integral one with immutable requested member identity and validated fields; false/zero gives NativeFailure without Data. It does not distinguish missing member from no fireproofing.</returns>
        /// <remarks>When to use: inspect one member without resolving a specification index. Requires written native type BFP=1/CFP=2 and finite nonnegative floating thickness/density, including explicit zero. Native density convention is retained without mass/weight assumptions. AssignedMemberCount and SpecificationIndex are null. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofDataForBeam(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Type);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<FireProofingData> GetFireProofDataForBeam(int memberId);

        /// <summary>Reads native fireproofing definition data and its scalar assigned-member count.</summary>
        /// <param name="specificationIndex">Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.</param>
        /// <returns>Success for true/integral one with immutable requested getter index, verified type, finite nonnegative thickness/density and a nonnegative scalar assigned-member count. False/zero gives NativeFailure.</returns>
        /// <remarks>When to use: inspect a fireproofing definition without converting the zero-based creation result into a getter index. The vendor VBA example declares varAssignCount as Long; the array-of-Long wording in one parameter row is a typo. Only a written scalar integral count is accepted; an array is not silently unwrapped. MemberId is null. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofingSpecDetails(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.AssignedMemberCount);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<FireProofingData> GetFireProofingSpecDetails(int specificationIndex);

        /// <summary>Reads the member count assigned to a native fireproofing getter index.</summary>
        /// <param name="specificationIndex">Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.</param>
        /// <returns>Success with the nonnegative native count, including zero. Undocumented negatives/malformed counts throw; no specification-existence flag is inferred from zero.</returns>
        /// <remarks>When to use: inspect assignment size for an explicitly established positive native getter index. It is not an assignment ID or a conversion of CreatedSpecificationIndex. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofingSpecAssignedBeamCount(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetFireProofingSpecAssignedBeamCount(int specificationIndex);

        /// <summary>Reads a bounded owned member-ID list for a native fireproofing getter index.</summary>
        /// <param name="specificationIndex">Positive nonzero-based native fireproofing getter index. Not a specification ID or the zero-based creation index; no +1 conversion is performed.</param>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.</param>
        /// <returns>Success with a read-only native-order ID list, including duplicates; zero count gives empty data without retrieval. False/zero gives NativeFailure, count changes StateChanged, and oversize counts LimitExceeded.</returns>
        /// <remarks>When to use: enumerate a specification&#x27;s assignments without synthesizing a specification ID. Uses the indexed count before/after retrieval and exact rank-one Int32 positive-ID validation. Every dependent dispatch rechecks binding. Count equality does not establish a model-wide atomic snapshot. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetFireProofingSpecAssignedBeamList(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IReadOnlyList<int>> GetFireProofingSpecAssignedBeamList(int specificationIndex, int maximumResults = 100000);

        /// <summary>Creates/updates an exact native member attribute definition.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native member scope. No attribute ID or Created/Updated distinction is fabricated.</returns>
        /// <remarks>When to use: request an exact name/value definition without assuming that zero means a newly allocated ID or distinguishing creation from update. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.CreateMemberAttribute(attribute);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateMemberAttribute(PropertyAttribute attribute);

        /// <summary>Creates/updates an exact native element attribute definition.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native element scope. No attribute ID or Created/Updated distinction is fabricated.</returns>
        /// <remarks>When to use: request an exact name/value definition without assuming that zero means a newly allocated ID or distinguishing creation from update. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.CreateElementAttribute(attribute);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateElementAttribute(PropertyAttribute attribute);

        /// <summary>Deletes an exact native member attribute definition.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native member scope. No attribute ID or Created/Updated distinction is fabricated.</returns>
        /// <remarks>When to use: deliberately remove the exact definition; existing native assignments/effects are subject to STAAD and no recreation/rollback is attempted. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.DeleteMemberAttribute(attribute);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteMemberAttribute(PropertyAttribute attribute);

        /// <summary>Deletes an exact native element attribute definition.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable pair and explicit native element scope. No attribute ID or Created/Updated distinction is fabricated.</returns>
        /// <remarks>When to use: deliberately remove the exact definition; existing native assignments/effects are subject to STAAD and no recreation/rollback is attempted. Does not implicitly assign entities, trim text or convert strings. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.DeleteElementAttribute(attribute);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteElementAttribute(PropertyAttribute attribute);

        /// <summary>Reads the native member count for one exact attribute pair.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Success with an immutable requested pair/domain and nonnegative count, including zero. Native -1 gives NativeFailure. A rewritten by-reference input value gives StateChanged with no Data.</returns>
        /// <remarks>When to use: inspect an attribute subset in the explicit member domain. Vendor metadata passes the value string by reference although the help describes it as input; a local copy is sent and compared ordinally afterward. No data is attributed to the original pair if native rewrites it. Zero does not prove a definition exists. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        /// var result = property.GetMemberCountByAttribute(attribute);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<PropertyAttributeMatchCount> GetMemberCountByAttribute(PropertyAttribute attribute);

        /// <summary>Reads a bounded owned native member ID snapshot for an exact attribute pair.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.</param>
        /// <returns>Success with immutable pair/domain correspondence and read-only positive EntityIds in native order, retaining duplicates. Native -1 gives NativeFailure, oversize count LimitExceeded, and count/input-value changes StateChanged. Zero inventory is successful empty data without list retrieval.</returns>
        /// <remarks>When to use: enumerate matching member IDs without geometry hydration. Uses the matching native count/list/count sequence within the shared gate; the list&#x27;s integral zero is success, not a returned count. Native metadata requires ref string and ref Object outputs; local copies keep caller text immutable. Exact Int32 rank/length and every written positive ID are validated. Count equality cannot exclude same-count external edits. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        /// var result = property.GetMemberListByAttribute(attribute);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.EntityIds.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<PropertyAttributeEntityIds> GetMemberListByAttribute(PropertyAttribute attribute, int maximumResults = 100000);

        /// <summary>Reads the native element count for one exact attribute pair.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <returns>Success with an immutable requested pair/domain and nonnegative count, including zero. Native -1 gives NativeFailure. A rewritten by-reference input value gives StateChanged with no Data.</returns>
        /// <remarks>When to use: inspect an attribute subset in the explicit element domain. Vendor metadata passes the value string by reference although the help describes it as input; a local copy is sent and compared ordinally afterward. No data is attributed to the original pair if native rewrites it. Zero does not prove a definition exists. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        /// var result = property.GetElementCountByAttribute(attribute);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<PropertyAttributeMatchCount> GetElementCountByAttribute(PropertyAttribute attribute);

        /// <summary>Reads a bounded owned native element ID snapshot for an exact attribute pair.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.</param>
        /// <returns>Success with immutable pair/domain correspondence and read-only positive EntityIds in native order, retaining duplicates. Native -1 gives NativeFailure, oversize count LimitExceeded, and count/input-value changes StateChanged. Zero inventory is successful empty data without list retrieval.</returns>
        /// <remarks>When to use: enumerate matching element IDs without geometry hydration. Uses the matching native count/list/count sequence within the shared gate; the list&#x27;s integral zero is success, not a returned count. Native metadata requires ref string and ref Object outputs; local copies keep caller text immutable. Exact Int32 rank/length and every written positive ID are validated. Count equality cannot exclude same-count external edits. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        /// var result = property.GetElementListByAttribute(attribute);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.EntityIds.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<PropertyAttributeEntityIds> GetElementListByAttribute(PropertyAttribute attribute, int maximumResults = 100000);

        /// <summary>Reads the native member-attribute definition count.</summary>
        /// <returns>Success with an actual nonnegative integral count, including zero; malformed/negative counts throw.</returns>
        /// <remarks>When to use: size the paired name/value inventory without assuming attributes have numerical definition IDs or that the count represents assignment count. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetMemberAttributeCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetMemberAttributeCount();

        /// <summary>Reads a bounded owned paired inventory of native member attribute names and values.</summary>
        /// <param name="maximumResults">Allocation limit from 1 through 100000, default 100000. Larger native counts give LimitExceeded before buffer allocation/retrieval.</param>
        /// <returns>Success with read-only immutable pairs in native order, retaining duplicates and empty/whitespace strings. Count/input state changes give StateChanged and allocation excess LimitExceeded. Zero count skips list dispatch.</returns>
        /// <remarks>When to use: enumerate paired metadata without losing name/value correspondence. GetMemberAttributeList returns an integral count, not a success flag; it must equal the preflight count and separate postflight count. Both output buffers must be rank-one String arrays of exactly that size, with every string written and free of NUL. Null sentinels detect unwritten output. No deduplication, case folding or numerical parsing occurs. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetMemberAttributeList();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IReadOnlyList<PropertyAttribute>> GetMemberAttributeList(int maximumResults = 100000);

        /// <summary>Reads the native assigned-attribute count under member-first/plate-fallback identity rules.</summary>
        /// <param name="memberOrPlateId">Positive native integer ID. STAAD resolves an existing member with that ID first, or a plate if no member exists; the wrapper cannot force plate selection when IDs overlap.</param>
        /// <returns>Success with immutable requested integer identity and a nonnegative count, including zero. Resolved geometry kind, assigned-index base and enumeration context are not certified.</returns>
        /// <remarks>When to use: observe assignment count with native identity priority made explicit. No GetAssignedAttributeByIndex call follows automatically: its index context/base remain gated. This getter can affect native observation context, but no model write is requested. All native calls use the shared captured-binding gate with before/after lifetime checks. No native model mutation, geometry hydration, unit conversion, save or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetAssignedAttributeCount(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<AssignedAttributeSummary> GetAssignedAttributeCount(int memberOrPlateId);

        /// <summary>Removes native attribute assignments from explicit ambiguous member/plate IDs.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <param name="memberOrPlateIds">Finite sequence of 1..100000 positive native ambiguous member/plate integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.</returns>
        /// <remarks>When to use: deliberately remove the pair from supplied native identities. The help calls these member IDs while vendor metadata names a member/plate list; the result preserves this ambiguity and does not claim a resolved entity kind. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.RemoveAttribute(attribute, new[] { 17, 41 });
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveAttribute(PropertyAttribute attribute, IEnumerable<int> memberOrPlateIds);

        /// <summary>Assigns an exact native attribute to explicit member IDs.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <param name="memberIds">Finite sequence of 1..100000 positive native member integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.</returns>
        /// <remarks>When to use: deliberately assign the existing pair in the explicitly named native member domain without creating a definition or hydrating entities. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.AssignMemberAttribute(attribute, new[] { 17, 41 });
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AssignMemberAttribute(PropertyAttribute attribute, IEnumerable<int> memberIds);

        /// <summary>Assigns an exact native attribute to explicit element IDs.</summary>
        /// <param name="attribute">Non-null immutable exact attribute name/value pair. Both strings are non-null without NUL; empty/whitespace strings and case are preserved, with no normalization or numeric/GUID parsing.</param>
        /// <param name="elementIds">Finite sequence of 1..100000 positive native element integer IDs. Enumerated once with a hard limit, copied, and sent as an Int32 array even for one ID. Order/duplicates retained; empty/null/invalid input is rejected before native writes.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for -1; retains actual code, immutable requested pair/domain and owned requested TargetIds. No per-entity completion result or created identity is inferred.</returns>
        /// <remarks>When to use: deliberately assign the existing pair in the explicitly named native element domain without creating a definition or hydrating entities. Native failure may leave a partially applied change. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var attribute = new StaadPro.Interop.Models.Property.PropertyAttribute(&quot;Purpose&quot;, &quot; REVIEWED &quot;);
        ///     var result = property.AssignElementAttribute(attribute, new[] { 17, 41 });
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AssignElementAttribute(PropertyAttribute attribute, IEnumerable<int> elementIds);

        /// <summary>Creates a native user-provided table of an explicit reviewed type.</summary>
        /// <param name="tableType">Defined native type 1..10: WideFlange, Channel, Angle, DoubleAngle, Tee, Pipe, Tube, General, ISection or Prismatic. This is not a section property reference or country code.</param>
        /// <returns>Created with a separate positive CreatedTableId for a positive integral native result. -6031 gives InvalidInput; zero/other undocumented responses throw. No section property/specification ID is populated.</returns>
        /// <remarks>When to use: create an empty UPT table independently from adding a named section or assigning members. Requests run on the captured native receiver using the reviewed metadata&#x27;s by-value input, despite C++ VARIANT-reference spelling. No staged workflow/cleanup or table-type lookup is hidden. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateUptTable(StaadPro.Interop.Enums.Property.UserProvidedTableType.WideFlange);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateUptTable(UserProvidedTableType tableType);

        /// <summary>Adds an explicit wide-flange section to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table ID, not an analytical member or section property ID. Caller must establish that the table type supports the requested insertion.</param>
        /// <param name="sectionName">Exact nonblank native UPT section name without NUL. Case/whitespace preserved; no catalog normalization or automatic replacement.</param>
        /// <param name="definition">Non-null immutable AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values in this native order. Required area/dimensions are finite positive; inertias/shear areas finite nonnegative. Areas use length squared, inertia slots length to fourth power, dimensions length. This route documents IZ as torsional constant and IX about local Z; no relabeling from another layout.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045. Retains actual response and requested TableId/SectionName. No created section property reference or table ID is fabricated.</returns>
        /// <remarks>When to use: insert explicit scalar UPT values without silently changing a duplicate named section. Table creation and insertion are separate model mutations; an insertion failure does not remove the table. Vendor metadata accepts inputs by value. Native success does not prove a new definition rather than an accepted insertion and does not assign members. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var result = property.AddUptPropertyWideFlange(7, &quot;EXPLICIT_WF&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyWideFlange(int tableId, string sectionName, UptWideFlangeDefinition definition);

        /// <summary>Adds an explicit unequal wide-flange section through the native twelve-double UPT route.</summary>
        /// <param name="tableId">Positive existing native UPT table ID, not an analytical member or section property ID. Caller must establish that the table type supports the requested insertion.</param>
        /// <param name="sectionName">Exact nonblank native UPT section name without NUL. Case/whitespace preserved; no catalog normalization or automatic replacement.</param>
        /// <param name="definition">Non-null immutable primary AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values followed by explicit WF1 and TF1 in native slots ten/eleven. A fresh exact twelve-double array is sent by value per reviewed vendor metadata. WF1/TF1 retain native second-flange labels without guessing their physical orientation.</param>
        /// <returns>Succeeded for true/integral one and NativeFailure for false/integral zero. Actual Boolean/integral evidence is retained separately with requested TableId/SectionName; no duplicate-specific status or created section ID is inferred.</returns>
        /// <remarks>When to use: insert the unequal shape using its distinct Boolean-returning native route; zero is failure here, unlike the scalar wide-flange route. Input values remain immutable even if a native buffer is altered. Does not create a table, replace a duplicate by fallback, create an assigned property or assign members. Input snapshots are validated before native writes. Native acknowledgement is not persistence read-back; partial changes may remain after failure or a stale-binding/transport exception. No hidden save, retry, transaction, unit conversion or rollback occurs.</remarks>
        /// <exception cref="ArgumentException">A required packet, selector, identity, bounded sequence, text or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed or a native count, flag, status, output buffer or field violates this operation's contract; earlier native changes may remain.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var unequal = new StaadPro.Interop.Models.Property.UptUnequalWideFlangeDefinition(definition, 0.2, 0.015);
        ///     var result = property.AddUptPropertyWideFlangeUnequal(7, &quot;EXPLICIT_UNEQUAL&quot;, unequal);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyWideFlangeUnequal(int tableId, string sectionName, UptUnequalWideFlangeDefinition definition);

        #endregion

        #region User-provided profiles and standard database context

        /// <summary>Adds explicit channel data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptChannelDefinition with every applicable native input supplied. Native order after table/name is AX,D,TW,WF,TF,IZ,IY,IX,CZ,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the channel using its specific scalar native route. Channel CZ is retained in the native convention without guessing a centroid axis/dimension. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var definition = new StaadPro.Interop.Models.Property.UptChannelDefinition(primary, 0.04);
        ///     var result = property.AddUptPropertyChannel(7, &quot;EXPLICIT_CHANNEL&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyChannel(int tableId, string sectionName, UptChannelDefinition definition);

        /// <summary>Adds explicit angle data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptAngleDefinition with every applicable native input supplied. Native order after table/name is D,WF,TF,R,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the angle using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptAngleDefinition(0.2, 0.15, 0.01, 0.03, 0.002, 0.003);
        ///     var result = property.AddUptPropertyAngle(7, &quot;EXPLICIT_ANGLE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyAngle(int tableId, string sectionName, UptAngleDefinition definition);

        /// <summary>Adds explicit double angle data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptDoubleAngleDefinition with every applicable native input supplied. Native order after table/name is D,WF,TF,SP,IZ,IY,IX,CY,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the double angle using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptDoubleAngleDefinition(0.2, 0.15, 0.01, 0.02, 0.000001, 0.00001, 0.0001, 0.04, 0.002, 0.003);
        ///     var result = property.AddUptPropertyDoubleAngle(7, &quot;EXPLICIT_DOUBLEANGLE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyDoubleAngle(int tableId, string sectionName, UptDoubleAngleDefinition definition);

        /// <summary>Adds explicit tee data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptTeeDefinition with every applicable native input supplied. Native order after table/name is AX,D,WF,TF,TW,IZ,IY,IX,CY,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the tee using its specific scalar native route. The tee native order sends WF/TF before TW; it does not reuse the common wide-flange slot order blindly. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var definition = new StaadPro.Interop.Models.Property.UptTeeDefinition(primary, 0.04);
        ///     var result = property.AddUptPropertyTee(7, &quot;EXPLICIT_TEE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyTee(int tableId, string sectionName, UptTeeDefinition definition);

        /// <summary>Adds explicit pipe data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptPipeDefinition with every applicable native input supplied. Native order after table/name is OD,ID,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the pipe using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptPipeDefinition(0.2, 0.18, 0.002, 0.003);
        ///     var result = property.AddUptPropertyPipe(7, &quot;EXPLICIT_PIPE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyPipe(int tableId, string sectionName, UptPipeDefinition definition);

        /// <summary>Adds explicit tube data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptTubeDefinition with every applicable native input supplied. Native order after table/name is AX,D,WF,TF,IZ,IY,IX,AY,AZ. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the tube using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptTubeDefinition(0.01, 0.3, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var result = property.AddUptPropertyTube(7, &quot;EXPLICIT_TUBE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyTube(int tableId, string sectionName, UptTubeDefinition definition);

        /// <summary>Adds explicit general profile data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptGeneralDefinition with every applicable native input supplied. Native order after table/name is AX,D,TD,B,TB,IZ,IY,IX,SZ,SY,AY,AZ,PZ,PY,HSS,DEE. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the general profile using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptGeneralDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.0002, 0.0003, 0.002, 0.003, 0.0004, 0.0005, 0.00000001, 0.28);
        ///     var result = property.AddUptPropertyGeneral(7, &quot;EXPLICIT_GENERAL&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyGeneral(int tableId, string sectionName, UptGeneralDefinition definition);

        /// <summary>Adds explicit I-section data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptISectionDefinition with every applicable native input supplied. Native order after table/name is DWW,TWW,DWW1,BFF,TFF,BFF1,TFF1,AYF,AZF,XIF. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the I-section using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptISectionDefinition(0.4, 0.006, 0.3, 0.2, 0.01, 0.15, 0.01, 0.002, 0.003, 0.000001);
        ///     var result = property.AddUptPropertyISection(7, &quot;EXPLICIT_ISECTION&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyISection(int tableId, string sectionName, UptISectionDefinition definition);

        /// <summary>Adds explicit prismatic profile data to an existing native UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable UptPrismaticDefinition with every applicable native input supplied. Native order after table/name is AX,IZ,IY,IX,AY,AZ,YD,ZD. Values are validated finite and against each constructor&#x27;s documented range; no defaults or dimensional conversion.</param>
        /// <returns>Succeeded for integral zero, NativeFailure for -6032 and AlreadyExists for -6045, with exact requested TableId/SectionName and actual code retained. No section property ID or new table ID is fabricated.</returns>
        /// <remarks>When to use: insert the prismatic profile using its specific scalar native route. Native IZ/IY/IX labels retain this UPT route&#x27;s documented meanings instead of borrowing another section layout. Native AY/AZ zero retains ignore-shear semantics. Caller establishes table-type compatibility; no table is created, duplicate replaced or member assigned. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var definition = new StaadPro.Interop.Models.Property.UptPrismaticDefinition(0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003, 0.3, 0.15);
        ///     var result = property.AddUptPropertyPrismatic(7, &quot;EXPLICIT_PRISMATIC&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyPrismatic(int tableId, string sectionName, UptPrismaticDefinition definition);

        /// <summary>Adds an explicit 12-, 16- or 19-slot composite wide-flange UPT section.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="definition">Non-null immutable twelve-slot steel definition, optionally followed by slab CFR/CFL/CFT/MR and then plate BPR/BPL/BPT. Valid lengths are exactly 12,16,19. A plate without slab is rejected; no engineering zeros are substituted for omitted packets.</param>
        /// <returns>Succeeded for true/integral one and NativeFailure for false/integral zero. Actual Boolean/integral evidence and requested table/name are retained; no insertion ID or duplicate-specific status is inferred.</returns>
        /// <remarks>When to use: insert a supported composite layout without guessing optional-buffer length. Sends a fresh owned double array. CFR/CFL retain documented right/left meanings; BPR/BPL labels retain the help&#x27;s duplicated side-description ambiguity. No automatic table creation, material conversion or staged cleanup occurs. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
        ///     var steel = new StaadPro.Interop.Models.Property.UptUnequalWideFlangeDefinition(primary, 0.2, 0.015);
        ///     var slab = new StaadPro.Interop.Models.Property.UptCompositeSlab(0.5, 0.6, 0.12, 8.0);
        ///     var plate = new StaadPro.Interop.Models.Property.UptCompositeBottomPlate(0.1, 0.11, 0.01);
        ///     var definition = new StaadPro.Interop.Models.Property.UptCompositeWideFlangeDefinition(steel, slab, plate);
        ///     var result = property.AddUptPropertyWideFlangeComposite(7, &quot;EXPLICIT_COMPOSITE&quot;, definition);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult AddUptPropertyWideFlangeComposite(int tableId, string sectionName, UptCompositeWideFlangeDefinition definition);

        /// <summary>Creates an assigned section-property reference from an existing named UPT entry.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <returns>Created with a positive CreatedPropertyId; native -1 gives NativeFailure. TableId/SectionName and actual code are retained separately. Zero and other undocumented nonpositive results throw.</returns>
        /// <remarks>When to use: obtain a property reference after the table and named entry already exist. A positive return does not certify a new globally unique allocation. This native route sends tableId before sectionName and does not insert a section, assign members or synthesize a table lookup. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePropertyFromUptTable(7, &quot;EXPLICIT_WF&quot;);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePropertyFromUptTable(int tableId, string sectionName);

        /// <summary>Obtains a positive section-property reference through the native section-name-first user-table route.</summary>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <returns>Created with separate positive CreatedPropertyId and requested table/name identity. No failure return code is documented: nonpositive/malformed responses throw without an inferred failure mapping.</returns>
        /// <remarks>When to use: use the distinct CreatePropertyFromUserTable route and its native sectionName,tableId order. It does not fall back to CreatePropertyFromUptTable, create the table/entry, assign geometry or establish a new-unique allocation. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreatePropertyFromUserTable(&quot;EXPLICIT_WF&quot;, 7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreatePropertyFromUserTable(string sectionName, int tableId);

        /// <summary>Creates a UPT table using an explicit requested table number and type.</summary>
        /// <param name="requestedTableId">Positive requested new native table number. It is retained in TableId separately from the actual returned CreatedTableId.</param>
        /// <param name="tableType">Defined reviewed UPT type 1..10; not a country, property reference or table inventory index.</param>
        /// <returns>Created for a positive integral native table ID, NativeFailure for zero; undocumented negatives throw. Requested and actual returned table IDs remain separate even if they differ. No section property ID is populated.</returns>
        /// <remarks>When to use: request a specific table number without hiding native renumbering, overwrite behavior or collisions. No table/section deletion, insertion or rollback is performed automatically. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.CreateUptTableEx(7, StaadPro.Interop.Enums.Property.UserProvidedTableType.WideFlange);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult CreateUptTableEx(int requestedTableId, UserProvidedTableType tableType);

        /// <summary>Requests native removal of an explicitly identified UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <returns>Succeeded for true/integral one and NativeFailure for false/integral zero. Actual return representation and requested TableId are retained; no completion report for dependent sections/properties is fabricated.</returns>
        /// <remarks>When to use: deliberately remove the native table only after reviewing its current contents and references. Effects on dependent native data are not inferred or rolled back; no automatic replacement or force-removal fallback is attempted. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveUptTable(7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveUptTable(int tableId);

        /// <summary>Requests deletion of an exact named section from a UPT table.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <returns>Succeeded for integral one, NativeFailure for zero and InvalidInput for -100 (invalid table or name). Actual integral code and requested identity are retained; Boolean and undocumented responses throw.</returns>
        /// <remarks>When to use: deliberately remove one native UPT entry, retaining the table/name identity. Zero is failure here; the scalar insertion zero-success convention is not reused. No dependent-property cleanup, cascade guarantee or force-removal fallback is supplied. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemovePropertyFromUptTable(7, &quot;EXPLICIT_WF&quot;);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult RemovePropertyFromUptTable(int tableId, string sectionName);

        /// <summary>Reads the native user-provided table count.</summary>
        /// <returns>Success with the actual nonnegative integral count, including zero; Boolean/fractional/text/null and undocumented negative returns throw.</returns>
        /// <remarks>When to use: size or inspect UPT inventory without confusing a count with a maximum table ID or an index base. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<int> GetUserProvidedTableCount();

        /// <summary>Reads a bounded owned snapshot of native UPT table IDs.</summary>
        /// <param name="maximumResults">Allocation limit 1..100000, default 100000. An oversize native count gives LimitExceeded before list allocation/retrieval.</param>
        /// <returns>Success with read-only positive IDs in native order, retaining duplicates. Zero inventory skips list retrieval and returns empty data after count recheck. False/zero gives NativeFailure; count changes give StateChanged.</returns>
        /// <remarks>When to use: enumerate table numbers without assuming contiguous or zero/one-based IDs. Uses count/list/count within the shared gate, validates exact rank-one Int32 shape/length and every written positive ID. Equal counts cannot exclude same-count external edits. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableList();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<IReadOnlyList<int>> GetUserProvidedTableList(int maximumResults = 100000);

        /// <summary>Reads a native table&#x27;s section count with explicit table identity.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <returns>Success with an immutable requested TableId and nonnegative Count, including zero. Zero does not prove the table exists; undocumented negatives or malformed counts throw.</returns>
        /// <remarks>When to use: observe table population independently from section-name transport. It does not expose the unverified indexed CSStringList representation or infer a section list from count. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableSectionCount(7);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<UptTableSectionCount> GetUserProvidedTableSectionCount(int tableId);

        /// <summary>Reads the written scalar native UPT section type with count consistency checks.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <returns>Success with requested TableId, a positive raw NativeTypeCode and the checked SectionCount. Count changes yield StateChanged. An empty-count observation without a written positive type yields EmptyOrUnavailable; no default type is exposed.</returns>
        /// <remarks>When to use: observe the singular type value described by native help without treating it as an array. The function&#x27;s integral return is a section count, not a success flag; it must match separate before/after counts. Unknown positive codes are retained but do not imply a supported value layout. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableSectionType(7);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<UptTableSectionType> GetUserProvidedTableSectionType(int tableId);

        /// <summary>Retains the native named-section count with exact requested UPT identity.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <returns>Success with requested table/name and the actual nonnegative NativeCount, including zero. The copied return wording is ambiguous; no engineering value count, buffer size or missing-section status is inferred.</returns>
        /// <remarks>When to use: inspect this native scalar without using it as a memory allocation contract. The help calls the return a number of sections despite the method name. GetUserProvidedTableSectionProperties instead sizes its buffer using GetCountofSectionPropertyValuesEx. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableSectionPropertyCount(7, &quot;EXPLICIT_WF&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeCount);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<UptNamedSectionCount> GetUserProvidedTableSectionPropertyCount(int tableId, string sectionName);

        /// <summary>Reads an owned version-qualified native UPT section-value layout.</summary>
        /// <param name="tableId">Positive existing native UPT table number ID, distinct from member IDs, property references and zero/one-based inventory indices.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <returns>Success for true/integral one with exact requested identity, native type/version/capacity and read-only applicable field names/values. False/zero gives NativeFailure; capacity/version changes give StateChanged. Unreviewed profiles/types fail explicitly.</returns>
        /// <remarks>When to use: decode one of ten reviewed UPT type codes 412..502 without confusing them with common section-property type codes. Reviewed acquired native profiles are exactly 24.00.02.354, 25.00.01.424 and 26.00.00.340. Unreviewed/unknown versions fail before unsafe access. The profile uses wrapper.StaadVersion, never host ApplicationVersion. Native capacity from GetCountofSectionPropertyValuesEx must be 1..100000 and cover every applicable field. All required slots must be explicitly written finite floating-point values; unused padding is excluded, not turned into engineering zeros. Capacity is rechecked and native version frozen before returning. Native labels, inertia identities and units are retained without geometry/area substitution. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableSectionProperties(7, &quot;EXPLICIT_WF&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeTypeCode);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<UptSectionProperties> GetUserProvidedTableSectionProperties(int tableId, string sectionName);

        /// <summary>Reads a table number for an explicitly supplied native UPT inventory index.</summary>
        /// <param name="nativeTableIndex">Nonnegative native table index independently established by the caller. The help example uses one but does not establish an index base; this method sends the exact supplied value without incrementing/decrementing it. It is not automatically a section property reference.</param>
        /// <returns>Success with the exact requested NativeIndex and positive returned TableId; -1 gives NativeFailure. Other nonpositive/malformed results throw.</returns>
        /// <remarks>When to use: use the native index-to-table observation without treating the varSecRefNo declaration spelling as a verified property-ID alias. No enumeration or automatic mapping from another reference domain occurs. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetUserProvidedTableNo(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.TableId);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<UptTableReference> GetUserProvidedTableNo(int nativeTableIndex);

        /// <summary>Reads a raw country-specific section shape code with exact catalog identity.</summary>
        /// <param name="country">Reviewed repository country selector codes 1..37. Source-only Chilean=38 and undefined selectors are rejected; catalog availability is not inferred.</param>
        /// <param name="sectionName">Exact nonblank native section name without NUL characters. Case/whitespace are preserved; no database normalization or automatic rename.</param>
        /// <returns>Success with exact requested country/name and nonnegative raw NativeCode, including zero and unmapped values. -1 gives NativeFailure; other negatives/malformed returns throw.</returns>
        /// <remarks>When to use: inspect the native code without coercing different countries&#x27; shape domains into a universal enum or asserting a supported engineering family. No catalog section or geometry is synthesized from the integer. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetShapeCode(Country.Indian, &quot;ISMB300&quot;);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeCode);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<SectionShapeCode> GetShapeCode(Country country, string sectionName);

        /// <summary>Requests a standard-profile database folder change under an exact native return-policy profile.</summary>
        /// <param name="folderPath">Exact non-null path text without NUL. Empty/whitespace/relative text is forwarded unchanged for native validation; no directory creation, existence check or path normalization.</param>
        /// <returns>Succeeded/NativeFailure according to the exact acquired profile: 2024 uses 1/0, 2025/2026 use 0/-1. Retains actual integral response, exact FolderPath and immutable NativeContractVersion. Boolean/undocumented responses throw.</returns>
        /// <remarks>When to use: deliberately change the acquired application&#x27;s profile database context, after reviewing effects on subsequent catalog operations. Reviewed acquired native profiles are exactly 24.00.02.354, 25.00.01.424 and 26.00.00.340. Unreviewed/unknown versions fail before unsafe access. The profile uses wrapper.StaadVersion, never host ApplicationVersion. Unknown servicing profiles fail before native dispatch. No default-folder reset, backup, implicit reloading or restoration is performed; a version change during the mutation throws and effects may remain. Uses the captured receiver and shared serialization gate on the caller&#x27;s thread. Native changes may remain after failure or a stale-binding/transport exception. No automatic assignment, analysis, conversion, hidden save, retry or rollback occurs. An acknowledgement does not certify persistence or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed inputs.
        /// if (applyChanges)
        /// {
        ///     var result = property.SetStandardProfileDBFolder(@&quot;C:\STAAD\Profiles&quot;);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.NativeFunction);
        /// }
        /// </code></example>
        PropertyMutationResult SetStandardProfileDBFolder(string folderPath);

        /// <summary>Reads the exact native standard-profile database folder path.</summary>
        /// <returns>Success with the exact nonempty string, preserving case/whitespace. Null/DBNull/empty gives EmptyOrUnavailable; non-string/NUL text throws. No path-existence guarantee is inferred.</returns>
        /// <remarks>When to use: observe current catalog context without changing it or substituting a default folder. No filesystem access, normalization or directory creation occurs. Uses the shared captured-binding gate; dependent dispatches check lifetime individually. Inspect IsSuccessful/Status before Data. No model mutation, geometry hydration, private catalog lookup, unit conversion or engineering certification occurs.</remarks>
        /// <exception cref="ArgumentException">A required definition, selector, identity, text, allocation limit or numeric value violates the documented managed preconditions.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or native status, count, capacity or required output is malformed; earlier native effects may remain.</exception>
        /// <exception cref="NotSupportedException">The acquired native profile or requested UPT layout has no reviewed contract. No guessed version/status/layout fallback is used.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; no implicit retry, save or rollback occurs.</exception>
        /// <example><code>
        /// var result = property.GetStandardProfileDBFolder();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);
        /// </code></example>
        PropertyQueryResult<string> GetStandardProfileDBFolder();

        #endregion

        /// <summary>Remove Thickness Property From the specific Plate.</summary>
        /// <param name="plateId">Positive existing native analytical plate ID.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemovePropertyFromPlate(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemovePropertyFromPlate(int plateId);
        /// <summary>Remove Material From the specific Plates.</summary>
        /// <param name="plateIds">Non-null sequence of 1..100000 positive analytical IDs, enumerated once; input order and duplicates are preserved in an owned copy. No inferred member/plate/solid conversion or deduplication.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMaterialFromPlate(new[] { 101, 102 });
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMaterialFromPlate(IEnumerable<int> plateIds);
        /// <summary>Remove Material From the specific Solids.</summary>
        /// <param name="solidIds">Non-null sequence of 1..100000 positive analytical IDs, enumerated once; input order and duplicates are preserved in an owned copy. No inferred member/plate/solid conversion or deduplication.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMaterialFromSolid(new[] { 101, 102 });
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMaterialFromSolid(IEnumerable<int> solidIds);
        /// <summary>Delete MEMBER RELEASE specification.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <param name="memberEnd">Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.DeleteMemberReleaseSpec(101, StaadPro.Interop.Enums.MemberEnd.Start);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteMemberReleaseSpec(int memberId, MemberEnd memberEnd);
        /// <summary>Delete all control/dependent joint specifications from model.</summary>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Applies to all native control/dependent relations in the acquired model; no selective filter, confirmation dialog or implicit undo is supplied. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.DeleteAllControlDependentRelations();
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteAllControlDependentRelations();
        /// <summary>Remove all element node offset specification from the model.</summary>
        /// <returns>Succeeded for integral one; NoSpecification for integral zero, which explicitly reports that no element offset specification is present. IsSuccessful is false for NoSpecification; no success is fabricated.</returns>
        /// <remarks>When to use: Applies to all element offsets in the acquired model; zero is documented absence, not a failed engineering read. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveAllElementOffsetSpec();
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveAllElementOffsetSpec();
        /// <summary>Remove property from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemovePropertyFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemovePropertyFromBeam(int memberId);
        /// <summary>Delete property based the on the property ID passed.</summary>
        /// <param name="propertyId">Positive existing native section property reference, distinct from analytical entity IDs.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Deletes a definition using its native ID; do not confuse this with merely removing one assignment. Native dependencies may prevent deletion. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.DeleteProperty(7);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteProperty(int propertyId);
        /// <summary>Delete specification based on the specification number passed.</summary>
        /// <param name="specificationId">Positive existing native specification definition ID, distinct from assigned analytical member IDs.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Deletes a definition using its native ID; do not confuse this with merely removing one assignment. Native dependencies may prevent deletion. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.DeleteMemberSpec(7);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult DeleteMemberSpec(int specificationId);
        /// <summary>Removes the member specification from a particular member at the provided location (Start or End)..</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <param name="memberEnd">Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberReleaseSpecFromBeam(101, StaadPro.Interop.Enums.MemberEnd.Start);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberReleaseSpecFromBeam(int memberId, MemberEnd memberEnd);
        /// <summary>Removes the member offset specification from a particular member at the provided location (Start or End).</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <param name="memberEnd">Native Start=0 or End=1 selector. Other values are rejected; no both-ends or whole-member default is inferred.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberOffsetSpecFromBeam(101, StaadPro.Interop.Enums.MemberEnd.Start);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberOffsetSpecFromBeam(int memberId, MemberEnd memberEnd);
        /// <summary>Remove member truss specification from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberTrussSpecFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberTrussSpecFromBeam(int memberId);
        /// <summary>Remove member inactive specification from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberInactiveSpecFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberInactiveSpecFromBeam(int memberId);
        /// <summary>Remove member tension specification from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberTensionSpecFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberTensionSpecFromBeam(int memberId);
        /// <summary>Remove member compression specification from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberCompressionSpecFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberCompressionSpecFromBeam(int memberId);
        /// <summary>Remove member ignore stiff specification from beam.</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberIgnoreStiffSpecFromBeam(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberIgnoreStiffSpecFromBeam(int memberId);
        /// <summary>Removes the member cable specification from a particular member at the provided location type (Tension or Length).</summary>
        /// <param name="memberId">Positive existing native analytical member ID.</param>
        /// <param name="definitionKind">Native InitialTension=0 or UnstressedLength=1 cable-definition selector. This is not a member-end location despite the native parameter label.</param>
        /// <returns>Succeeded for actual Boolean true or integral one; NativeFailure for Boolean false or integral zero. Other scalar shapes/codes are rejected. The actual native Boolean/integer response and requested target identity are retained separately.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveMemberCableSpecFromBeam(101, StaadPro.Interop.Enums.Property.CableDefinitionKind.InitialTension);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveMemberCableSpecFromBeam(int memberId, CableDefinitionKind definitionKind);
        /// <summary>Remove element plane stress specification from plate.</summary>
        /// <param name="plateId">Positive existing native analytical plate ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveElementPlaneStressSpecFromPlate(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveElementPlaneStressSpecFromPlate(int plateId);
        /// <summary>Remove element ignore in plane rotation specification from plate.</summary>
        /// <param name="plateId">Positive existing native analytical plate ID.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveElementIgnoreInplaneRotnSpecFromPlate(101);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveElementIgnoreInplaneRotnSpecFromPlate(int plateId);
        /// <summary>Remove element node release specification from plate.</summary>
        /// <param name="plateId">Positive existing native analytical plate ID.</param>
        /// <param name="nodeId">Positive analytical node ID associated with the intended plate. This is not a plate corner index; no membership check or local/global conversion is inferred.</param>
        /// <returns>Succeeded for integral zero and NativeFailure for integral -1. Boolean, floating/string/null and undocumented integral responses are rejected.</returns>
        /// <remarks>When to use: Removes the specified assignment/specification using the named native route; it does not imply deletion of a shared section/material/specification definition. Uses the captured Property receiver and shared serialization gate on the caller thread. Invalid input is rejected before native mutation. No save, analysis, retry, implicit assignment or rollback is added. Failure or a transport/stale-binding exception can leave partial native changes. A successful acknowledgement does not establish persistence, definition deletion, model-wide atomicity or engineering adequacy.</remarks>
        /// <exception cref="ArgumentException">An ID, bounded input sequence or selector is invalid; no native mutation is made.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// bool applyChanges = false; // Enable only for the intended/disposable model and reviewed targets.
        /// if (applyChanges)
        /// {
        ///     var result = property.RemoveElementNodeReleaseSpecFromPlate(101, 11);
        ///     System.Console.WriteLine(result.Status);
        /// }
        /// </code></example>
        PropertyMutationResult RemoveElementNodeReleaseSpecFromPlate(int plateId, int nodeId);
        /// <summary>Gets the captured Property capability acquisition boundary without invoking catalog operations.</summary>
        /// <value>A version/signature-qualified acquisition interface for newer catalog functions.</value>
        /// <remarks>Acquire GetStandardCatalog before calling newer APIs; the common adapter does not implement that optional interface.</remarks>
        IOSPropertyCapabilities Capabilities { get; }

        #region Remaining release, fireproofing and profile-name operations

        /// <summary>Removes every element-node release specification from the acquired model.</summary>
        /// <returns>A model-scoped acknowledgement: Succeeded for native 1/true, or NoSpecification for 0/false. NoSpecification means that no release was present and is not a fabricated successful mutation.</returns>
        /// <remarks><para>When to use: remove all element-node release specifications in one explicit model-wide operation. RemoveElementNodeReleaseSpecFromPlate targets a particular plate and analytical node instead; DeleteMemberReleaseSpec concerns a member end. This operation does not remove member releases or element offsets. It can affect many assignments; native failure or a transport exception may follow partial changes. No implicit save, retry, rollback, analysis or geometry selection occurs.</para><para>Uses the common installed 2024–2026 contract and the captured Property receiver. Calls are synchronous on the caller’s established COM apartment and share the adapter gate. Other adapters, raw calls and external edits remain outside that gate. Reacquire wrapper.Property after RefreshComObjects. Stale or disposed bindings are rejected before access and checked again after return. Live native and engineering qualification remains required.</para></remarks>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or native data has an undocumented status, invalid type or malformed text.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks the required compatible native member.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = property.RemoveAllElementNodeReleaseSpec();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        PropertyMutationResult RemoveAllElementNodeReleaseSpec();

        /// <summary>Removes the fireproofing specification assigned to one analytical member.</summary>
        /// <param name="memberId">Positive analytical member ID, not a fireproofing specification index or specification definition ID.</param>
        /// <returns>A member-scoped result retaining the requested ID and actual native acknowledgement. Native 1/true is Succeeded; 0/false is NativeFailure and does not prove that no specification existed.</returns>
        /// <remarks><para>When to use: remove fireproofing from one member without deleting its section, material, release or offset specification. GetFireProofDataForBeam observes the existing assignment; GetFireProofingSpecDetails takes a zero-based specification index and does not identify a member. No specification index is derived from memberId. This mutation does not recompute design results or save the model, and partial native effects are not rolled back.</para><para>Uses the common installed 2024–2026 contract and the captured Property receiver. Calls are synchronous on the caller’s established COM apartment and share the adapter gate. Other adapters, raw calls and external edits remain outside that gate. Reacquire wrapper.Property after RefreshComObjects. Stale or disposed bindings are rejected before access and checked again after return. Live native and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException">memberId is not positive.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or native data has an undocumented status, invalid type or malformed text.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks the required compatible native member.</exception>
        /// <example><code>bool allowNativeMutation = false;
        /// if (allowNativeMutation)
        /// {
        ///     var result = property.RemoveMemberFireProofingSpecFromBeam(17);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        PropertyMutationResult RemoveMemberFireProofingSpecFromBeam(int memberId);

        /// <summary>Looks up a published catalog profile name from its STAAD profile name and country table.</summary>
        /// <param name="staadProfileName">Exact nonblank STAAD catalog profile name without NUL characters; case and whitespace are preserved. This is not a model property ID or UPT section index.</param>
        /// <param name="country">Existing Country selector in the documented range 1 through 37. Country.Chilean (38) is not included in the reviewed table.</param>
        /// <returns>The exact returned published name on Success. A null, DBNull or empty native name produces EmptyOrUnavailable without a data payload; non-string or embedded-NUL responses are rejected.</returns>
        /// <remarks><para>When to use: translate an existing STAAD catalog label to its published label for a specified country. GetStaadProfileName performs the reverse lookup. These mappings are not assumed to be one-to-one or reversible, and this query does not publish a section, create a property, search model assignments or change the database folder. Native text is preserved without trimming, case changes, shape inference or unit conversion.</para><para>Uses the common installed 2024–2026 contract and the captured Property receiver. Calls are synchronous on the caller’s established COM apartment and share the adapter gate. Other adapters, raw calls and external edits remain outside that gate. Reacquire wrapper.Property after RefreshComObjects. Stale or disposed bindings are rejected before access and checked again after return. Live native and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">The profile-name input is null.</exception>
        /// <exception cref="ArgumentException">The profile-name input is blank or contains a NUL character.</exception>
        /// <exception cref="ArgumentOutOfRangeException">country is outside the documented range 1 through 37.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or native data has an undocumented status, invalid type or malformed text.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks the required compatible native member.</exception>
        /// <example><code>var result = property.GetPublishedProfileName("W12X26", Country.American);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        PropertyQueryResult<string> GetPublishedProfileName(string staadProfileName, Country country);

        /// <summary>Looks up a STAAD catalog profile name from its published name and country table.</summary>
        /// <param name="publishedProfileName">Exact nonblank published catalog profile name without NUL characters; case and whitespace are preserved. This is not a file path or a model section reference.</param>
        /// <param name="country">Existing Country selector in the documented range 1 through 37. Country.Chilean (38) is not included in the reviewed table.</param>
        /// <returns>The exact returned STAAD profile name on Success. A null, DBNull or empty native name produces EmptyOrUnavailable without a data payload; malformed text or non-string returns are rejected.</returns>
        /// <remarks><para>When to use: translate a published catalog label to the corresponding STAAD label. GetPublishedProfileName performs the opposite lookup. The translation uses the acquired native catalog and explicit country; it does not create an ASSIGN/UPT section, hydrate a SectionProperty object, alter current material or certify a round trip. Native names and country identity are preserved, and no model mutation or unit conversion occurs.</para><para>Uses the common installed 2024–2026 contract and the captured Property receiver. Calls are synchronous on the caller’s established COM apartment and share the adapter gate. Other adapters, raw calls and external edits remain outside that gate. Reacquire wrapper.Property after RefreshComObjects. Stale or disposed bindings are rejected before access and checked again after return. Live native and engineering qualification remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">The profile-name input is null.</exception>
        /// <exception cref="ArgumentException">The profile-name input is blank or contains a NUL character.</exception>
        /// <exception cref="ArgumentOutOfRangeException">country is outside the documented range 1 through 37.</exception>
        /// <exception cref="InvalidOperationException">The binding is stale/disposed, or native data has an undocumented status, invalid type or malformed text.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks the required compatible native member.</exception>
        /// <example><code>var result = property.GetStaadProfileName("W12X26", Country.American);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        PropertyQueryResult<string> GetStaadProfileName(string publishedProfileName, Country country);

        #endregion
    }
}
