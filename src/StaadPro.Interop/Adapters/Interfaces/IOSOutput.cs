using System.Collections.Generic;
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
    }
}
