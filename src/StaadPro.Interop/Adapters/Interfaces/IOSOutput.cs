using System.Collections.Generic;
using StaadPro.Interop.Entities;
using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Output;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides qualified, strongly typed analysis/design result observations from the acquired Output object.</summary>
    /// <remarks>All methods preserve native units and run on the caller's COM apartment. Status/data results distinguish native failure from valid false availability observations.</remarks>
    public interface IOSOutput : IOSBase
    {
        /// <summary>Gets the captured native Output automation binding.</summary>
        /// <value>The acquired raw object; direct use bypasses managed validation and lifetime checks.</value>
        dynamic ComObject { get; }

        /// <summary>Observes whether native analysis results are available.</summary>
        /// <returns>A successful observation containing the native availability Boolean. False is valid data, not query failure. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>No solver is started and no result freshness or completed-analysis guarantee is inferred. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetAnalysisResultAvailability();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<bool> GetAnalysisResultAvailability();

        /// <summary>Retrieves all six translations and rotations for one node and load case.</summary>
        /// <param name="nodeId">Positive native node number, not a collection index.</param>
        /// <param name="loadCaseId">Positive native result load-case number; case suitability is decided by STAAD.</param>
        /// <returns>Status and an owned X/Y/Z/Rx/Ry/Rz snapshot; native false exposes no displacement data. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Forwards GetNodeDisplacements once with a six-Double sentinel buffer. Native result length/angular units are preserved without conversion. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetNodeDisplacements(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.X);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<NodeDisplacements> GetNodeDisplacements(int nodeId, int loadCaseId);

        /// <summary>Retrieves the six global support-reaction components at one node.</summary>
        /// <param name="nodeId">Positive supported-node number in the acquired model.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and an immutable FX/FY/FZ/MX/MY/MZ reaction snapshot. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Reactions retain native global axes, force/moment units and signs. They are not negated into actions applied by the support. A six-Double output is required on success. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetSupportReactions(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<ForceComponents> GetSupportReactions(int nodeId, int loadCaseId);

        /// <summary>Retrieves six local member-end actions for one member end and load case.</summary>
        /// <param name="memberId">Positive native analytical member number.</param>
        /// <param name="memberEnd">Start (native 0) or End (native 1).</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and an owned FX/FY/FZ/MX/MY/MZ local action snapshot, with requested member/case/end identity. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Calls GetMemberEndForces once with native end 0 or 1 and the final local-coordinate flag 0. No face/sign reversal, offset transfer or global conversion is performed. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForces(17, MemberEnd.Start, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<ForceComponents> GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId);

        /// <summary>Retrieves applied-load and reaction totals for a static-check observation.</summary>
        /// <param name="loadCaseId">Positive native load-case number for the static-check totals.</param>
        /// <returns>Status and two independently validated six-component snapshots, Forces and Reactions. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Both buffers must be completely written and finite before data is published. Native signs and reference-point conventions are retained; no assumed residual or equilibrium check is computed. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetStaticCheckResult(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Forces.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<StaticCheckResult> GetStaticCheckResult(int loadCaseId);

        /// <summary>Retrieves eight plate-center stress and moment-per-width components.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and named stress/moment groups copied from SQX/SQY/MX/MY/MXY/SX/SY/SXY. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Uses one GetAllPlateCenterStressesAndMoments call, not several independent queries. Components remain in plate-local axes; stress and moment-per-width dimensions are kept separate. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterStressesAndMoments(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Forces.Sx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterStressesAndMoments> GetPlateCenterStressesAndMoments(int plateId, int loadCaseId);

        /// <summary>Retrieves top and bottom maximum/minimum normal principal stresses.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and four named stresses in top-maximum, top-minimum, bottom-maximum, bottom-minimum order. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>All four native scalar outputs must be written and finite after native success. Surface identity and native stress units are retained. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterNormalPrincipalStresses(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.MaximumTop);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlatePrincipalStresses> GetPlateCenterNormalPrincipalStresses(int plateId, int loadCaseId);

        /// <summary>Retrieves the five plate-center stress components returned by GetAllPlateCenterForces.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and named SQX/SQY/SX/SY/SXY stresses. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Despite the native function and source model names, these are plate-local stresses, not concentrated force-vector components. The five-Double buffer is validated and copied. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterForces(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Sx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterForces> GetPlateCenterForces(int plateId, int loadCaseId);

        /// <summary>Retrieves local plate-center moments per unit width.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and named MX/MY/MXY values in native moment-per-width units. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Forwards GetAllPlateCenterMoments once using a three-Double buffer. Does not convert these values to concentrated nodal moments. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterMoments(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Mx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterMoments> GetPlateCenterMoments(int plateId, int loadCaseId);

        /// <summary>Retrieves six principal/shear stresses and two principal directions using the combined-buffer API.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and top/bottom maximum, minimum, maximum-shear stresses followed by top/bottom principal angles. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Initializes eight Double values as shown by the installed VBA example; the parameter prose says float. Accepts a returned Single or Double SAFEARRAY. Native angular units are preserved. No automatic retry through the separate-buffer API occurs. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterPrincipalStressesAndAngles(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.AngleTop);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlatePrincipalStressesAndAngles> GetPlateCenterPrincipalStressesAndAngles(int plateId, int loadCaseId);

        /// <summary>Retrieves principal stresses and directions using the explicit separate-buffer native API.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and the same named eight-value payload as the combined-buffer method. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Calls GetAllPlateCenterPrincipalStressesAndAnglesEx once with six-stress and two-angle Double buffers as shown in the installed VBA example, accepting returned Single or Double arrays. Both outputs must validate. This is an explicit alternative, not a failure fallback. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterPrincipalStressesAndAnglesEx(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.AngleTop);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlatePrincipalStressesAndAngles> GetPlateCenterPrincipalStressesAndAnglesEx(int plateId, int loadCaseId);

        /// <summary>Retrieves top and bottom Von Mises stresses at a plate center.</summary>
        /// <param name="plateId">Positive native plate element number.</param>
        /// <param name="loadCaseId">Positive native result load-case number.</param>
        /// <returns>Status and named Top/Bottom stress values, preserving native units and surface order. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Both scalar outputs must be written and finite on native success. No averaging, sign change or stress-unit conversion is applied. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCenterVonMisesStresses(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Top);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterVonMises> GetPlateCenterVonMisesStresses(int plateId, int loadCaseId);

        /// <summary>Retrieves the steel-design section name reported for one member.</summary>
        /// <param name="memberId">Positive native member number whose design result is requested.</param>
        /// <returns>Success with exact nonempty native text, or NotFound when STAAD returns an empty string. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Does not substitute the assigned section, trim the reported name or infer that design passed. A null/non-string native response is malformed and throws. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberDesignSectionName(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<string> GetMemberDesignSectionName(int memberId);

        /// <summary>Retrieves the critical steel-design utilization ratio for one member.</summary>
        /// <param name="memberId">Positive native analytical member number.</param>
        /// <returns>Success with a finite nonnegative ratio; MemberNotDesigned for -999, AnalysisNotPerformed for -1, or NativeFailure for native false. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Only a successful native call interprets sentinel outputs. Sentinels are never exposed as engineering ratios. A ratio above one is valid data, not a query failure. No beam entity is mutated and no design pass/fail is inferred. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">An identifier is nonpositive or memberEnd is undefined.</exception>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberSteelUtilizationRatio(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<double> GetMemberSteelUtilizationRatio(int memberId);

        /// <summary>Observes whether native buckling-analysis results are available.</summary>
        /// <returns>A successful Boolean observation; false means buckling results are unavailable. Inspect IsSuccessful before accessing Data.</returns>
        /// <remarks>Uses IsBucklingAnalysisResultsAvailable. Accepts Boolean values or exact integer 0/1; unsupported numeric/status shapes throw. No buckling analysis is started. Calls are synchronous on the caller's COM apartment. Serialize model access; do not move the call to Task.Run.
        /// Common installed 2024–2026 contract; live marshaling, units and engineering behavior still require qualification for the acquired build.
        /// Wrapper input-unit caches do not certify Output units. This query does not activate a case, run analysis, save or mutate the model.</remarks>
        /// <exception cref="System.InvalidOperationException">The captured binding is stale/disposed, the status is unsupported, or a successful response is malformed/unwritten/nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates.</exception>
        /// <example><code>var result = wrapper.Output.GetBucklingResultAvailability();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<bool> GetBucklingResultAvailability();

        #region Member, solid, modal and time-history observations

        /// <summary>Reads the complete single-block steel-design result packet for one analytical member.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Prefer this query when code, decision, governing case/station/clause/section, allowable ratio, FX/MY/MZ and slenderness are needed. GetMemberSteelUtilizationRatio returns only a ratio. PASS/FAIL text is classified explicitly; numeric, Boolean or other status variants are preserved as Unknown rather than guessed. The three unreturned FY/FZ/MX actions are unavailable and are never set to zero. Governing case IDs are not hydrated or activated. Native false takes precedence over any written outputs.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMemberSteelDesignResults(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.DesignStatus);</code></example>
        OutputQueryResult<MemberSteelDesignResults> GetMemberSteelDesignResults(int memberId);

        /// <summary>Counts the native eigen-buckling factors already extracted.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: This returns an actual integral count, not a success flag or a factor value. Zero is a valid count and does not prove that another result family is unavailable. GetBucklingResultAvailability observes availability; GetBucklingModeDisplacements retrieves one shape. No eigenanalysis is started.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetBucklingFactorCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<int> GetBucklingFactorCount();

        /// <summary>Reads all six native eigen-buckling mode-shape components at one node.</summary>
        /// <param name="nativeBucklingModeNumber">Nonnegative native eigen-buckling mode number/index, passed unchanged and not mapped to a dynamic mode.</param>
        /// <param name="nodeId">Positive native analytical node ID, not an array index.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for a buckling mode shape, not static displacement or a scalar buckling factor. The native mode number/index is passed unchanged; no zero/one-based conversion or count-derived remapping occurs. Six initialized Double slots are passed by reference and must be written on native 1/true success. Mode-shape normalization is not converted into physical static length/rotation units. Native numeric -1 is not accepted as this operation’s documented 1/0 flag.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetBucklingModeDisplacements(1, 11);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Translation);</code></example>
        OutputQueryResult<ModeDisplacements> GetBucklingModeDisplacements(int nativeBucklingModeNumber, int nodeId);

        /// <summary>Reads the relative native X/Y/Z member translation at a specified station.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <param name="distanceFromStart">Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for the native relative translations described in local X/Y/Z order. GetAbsoluteMemberDisplacement invokes a different native operation whose prose is ambiguous; neither query applies a managed frame or offset transformation. This returns three translations, not six nodal translation/rotation values.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetRelativeMemberDisplacement(17, 0.5, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<Vector3D> GetRelativeMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId);

        /// <summary>Reads the absolute-labelled native X/Y/Z member translation at a specified station.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <param name="distanceFromStart">Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Calls the native AbsTrans operation, separately from GetRelativeMemberDisplacement. The installed name says absolute while its prose repeats relative; the result preserves native X/Y/Z slots and does not certify an absolute/relative transformation. No offset transfer, basis reconstruction, rotation, length-fraction conversion or zero substitution is applied.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetAbsoluteMemberDisplacement(17, 0.5, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<Vector3D> GetAbsoluteMemberDisplacement(int memberId, double distanceFromStart, int loadCaseId);

        /// <summary>Reads six physical-member end actions in explicitly selected native local/global axes.</summary>
        /// <param name="physicalMemberId">Positive native physical-member ID. Analytical member IDs are not interchangeable with this identity.</param>
        /// <param name="memberEnd">Existing MemberEnd.Start (native 0) or MemberEnd.End (native 1), interpreted for the physical-member incidence.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <param name="coordinateSystem">Local=0 or Global=1 for this native physical-member force query. No managed rotation is applied.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for physical-member IDs and native physical start/end incidence. GetMemberEndForces takes analytical member IDs. MemberEnd.Start/End map to native 0/1; ResultCoordinateSystem.Local/Global map independently to 0/1 and do not reuse the oppositely numbered offset enum. Physical incidence reordering may change signs; no managed reordering, rotation or sign correction is applied. FX/FY/FZ retain force units and MX/MY/MZ force-times-length units.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetPhysicalMemberEndForces(7, MemberEnd.Start, 1, ResultCoordinateSystem.Local);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);</code></example>
        OutputQueryResult<ForceComponents> GetPhysicalMemberEndForces(int physicalMemberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem = ResultCoordinateSystem.Local);

        /// <summary>Reads signed axial-force minimum/maximum values and both native stations.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Unlike GetMemberForcesAtDistance, this requests native extrema over the member. Values are not absolute extrema, sorted envelopes across load cases or recalculated forces. Minimum must not exceed maximum; both written stations must be nonnegative. Positions retain native length units from the analytical start.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMemberAxialForceExtrema(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.MinimumValue);</code></example>
        OutputQueryResult<MemberForceExtrema> GetMemberAxialForceExtrema(int memberId, int loadCaseId);

        /// <summary>Reads six local section actions at a specified analytical member station.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <param name="distanceFromStart">Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: This returns FX/FY/FZ/MX/MY/MZ at a native distance from the analytical start. GetPhysicalMemberForcesAtDistance uses physical-member identity and total physical length. It is not an end-action sign conversion, result envelope or normalized eigenmode. Station zero is allowed; member membership and upper station validity remain native decisions. No implicit length-fraction conversion, unit scaling or geometry hydration occurs.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMemberForcesAtDistance(17, 0.5, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);</code></example>
        OutputQueryResult<ForceComponents> GetMemberForcesAtDistance(int memberId, double distanceFromStart, int loadCaseId);

        /// <summary>Reads six local section actions at a specified physical member station.</summary>
        /// <param name="physicalMemberId">Positive native physical-member ID. Analytical member IDs are not interchangeable with this identity.</param>
        /// <param name="distanceFromStart">Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: This returns FX/FY/FZ/MX/MY/MZ at a native distance from the physical start. GetMemberForcesAtDistance uses analytical member identity; IDs are not interchangeable. It is not an end-action sign conversion, result envelope or normalized eigenmode. Station zero is allowed; member membership and upper station validity remain native decisions. No implicit length-fraction conversion, unit scaling or geometry hydration occurs.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetPhysicalMemberForcesAtDistance(17, 0.5, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Fx);</code></example>
        OutputQueryResult<ForceComponents> GetPhysicalMemberForcesAtDistance(int physicalMemberId, double distanceFromStart, int loadCaseId);

        /// <summary>Reads the two native Y/Z deflection outputs at one analytical member station.</summary>
        /// <param name="memberId">Positive analytical member ID; not a physical-member ID or a collection index.</param>
        /// <param name="distanceFromStart">Finite nonnegative native station measured from the relevant member start. No fraction/length conversion occurs; member existence and upper station validity remain native decisions.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Choose this for the two-output native deflection operation; GetRelativeMemberDisplacement and GetAbsoluteMemberDisplacement return three translation slots through different functions. The help does not fully establish the Y/Z frame or absolute/relative convention, so NativeY/NativeZ preserve slot identity without asserting a transformation. No missing X value or rotations are invented.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMemberDeflectionAtDistance(17, 0.5, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.NativeY);</code></example>
        OutputQueryResult<MemberDeflection> GetMemberDeflectionAtDistance(int memberId, double distanceFromStart, int loadCaseId);

        /// <summary>Reads native ELASTIC MAT influence areas corresponding to an explicit support-node selection.</summary>
        /// <param name="supportNodeIds">Non-null one-pass selection of positive support-node IDs. Order and duplicates are preserved. Empty input is valid; native ELASTIC MAT/BASE PRESSURE prerequisites remain the caller’s responsibility.</param>
        /// <param name="maximumResults">Maximum support-node selection size, from 1 through 100000. Input beyond this bound is rejected before native access.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for YZ/ZX/XY influence areas, not base pressures or Load floor influence areas. Input is snapshotted once with a bounded selection; order and duplicates are retained. Empty input returns an owned empty result without native result calls. Exported metadata passes initialized arrays by value, so every applicable Double slot must be written in place. Native integer 0 means success; -107/-108 mean buffer failure. Actual Boolean acknowledgements are interpreted separately, never converted to an integer zero. Changed node correspondence rejects mixed data. Areas retain native length-squared units.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMatInfluenceAreas(new[] { 11, 12, 11 });
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);</code></example>
        OutputQueryResult<IReadOnlyList<MatInfluenceArea>> GetMatInfluenceAreas(IEnumerable<int> supportNodeIds, int maximumResults = 100000);

        /// <summary>Reads signed base pressures for explicitly ordered support nodes and one result case.</summary>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <param name="supportNodeIds">Non-null one-pass selection of positive support-node IDs. Order and duplicates are preserved. Empty input is valid; native ELASTIC MAT/BASE PRESSURE prerequisites remain the caller’s responsibility.</param>
        /// <param name="maximumResults">Maximum support-node selection size, from 1 through 100000. Input beyond this bound is rejected before native access.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Requires native BASE PRESSURE results; it does not calculate contact pressure from support reactions or ELASTIC MAT influence areas. Native YZ/ZX/XY arrays correspond to global X/Y/Z pressure components in force-per-area units. The Boolean-success contract differs from GetMatInfluenceAreas integer-zero success. Bounded one-pass input snapshots preserve duplicates/order, validate written fixed-size arrays and reject changed node correspondence. Empty input returns an owned empty result without native retrieval.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetBasePressures(1, new[] { 11, 12 });
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Count);</code></example>
        OutputQueryResult<IReadOnlyList<NodeBasePressure>> GetBasePressures(int loadCaseId, IEnumerable<int> supportNodeIds, int maximumResults = 100000);

        /// <summary>Reads SXX/SYY/SZZ normal stresses at an explicitly identified native solid corner.</summary>
        /// <param name="solidId">Positive native solid-element ID; not a plate or analytical member ID.</param>
        /// <param name="nativeCornerIndex">Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. Named component ordering follows the native output slots; no principal transformation is performed. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetSolidNormalStresses(101, 1, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<SolidNormalStresses> GetSolidNormalStresses(int solidId, int nativeCornerIndex, int loadCaseId);

        /// <summary>Reads SXY/SYZ/SZX shear stresses at an explicitly identified native solid corner.</summary>
        /// <param name="solidId">Positive native solid-element ID; not a plate or analytical member ID.</param>
        /// <param name="nativeCornerIndex">Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. Named component ordering follows the native output slots; no principal transformation is performed. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetSolidShearStresses(101, 1, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<SolidShearStresses> GetSolidShearStresses(int solidId, int nativeCornerIndex, int loadCaseId);

        /// <summary>Reads three native principal-stress slots at an explicitly identified native solid corner.</summary>
        /// <param name="solidId">Positive native solid-element ID; not a plate or analytical member ID.</param>
        /// <param name="nativeCornerIndex">Nonnegative native solid-corner selector, passed unchanged. No analytical node-ID mapping, zero/one rebasing or center sentinel is supplied.</param>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for solid elements, not plate-center stresses. The nonnegative native corner selector is passed unchanged; it is not an analytical node ID and no zero/one-based corner mapping or center sentinel is invented. The help repeats S1 labels, so FirstNativeValue/SecondNativeValue/ThirdNativeValue retain native slot order without sorting or claiming descending S1/S2/S3. All three native stress values must be written and finite on success. Native result stress units and signs are preserved.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetSolidPrincipalStresses(101, 1, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<SolidPrincipalStresses> GetSolidPrincipalStresses(int solidId, int nativeCornerIndex, int loadCaseId);

        /// <summary>Reads the native nonlinear load-step value for one result load case.</summary>
        /// <param name="loadCaseId">Positive native result load-case number. No activation is performed; suitability is determined by STAAD.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: The return is numeric data, not a Boolean flag or a certified step count/index range. Nonnegative finite values are preserved without rounding. Native -1 is NativeFailure, -8002 NotFound and -9911 ResultsUnavailable. No arbitrary nonzero success rule is used. This does not retrieve nonlinear node displacements or activate a load step.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetNonlinearLoadStep(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetNonlinearLoadStep(int loadCaseId);

        /// <summary>Counts the native steel-design parameter blocks in the applicable result context.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: The reviewed help limits multiple-block extraction to AISC 360-16. GetMultipleSteelDesignResultAvailability observes that capability separately. Zero is a valid count; it is not a fabricated successful design result or a Boolean false. No block names, governing blocks, design changes or results freshness are inferred.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetSteelDesignParameterBlockCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<int> GetSteelDesignParameterBlockCount();

        /// <summary>Counts modes already extracted by native dynamic analysis.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: This is an integral mode count, not buckling-factor count or a success flag. Zero remains valid data. It does not establish mode numbering, result normalization or whether the modal set is current. No dynamic analysis is started.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetExtractedModeCount();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<int> GetExtractedModeCount();

        /// <summary>Reads the native frequency in Hz for an explicitly identified dynamic mode.</summary>
        /// <param name="nativeModeNumber">Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for modal frequency, not a buckling eigenvalue or time-history interval. The nonnegative native mode number/index is passed unchanged and is not rebased from a managed index. Zero frequency is retained when native success reports it; negative or unwritten/nonfinite frequency is rejected. No cycles/radians conversion occurs.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetModeFrequency(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetModeFrequency(int nativeModeNumber);

        /// <summary>Reads six dynamic eigenmode shape components at an analytical node.</summary>
        /// <param name="nativeModeNumber">Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred.</param>
        /// <param name="nodeId">Positive native analytical node ID, not an array index.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for a dynamic modal shape; GetBucklingModeDisplacements queries a different mode family and GetNodeDisplacements retrieves load-case responses. The native mode number/index is passed unchanged. Exported metadata takes the six-element initialized array by value, so all applicable slots must be written in place. Native normalization is not rescaled into physical static displacement and no rotations are invented.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetModalNodeDisplacements(1, 11);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Translation);</code></example>
        OutputQueryResult<ModeDisplacements> GetModalNodeDisplacements(int nativeModeNumber, int nodeId);

        /// <summary>Reads native X/Y/Z mass participation factors for an explicit dynamic mode.</summary>
        /// <param name="nativeModeNumber">Nonnegative native dynamic-mode number/index, passed unchanged. The native result context decides validity; no zero/one-based conversion is inferred.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for one mode, not missing-mass factors, cumulative participation or a load-case force result. The native mode number/index is passed unchanged. All three scalar slots must be written and finite. Fraction-versus-percentage and eigenvector normalization are not inferred; no factors are rescaled or summed.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetModalMassParticipationFactors(1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Factors);</code></example>
        OutputQueryResult<ModalMassParticipation> GetModalMassParticipationFactors(int nativeModeNumber);

        /// <summary>Reads native time-history response-sample count and integration interval.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Unlike a Boolean availability query, this returns a sample count plus an interval in seconds. Native 0 means ResultsUnavailable and -1 NativeFailure; a positive count requires a positive finite interval. The first response is at time zero and the last at (count-1)*interval; no extra endpoint is invented. It does not run integration or certify result freshness.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetTimeHistoryIntegrationInfo();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.SampleCount);</code></example>
        OutputQueryResult<TimeHistoryIntegrationInfo> GetTimeHistoryIntegrationInfo();

        /// <summary>Reads a native time-history response at one bounded time.</summary>
        /// <param name="nodeId">Positive native analytical node ID, not an array index.</param>
        /// <param name="degreeOfFreedom">A defined translational or rotational response DOF, mapping exactly to native selectors 1 through 6.</param>
        /// <param name="responseKind">Displacement=0, Velocity=1 or Acceleration=2. Units depend on the requested translational/rotational DOF.</param>
        /// <param name="timeSeconds">Finite nonnegative time in seconds within the observed native integration-sample bounds.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: GetTimeHistoryResponse retrieves the full integration series. The reserved native load-case argument is always internal zero; no caller case is activated. The native response DOF/kind selects translational or rotational displacement/velocity/acceleration units. Integration metadata is read before/after; requested time must lie in 0..(count-1)*interval. Native 1/true succeeds, 0/false means ResultsUnavailable and integer -1 means NativeFailure, never VARIANT_TRUE. Native interpolation, if any, is not reproduced in managed code. Changed integration metadata returns StateChanged with no sample.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetTimeHistoryResponseAtTime(11, ResponseDegreeOfFreedom.TranslationX, TimeHistoryResponseKind.Displacement, 0.0);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Value);</code></example>
        OutputQueryResult<TimeHistorySample> GetTimeHistoryResponseAtTime(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, double timeSeconds);

        /// <summary>Reads a bounded, owned native time-history response series.</summary>
        /// <param name="nodeId">Positive native analytical node ID, not an array index.</param>
        /// <param name="degreeOfFreedom">A defined translational or rotational response DOF, mapping exactly to native selectors 1 through 6.</param>
        /// <param name="responseKind">Displacement=0, Velocity=1 or Acceleration=2. Units depend on the requested translational/rotational DOF.</param>
        /// <param name="maximumSamples">Maximum allocated response samples, from 1 through 100000. A larger native count returns LimitExceeded without allocating or fetching the series.</param>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: Use for all integration samples rather than one requested time. The reserved native load-case argument stays internal zero. A checked integration count sizes the Double buffer; counts exceeding maximumSamples produce LimitExceeded before allocation or retrieval. Sample zero is at time zero; no extra final sample is added. Native 1/true succeeds, 0/false means ResultsUnavailable and integer -1 NativeFailure. Count/interval are rechecked after retrieval; StateChanged discards mixed data. Samples preserve signs and response-dependent native units; no smoothing, interpolation, differentiation or unit conversion occurs.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID, native selector, enum, station, time or bounded selection is invalid; invalid input is rejected before result retrieval.</exception>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetTimeHistoryResponse(11, ResponseDegreeOfFreedom.TranslationX, TimeHistoryResponseKind.Displacement);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Samples.Count);</code></example>
        OutputQueryResult<TimeHistorySeries> GetTimeHistoryResponse(int nodeId, ResponseDegreeOfFreedom degreeOfFreedom, TimeHistoryResponseKind responseKind, int maximumSamples = 100000);

        /// <summary>Observes native availability for multiple steel-design parameter-block extraction.</summary>
        /// <returns>A named status/data result. Inspect IsSuccessful before Data; native failure, unavailable results, changed state or managed limits expose no payload. Actual Boolean and integral return evidence remain distinct.</returns>
        /// <remarks><para>When to use: This is distinct from general analysis or buckling availability. The reviewed contract identifies AISC 360-16 as the applicable design context. A successful observation with Data=false is valid data, not a query failure. Native Boolean or integral 0/1 is accepted; an integral -1 is not borrowed as success. No design code is changed and no multiple-block result packet is inferred.</para><para>Common reviewed 2024–2026 contract; no changed direction-code or displacement-layout policy is guessed. Calls execute synchronously on the caller’s established COM apartment under the shared expanded-query gate. Coordinate other query paths, adapters, raw access and external model edits. Reacquire wrapper.Output after refresh; captured binding identity is checked before and after the observation. Success requires applicable initialized outputs to be written, finite and correctly shaped. No case activation, analysis, save, retry, implicit unit conversion or engineering certification occurs. Wrapper input-unit caches do not prove Output units; live native qualification remains required.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The binding is stale/disposed, or native output has an undocumented type/code, inconsistent shape/correspondence or unwritten/nonfinite applicable data.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native member.</exception>
        /// <example><code>var result = output.GetMultipleSteelDesignResultAvailability();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<bool> GetMultipleSteelDesignResultAvailability();

        #endregion
        #region Steel-design parameter blocks and failure ratios

        /// <summary>Reads the native name of one zero-based steel-design parameter block.</summary>
        /// <param name="parameterBlockIndex">Nonnegative zero-based native design-parameter-block index. Count and block availability remain native result-context decisions.</param>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: Use this name with the parameter-block overloads of GetMemberSteelUtilizationRatio and GetMemberSteelDesignResults. GetSteelDesignParameterBlockCount is a count, not a last index. This query passes the nonnegative index unchanged; it does not enumerate blocks, select a design code, run design or verify that all blocks are supported. Native false is failure; unwritten or empty successful names are rejected rather than used as a default block.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID/index/name is invalid; rejected before native invocation.</exception>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetSteelDesignParameterBlockName(0);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<string> GetSteelDesignParameterBlockName(int parameterBlockIndex);

        /// <summary>Reads a member utilization ratio for one explicitly named steel-design parameter block.</summary>
        /// <param name="memberId">Positive native analytical member ID; not a physical member, design-block index or load-case number.</param>
        /// <param name="parameterBlockName">Exact nonblank native parameter-block name, without NUL characters. Case and surrounding whitespace are preserved; obtain it with GetSteelDesignParameterBlockName when discovering names.</param>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: Unlike the member-only overload, this calls the multiple-block native API and retains the exact requested ParameterBlockName in the result. The reviewed help describes AISC 360-16 multiple-design results; native availability and applicable code/context remain STAAD decisions. It does not select a block or take a maximum across blocks. Only finite nonnegative ratios are data; the single-block -999/-1 sentinels are not reused here. Native false takes precedence over any ratio buffer.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID/index/name is invalid; rejected before native invocation.</exception>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetMemberSteelUtilizationRatio(17, "PARAMETER 1");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetMemberSteelUtilizationRatio(int memberId, string parameterBlockName);

        /// <summary>Reads the native maximum utilization ratio for one member across available design parameter blocks.</summary>
        /// <param name="memberId">Positive native analytical member ID; not a physical member, design-block index or load-case number.</param>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: Choose this to ask STAAD for its maximum across parameter blocks, rather than the member-only or explicitly named-block ratio query. The native output does not identify a governing block, so ParameterBlockName is null and no identity is guessed. A finite nonnegative ratio is required on native true; it is not converted to PASS/FAIL and no managed envelope, single-block sentinel interpretation or design execution occurs.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID/index/name is invalid; rejected before native invocation.</exception>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetMaximumMemberSteelUtilizationRatio(17);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetMaximumMemberSteelUtilizationRatio(int memberId);

        /// <summary>Reads the named parameter-block steel-design packet for one analytical member.</summary>
        /// <param name="memberId">Positive native analytical member ID; not a physical member, design-block index or load-case number.</param>
        /// <param name="parameterBlockName">Exact nonblank native parameter-block name, without NUL characters. Case and surrounding whitespace are preserved; obtain it with GetSteelDesignParameterBlockName when discovering names.</param>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: Unlike GetMemberSteelDesignResults(memberId), this native packet supplies code, PASS/FAIL text, critical/allowable ratios, critical load case, clause and section for an explicit block. It supplies no critical station, FX/MY/MZ or slenderness; those fields are absent from ParameterBlockSteelDesignResults, never synthesized as zero. PASS/FAIL text is classified explicitly; other written text remains Unknown. Exact requested block and native status text are preserved. Native false prevents publication of any packet; no block selection, case activation or design is performed.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.ArgumentException">An ID/index/name is invalid; rejected before native invocation.</exception>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetMemberSteelDesignResults(17, "PARAMETER 1");
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.DesignStatus);</code></example>
        OutputQueryResult<ParameterBlockSteelDesignResults> GetMemberSteelDesignResults(int memberId, string parameterBlockName);

        /// <summary>Reads the native model-wide minimum steel-design failure ratio.</summary>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: This function has no member ID or parameter-block input. It is a model-wide native failure-ratio observation, not the minimum utilization of a requested member or a managed envelope. GetMaximumSteelDesignFailureRatio is a separate observation with its own success flag; the two are not combined into an allegedly atomic range. Native false means failure or unavailable data without proving the absence of failing members. A finite nonnegative ratio is required on native success.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetMinimumSteelDesignFailureRatio();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetMinimumSteelDesignFailureRatio();

        /// <summary>Reads the native model-wide maximum steel-design failure ratio.</summary>
        /// <returns>A qualified status/data observation retaining actual native flag evidence and applicable member/block identity. Inspect IsSuccessful before Data; native failure has no payload.</returns>
        /// <remarks><para>When to use: This function has no member ID or parameter-block input. It is independent from GetMinimumSteelDesignFailureRatio and from GetMaximumMemberSteelUtilizationRatio, which targets a member across blocks. No governing member/block is returned or invented. Native false prevents consumption of its output; the method does not infer failure thresholds, result freshness or an engineering PASS/FAIL decision.</para><para>Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.</para></remarks>
        /// <exception cref="System.InvalidOperationException">The receiver is stale/disposed, or successful native output has an unknown flag, invalid text/type, unwritten scalar or nonfinite/negative ratio.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver lacks a compatible native operation.</exception>
        /// <example><code>var result = output.GetMaximumSteelDesignFailureRatio();
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);</code></example>
        OutputQueryResult<double> GetMaximumSteelDesignFailureRatio();

        #endregion

        #region Managed result workflows

        /// <summary>Retrieves a beam design packet and resolves its governing case from supplied metadata.</summary>
        /// <param name="beam">Caller-owned beam with a positive ID; geometry is required only for offset workflows.</param>
        /// <param name="loadCases">Non-null case metadata selection of at most 100000 items with positive IDs, valid families/types and no conflicting metadata for a shared number.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.GetSteelDesignResultsForBeam(beam, loadCases);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(Beam beam, IEnumerable<ILoadCase> loadCases);

        /// <summary>Retrieves a beam design packet using case metadata discovered from this wrapper.</summary>
        /// <param name="load">This acquired wrapper's current Load adapter, used to discover primary and combination case metadata.</param>
        /// <param name="beam">Caller-owned beam with a positive ID; geometry is required only for offset workflows.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Discovers primary and combination cases through the current Load adapter; reference cases are not governing design cases here. Pass this wrapper's Load adapter, not one acquired from a different model. Supplied-metadata overloads avoid discovery calls. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.GetSteelDesignResultsForBeam(wrapper.Load, beam);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<BeamSteelDesignResults> GetSteelDesignResultsForBeam(IOSLoad load, Beam beam);

        /// <summary>Retrieves independently qualified design packets for a beam selection.</summary>
        /// <param name="beams">Non-null selection, at most 100000 beams, one positive unique member ID per beam; enumerated once before dispatch.</param>
        /// <param name="loadCases">Non-null case metadata selection of at most 100000 items with positive IDs, valid families/types and no conflicting metadata for a shared number.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Selection is enumerated once before dispatch, retains order, and rejects duplicate member IDs. Ordinary native failures remain individual results; transport or malformed-data exceptions abort the operation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.GetSteelDesignResultsForBeams(new[] { beam }, loadCases);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IEnumerable<Beam> beams, IEnumerable<ILoadCase> loadCases);

        /// <summary>Retrieves beam design packets after discovering governing-case metadata once.</summary>
        /// <param name="load">This acquired wrapper's current Load adapter, used to discover primary and combination case metadata.</param>
        /// <param name="beams">Non-null selection, at most 100000 beams, one positive unique member ID per beam; enumerated once before dispatch.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Uses the existing complete native steel-design packet, preserving the raw design decision and unavailable action components. Unlike GetMemberSteelDesignResults(int), this resolves governing case metadata and retains the caller Beam. A native success whose governing case is absent returns MetadataUnavailable without Data. Conflicting metadata for the same case number is rejected; duplicate identical metadata is accepted. Cases must belong to this model and the numeric case identities must be unambiguous. Discovers primary/combination cases once per nonempty selection using this wrapper's current Load adapter. Empty selections perform no native discovery. Duplicate member IDs are rejected. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.GetSteelDesignResultsForBeams(wrapper.Load, new[] { beam });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<BeamSteelDesignResults> GetSteelDesignResultsForBeams(IOSLoad load, IEnumerable<Beam> beams);

        /// <summary>Retrieves per-member utilization observations without changing the supplied beams.</summary>
        /// <param name="beams">Non-null selection, at most 100000 beams, one positive unique member ID per beam; enumerated once before dispatch.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Unlike PopulateMemberSteelUtilizationRatios, this only reads. Snapshots the set once in its enumeration order; distinct beam objects with the same member ID are rejected. Retains each query's status and member identity, including failures. Ratios are dimensionless. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.GetMemberSteelUtilizationRatios(new HashSet&lt;Beam&gt; { beam });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<double> GetMemberSteelUtilizationRatios(HashSet<Beam> beams);

        /// <summary>Stages utilization observations and applies them to caller beams only after all reads succeed.</summary>
        /// <param name="beams">Non-null selection, at most 100000 beams, one positive unique member ID per beam; enumerated once before dispatch.</param>
        /// <returns>Staged per-beam query statuses and whether the managed cache assignment phase completed. Inspect IsSuccessful and WasApplied.</returns>
        /// <remarks>Unlike GetMemberSteelUtilizationRatios, this updates the caller-owned Beam.UtilizationRatio caches, never the STAAD model. Any ordinary failed query prevents all managed assignments. Empty selections apply nothing. Successful native reads are staged before assignment; an exception from a property setter, notification handler or lifetime check can interrupt assignment after earlier beams were updated. This is not an atomic rollback transaction. Prior cache values do not certify current design results. Ratios are dimensionless. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var loadCases = new ILoadCase[] { new LoadCase(1, "Dead") };
        /// var result = wrapper.Output.PopulateMemberSteelUtilizationRatios(new HashSet&lt;Beam&gt; { beam });
        /// System.Console.WriteLine(result.IsSuccessful);
        /// System.Console.WriteLine(result.WasApplied);</code></example>
        OutputPopulationResult PopulateMemberSteelUtilizationRatios(HashSet<Beam> beams);

        /// <summary>Retrieves identified analytical member-end actions in native local axes.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="memberId">Positive analytical member ID, not a physical-member ID.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike the three-argument numeric overload, this returns an identified packet containing the exact caller-supplied case title and coordinate system. The title is a label, not a native lookup or proof of case identity. Values keep native member-end signs; no support-reaction sign reversal is applied. Local X follows native member incidence; beta and native local conventions are handled by STAAD. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd);

        /// <summary>Retrieves identified analytical member-end actions in the requested native axes.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="memberId">Positive analytical member ID, not a physical-member ID.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <param name="coordinateSystem">Local or Global as interpreted directly by STAAD; no other selector is accepted.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike the three-argument numeric overload, this returns an identified packet containing the exact caller-supplied case title and coordinate system. The title is a label, not a native lookup or proof of case identity. Values keep native member-end signs; no support-reaction sign reversal is applied. Local or Global is sent directly to the native call. This performs no managed axis reconstruction. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForces("Dead", 17, 1, MemberEnd.Start, ResultCoordinateSystem.Global);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<IdentifiedMemberForces> GetMemberEndForces(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd, ResultCoordinateSystem coordinateSystem);

        /// <summary>Retrieves native global member-end forces and an offset-transfer audit packet.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="beam">Caller-owned beam with a positive ID; geometry is required only for offset workflows.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Uses supplied hydrated incidence/coordinates; stale caller geometry cannot be certified against STAAD. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var result = wrapper.Output.GetMemberEndForcesConsideringOffsets("Dead", beam, 1, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd);

        /// <summary>Retrieves the managed global member-end action transferred to the physical joint.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="beam">Caller-owned beam with a positive ID; geometry is required only for offset workflows.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Returns only the physical-joint projection; choose GetMemberEndForcesConsideringOffsets to inspect both face and joint results with the offset. This uses analytical members; it is not GetPhysicalMemberEndForces for a native physical-member ID. Supply hydrated incidence/coordinates. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var result = wrapper.Output.GetMemberEndForcesAtPhysicalJoint("Dead", beam, 1, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, Beam beam, int loadCaseId, MemberEnd memberEnd);

        /// <summary>Retrieves native global member-end forces and an offset-transfer audit packet.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="memberId">Positive analytical member ID, not a physical-member ID.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Reads incidence and node coordinates from the current Geometry binding with strict status/written-output validation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForcesConsideringOffsets("Dead", 17, 1, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<OffsetAwareMemberEndForces> GetMemberEndForcesConsideringOffsets(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd);

        /// <summary>Retrieves the managed global member-end action transferred to the physical joint.</summary>
        /// <param name="loadCaseTitle">Caller-supplied case label retained exactly; cannot be null or contain NUL. Empty is allowed; it is not a native title lookup.</param>
        /// <param name="memberId">Positive analytical member ID, not a physical-member ID.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike GetMemberEndForces, this includes native global member-end forces at the analytical face and a managed transfer to the physical joint, with an audit of the global joint-to-face offset. Uses Mjoint = Mface + offset cross F; forces are unchanged. Offset lengths and force/moment units must be mutually consistent; no unit conversion or independent engineering validation is performed. Explicit native NoSpecification is recorded as an absent offset; other offset failures do not become zero. Joint/face naming and transfer follow the main-project convention and require live model qualification before engineering use. Returns only the physical-joint projection; choose GetMemberEndForcesConsideringOffsets to inspect both face and joint results with the offset. This uses analytical members; it is not GetPhysicalMemberEndForces for a native physical-member ID. Resolves native incidence/coordinates. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForcesAtPhysicalJoint("Dead", 17, 1, MemberEnd.Start);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<IdentifiedMemberForces> GetMemberEndForcesAtPhysicalJoint(string loadCaseTitle, int memberId, int loadCaseId, MemberEnd memberEnd);

        /// <summary>Computes a right-handed managed member basis from supplied geometry and beta degrees.</summary>
        /// <param name="member">Caller-owned beam with a positive ID, hydrated distinct start/end nodes, finite coordinates and beta degrees.</param>
        /// <returns>An owned orthonormal basis from supplied geometry; no native status is fabricated.</returns>
        /// <remarks>Uses no native result query. Unlike requesting native Local/Global axes, this is a geometry convention, not native orientation read-back. Supply hydrated distinct nodes and the intended finite beta angle. X follows start to end; projected global Y defines the reference except near vertical, where projected global Z defines Z. Beta rotates Y/Z about positive X. Native orientation rules or stale caller geometry can differ; prefer native axis selection for Output extraction. The returned pure transforms change axes without converting units. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var result = wrapper.Output.GetMemberLocalCoordinateSystem(beam);
        /// System.Console.WriteLine(result.XAxis);</code></example>
        MemberCoordinateSystem GetMemberLocalCoordinateSystem(Beam member);

        /// <summary>Retrieves the selected result family at a plate center using entity identities.</summary>
        /// <param name="loadCase">Caller case metadata with a positive native result case ID; no native case discovery or activation is performed.</param>
        /// <param name="plate">Caller plate with a positive ID; geometry is not required for center extraction.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Returns the five native center force/stress components, not the three center moments or combined eight-value stress/moment packet. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterForces(loadCase, plate);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterForces> GetPlateCenterForces(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves moments at a plate center using entity identities.</summary>
        /// <param name="loadCase">Caller case metadata with a positive native result case ID; no native case discovery or activation is performed.</param>
        /// <param name="plate">Caller plate with a positive ID; geometry is not required for center extraction.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Returns the three native center bending/twisting moments per unit width, not the five center force/stress components. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterMoments(loadCase, plate);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterMoments> GetPlateCenterMoments(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves the selected result family at a plate center using entity identities.</summary>
        /// <param name="loadCase">Caller case metadata with a positive native result case ID; no native case discovery or activation is performed.</param>
        /// <param name="plate">Caller plate with a positive ID; geometry is not required for center extraction.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Returns top/bottom center von Mises stresses, not normal principal stresses or principal directions. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterVonMises(loadCase, plate);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterVonMises> GetPlateCenterVonMises(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves the selected result family at a plate center using entity identities.</summary>
        /// <param name="loadCase">Caller case metadata with a positive native result case ID; no native case discovery or activation is performed.</param>
        /// <param name="plate">Caller plate with a positive ID; geometry is not required for center extraction.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Returns top/bottom center normal principal stresses, not von Mises stresses or principal directions. Equivalent to the corresponding numeric-ID query; only case and plate IDs select native results. Captures identities before dispatch and does not hydrate geometry, validate case membership or read native case titles. Uses native plate-local axes and face conventions; no corner or arbitrary-point interpolation. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlatePrincipalStresses(loadCase, plate);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlatePrincipalStresses> GetPlatePrincipalStresses(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves four qualified result families for one plate center.</summary>
        /// <param name="loadCase">Caller case metadata with a positive native result case ID; no native case discovery or activation is performed.</param>
        /// <param name="plate">Caller plate with a positive ID; geometry is not required for center extraction.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterResults(loadCase, plate);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCenterResults> GetPlateCenterResults(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves four plate-center result families for a Cartesian selection.</summary>
        /// <param name="loadCases">Non-null case metadata selection of at most 100000 items with positive IDs, valid families/types and no conflicting metadata for a shared number.</param>
        /// <param name="plates">Non-null plate selection with positive IDs, at most 100000 entries; Cartesian output is limited to 100000 pairs.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved. Snapshots both sequences once, then queries case-major/plate-minor Cartesian order. Duplicate selections are retained; conflicting metadata for the same case number is rejected. Ordinary per-pair failures remain in Items. Empty products perform no native queries. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterResults(new[] { loadCase }, new[] { plate });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates);

        /// <summary>Retrieves plate-center result sets with an explicit compatibility scheduling hint.</summary>
        /// <param name="loadCases">Non-null case metadata selection of at most 100000 items with positive IDs, valid families/types and no conflicting metadata for a shared number.</param>
        /// <param name="plates">Non-null plate selection with positive IDs, at most 100000 entries; Cartesian output is limited to 100000 pairs.</param>
        /// <param name="degreeOfParallelism">Compatibility planning hint from 1 through 64; native calls remain serialized on the caller thread.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Unlike individual plate-center family queries, this composes forces, moments, von Mises and normal principal stresses from four existing native calls. It is not the eight-value combined stress/moment native query. All four are observed even when an ordinary native failure occurs; the first failure determines Status and Data remains unavailable. Observations retains each contributing status; exceptions abort immediately. The calls are sequential observations, not an atomic solver snapshot. Native plate-local axes, top/bottom conventions and family-specific units are preserved. Snapshots both sequences once, then queries case-major/plate-minor Cartesian order. Duplicate selections are retained; conflicting metadata for the same case number is rejected. Ordinary per-pair failures remain in Items. Empty products perform no native queries. Unlike the main-source parallel implementation, degreeOfParallelism (1-64) is retained as a compatibility planning hint; every COM call remains serialized on the caller thread. It does not request native worker threads or promise parallel speedup. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var plate = new Plate { Id = 17 };
        /// var result = wrapper.Output.GetPlateCenterResults(new[] { loadCase }, new[] { plate }, 2);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<PlateCenterResults> GetPlateCenterResults(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates, int degreeOfParallelism);

        /// <summary>Retrieves static equilibrium observations for an ordered case selection.</summary>
        /// <param name="loadCaseIds">Non-null sequence of positive result case IDs, at most 100000 entries; order and duplicates are retained.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Unlike GetStaticCheckResult(int), this returns an ordered list of independent case results, including duplicate case requests and failed observations. It does not collapse equal force records in a HashSet. Preserves native global total loads/reactions and their original signs; does not negate reactions, compute an equilibrium tolerance or assert that analysis is current. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetStaticCheckResults(new[] { 1, 2 });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds);

        /// <summary>Retrieves ordered static checks with an explicit compatibility scheduling hint.</summary>
        /// <param name="loadCaseIds">Non-null sequence of positive result case IDs, at most 100000 entries; order and duplicates are retained.</param>
        /// <param name="degreeOfParallelism">Compatibility planning hint from 1 through 64; native calls remain serialized on the caller thread.</param>
        /// <returns>An immutable ordered batch with per-item status and data; empty batches are successful. Inspect each item's IsSuccessful before Data; ordinary failure retains the item.</returns>
        /// <remarks>Unlike GetStaticCheckResult(int), this returns an ordered list of independent case results, including duplicate case requests and failed observations. It does not collapse equal force records in a HashSet. Preserves native global total loads/reactions and their original signs; does not negate reactions, compute an equilibrium tolerance or assert that analysis is current. degreeOfParallelism (1-64) is a compatibility planning hint; COM dispatch stays serialized on the caller thread and creates no tasks. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetStaticCheckResults(new[] { 1, 2 }, 2);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<StaticCheckResult> GetStaticCheckResults(IEnumerable<int> loadCaseIds, int degreeOfParallelism);

        /// <summary>Retrieves the opposite of the native support reaction for use as an applied support action.</summary>
        /// <param name="nodeId">Positive native support-node ID in the analyzed model.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike GetSupportReactions, this negates all six global components, including moments, to preserve the main-project support-action convention. Native failure remains a failure without Data. No force or moment unit conversion, support assignment or model update is performed. Choose GetSupportReactions when the original STAAD reaction sign is required. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetSupportForces(17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<ForceComponents> GetSupportForces(int nodeId, int loadCaseId);

        /// <summary>Retrieves analytical member-end actions in explicitly selected native axes.</summary>
        /// <param name="memberId">Positive analytical member ID, not a physical-member ID.</param>
        /// <param name="memberEnd">Start or End of the native member incidence.</param>
        /// <param name="loadCaseId">Positive native result case number present in the analyzed model.</param>
        /// <param name="coordinateSystem">Local or Global as interpreted directly by STAAD; no other selector is accepted.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Unlike the existing three-argument overload, this sends the requested Local or Global selector to STAAD. Unlike the title overloads, it returns only the six components with envelope identities. It does not reconstruct beta/local axes or transfer offsets. Native member-end signs are unchanged. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var result = wrapper.Output.GetMemberEndForces(17, MemberEnd.Start, 1, ResultCoordinateSystem.Global);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<ForceComponents> GetMemberEndForces(int memberId, MemberEnd memberEnd, int loadCaseId, ResultCoordinateSystem coordinateSystem);

        /// <summary>Retrieves the current native utilization observation for a caller beam identity.</summary>
        /// <param name="beam">Caller-owned beam with a positive ID; geometry is required only for offset workflows.</param>
        /// <returns>A status/data observation with requested identities. Inspect IsSuccessful before Data; unavailable data throws. Composite Observations retains participating native query evidence.</returns>
        /// <remarks>Equivalent to the numeric-ID query, capturing the positive beam ID and checking it after dispatch. It does not populate Beam.UtilizationRatio; choose PopulateMemberSteelUtilizationRatios for staged cache updates. The ratio is dimensionless and this does not run or certify current steel design. Runs synchronously on the caller's COM apartment. Serialize model access; do not dispatch these calls through Task.Run. Selections are bounded to 100000 items/results and materialized before COM dispatch. Values preserve native units; wrapper input-unit caches do not certify Output units. These methods do not run analysis, activate cases, save or write native model data. Lifecycle/rebinding checks reject replaced participating bindings; external model edits can still invalidate observations.</remarks>
        /// <exception cref="System.ArgumentNullException">A required object or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An ID, enum, selection, metadata identity, finite geometry or scheduling hint is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding was disposed/replaced, caller beam identity changed, or native successful data/status is malformed or unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport failed; the original exception propagates without retries.</exception>
        /// <example><code>var beam = new Beam(17, new Node(1, 0, 0, 0), new Node(2, 2, 0, 0));
        /// var result = wrapper.Output.GetMemberSteelUtilizationRatio(beam);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<double> GetMemberSteelUtilizationRatio(Beam beam);

        #endregion

        #region Plate corner result workflows

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.   Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode);

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="loadCaseId">Positive result case number; metadata resolution performs no case activation.</param>
        /// <param name="plateId">Positive native plate ID.</param>
        /// <param name="cornerNodeId">Positive plate-incidence node ID, not a corner array index.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(1, 17, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId);

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="loadCaseId">Positive result case number; metadata resolution performs no case activation.</param>
        /// <param name="plateId">Positive native plate ID.</param>
        /// <param name="cornerNodeId">Positive plate-incidence node ID, not a corner array index.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(1, 17, 1, wrapper.Geometry, wrapper.Load);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load);

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="loadCaseId">Positive result case number; metadata resolution performs no case activation.</param>
        /// <param name="plateId">Positive native plate ID.</param>
        /// <param name="cornerNodeId">Positive plate-incidence node ID, not a corner array index.</param>
        /// <param name="loadCaseType">Primary, combination or reference metadata family. Repeat metadata is rejected; family does not add a native Output selector.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.  Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(1, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForces(int loadCaseId, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCases">Non-null bounded case metadata selection; conflicting metadata for one family/number is rejected.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, plate, cornerNode);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate, Node cornerNode);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(loadCase, plate);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, Plate plate);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCases">Non-null bounded case metadata selection; conflicting metadata for one family/number is rejected.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, plate);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, Plate plate);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plates">Non-null bounded selection of hydrated plates; conflicting geometry for one plate ID is rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(loadCase, new[] { plate });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(ILoadCase loadCase, IEnumerable<Plate> plates);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCases">Non-null bounded case metadata selection; conflicting metadata for one family/number is rejected.</param>
        /// <param name="plates">Non-null bounded selection of hydrated plates; conflicting geometry for one plate ID is rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(new[] { loadCase }, new[] { plate });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<ILoadCase> loadCases, IEnumerable<Plate> plates);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="requests">Non-null sparse selection of at most 100000 requests; identities and supplied geometry are captured once.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) });
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesRequest> requests);

        /// <summary>Retrieves and indexes the requested plate-corner outcomes.</summary>
        /// <param name="loadCaseIds">Non-null selection of positive case numbers, at most 100000 entries; repeated IDs are coalesced.</param>
        /// <param name="plateId">Positive native plate ID.</param>
        /// <param name="cornerNodeId">Positive plate-incidence node ID, not a corner array index.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(new[] { 1 }, 17, 1, wrapper.Geometry, wrapper.Load);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, IOSGeometry geometry, IOSLoad load);

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.   Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForcesInGlobalCoordinates(loadCase, plate, cornerNode);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForcesInGlobalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode);

        /// <summary>Retrieves one qualified global plate-corner force/moment observation.</summary>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <param name="loadCaseId">Positive result case number; metadata resolution performs no case activation.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself.   Replaces the source Boolean/out-vector convenience with a named status/data result; failures do not become zero vectors. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForcesGlobal(plate, cornerNode, 1);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForcesGlobal(Plate plate, Node cornerNode, int loadCaseId);

        /// <summary>Retrieves a plate-corner packet and projects it into a described managed member basis.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <param name="otherMemberNode">Distinct positive direction-node ID with finite coordinates; defines a conceptual zero-beta local X from the corner.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var otherMemberNode = plate.B;
        /// var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode, otherMemberNode);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode);

        /// <summary>Retrieves a plate-corner packet and projects it into a described managed member basis.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <param name="member">Positive real member with distinct hydrated end nodes and finite beta degrees; selected corners must match an end ID and point.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.  Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var otherMemberNode = plate.B;
        /// var result = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode, member);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForces(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member);

        /// <summary>Transforms an owned global plate-corner packet into a described managed member basis.</summary>
        /// <param name="plateCornerForces">Owned global source packet, or a bounded non-null sequence of such packets for the collection overload; no native result is re-read.</param>
        /// <param name="otherMemberNode">Distinct positive direction-node ID with finite coordinates; defines a conceptual zero-beta local X from the corner.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. This overload is pure managed math over an existing packet and performs no native result query. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var otherMemberNode = plate.B;
        /// var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
        /// if (!source.IsSuccessful) return;
        /// var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(source.Data, otherMemberNode);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Node otherMemberNode);

        /// <summary>Transforms an owned global plate-corner packet into a described managed member basis.</summary>
        /// <param name="plateCornerForces">Owned global source packet, or a bounded non-null sequence of such packets for the collection overload; no native result is re-read.</param>
        /// <param name="member">Positive real member with distinct hydrated end nodes and finite beta degrees; selected corners must match an end ID and point.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. This overload is pure managed math over an existing packet and performs no native result query. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var otherMemberNode = plate.B;
        /// var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
        /// if (!source.IsSuccessful) return;
        /// var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(source.Data, member);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(PlateCornerForces plateCornerForces, Beam member);

        /// <summary>Retrieves a plate-corner packet and projects it into a described managed member basis.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <param name="otherMemberNode">Distinct positive direction-node ID with finite coordinates; defines a conceptual zero-beta local X from the corner.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back.  Returns PlateCornerMemberForces rather than relabeling a global PlateCornerForces packet. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var otherMemberNode = plate.B;
        /// var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, otherMemberNode);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Node otherMemberNode);

        /// <summary>Retrieves one sparse typed plate-corner request.</summary>
        /// <param name="request">Non-null typed sparse request; referenced entities are captured before dispatch.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Unlike the enumerable request overload, this returns one status/data observation without allocating an indexed set. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var result = wrapper.Output.GetPlateCornerForces(new PlateCornerForcesRequest(loadCase, plate, cornerNode));
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerForces> GetPlateCornerForces(PlateCornerForcesRequest request);

        /// <summary>Retrieves selected numeric cases using an explicit metadata family.</summary>
        /// <param name="loadCaseIds">Non-null selection of positive case numbers, at most 100000 entries; repeated IDs are coalesced.</param>
        /// <param name="plateId">Positive native plate ID.</param>
        /// <param name="cornerNodeId">Positive plate-incidence node ID, not a corner array index.</param>
        /// <param name="loadCaseType">Primary, combination or reference metadata family. Repeat metadata is rejected; family does not add a native Output selector.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(new[] { 1 }, 17, 1, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<int> loadCaseIds, int plateId, int cornerNodeId, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load);

        /// <summary>Retrieves a corner packet in the beta-aware managed basis of a supplied member.</summary>
        /// <param name="loadCase">Caller case metadata with positive ID and primary/reference/combination family; captured before dispatch.</param>
        /// <param name="plate">Caller plate with a positive ID. Supplied A/B/C incidence is required for all-corner expansion; single/sparse queries can hydrate ID-only plates.</param>
        /// <param name="cornerNode">Positive incidence-node selector. The plate's captured canonical corner coordinates are used; unrelated supplied coordinates are not substituted.</param>
        /// <param name="member">Positive real member with distinct hydrated end nodes and finite beta degrees; selected corners must match an end ID and point.</param>
        /// <returns>An identified status/data observation. Inspect IsSuccessful before Data; failed Data access throws. MetadataUnavailable means no qualified Output payload was obtained.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Unlike the other-node overload, this retains a real member ID and Start/End identity. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(loadCase, plate, cornerNode, member);
        /// if (result.IsSuccessful) System.Console.WriteLine(result.Data.Components.Fx);
        /// else System.Console.WriteLine(result.Status);</code></example>
        OutputQueryResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(ILoadCase loadCase, Plate plate, Node cornerNode, Beam member);

        /// <summary>Retrieves sparse corner requests in one supplied member basis.</summary>
        /// <param name="requests">Non-null sparse selection of at most 100000 requests; identities and supplied geometry are captured once.</param>
        /// <param name="member">Positive real member with distinct hydrated end nodes and finite beta degrees; selected corners must match an end ID and point.</param>
        /// <returns>An owned ordered result collection. Inspect each item's IsSuccessful before Data.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Every requested corner must be a member end; an ordinary failed extraction remains an identified failed item. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var result = wrapper.Output.GetPlateCornerForcesInMemberLocalCoordinates(new[] { new PlateCornerForcesRequest(loadCase, plate, cornerNode) }, member);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<PlateCornerMemberForces> GetPlateCornerForcesInMemberLocalCoordinates(IEnumerable<PlateCornerForcesRequest> requests, Beam member);

        /// <summary>Projects an ordered collection of owned corner packets without native result calls.</summary>
        /// <param name="plateCornerForces">Owned global source packet, or a bounded non-null sequence of such packets for the collection overload; no native result is re-read.</param>
        /// <param name="member">Positive real member with distinct hydrated end nodes and finite beta degrees; selected corners must match an end ID and point.</param>
        /// <returns>An owned ordered result collection. Inspect each item's IsSuccessful before Data.</returns>
        /// <remarks>Projects global corner forces and moments into a managed member basis at the same corner point. It does not reverse member-end signs, transfer moments, sum adjacent plates or return native analytical-member result forces. Beam overloads use supplied beta degrees and require the corner to match a supplied member end ID and point; node-direction overloads use zero beta and no fabricated native member ID. Projected global Y defines the reference except near vertical, where projected global Z is used. This convention is not native orientation read-back. Unlike sparse extraction, this preserves duplicate source observations and performs no hydration or COM result query. Every source packet is validated before a result collection is returned. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>ILoadCase loadCase = new LoadCase(1, "Dead");
        /// var cornerNode = new Node(1, 0, 0, 0);
        /// var plate = new Plate(17, cornerNode, new Node(2, 2, 0, 0), new Node(3, 2, 2, 0), new Node(4, 0, 2, 0));
        /// var member = new Beam(18, cornerNode, plate.B) { BetaAngle = 30 };
        /// var source = wrapper.Output.GetPlateCornerForces(loadCase, plate, cornerNode);
        /// if (!source.IsSuccessful) return;
        /// var result = wrapper.Output.TransformPlateCornerForcesToMemberLocal(new[] { source.Data }, member);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        OutputBatchResult<PlateCornerMemberForces> TransformPlateCornerForcesToMemberLocal(IEnumerable<PlateCornerForces> plateCornerForces, Beam member);

        /// <summary>Resolves typed keys into indexed global corner observations.</summary>
        /// <param name="keys">Non-null bounded key selection; default/invalid IDs are rejected. Null-family keys require automatic unambiguous metadata resolution.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Family-aware keys select their metadata family; keys without a family require unambiguous discovery. Equivalent resolved keys are coalesced before native extraction. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, wrapper.Geometry, wrapper.Load);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, IOSGeometry geometry, IOSLoad load);

        /// <summary>Resolves typed keys using one explicitly selected metadata family.</summary>
        /// <param name="keys">Non-null bounded key selection; default/invalid IDs are rejected. Null-family keys require automatic unambiguous metadata resolution.</param>
        /// <param name="loadCaseType">Primary, combination or reference metadata family. Repeat metadata is rejected; family does not add a native Output selector.</param>
        /// <param name="geometry">This acquired wrapper's current Geometry adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <param name="load">This acquired wrapper's current Load adapter; cross-wrapper/stale adapters are rejected.</param>
        /// <returns>An eager indexed set with family-aware keys, ordered per-request outcomes and failure-safe data access. Empty validated selections succeed.</returns>
        /// <remarks>Returns nodal Fx/Fy/Fz/Mx/My/Mz in global axes, not plate-center stresses or moments per unit width. The six-slot Double buffer follows installed 2024-2026 VBA examples; global axes follow the main adapter and read-only 2026 equilibrium checks. No extra plate-basis rotation is applied. Native false discards every buffer value. Case family describes metadata only: this native API accepts the numeric case selector and does not activate or disambiguate analysis families itself. Resolves case metadata and plate incidence through this wrapper's current bindings before extraction. Automatic case resolution searches primary, combination and reference metadata and returns MetadataUnavailable for a missing or ambiguous number. Explicit-family overloads avoid this ambiguity; they do not add a native family selector. No case activation or model loading occurs. Returns an indexed, owned outcome set, including ordinary native failures. Exact duplicates are coalesced in first-seen order by family/case/plate/corner. Conflicting supplied geometry or metadata is rejected. All-corner expansions deduplicate repeated closing/triangular incidence and use case-major, plate-major, incidence order; they require supplied A/B/C nodes. Sparse/single ID-only plates are hydrated with checked incidence and written coordinate outputs. At most 100000 input entries and expanded requests are allowed before extraction; no thread-pool dispatch. Keys without a family inherit the explicit family; a key that declares a different family is rejected. Runs synchronously on the caller's COM apartment; serialize model access and do not use Task.Run for COM dispatch. Native units and action signs are preserved; wrapper input-unit caches do not certify Output units. No analysis, case activation, save or native model mutation is performed. Captured binding replacements are rejected. Multiple reads are observations, not an atomic or fresh analysis snapshot. Live axis evidence is limited to the sampled 2026 build; 2024/2025 and other models remain qualification work.</remarks>
        /// <exception cref="System.ArgumentNullException">A required entity, adapter, packet or selection is null.</exception>
        /// <exception cref="System.ArgumentException">An identity, family, geometry, selection, conflicting snapshot or member association is invalid.</exception>
        /// <exception cref="System.InvalidOperationException">A binding is stale/disposed, discovery exceeds its bound, or a native successful response is malformed, unwritten or nonfinite.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Original native transport failure propagates without an alternate-API retry.</exception>
        /// <example><code>var result = wrapper.Output.GetPlateCornerForces(new[] { new PlateCornerForcesKey(1, 17, 1) }, LoadCaseType.PrimaryLoad, wrapper.Geometry, wrapper.Load);
        /// foreach (var item in result.Items)
        ///     if (item.IsSuccessful) System.Console.WriteLine(item.Data.Components.Fx);
        ///     else System.Console.WriteLine(item.Status);</code></example>
        PlateCornerForcesResultSet GetPlateCornerForces(IEnumerable<PlateCornerForcesKey> keys, LoadCaseType loadCaseType, IOSGeometry geometry, IOSLoad load);

        #endregion
    }
}
