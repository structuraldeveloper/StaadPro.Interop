using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.AnalysisCommands;
using StaadPro.Interop.Models.Commands;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Configures analysis instructions on the captured native Commands receiver.</summary>
    /// <remarks>These operations configure commands; Root methods perform actual solver execution. A native binding or successful submission does not certify model applicability, licensing, convergence or result freshness.</remarks>
    public interface IOSCommands : IOSBase
    {
        /// <summary>Gets the captured native receiver; raw calls bypass typed validation and coordination.</summary>
        dynamic ComObject { get; }

        /// <summary>Configures first-order elastic analysis instructions.</summary>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Unlike Root.AnalyzeEx, this adds model commands and never runs the solver. Native void completion supplies no success flag.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformAnalysis(PrintOption.PrintStaticsCheck);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformAnalysis(PrintOption printOption);

        /// <summary>Configures the documented small-delta P-Delta command.</summary>
        /// <param name="numberOfIterations">Positive iteration count for the configured command; no iterations are executed by this call.</param>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Use PerformPDeltaAnalysisEx to select large versus small delta or geometric stiffness. The deprecated Converge entry is not used. Native void gives no acknowledgement.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformPDeltaAnalysisNoConverge(15, PrintOption.NoPrint);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformPDeltaAnalysisNoConverge(int numberOfIterations, PrintOption printOption);

        /// <summary>Configures P-Delta instructions with an explicit displacement formulation.</summary>
        /// <param name="numberOfIterations">Positive iteration count for the configured command; no iterations are executed by this call.</param>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <param name="displacementEffect">LargeDelta (0) or SmallDelta (1), selecting the documented P-Delta formulation.</param>
        /// <param name="includeGeometricStiffness">Whether the native geometric-stiffness option is enabled; marshaled as Int16 0/1.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: SmallDelta is native 1 and LargeDelta 0; geometric stiffness is a separate Int16 0/1 flag. Unlike the NoConverge shortcut, both choices are explicit. Native void gives no acknowledgement.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformPDeltaAnalysisEx(15, PrintOption.NoPrint, PDeltaDisplacementEffect.SmallDelta, true);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformPDeltaAnalysisEx(int numberOfIterations, PrintOption printOption, PDeltaDisplacementEffect displacementEffect, bool includeGeometricStiffness);

        /// <summary>Configures P-Delta instructions from the main-project typed options.</summary>
        /// <param name="options">Non-null typed command settings. Do not mutate this object or nested settings concurrently. Values and active parameter packets are validated before dispatch.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: This convenience delegates once to PerformPDeltaAnalysisEx and reports that exact native function. It does not call both P-Delta variants or execute analysis. Native void gives no acknowledgement.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformPDeltaAnalysis(new StaadPro.Interop.Models.Commands.PDeltaAnalysisOptions());
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformPDeltaAnalysis(PDeltaAnalysisOptions options);

        /// <summary>Configures the legacy cable-analysis instruction.</summary>
        /// <param name="numberOfIterations">Positive iteration count for the configured command; no iterations are executed by this call.</param>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Requires cable members and a compatible license/context. Native may select its licensed algorithm. Use PerformCableAnalysisEx for explicit algorithm and parameter packets. PrintModeShapes=4 is unavailable here. Native void gives no acknowledgement.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformCableAnalysis(300, PrintOption.NoPrint);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformCableAnalysis(int numberOfIterations, PrintOption printOption);

        /// <summary>Configures basic or advanced cable analysis using typed settings.</summary>
        /// <param name="options">Non-null typed command settings. Do not mutate this object or nested settings concurrently. Values and active parameter packets are validated before dispatch.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Basic sends seven Double parameters and null advanced flags; Advanced sends three Double parameters and two Int16 flags. Source defaults and validation are retained. PrintModeShapes=4 is unavailable. Native 1/true acknowledges configuration, 0/false reports failure; neither proves license availability or convergence.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformCableAnalysisEx(new StaadPro.Interop.Models.Commands.CableAnalysisOptions());
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformCableAnalysisEx(CableAnalysisOptions options);

        /// <summary>Configures legacy buckling-analysis instructions.</summary>
        /// <param name="maximumSteps">Positive maximum iteration/step setting; solver applicability follows the documented native method.</param>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Use PerformBucklingAnalysisEx to choose Iterative or Eigen explicitly and obtain a native acknowledgement. PrintModeShapes=4 is unavailable here. Native void gives no acknowledgement.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformBucklingAnalysis(15, PrintOption.NoPrint);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformBucklingAnalysis(int maximumSteps, PrintOption printOption);

        /// <summary>Configures buckling analysis with an explicit solver method.</summary>
        /// <param name="method">Iterative (0) or Eigen (1) buckling method.</param>
        /// <param name="maximumSteps">Positive maximum iteration/step setting; solver applicability follows the documented native method.</param>
        /// <param name="printOption">Native print selector 0..6. Cable and buckling operations reject PrintModeShapes (4); this is not a bit mask.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Iterative=0 and Eigen=1 are native selectors. maximumSteps applies to the basic solver as documented; it is not a promised iteration count for every engine. PrintModeShapes=4 is reserved. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformBucklingAnalysisEx(BucklingAnalysisMethod.Eigen, 15, PrintOption.NoPrint);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformBucklingAnalysisEx(BucklingAnalysisMethod method, int maximumSteps, PrintOption printOption);

        /// <summary>Configures AISC Direct Analysis instructions.</summary>
        /// <param name="options">Non-null typed command settings. Do not mutate this object or nested settings concurrently. Values and active parameter packets are validated before dispatch.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Uses LRFD=1 or ASD=2, four Double parameters and two Int16 flags in the documented order. P-Delta iterations above 15 are rejected rather than silently reset. Translation/rotation displacement tolerance retains the native mixed convention; no unit conversion occurs. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformDirectAnalysis(new StaadPro.Interop.Models.Commands.DirectAnalysisOptions());
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformDirectAnalysis(DirectAnalysisOptions options);

        /// <summary>Configures nonlinear-analysis and optional displacement-control instructions.</summary>
        /// <param name="options">Non-null typed command settings. Do not mutate this object or nested settings concurrently. Values and active parameter packets are validated before dispatch.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: ArcLength zero disables displacement control. Null target sends native null; an explicit target sends joint ID, global DOF and signed target value as three Doubles. Rebuild and geometric stiffness remain independent selectors. Native 1/true acknowledges and 0/false fails. No result or convergence is inferred.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.PerformNonlinearAnalysisEx(new StaadPro.Interop.Models.Commands.NonlinearAnalysisOptions());
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult PerformNonlinearAnalysisEx(NonlinearAnalysisOptions options);

        /// <summary>Adds or updates the floor-diaphragm base instruction.</summary>
        /// <param name="baseElevation">Finite signed base elevation in the current native length units; no conversion or floor lookup occurs.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Elevation is a finite signed value in current native length units, not a node ID or floor number. Does not create diaphragm geometry. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.SetFloorDiaphragmBaseCommand(0.0);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult SetFloorDiaphragmBaseCommand(double baseElevation);

        /// <summary>Deletes the floor-diaphragm base instruction.</summary>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: This removes command configuration, not diaphragm entities or an analysis result. Native 1/true acknowledges and 0/false fails; false is not interpreted as proven absence.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.DeleteFloorDiaphragmBaseCommand();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult DeleteFloorDiaphragmBaseCommand();

        /// <summary>Adds or updates the soft-story checking instruction.</summary>
        /// <param name="designCode">A defined operation-specific code enum. Soft-story and irregularities selectors have different valid values.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Native codes are IS1893:2002=1, ASCE7:2005/2010/2016=2, IS1893:2016=3. This selector differs from the irregularities selector, whose value 1 is unavailable. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.SetCheckSoftStoryCommand(SoftStoryDesignCode.Is1893_2016);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult SetCheckSoftStoryCommand(SoftStoryDesignCode designCode);

        /// <summary>Deletes the soft-story checking instruction.</summary>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Does not run a structural-code check or delete model stories. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.DeleteCheckSoftStoryCommand();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult DeleteCheckSoftStoryCommand();

        /// <summary>Adds or updates the irregularities checking instruction.</summary>
        /// <param name="designCode">A defined operation-specific code enum. Soft-story and irregularities selectors have different valid values.</param>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Requires floor-diaphragm data in the native model. ASCE7:2016=2 and IS1893:2016=3; value 1 is not supported by this contract. No applicability or code-compliance conclusion is produced. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.SetCheckIrregularitiesCommand(IrregularitiesDesignCode.Is1893_2016);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult SetCheckIrregularitiesCommand(IrregularitiesDesignCode designCode);

        /// <summary>Deletes the irregularities checking instruction.</summary>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Does not delete floor diaphragms or clear existing engineering results. Native 1/true acknowledges and 0/false fails.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.DeleteCheckIrregularitiesCommand();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult DeleteCheckIrregularitiesCommand();

        /// <summary>Deletes the native perform-analysis command inventory.</summary>
        /// <returns>A named CommandSubmissionResult receipt with actual native evidence and operation-specific status. No Data property, native reference output, fabricated identifier or analysis result is returned.</returns>
        /// <remarks><para>When to use: Native 1/true acknowledges deletion. Native 0/false specifically means no perform-analysis commands were present and returns NoCommandsPresent, not a generic failure. Other model instructions and files are not asserted to be cleared.</para><para>Runs synchronously on the caller's established COM apartment, under one Commands gate, with captured binding checks before and after the operation. Configures model instructions only; use Root.AnalyzeEx or another explicit Root analysis operation to execute the solver. No save, retry, rollback, read-back, unit conversion or native license/result/convergence certification occurs. Options are copied into fresh native arrays before dispatch. Do not mutate settings concurrently. Other adapters, raw access and external STAAD edits are not locked out. Native changes may precede an exception. Reviewed declarations and non-example contracts match across installed STAAD.Pro 2024, 2025 and 2026; one common internal service supplies these routes. Binding presence and managed tests do not certify live engineering behavior. Reacquire the adapter after wrapper refresh.</para></remarks>
        /// <exception cref="ArgumentException">A required input is null, an identifier/enum/limit is invalid, or a numeric setting violates its documented finite/range requirements; no native mutation is invoked.</exception>
        /// <exception cref="InvalidOperationException">The binding is disposed/replaced, or native acknowledgement/identifier has an undocumented type or value. A post-call error can follow native changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The original native transport exception propagates; no retry or fallback mutation occurs.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired receiver does not expose the required compatible native operation.</exception>
        /// <example><code>bool applyChanges = false;
        /// if (applyChanges)
        /// {
        ///     var result = commands.DeleteAllAnalysisCommands();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        CommandSubmissionResult DeleteAllAnalysisCommands();
    }
}
