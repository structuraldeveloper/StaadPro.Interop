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
    }
}
