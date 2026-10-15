using System;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using System.Collections.Generic;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Root;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Exposes OpenSTAAD model identity, metadata, units, file lifecycle, polling, application-window control and analysis requests.</summary>
    /// <remarks>Combines read-only queries with explicit native model/file and window mutations. Other IOSRoot operations are ported separately.
    /// Native access uses runtime dispatch and requires an undisposed connection on the correct COM thread.</remarks>
    public interface IOSRoot
    {
        /// <summary>Gets the root OpenSTAAD automation object associated with this binding.</summary>
        /// <value>The raw root object; its Geometry property identifies the model modified by extended operations.</value>
        dynamic ComObject { get; }

        /// <summary>Gets the native STAAD.Pro version associated with this root binding.</summary>
        /// <returns>An independent four-part native version, or null when the optional native version member is absent.</returns>
        /// <remarks><para>For a wrapper-owned Root, returns the owning wrapper's cached StaadVersion without native dispatch. This is the STAAD version, not the client assembly ApplicationVersion. RefreshComObjects updates the snapshot. For a directly constructed adapter, queries the captured root on each call and validates returned text against any written numeric outputs. Does not acquire another session or establish feature availability.</para>
        /// <para>Use the bound session's COM thread and serialize refresh and access. These reads do not certify connection liveness or make a native call interruptible.</para></remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper has been disposed; applies to wrapper-owned bindings.</exception>
        /// <exception cref="InvalidOperationException">Malformed or contradictory native version data was returned.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native version, base-unit or handle request fails; non-missing-member failures propagate.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A native dispatch has an incompatible signature that is not handled as an absent optional member.</exception>
        /// <example>With a connected IOSRoot named root:<code>
        /// ApplicationVersion version = root.GetApplicationVersion();
        /// Console.WriteLine(version?.Display ?? "Unavailable");
        /// </code></example>
        ApplicationVersion GetApplicationVersion();

        /// <summary>Gets the model base unit system associated with this root binding.</summary>
        /// <returns>The normalized base-system value; Unknown for an absent member, null or DBNull. Unrecognized integral native codes are preserved as enum values.</returns>
        /// <remarks><para>For a wrapper-owned Root, returns the BaseUnitSystem snapshot without native dispatch; call RefreshUnits or RefreshComObjects on the wrapper to update it. A directly constructed adapter queries its captured native root on each call. Does not convert model data, change units or infer a base system from the input units.</para>
        /// <para>Use the bound session's COM thread and serialize refresh and access. These reads do not certify connection liveness or make a native call interruptible.</para></remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper has been disposed; applies to wrapper-owned bindings.</exception>
        /// <exception cref="InvalidOperationException">The native base-unit value has a nonintegral or out-of-range representation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native version, base-unit or handle request fails; non-missing-member failures propagate.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A native dispatch has an incompatible signature that is not handled as an absent optional member.</exception>
        /// <example>With a connected IOSRoot named root:<code>
        /// BaseUnitSystem baseSystem = root.GetBaseUnit();
        /// Console.WriteLine(baseSystem);
        /// </code></example>
        BaseUnitSystem GetBaseUnit();

        /// <summary>Gets the model input force unit associated with this root binding.</summary>
        /// <returns>The wrapper-compatible force-unit enum. If no usable unit is reported, the compatibility fallback is KiloPound for Imperial base units and KiloNewton otherwise; that fallback is not a measured unit.</returns>
        /// <remarks><para>For a wrapper-owned Root, returns InputForceUnit without native dispatch; refresh the wrapper units explicitly after external changes. A directly constructed adapter rereads version, base system and force unit from the same captured root on every call. Internal native reference output takes precedence over an eligible numeric return. Version 24 success flags are not interpreted as unit codes. Native unit-query failures retain a usable written output or use the documented fallback; version/base-unit failures still propagate. No model mutation or implicit save occurs.</para>
        /// <para>Use the bound session's COM thread and serialize refresh and access. These reads do not certify connection liveness or make a native call interruptible.</para></remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper has been disposed; applies to wrapper-owned bindings.</exception>
        /// <exception cref="InvalidOperationException">Native version or base-unit metadata is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native version, base-unit or handle request fails; non-missing-member failures propagate.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A native dispatch has an incompatible signature that is not handled as an absent optional member.</exception>
        /// <example>With a connected IOSRoot named root:<code>
        /// ForceInputUnit forceUnit = root.GetInputUnitForForce();
        /// Console.WriteLine(forceUnit);
        /// </code></example>
        ForceInputUnit GetInputUnitForForce();

        /// <summary>Gets the model input length unit associated with this root binding.</summary>
        /// <returns>The wrapper-compatible length-unit enum, with native feet aliases normalized to Feet. If no usable unit is reported, the fallback is Feet for Imperial base units and Meter otherwise; that fallback is not a measured unit.</returns>
        /// <remarks><para>For a wrapper-owned Root, returns InputLengthUnit without native dispatch; refresh the wrapper units explicitly after external changes. A directly constructed adapter rereads version, base system and length unit from the same captured root on every call. Internal native reference output takes precedence over an eligible numeric return. Version 24 success flags are not interpreted as unit codes. Native unit-query failures retain a usable written output or use the documented fallback; version/base-unit failures still propagate. No model mutation or implicit save occurs.</para>
        /// <para>Use the bound session's COM thread and serialize refresh and access. These reads do not certify connection liveness or make a native call interruptible.</para></remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper has been disposed; applies to wrapper-owned bindings.</exception>
        /// <exception cref="InvalidOperationException">Native version or base-unit metadata is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native version, base-unit or handle request fails; non-missing-member failures propagate.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A native dispatch has an incompatible signature that is not handled as an absent optional member.</exception>
        /// <example>With a connected IOSRoot named root:<code>
        /// LengthInputUnit lengthUnit = root.GetInputUnitForLength();
        /// Console.WriteLine(lengthUnit);
        /// </code></example>
        LengthInputUnit GetInputUnitForLength();

        /// <summary>Gets the non-owning STAAD main-window handle associated with this root binding.</summary>
        /// <returns>The cached wrapper handle or a standalone same-root native handle. Zero when the native version is unavailable, its major version is 20 or earlier, or the optional window member is absent. Zero can also be returned by the native API or a configured wrapper window service.</returns>
        /// <remarks><para>For a wrapper-owned Root, reuses StaadApplicationWindowHandle, including its configured model service; RefreshComObjects updates the cache. A directly constructed adapter rereads native version and, for major versions greater than 20, the handle from the captured root. Does not inspect global active objects or choose a process by name. The handle is borrowed: do not close it. A cached handle can expire and does not establish live-window validity, PID ownership or model readiness. Existing window-control methods perform their own live validation.</para>
        /// <para>Use the bound session's COM thread and serialize refresh and access. These reads do not certify connection liveness or make a native call interruptible.</para></remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper has been disposed; applies to wrapper-owned bindings.</exception>
        /// <exception cref="InvalidOperationException">Native version metadata or the default native window-handle representation is invalid.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native version, base-unit or handle request fails; non-missing-member failures propagate.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">A native dispatch has an incompatible signature that is not handled as an absent optional member.</exception>
        /// <example>With a connected IOSRoot named root:<code>
        /// IntPtr windowHandle = root.GetMainWindowHandle();
        /// Console.WriteLine(windowHandle);
        /// </code></example>
        IntPtr GetMainWindowHandle();

        /// <summary>Reads whether analysis is currently running in the bound STAAD application.</summary>
        /// <returns>True for native Boolean true or integral one; false for Boolean false or integral zero.</returns>
        /// <remarks><para>Calls native IsAnalyzing once. Other integers, null, strings, fractional numbers and unsupported shapes
        /// are rejected rather than interpreted as flags. A false value does not mean an analysis completed or succeeded;
        /// use GetAnalysisStatus to obtain the state of a particular model.</para>
        /// <para>This live predicate is separate from the wrapper metadata cache. It does not start or wait for analysis,
        /// query a filename, read result files, save, change silent mode or launch STAAD. Use the bound session's COM thread
        /// and coordinate shared access. The returned value is a snapshot; a native call may block.</para></remarks>
        /// <exception cref="InvalidOperationException">The native response is neither a Boolean nor an integral zero/one flag.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native predicate fails; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured root lacks a compatible IsAnalyzing member.</exception>
        /// <example><code>
        /// bool isRunning = root.IsAnalyzing();
        /// Console.WriteLine(isRunning);
        /// </code></example>
        bool IsAnalyzing();

        /// <summary>Reads native analysis state, diagnostic counts and CPU time for the specified model.</summary>
        /// <param name="modelFilePath">Non-null, non-whitespace model path. Relative paths are resolved against the caller process's current directory before native access. No file existence or extension check is performed.</param>
        /// <returns>An immutable result containing the absolute queried path, exact native status, nullable recognized status,
        /// query success, completion state, and available warning count, error count and CPU time in seconds.</returns>
        /// <remarks><para>Calls GetAnalysisStatus once on the captured root, using internal Int32 reference buffers for warning/error
        /// counts and Double for CPU time. No reference parameters or native output buffers are exposed to callers.
        /// Native codes are -2 invalid path, -1 terminated, 0 general error, 1 running, 2 completed cleanly,
        /// 3 completed with warnings, 4 completed with errors and 5 not performed. Unknown signed Int32 codes remain inspectable.</para>
        /// <para>IsSuccessful means a recognized analysis state was retrieved (-1 or 1 through 5), not that analysis succeeded.
        /// Invalid-path, general-error and unknown-status results expose no output data. Counts and CPU time are required,
        /// nonnegative and finite for completed states 2 through 4. In other recognized states, written valid fields are retained
        /// independently; initial Int32.MinValue count buffers and a NaN time buffer represent unavailable fields. An unwritten
        /// time and an explicitly written NaN cannot be distinguished for these noncompleted states. Other malformed outputs throw.
        /// Numeric zero is valid data. Count/status consistency beyond these shape/range checks is not inferred.</para>
        /// <para>The model need not be the currently open model. No filename read, file opening, analysis, polling, save,
        /// result-file parsing or filesystem mutation occurs. Use the session's COM thread; serialize shared access.
        /// This is a live, sequential native observation, not a transaction or proof of current result freshness. No automatic retry
        /// or time limit is imposed; a native call may block.</para></remarks>
        /// <exception cref="ArgumentNullException"><paramref name="modelFilePath"/> is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException"><paramref name="modelFilePath"/> is blank or contains invalid Windows path characters.</exception>
        /// <exception cref="NotSupportedException">The requested path format is unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">The requested path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">The native status is not an integral Int32-range value, or a recognized-state output is invalid or a completed-state output is unwritten.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native query fails; the original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible native signature, including the required reference argument types.</exception>
        /// <example><code>
        /// var status = root.GetAnalysisStatus(@"C:\Models\Frame.STD");
        /// Console.WriteLine(status.NativeStatus);
        /// if (status.IsCompleted) Console.WriteLine(status.ErrorCount);
        /// </code></example>
        AnalysisStatusResult GetAnalysisStatus(string modelFilePath);

        /// <summary>Reads analysis status for the model path currently reported by this root session.</summary>
        /// <returns>The same immutable status/data contract as GetAnalysisStatus(string), for the path captured by one native filename read.</returns>
        /// <remarks><para>Reads GetStaadFileFullPath once, rejects an empty or whitespace-only filename, then delegates to
        /// the explicit-path overload. An unnamed session throws before any analysis-status query; use the explicit-path overload
        /// to query a known model without relying on an open filename. Relative native paths are normalized by the delegated overload.</para>
        /// <para>Uses live native reads rather than the wrapper metadata cache. The model can change between the filename and status
        /// reads; the result identifies the path actually queried and does not guarantee that it remains the open model.
        /// Does not start analysis, open/save/close models, poll or launch STAAD. Use the bound session's COM thread and serialize
        /// shared access. No retries or bounded native-call completion are provided. IsSuccessful indicates state retrieval,
        /// including terminated, running, completed-with-errors and not-performed states; inspect Status for the analysis outcome.</para></remarks>
        /// <exception cref="InvalidOperationException">No named model is available, the filename/status response is malformed, or a required completed-state output is missing or invalid.</exception>
        /// <exception cref="ArgumentException">The reported model path contains invalid Windows path characters.</exception>
        /// <exception cref="NotSupportedException">The reported model path format is unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">The reported path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native filename or status query fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured root lacks a compatible filename or analysis-status member.</exception>
        /// <example><code>
        /// var status = root.GetAnalysisStatus();
        /// Console.WriteLine(status.ModelFilePath);
        /// Console.WriteLine(status.Status);
        /// </code></example>
        AnalysisStatusResult GetAnalysisStatus();

        /// <summary>Reads the current model's folder directly from the bound OpenSTAAD root.</summary>
        /// <returns>The exact directory string written by native GetSTAADFileFolder, including its original spelling,
        /// whitespace, separator style and any trailing separator. An explicitly written empty string remains empty.</returns>
        /// <remarks><para>Calls the native void GetSTAADFileFolder operation once with an internally initialized string VARIANT
        /// reference. Rejects an unwritten buffer, null or any non-string value. The public API has no reference parameters
        /// and does not invent a success flag for a native void query.</para>
        /// <para>Does not read the model filename or derive a directory using Path.GetDirectoryName. No path normalization,
        /// filesystem lookup, folder creation, model opening or wrapper cache refresh occurs. Empty output is preserved as reported;
        /// it does not establish why a folder is unavailable or prove a blank/unnamed model.</para>
        /// <para>Uses the captured root on the caller's COM thread. Serialize shared-session access. This live read is a snapshot,
        /// not a readiness check or a guarantee that the directory exists. A native call can block; no retry is performed.</para></remarks>
        /// <exception cref="InvalidOperationException">The native folder output is unwritten, null or not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native folder query fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFileFolder reference-output signature.</exception>
        /// <example><code>
        /// string modelFolder = root.GetStaadFileFolder();
        /// Console.WriteLine(modelFolder);
        /// </code></example>
        string GetStaadFileFolder();

        /// <summary>Reads whether the model bound to this OpenSTAAD root is a physical model.</summary>
        /// <returns>True for a native Boolean true or integral one; false for Boolean false or integral zero.</returns>
        /// <remarks><para>Calls native IsPhysicalModel once and validates its flag. Other integer values, null, strings,
        /// floating-point and decimal values are rejected instead of being coerced into a model classification.
        /// A false result reports the native predicate only; it is not a guarantee that an analytical model is loaded or ready.
        /// Native behavior for blank or unsupported sessions is not inferred.</para>
        /// <para>Does not convert the model between physical and analytical representations, start analysis, open/save models,
        /// query a filename or refresh metadata. Uses the captured root and calling COM thread; coordinate shared access.
        /// The returned classification is a live snapshot and can change after the read. No retry or native-call timeout is imposed.</para></remarks>
        /// <exception cref="InvalidOperationException">The native response is neither a Boolean nor an integral zero/one flag.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native classification query fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible IsPhysicalModel member.</exception>
        /// <example><code>
        /// bool isPhysical = root.IsPhysicalModel();
        /// Console.WriteLine(isPhysical);
        /// </code></example>
        bool IsPhysicalModel();

        /// <summary>Reads the error-message text currently reported by the bound OpenSTAAD root.</summary>
        /// <returns>The exact native string, preserving Unicode, line breaks and whitespace; an explicitly returned empty string remains empty.</returns>
        /// <remarks><para>Calls native GetErrorMessage once when explicitly requested. Rejects null and non-string responses;
        /// does not substitute a managed exception message or manufacture an error from other session state.
        /// An empty message does not establish that every prior operation succeeded.</para>
        /// <para>The vendor help does not specify whether reading consumes or clears the native error state. Native semantics
        /// therefore apply; this method does not promise a nondestructive peek, stable repeated reads or error history.
        /// It is not automatically called from another operation's failure handler, so it cannot replace that operation's original exception.</para>
        /// <para>Uses the captured root on the caller's COM thread. Coordinate access so other operations do not replace the native error
        /// before it is read. No model opening/saving, error-triggering probe, analysis, retry or wrapper cache refresh is performed.
        /// Native calls may block. Availability and licensing errors remain the server's responsibility.</para></remarks>
        /// <exception cref="InvalidOperationException">The native error-message response is null or not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native message query fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetErrorMessage member.</exception>
        /// <example><code>
        /// string nativeMessage = root.GetErrorMessage();
        /// Console.WriteLine(nativeMessage);
        /// </code></example>
        string GetErrorMessage();

        /// <summary>Reads the process identifier reported by this acquired OpenSTAAD root.</summary>
        /// <returns>A positive UInt32 process identifier from the bound native root.</returns>
        /// <remarks><para>Calls native GetProcessId once and accepts integral CLR representations only, within 1 through UInt32.MaxValue.
        /// Rejects zero, negative, overflowing, Boolean, string, floating-point, decimal and pointer-shaped values.
        /// Shares validation with the existing window-control operations.</para>
        /// <para>Does not enumerate processes, consult a global active object, read a window handle, acquire another session,
        /// launch STAAD or construct a Process object. A PID is an identifier, not an owning process handle. It may become stale
        /// or be recycled after this snapshot; no process liveness or model-readiness guarantee is made.
        /// Use the bound session's COM thread and coordinate access. No retry or bounded native-call completion is provided.</para></remarks>
        /// <exception cref="InvalidOperationException">The native process identifier is not a supported positive UInt32-range integral value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native query fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetProcessId member.</exception>
        /// <example><code>
        /// uint processId = root.GetProcessId();
        /// Console.WriteLine(processId);
        /// </code></example>
        uint GetProcessId();

        /// <summary>Requests changing only the current model's input length unit.</summary>
        /// <param name="lengthUnit">LengthInputUnit identifying native code 0 through 7. Named values are Inch=0, Feet=2, CentiMeter=3, Meter=4, MilliMeter=5, DeciMeter=6 and KiloMeter=7. The documented feet alias (LengthInputUnit)1 is also accepted and dispatched unchanged.</param>
        /// <remarks><para>Validates the complete unit argument before one native SetInputUnitForLength call with an Int32 code.
        /// Does not route through the combined unit setter, read or set force units, or change the managed unit cache.
        /// Unlike this method, the existing SetInputUnitsEx retains its named-enum validation and still rejects unnamed code 1.</para>
        /// <para>The native operation is void: normal return means dispatch completed, not verified read-back or persistence.
        /// No save, analysis, structure update, value rescaling, base-system change or rollback is promised or added by this library.
        /// On a wrapper-owned connection, explicitly call wrapper.RefreshUnits when fresh cached unit values are needed.
        /// Use the session's COM thread and serialize native mutations and refresh. No automatic retry occurs after an uncertain failure;
        /// the request may have taken effect before an exception. Native calls may block or display server UI.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lengthUnit"/> is outside native codes 0 through 7; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native unit request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SetInputUnitForLength member.</exception>
        /// <example><code>
        /// bool changeLengthUnit = false; // enable only when the model should be changed
        /// if (changeLengthUnit) root.SetInputUnitForLength(LengthInputUnit.Meter);
        /// </code></example>
        void SetInputUnitForLength(LengthInputUnit lengthUnit);

        /// <summary>Requests changing only the current model's input force unit.</summary>
        /// <param name="forceUnit">Defined ForceInputUnit: KiloPound=0, Pound=1, KiloGram=2, MetricTon=3, Newton=4, KiloNewton=5, MegaNewton=6 or DecaNewton=7.</param>
        /// <remarks><para>Validates the enum before one native SetInputUnitForForce call with its Int32 code.
        /// Does not read or set length units, invoke the combined unit setter or refresh cached wrapper properties.
        /// Explicitly call wrapper.RefreshUnits after the request when fresh cached unit information is needed.</para>
        /// <para>Native void completion does not verify the resulting unit or file persistence. The library adds no save,
        /// analysis, structure update, value rescaling, base-system change or transaction. Native failures may follow partial effects;
        /// no retry or rollback occurs. Use the session's COM thread and coordinate mutations and refresh.
        /// Native calls may block or display server UI.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="forceUnit"/> is not defined; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native unit request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SetInputUnitForForce member.</exception>
        /// <example><code>
        /// bool changeForceUnit = false; // enable only when the model should be changed
        /// if (changeForceUnit) root.SetInputUnitForForce(ForceInputUnit.KiloNewton);
        /// </code></example>
        void SetInputUnitForForce(ForceInputUnit forceUnit);

        /// <summary>Requests updating the current model's job name, client and originating engineer.</summary>
        /// <param name="jobName">Non-null job name text, sent unchanged. Empty or whitespace text is permitted.</param>
        /// <param name="jobClient">Non-null client text, sent unchanged. Empty or whitespace text is permitted.</param>
        /// <param name="engineerName">Non-null originating engineer name, sent unchanged in the native engineer position.</param>
        /// <remarks><para>Validates all three inputs before one native SetShortJobInfo call in job-name, client, engineer order.
        /// Preserves supplied Unicode, spaces and line breaks; native text limits and interpretation still apply.
        /// Empty input is sent as supplied, not replaced with another value. This does not establish the server's clearing or storage behavior.</para>
        /// <para>The native function is void: return indicates dispatch completion, not verified storage or persistence.
        /// No full-job metadata update, save, model opening/closing, analysis or read-back is added. Use GetShortJobInfo explicitly
        /// when read-back is required. Serialize shared model access on the session's COM thread.
        /// Native calls may block or display UI. Exceptions propagate without retry or rollback; side effects may already have occurred.</para></remarks>
        /// <exception cref="ArgumentNullException">One of the three strings is null; all input validation precedes native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native metadata request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible three-input SetShortJobInfo member.</exception>
        /// <example><code>
        /// bool updateJobMetadata = false; // enable only when the model should be changed
        /// if (updateJobMetadata) root.SetShortJobInfo("Frame study", "Client", "Engineer");
        /// </code></example>
        void SetShortJobInfo(string jobName, string jobClient, string engineerName);

        /// <summary>Requests updating short job metadata from an existing managed job-information object.</summary>
        /// <param name="jobInfo">Non-null ShortJobInfo with non-null Name, Client and Originator. Empty strings are permitted. A FullJobInfo is accepted through its inherited short fields; its other fields are not submitted.</param>
        /// <remarks><para>Reads each of the three mutable properties once into local string snapshots, validates them before native access,
        /// then delegates to the three-string overload. Reuses the existing model class without creating a parallel write-options type.
        /// Editing the managed object alone does not update STAAD. Coordinate concurrent edits to obtain a coherent snapshot;
        /// these property reads are not an atomic transaction.</para>
        /// <para>Preserves text and native argument order. Normal void return indicates native dispatch completion only.
        /// No full-job update, date conversion, save, read-back, structure update or analysis occurs. Use the appropriate session COM thread;
        /// native failures propagate without automatic retry or rollback and may follow native side effects.</para></remarks>
        /// <exception cref="ArgumentNullException"><paramref name="jobInfo"/> is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">Name, Client or Originator is null; the exception identifies jobInfo and no native call is made.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native metadata request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SetShortJobInfo member.</exception>
        /// <example><code>
        /// var jobInfo = ShortJobInfo.From("Frame study", "Client", "Engineer");
        /// bool applyJobInfo = false; // enable only when the model should be changed
        /// if (applyJobInfo) root.SetShortJobInfo(jobInfo);
        /// </code></example>
        void SetShortJobInfo(ShortJobInfo jobInfo);

        /// <summary>Requests a native update of the current STAAD structure.</summary>
        /// <remarks><para>Invokes native UpdateStructure once with no arguments. The vendor reference describes a structure update
        /// but specifies no returned status, completion/readiness protocol, save behavior or detailed rebuild guarantees.
        /// A normal void return establishes dispatch completion only; it does not certify that pending edits are persisted or results are current.</para>
        /// <para>The library adds no analysis, save, model opening/closing, wrapper refresh, unit refresh, wait or read-back.
        /// This explicit native mutation may affect the current model; use the connected session's COM thread and coordinate edits.
        /// Native UI or a blocking call remains possible. Failures propagate without retry or rollback and can follow partial native effects.</para></remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native update request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible UpdateStructure member.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The captured COM wrapper is no longer associated with a usable native object.</exception>
        /// <example><code>
        /// bool updateCurrentStructure = false; // enable only when a native update should be requested
        /// if (updateCurrentStructure) root.UpdateStructure();
        /// </code></example>
        void UpdateStructure();

        /// <summary>Requests updating all thirteen job-information fields on the currently bound model.</summary>
        /// <param name="jobName">Non-null job name, sent unchanged; empty text is permitted.</param>
        /// <param name="jobClient">Non-null client text, sent unchanged; empty text is permitted.</param>
        /// <param name="engineerName">Non-null originating engineer name, sent in the native engineer-name position.</param>
        /// <param name="engineerDate">Non-null engineer date text in a format accepted by the connected STAAD instance. Sent literally; no DateTime conversion or parsing is performed.</param>
        /// <param name="jobNumber">Non-null job number text, sent unchanged.</param>
        /// <param name="revision">Non-null revision text, sent unchanged.</param>
        /// <param name="part">Non-null job-part text, sent unchanged.</param>
        /// <param name="reference">Non-null job-reference text, sent unchanged.</param>
        /// <param name="checkerName">Non-null checker name, sent unchanged.</param>
        /// <param name="checkerDate">Non-null checker date text accepted by the native server; empty text is sent literally.</param>
        /// <param name="approverName">Non-null approver name, sent unchanged.</param>
        /// <param name="approverDate">Non-null approval date text accepted by the native server; empty text is sent literally.</param>
        /// <param name="comments">Non-null comment text, preserving Unicode, whitespace and line breaks.</param>
        /// <remarks><para>Validates every input for null before one native SetFullJobInfo call in documented order:
        /// name, client, engineer, engineer date, number, revision, part, reference, checker, checker date, approver, approval date, comments.
        /// Empty and whitespace strings are permitted and sent unchanged; native field limits and clearing behavior still apply.</para>
        /// <para>All dates are native text, not DATE values. No culture-dependent formatting, date guessing or model-factory reordering occurs.
        /// The existing FullJobInfo DateTime-based model is not automatically serialized; callers provide native date strings explicitly.
        /// Native acceptance of a date format and storage/persistence remain the server's responsibility.</para>
        /// <para>The native function returns void: normal return means dispatch completed, not verified storage or save.
        /// No automatic SaveModel, read-back, analysis, refresh, retry or rollback is added. A native error can occur after side effects.
        /// Use the session's COM thread and serialize shared edits. Native calls can block or display dialogs.</para></remarks>
        /// <exception cref="ArgumentNullException">Any text input is null; identifies that parameter and prevents native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native metadata update fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible thirteen-input SetFullJobInfo member.</exception>
        /// <example><code>
        /// bool updateFullJob = false; // enable only for an intended native model change
        /// if (updateFullJob)
        ///     root.SetFullJobInfo("Frame", "Client", "Engineer", "", "J-1", "A", "Part", "Ref", "Checker", "", "Approver", "", "Notes");
        /// </code></example>
        void SetFullJobInfo(string jobName, string jobClient, string engineerName, string engineerDate, string jobNumber, string revision, string part, string reference, string checkerName, string checkerDate, string approverName, string approverDate, string comments);

        /// <summary>Requests analysis through the native Analyze entry point for the current model.</summary>
        /// <remarks><para>Calls native Analyze once with no arguments. This is the documented UI-equivalent analysis request;
        /// it is not substituted with AnalyzeEx or AnalyzeModel and no guessed analysis flags are applied.
        /// Application silent-mode, license and native dialog behavior still apply.</para>
        /// <para>The native return is void. Normal return does not report analysis success, completion, result availability or persistence.
        /// Use IsAnalyzing/GetAnalysisStatus explicitly to observe state, or AnalyzeEx when its documented controls are needed.
        /// No save, polling, timeout, wrapper refresh, retry or rollback is added. The call may block or display UI.
        /// Use the acquired session's COM thread and serialize analysis and edits; an exception may follow a started analysis.</para></remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native analysis request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible Analyze member.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The captured COM wrapper is no longer associated with a usable native object.</exception>
        /// <example><code>
        /// bool runAnalysis = false; // enable only when analysis should actually be requested
        /// if (runAnalysis) root.Analyze();
        /// </code></example>
        void Analyze();

        /// <summary>Requests analysis using the documented native engine selector.</summary>
        /// <param name="engine">AnalysisEngine.StaadPro, native code -1. It is the only documented supported value and the managed default; other values are rejected before dispatch.</param>
        /// <remarks><para>Invokes native AnalyzeModel once with an Int32 -1 engine code. The optional managed default still sends that
        /// argument explicitly. The native void signature is confirmed by installed 2025/2026 interop metadata despite a help example
        /// assigning a return value. No success code is fabricated or interpreted.</para>
        /// <para>This operation is distinct from Analyze and AnalyzeEx. Normal return does not guarantee analysis completion or valid results.
        /// No automatic save, silent-mode change, analysis-state query, polling, wrapper refresh, retry or rollback occurs.
        /// Calls may block or display native UI. Use the session's COM thread; coordinate analysis and edits.
        /// A failure can follow native effects, including a launched analysis.</para></remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="engine"/> is not the documented STAAD.Pro engine; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native engine request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible AnalyzeModel member.</exception>
        /// <example><code>
        /// bool runEngine = false; // enable only when the engine should actually be requested
        /// if (runEngine) root.AnalyzeModel(); // sends the documented -1 engine argument
        /// </code></example>
        void AnalyzeModel(AnalysisEngine engine = AnalysisEngine.StaadPro);

        /// <summary>Reads the native success flag and qualified CONNECTED project information from this root.</summary>
        /// <returns>An immutable result containing IsSuccessful and project data. Project is non-null only after native success and validated string outputs; native false returns a result with null Project.</returns>
        /// <remarks><para>Calls GetCONNECTEDProjectInfo once with two internal VARIANT references initialized as distinguishable string buffers.
        /// Native Boolean values or integral 0/1 flags are accepted. On success, both outputs must be written strings;
        /// text, whitespace, Unicode and explicit empty strings are preserved without GUID coercion. Other flag/output shapes throw.
        /// On native false, partial, malformed or unwritten output buffers are not exposed as project data.</para>
        /// <para>No public reference parameters, tuples, login prompts, project assignment, network retries or automatic save are added.
        /// Native false does not prove why retrieval failed or that no association exists. Native exceptions remain exceptions.
        /// Successful retrieval does not verify cloud synchronization, membership, account credentials or continued association.
        /// Use the captured root on its COM thread and coordinate shared operations; calls may block or show server UI.</para></remarks>
        /// <exception cref="InvalidOperationException">The native flag is malformed or a successful response contains unwritten/non-string outputs.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native project query fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible two-output GetCONNECTEDProjectInfo signature.</exception>
        /// <example><code>
        /// var projectResult = root.GetConnectedProjectInfo();
        /// if (projectResult.IsSuccessful) Console.WriteLine(projectResult.Project.Name);
        /// </code></example>
        ConnectedProjectResult GetConnectedProjectInfo();

        /// <summary>Requests assigning the supplied CONNECTED project identity to the bound model.</summary>
        /// <param name="projectId">Non-null, non-whitespace project identifier. Preserved exactly; no GUID parsing or trimming occurs.</param>
        /// <param name="projectName">Non-null, non-whitespace project name. Preserved exactly, including surrounding spaces and Unicode.</param>
        /// <returns>The documented native success flag: true for Boolean true/integral one, false for Boolean false/integral zero.</returns>
        /// <remarks><para>Validates both inputs before one SetCONNECTEDProjectInfo call in ID/name order. Native unknown or malformed
        /// flags throw rather than becoming a successful assignment. Empty/whitespace values are intentionally rejected because
        /// clearing/unassigning semantics are not established; this API does not provide an implicit clearing operation.</para>
        /// <para>A true result is the native response, not verified cloud synchronization or read-back. No project creation, login,
        /// membership change, SaveModel, project query, retry or rollback is added. Native credential/network/licensing behavior still applies;
        /// calls may block or show server UI. Exceptions can follow native effects. Use the session's COM thread and serialize edits.</para></remarks>
        /// <exception cref="ArgumentNullException">Either input is null; identifies the parameter and prevents native access.</exception>
        /// <exception cref="ArgumentException">Either input is empty or whitespace-only; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The native return is not a Boolean or integral zero/one flag.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native assignment fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SetCONNECTEDProjectInfo member.</exception>
        /// <example><code>
        /// bool assignProject = false; // enable only with an intended project identity
        /// if (assignProject) Console.WriteLine(root.SetConnectedProjectInfo("project-id", "Project name"));
        /// </code></example>
        bool SetConnectedProjectInfo(string projectId, string projectName);

        /// <summary>Requests assigning CONNECTED project information from an immutable managed value.</summary>
        /// <param name="projectInfo">Non-null ConnectedProjectInfo whose ProjectId and Name contain non-whitespace text. Values obtained from a successful query can be reused after validation.</param>
        /// <returns>The native assignment flag, with the same meaning as the two-string overload.</returns>
        /// <remarks><para>Reuses the same data model returned by GetConnectedProjectInfo. Checks the object and fields before native access,
        /// then delegates to the string overload. The model itself permits empty query snapshots, but assignment rejects those fields.
        /// Does not mutate the supplied value, trim text, force GUID syntax or infer an unassignment.</para>
        /// <para>The native response does not certify cloud synchronization or persistence. No save, read-back, login, retry or rollback is added.
        /// Coordinate access on the session's COM thread; native calls may block, show UI or fail after effects.</para></remarks>
        /// <exception cref="ArgumentNullException"><paramref name="projectInfo"/> is null.</exception>
        /// <exception cref="ArgumentException">The supplied identity or name is blank; identifies projectInfo and prevents native access.</exception>
        /// <exception cref="InvalidOperationException">The native return flag has an unsupported shape or value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native assignment fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible native assignment member.</exception>
        /// <example><code>
        /// var projectInfo = root.GetConnectedProjectInfo().Project;
        /// bool reuseProject = false; // enable only when reassignment is intended
        /// if (reuseProject &amp;&amp; projectInfo != null) Console.WriteLine(root.SetConnectedProjectInfo(projectInfo));
        /// </code></example>
        bool SetConnectedProjectInfo(ConnectedProjectInfo projectInfo);

        /// <summary>Reads an opaque numeric process-handle value from the acquired OpenSTAAD root.</summary>
        /// <returns>A fresh immutable NativeProcessHandleValue containing the native bit pattern, without resource ownership or a Windows-handle usability guarantee.</returns>
        /// <remarks><para>Calls native GetProcessHandle once on this captured root. Accepts integral CLR values and IntPtr/UIntPtr
        /// representations only. Unsigned values are retained; signed negatives are sign-extended to an unsigned 64-bit bit pattern.
        /// Zero and all-one values are preserved without inventing validity or pseudo-handle semantics. The representation
        /// does not establish the server's actual handle width, lifetime, access rights or process context.</para>
        /// <para>This is diagnostic data, not an owning SafeHandle, PID, main-window handle or verified caller-process HANDLE.
        /// Do not close it or pass its numeric value to operating-system handle APIs. No process is opened, duplicated,
        /// enumerated or terminated. Use GetProcessId for process identification. Use the acquired COM thread and coordinate
        /// access; the snapshot does not prove connection liveness. There is no retry or global active-object lookup.</para></remarks>
        /// <exception cref="InvalidOperationException">The native response is null or has a nonintegral, Boolean, string, fractional or unsupported representation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native query fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetProcessHandle member.</exception>
        /// <example><code>
        /// var handleValue = root.GetProcessHandle();
        /// Console.WriteLine(handleValue.RawValue); // diagnostic value only; do not close or use as a Windows HANDLE
        /// </code></example>
        NativeProcessHandleValue GetProcessHandle();

        /// <summary>Requests native creation of a new STD model in an unnamed STAAD session.</summary>
        /// <param name="filePath">Non-null, non-whitespace new .std model path. Relative input is resolved against the process working directory. The parent directory must exist; an existing file or directory at the target is rejected.</param>
        /// <param name="lengthUnit">Native length code 0 through 7, including the documented feet alias (LengthInputUnit)1. Codes are sent unchanged.</param>
        /// <param name="forceUnit">Defined ForceInputUnit value 0 through 7. Validated before filesystem or native access.</param>
        /// <remarks><para>Normalizes the path, validates units and the .std filename, and checks the existing parent and absent target.
        /// Rejects Windows device names. Reads GetSTAADFile once and requires empty/whitespace filename text, then rechecks
        /// target absence before one NewSTAADFile call with the absolute path and Int32 length/force codes. Access and path
        /// errors propagate; the library does not hide them behind File.Exists. A blank filename is not proof that the
        /// application has no unsaved work or is ready for creation.</para>
        /// <para>Prechecks are not atomic with the native creation request. Another process can create the target or switch the
        /// session after validation; native overwrite and loaded-model behavior are unspecified. Coordinate exclusive session
        /// and destination use. This method is not an atomic no-overwrite guarantee. It creates no placeholder file, parent
        /// directory, process or managed STD contents, and does not close or save another model.</para>
        /// <para>The native function returns void. Normal return means dispatch completed, not file persistence, requested units,
        /// active-model identity or readiness. No polling, read-back, cache refresh, save, retry or rollback is added.
        /// Refresh wrapper metadata explicitly when needed. Call on the session's COM thread; native UI may block the request.</para></remarks>
        /// <exception cref="ArgumentNullException">filePath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">The path is blank, has invalid syntax, lacks a .std extension or names a Windows device.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A unit code is unsupported; rejected before filesystem or native access.</exception>
        /// <exception cref="InvalidOperationException">The bound session reports a nonblank model path or malformed/unwritten filename output.</exception>
        /// <exception cref="System.IO.IOException">The target exists, its parent is unavailable or a filesystem/path check fails.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller cannot inspect the destination or its parent.</exception>
        /// <exception cref="NotSupportedException">The runtime rejects the path format.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The filename query or native creation request fails; partial native effects are possible.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible filename or three-input NewSTAADFile member.</exception>
        /// <example><code>
        /// bool createNewModel = false; // enable only in an intended unnamed session with an exclusively controlled destination
        /// if (createNewModel)
        ///     root.NewSTAADFile(@"C:\Models\NewFrame.std", LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
        /// </code></example>
        void NewSTAADFile(string filePath, LengthInputUnit lengthUnit, ForceInputUnit forceUnit);

        /// <summary>Requests creating a named view using an explicitly supplied native creation flag.</summary>
        /// <param name="viewName">Non-null, non-whitespace view name, sent literally without trimming; Unicode and surrounding whitespace are retained.</param>
        /// <param name="creationFlag">Native Int32 creation flag whose meaning the caller has established for the connected build. All Int32 values are forwarded; the reviewed help does not define a valid domain, enum or default.</param>
        /// <remarks><para>Validates the name, then calls native CreateNamedView once with name and Int32 flag.
        /// Installed 2025/2026 managed interop declares two inputs and void return, unlike the help's additional nError
        /// parameter. This API follows that inspected signature and does not add an error buffer, guessed success result,
        /// default flag or retry with an alternate signature. Confirm the flag and signature on the actual target build.</para>
        /// <para>Normal return establishes dispatch completion only. Duplicate-name, overwrite, view contents, empty-model,
        /// persistence and flag semantics remain native decisions. No geometry is explicitly edited and no separate SaveModel,
        /// selection change, Create/Save substitution, refresh or rollback is added. Use the acquired session's COM thread;
        /// native calls may block, display UI or fail after effects. A raw flag is exposed because authoritative meanings
        /// are unavailable; no enum values or supported range are invented.</para></remarks>
        /// <exception cref="ArgumentNullException">viewName is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">viewName is empty or whitespace-only; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native creation fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible two-input CreateNamedView member.</exception>
        /// <example><code>
        /// bool createView = false; // enable only after establishing the creation flag for this build
        /// int confirmedCreationFlag = 0; // replace with the verified flag; zero is not asserted to be a valid/default mode
        /// if (createView) root.CreateNamedView("Frame view", confirmedCreationFlag);
        /// </code></example>
        void CreateNamedView(string viewName, int creationFlag);

        /// <summary>Requests saving the current view under the supplied name through the native SaveNamedView entry point.</summary>
        /// <param name="viewName">Non-null, non-whitespace name, preserved exactly including Unicode and surrounding whitespace.</param>
        /// <remarks><para>Validates the name and calls native SaveNamedView once with that string. Installed 2025/2026
        /// managed interop declares a single input and void return; the help's additional nError parameter is inconsistent.
        /// This API follows the inspected one-input signature, without an error buffer, inferred success flag,
        /// alternate-signature retry or substitution through CreateNamedView. Verify actual native behavior per target build.</para>
        /// <para>Normal return does not certify that a view was saved, persisted to disk or had particular contents.
        /// Existing-name/overwrite, no-model and view-state behavior remain native decisions. This method is not SaveModel
        /// and adds no model-file save, selection changes, refresh, read-back or rollback. Use the bound COM thread
        /// and coordinate shared view/model operations; native calls may block, show UI or fail after effects.</para></remarks>
        /// <exception cref="ArgumentNullException">viewName is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">viewName is empty or whitespace-only; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native view saving fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible one-input SaveNamedView member.</exception>
        /// <example><code>
        /// bool saveCurrentView = false; // enable only when this native view change is intended
        /// if (saveCurrentView) root.SaveNamedView("Frame view");
        /// </code></example>
        void SaveNamedView(string viewName);

        /// <summary>Explicitly requests termination of the STAAD application represented by this acquired root.</summary>
        /// <remarks><para>Calls native Quit once with no arguments. This targets the captured application instance and
        /// can affect unsaved work, every model/interface hosted by that instance and other clients sharing it.
        /// Save or discard work explicitly under the caller's policy before invoking this method. No silent-mode change,
        /// pre-save, model close, process kill, global lookup or other application-instance shutdown is added.</para>
        /// <para>The native return is void: normal return does not verify process exit. Native prompts or running analysis
        /// can delay or prevent termination. An exception can follow effects; no retry or rollback is attempted.
        /// Captured native interfaces may become unusable, but the managed wrapper is not automatically disposed or retargeted.
        /// OpenStaadWrapper.Dispose does not call this operation. Use the bound COM thread and coordinate all clients.</para></remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native shutdown request fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible no-argument Quit member.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The captured COM wrapper no longer represents a usable native object.</exception>
        /// <example><code>
        /// bool quitOwnedApplication = false; // enable only for an application whose shutdown the caller intends
        /// if (quitOwnedApplication) root.Quit();
        /// </code></example>
        void Quit();

        /// <summary>Gets a capture-only gateway for acquiring optional Root operation interfaces.</summary>
        /// <value>A gateway bound to this root and native metadata accessor; property access does not inspect or invoke native features.</value>
        /// <remarks>Obtain export/error/warning interfaces independently through this gateway. Exact candidate-build
        /// profiles and compatible native signatures are required. Unknown versions, uncovered builds and missing
        /// metadata grant no interface. Wrapper bindings reuse StaadVersion; standalone bindings read their captured
        /// root. The gateway does not acquire another process or certify native runtime compatibility.</remarks>
        /// <example><code>
        /// var exportGrant = root.Capabilities.GetAdinaExport();
        /// Console.WriteLine(exportGrant.Availability);
        /// </code></example>
        IOSRootCapabilities Capabilities { get; }

        /// <summary>Requests removal of a named view through the inspected native entry point.</summary>
        /// <param name="viewName">Non-null, non-whitespace view name, preserved exactly without trimming.</param>
        /// <remarks><para>Validates the name, then calls RemoveNamedView once with the name and an Int32 zero in the
        /// required nError position. Installed 2025/2026 interop passes that parameter by value, unlike the help's reference
        /// output. Zero initializes this native slot; it is not interpreted as success. No output error code is observable
        /// through the inspected signature, so this managed method returns void rather than fabricating a result.</para>
        /// <para>Normal return means dispatch completed, not verified removal or disk persistence. Missing-name, current-view,
        /// prompts and native error behavior remain server decisions. No geometry deletion, SaveModel, read-back, alternate
        /// signature, retry or rollback is added. Use the bound COM thread and coordinate changes; effects may precede exceptions.
        /// The actual 2024 signature and native view behavior still require controlled verification.</para></remarks>
        /// <exception cref="ArgumentNullException">viewName is null; rejected before dispatch.</exception>
        /// <exception cref="ArgumentException">viewName is empty or whitespace-only; rejected before dispatch.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request fails; the original failure propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible two-input RemoveNamedView member.</exception>
        /// <example><code>
        /// bool removeView = false; // enable only for an intended native view removal
        /// if (removeView) root.RemoveNamedView("Frame view");
        /// </code></example>
        void RemoveNamedView(string viewName);

        /// <summary>Requests modifying named-view membership using an owned snapshot of entity identifiers.</summary>
        /// <param name="viewName">Non-null, non-whitespace view name; text is preserved exactly.</param>
        /// <param name="entityIds">Non-null sequence of positive Int32 entity identifiers, enumerated once into a private zero-based Int32 array before native mutation. Order and duplicates are preserved; an empty array is forwarded rather than silently treated as a no-op.</param>
        /// <param name="entityType">Documented qualifier: Node=0, Beam=1, Plate=2, Solid=3 or Surface=4. Undefined values are rejected before enumeration/native access.</param>
        /// <param name="modificationFlag">Required native Int32 mode established by the caller for this build. The reviewed help supplies no meanings or valid range; every Int32 is sent unchanged, without a guessed enum or default.</param>
        /// <remarks><para>Validates name, sequence, qualifier and every identifier before one ModifyNamedView call.
        /// The inspected 2025/2026 signature has four inputs: name, Int32 array, qualifier and flag. It has no separate count
        /// or reference error output, unlike the help. The array carries its own length; no inconsistent caller count is accepted.
        /// Native return is void and no error/success result is fabricated. Input enumerator failures propagate before dispatch.</para>
        /// <para>The caller must establish mode and empty-array behavior on the actual build. Native entity existence,
        /// duplicate handling, membership rules and persistence are not inferred. No geometry mutation is explicitly requested,
        /// selection changes, SaveModel, read-back, rollback, signature fallback or retries are added. Native view effects may
        /// precede exceptions. Use the session's COM thread and coordinate changes; calls may block or display UI.
        /// Array marshaling, flag semantics and the 2024 signature still require controlled native validation.</para></remarks>
        /// <exception cref="ArgumentNullException">viewName or entityIds is null.</exception>
        /// <exception cref="ArgumentException">The name is blank or a supplied identifier is nonpositive; rejected before mutation.</exception>
        /// <exception cref="ArgumentOutOfRangeException">entityType is undefined; rejected before enumeration or native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible four-input ModifyNamedView member.</exception>
        /// <example><code>
        /// bool modifyView = false; // enable only after establishing mode and array semantics for the actual build
        /// int confirmedMode = 0; // replace with a verified value; zero is not asserted to be a supported default
        /// if (modifyView) root.ModifyNamedView("Frame view", new[] { 1, 2 },
        ///     StaadPro.Interop.Enums.Root.NamedViewEntityType.Beam, confirmedMode);
        /// </code></example>
        void ModifyNamedView(string viewName, IEnumerable<int> entityIds, NamedViewEntityType entityType, int modificationFlag);

        /// <summary>Retrieves local forces at both ends of each requested member/load-case pair.</summary>
        /// <param name="beamIds">Non-null positive member identifiers, enumerated once and deduplicated in first-seen order before native access.</param>
        /// <param name="loadCaseIds">Non-null positive load-case identifiers, enumerated once and deduplicated in first-seen order before native access.</param>
        /// <returns>A named batch result with native availability, per-end success/data and a read-only collection of qualified force records. Native false never exposes partial buffer data.</returns>
        /// <remarks><para>Completes the main project's Root helper without expanding the public Output placeholder.
        /// Validates and snapshots both sequences, limits the Cartesian batch to 100,000 end queries, then reads
        /// the bound root's raw Output.AreResultsAvailable once. An empty input makes no native calls: availability
        /// is null and the empty request succeeds. Native false availability returns an unsuccessful empty batch.</para>
        /// <para>For available results, reads each distinct case title once through the same root's raw Load.GetLoadCaseTitle,
        /// then requests start/end forces with native Int32 member/end/case values and local-coordinate flag zero.
        /// Internal ref object buffers carry six Double values in Fx,Fy,Fz,Mx,My,Mz order. Native true requires every
        /// value written and finite; native false is retained per query with null force data and does not stop other queries.
        /// Unknown/malformed flags and malformed successful buffers remain exceptions, not ordinary failure results.</para>
        /// <para>Ported MemberForces records remain editable managed models backed by the existing Forces class.
        /// Collections preserve distinct member/end/load identities even when components are identical; they are not
        /// collapsed through component-based HashSet equality. No unit conversion, global transform or member-offset
        /// transfer is applied. Values use native result-unit conventions; cached input-unit metadata does not certify them.</para>
        /// <para>No analysis, case activation, selection edit, save, UI warning, provider lookup, background task or retry
        /// is added. Reads are sequential snapshots rather than a transaction or result-freshness guarantee. Use the
        /// session's COM thread and coordinate edits/analysis. Exceptions return no partial batch; expected native false
        /// flags do return qualified per-query outcomes. Full runtime validation remains required.</para></remarks>
        /// <exception cref="ArgumentNullException">Either sequence is null.</exception>
        /// <exception cref="ArgumentException">An identifier is nonpositive or the deduplicated request exceeds 100,000 end queries; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">A required raw object is absent, a flag/title is malformed or a successful force buffer is unwritten, nonfinite or incorrectly shaped.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">A native read fails; its original exception propagates without retry.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The bound root/Output/Load lacks a compatible member.</exception>
        /// <example><code>
        /// bool retrieveForces = false; // enable for an intended query of existing native results
        /// if (retrieveForces)
        /// {
        ///     var result = root.RetrieveMemberForces(new[] { 1, 2 }, new[] { 1 });
        ///     foreach (var query in result.Queries)
        ///         if (query.IsSuccessful) Console.WriteLine(query.Forces.ToString());
        /// }
        /// </code></example>
        MemberForcesResult RetrieveMemberForces(IEnumerable<int> beamIds, IEnumerable<int> loadCaseIds);

        /// <summary>Reads the current model's native STAAD file path from this root session.</summary>
        /// <returns>The exact string reported by OpenSTAAD, including original case, whitespace and Unicode; an empty string when the native API reports no open filename.</returns>
        /// <remarks>
        /// <para>Calls the void GetSTAADFile API once with a VARIANT output and Boolean true
        /// to request the full path. No numeric success status is expected. The BSTR output buffer starts with a string sentinel to detect unwritten results. The output must be
        /// explicitly written as a string; null, non-string and unwritten output throw.</para>
        /// <para>Performs no filesystem lookup, path normalization, model opening/saving, analysis or
        /// STAAD launch. A relative native path is returned unchanged. Use the captured root on the
        /// appropriate COM thread; this object does not certify model readiness or session thread safety.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetStaadFileFullPath());
        /// </code>
        /// </example>
        string GetStaadFileFullPath();

        /// <summary>Reads the current STAAD model's filename, including its existing extension.</summary>
        /// <returns>The filename extracted from one native path read, or an empty string for an empty native path.</returns>
        /// <remarks>
        /// <para>Calls GetStaadFileFullPath once, then applies System.IO.Path.GetFileName using
        /// Windows path semantics. The name retains case, Unicode, spaces and multiple periods.
        /// A relative filename is accepted. Native data is not normalized or checked for file existence.</para>
        /// <para>No native write, model activation, file opening or analysis is performed.
        /// Invalid path syntax is delegated to the .NET Framework Path API.
        /// Use the captured root on the correct session COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">The native string contains invalid path characters for the Windows Path API.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetStaadFileNameOnly());
        /// </code>
        /// </example>
        string GetStaadFileNameOnly();

        /// <summary>Derives the expected .ANL analysis-file path from the current model's path.</summary>
        /// <returns>The model directory and filename stem with uppercase .ANL, or an empty string for an empty native model path.</returns>
        /// <remarks>
        /// <para>Reads GetStaadFileFullPath exactly once, removes only the final filename extension,
        /// then combines that stem plus .ANL with the same directory using the Windows Path API.
        /// Relative filenames yield relative analysis-file names. A model-path read does not prove
        /// that the returned analysis file exists or that analysis results are current.</para>
        /// <para>This improves on the reference's two path reads, which could mix model names if
        /// the session changed. No file is created, opened or checked, and no analysis is started.
        /// Native path changes after the read remain possible. Coordinate root access on the session's COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">The native string contains invalid path characters for the Windows Path API.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves the output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root object lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetAnlFileFullPath());
        /// </code>
        /// </example>
        string GetAnlFileFullPath();

        /// <summary>Reads and normalizes the filename of the model currently associated with this root session.</summary>
        /// <returns>The absolute Windows path resolved from one native full-path read, or an empty string when OpenSTAAD explicitly reports no filename.</returns>
        /// <remarks>
        /// <para>Calls GetStaadFileFullPath once, then Path.GetFullPath for nonempty text.
        /// Relative native paths are resolved against the process working directory.
        /// Unlike GetStaadFileFullPath, this method normalizes separators and relative segments.
        /// It preserves path identity without checking file existence, resolving filesystem links,
        /// querying model readiness, or opening/saving a model.</para>
        /// <para>A blank/unnamed native session yields an empty string rather than the reference's
        /// exception from normalizing empty text. Native/path errors still throw and are not treated
        /// as a blank session. COM calls run on the calling thread; coordinate shared-session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.GetOpenedStaadFilename());
        /// </code>
        /// </example>
        string GetOpenedStaadFilename();

        /// <summary>Checks whether this root session currently identifies the requested model path.</summary>
        /// <param name="staadFileFullPath">Non-null, non-whitespace Windows model path. Relative input is accepted and resolved against the process working directory before native access.</param>
        /// <returns>True when normalized requested and native paths match with ordinal case-insensitive comparison; false for a different path or an explicitly unnamed native session.</returns>
        /// <remarks>
        /// <para>Validates and normalizes the requested path first, then reads GetOpenedStaadFilename
        /// once. Compares complete paths, not just basenames or substring matches. Case and relative
        /// dot segments are normalized by the Windows Path API; filesystem links are not resolved.</para>
        /// <para>This read-only identity predicate does not check Geometry/Load readiness or file
        /// existence, open or switch models, save files, or change active cases/load lists.
        /// A true result describes the session at the read; later native model changes are possible.
        /// Native failures propagate. Call on the appropriate COM thread and serialize session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">staadFileFullPath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// bool matches = root.IsTarget(@"C:\Models\Frame.v2.STD");
        /// Console.WriteLine(matches);
        /// </code>
        /// </example>
        bool IsTarget(string staadFileFullPath);

        /// <summary>Checks whether the specified model path is the model identified by this root session.</summary>
        /// <param name="fileFullPath">Non-null, non-whitespace Windows model path; relative input is resolved before native access.</param>
        /// <returns>True for a normalized, ordinal case-insensitive full-path match; false for an explicitly unnamed session or a different model, even when basenames are equal.</returns>
        /// <remarks>
        /// <para>Uses the same complete-path identity check as IsTarget and performs exactly one
        /// native filename read after validating input. The reference incorrectly passes the path
        /// as GetSTAADFile's Boolean full-path flag and compares basenames; this port requests
        /// Boolean true and compares normalized complete paths, preventing cross-directory matches.</para>
        /// <para>This describes model identity rather than Geometry/Load readiness or analysis state.
        /// It does not open, close, launch or save STAAD, test file existence, or alter native state.
        /// Relative paths use the process working directory; filesystem links are not resolved.
        /// Native/path failures throw rather than returning false. Coordinate session COM access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">fileFullPath is null; rejected before native access.</exception>
        /// <exception cref="ArgumentException">A native or requested path contains invalid Windows path syntax; requested paths must contain non-whitespace text.</exception>
        /// <exception cref="NotSupportedException">A path uses a format unsupported by the Windows Path API.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds the limits enforced by the runtime.</exception>
        /// <exception cref="InvalidOperationException">GetSTAADFile leaves its string output unwritten or returns a non-string value.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native path read fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible GetSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// Console.WriteLine(root.ModelIsOpen(@"C:\Models\Frame.v2.STD"));
        /// </code>
        /// </example>
        bool ModelIsOpen(string fileFullPath);

        /// <summary>Reads the current model's job name, client and originating engineer.</summary>
        /// <returns>A fresh ShortJobInfo snapshot whose three properties contain exact native strings, including empty text, whitespace and Unicode.</returns>
        /// <remarks>
        /// <para>Calls the void GetShortJobInfo API once with three BSTR-initialized reference outputs
        /// in name/client/originator order. Every output must be explicitly written as a string.
        /// Unwritten or non-string responses throw; a failed read never returns a partial record.</para>
        /// <para>Edits to the returned property-notifying model affect managed data only.
        /// No model opening, saving, unit changes or analysis occurs. This is job metadata rather
        /// than readiness or result data. Coordinate the calling thread's COM session access.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">A native output is unwritten or is not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native call fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks the required native signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// var job = root.GetShortJobInfo();
        /// Console.WriteLine(job.Name + ": " + job.Client + " / " + job.Originator);
        /// </code>
        /// </example>
        ShortJobInfo GetShortJobInfo();

        /// <summary>Reads all thirteen job-information fields and converts the three date fields.</summary>
        /// <returns>A fresh FullJobInfo snapshot. Unspecified native dates map to DateTime.MinValue; text fields preserve their native contents.</returns>
        /// <remarks>
        /// <para>Calls void GetFullJobInfo once with BSTR-initialized reference outputs in native order:
        /// name, client, engineer, engineer date, number, revision, part, reference, checker,
        /// checked date, approver, approved date and comments. All thirteen must be written strings
        /// before mapping into FullJobInfo; invalid output or parsing failure returns no partial model.</para>
        /// <para>Empty/whitespace dates mean unspecified. Nonempty dates follow DateTime.Parse with
        /// the calling thread's CurrentCulture and DateTimeStyles.None, preserving reference culture
        /// behavior; ambiguous dates are not guessed using alternative cultures. Original date text
        /// is not retained by the DateTime properties.</para>
        /// <para>Results own their managed property storage, raise change notifications and perform no
        /// native writes when edited. No file lookup, saving, activation, unit conversion or analysis
        /// occurs. Use the correct COM thread; a metadata snapshot does not certify model readiness.</para>
        /// </remarks>
        /// <exception cref="FormatException">A nonempty native date cannot be parsed using the current culture.</exception>
        /// <exception cref="InvalidOperationException">A native output is unwritten or is not a string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native call fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks the required native signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// var job = root.GetFullJobInfo();
        /// Console.WriteLine(job.Number + ": " + job.Checker + " / " + job.Comments);
        /// </code>
        /// </example>
        FullJobInfo GetFullJobInfo();

        /// <summary>Sets the current model's input length and force units using existing enum codes.</summary>
        /// <param name="lengthUnit">Defined LengthInputUnit value: Inch=0, Feet=2, CentiMeter=3, Meter=4, MilliMeter=5, DeciMeter=6 or KiloMeter=7.</param>
        /// <param name="forceUnit">Defined ForceInputUnit value 0 through 7: KiloPound, Pound, KiloGram, MetricTon, Newton, KiloNewton, MegaNewton or DecaNewton.</param>
        /// <returns>This root adapter after the native void call returns without an exception.</returns>
        /// <remarks>
        /// <para>Validates both enums before one SetInputUnits call, sending integer length then
        /// force codes unchanged. The native API is void; no Boolean/numeric status is interpreted
        /// and no read-back is performed. Undefined enum values, including the unnamed length code 1,
        /// are rejected even though the installed vendor documentation lists 1 as another Feet code.</para>
        /// <para>This changes native input-unit state used by subsequent input operations. It does
        /// not rescale existing managed values, convert already-stored results, change the base
        /// unit system, save the model or restore previous units. A native failure may have partial
        /// effects; no rollback is supplied. Coordinate session access on the appropriate COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Either unit value is not defined by its public enum; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native unit change fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The installed root lacks a compatible SetInputUnits signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// root.SetInputUnitsEx(LengthInputUnit.Meter, ForceInputUnit.KiloNewton);
        /// </code>
        /// </example>
        IOSRoot SetInputUnitsEx(LengthInputUnit lengthUnit, ForceInputUnit forceUnit);

        /// <summary>Sets the STAAD application's silent-mode flag and reports whether the native call completed without throwing.</summary>
        /// <param name="silentMode">Defined SilentMode value: Disable=0 or Enable=1. Invalid values are rejected before native access.</param>
        /// <returns>True if the native call returns without an exception; false if native dispatch throws. This is not the previous flag, proof of the resulting setting, or a general readiness check.</returns>
        /// <remarks>
        /// <para>Validates the enum, then calls SetSilentMode once with its integer code. OpenSTAAD's
        /// native VARIANT return is the previous silent-mode value; this method deliberately ignores
        /// that value and preserves the main project's Boolean call-completion contract. A native
        /// previous value of zero is not a failure. No read-back or diagnostic exception is returned.</para>
        /// <para>This changes application-level warning suppression, which can affect later analysis
        /// and saves. Licence, inaccessible-folder and other error dialogs may still appear. The flag
        /// is not automatically restored. No model is saved or closed and no analysis is started.
        /// Dispatch exceptions, including missing signatures, are caught and return false; a failure
        /// can still have native side effects. Serialize access on the session's appropriate COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">silentMode is not a defined enum value; no native call is made.</exception>
        /// <example>
        /// With a connected IOSRoot named root:
        /// <code>
        /// bool completed = root.SetSilentMode(SilentMode.Enable);
        /// Console.WriteLine(completed);
        /// </code>
        /// </example>
        bool SetSilentMode(SilentMode silentMode);

        /// <summary>Requests a native save of the current STAAD model using the specified warning-suppression option.</summary>
        /// <param name="silentMode">Defined SilentMode value: Disable=0 or Enable=1, passed as the native integer save option.</param>
        /// <returns>This root adapter after the native void call returns without throwing. This does not independently verify file persistence or that a user completed a native prompt.</returns>
        /// <remarks>
        /// <para>Validates the enum before one SaveModel call. The native API is void, so no Boolean
        /// or numeric status is interpreted. It saves the current structure through STAAD rather
        /// than writing managed file contents. No separate SetSilentMode call, path query or
        /// automatic analysis is performed; this method does not restore application settings.</para>
        /// <para>The native call can write the model file or display dialogs, particularly for an
        /// unnamed model or an error. Silent mode is not a guarantee of unattended operation.
        /// Exceptions propagate unchanged; a failed call may have partial native effects and no
        /// rollback is provided. Call on the correct COM thread and coordinate shared-session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">silentMode is not a defined enum value; rejected before native access.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native save fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible SaveModel signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root; this writes through STAAD:
        /// <code>
        /// root.SaveModel(SilentMode.Enable);
        /// </code>
        /// </example>
        IOSRoot SaveModel(SilentMode silentMode);

        /// <summary>Requests that STAAD close the model currently associated with this root session.</summary>
        /// <returns>This root adapter after the native void call returns without throwing. The return does not certify that the model is closed or that the root remains ready for further operations.</returns>
        /// <remarks>
        /// <para>Calls CloseSTAADFile once without arguments. No Boolean/numeric status is interpreted,
        /// and no filename/readiness check is performed. The request concerns the current .STD file;
        /// it does not terminate the STAAD application, dispose this adapter or create another model.</para>
        /// <para>This method does not explicitly save unsaved changes or set/restore silent mode.
        /// Native dialogs or user decisions may affect closing and may block the call. Save first
        /// when persistence is required. Previously captured model interfaces can become unusable
        /// after closing; the unchanged adapter reference does not prove their validity. Exceptions
        /// propagate unchanged and no rollback occurs. Serialize calls on the session's COM thread.</para>
        /// </remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native close fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible CloseSTAADFile signature.</exception>
        /// <example>
        /// With a connected IOSRoot named root and any required saving already completed:
        /// <code>
        /// root.CloseSTAADFile();
        /// </code>
        /// </example>
        IOSRoot CloseSTAADFile();

        /// <summary>Requests that the captured STAAD application open the specified model file.</summary>
        /// <param name="filePath">Non-null, non-whitespace Windows model path, resolved to an absolute path before dispatch. Relative paths use the process working directory.</param>
        /// <returns>This adapter after the native void OpenSTAADFile call returns without throwing; the return does not verify model identity or readiness.</returns>
        /// <remarks>
        /// <para>Validates and normalizes the path, then sends that full path as a string to exactly
        /// one native call. Does not check file existence, create files, start another STAAD process,
        /// query Geometry/Load, or wait for the requested model to become available.</para>
        /// <para>The call can switch the current model and display native dialogs concerning unsaved
        /// work or inaccessible files. No automatic save or rollback is supplied. Captured interfaces
        /// can change validity after a model switch. Serialize access on the session's COM thread;
        /// native calls can block on dialogs. Use a separate identity/readiness check when required.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">filePath is null; no native call occurs.</exception>
        /// <exception cref="ArgumentException">filePath is empty, whitespace or invalid Windows path syntax.</exception>
        /// <exception cref="NotSupportedException">The path uses an unsupported Windows format.</exception>
        /// <exception cref="System.IO.PathTooLongException">The runtime rejects the path length.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native open fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible OpenSTAADFile signature.</exception>
        /// <example><code>
        /// root.OpenSTAADFile(@"C:\Models\Frame.v2.STD"); // native model-switch request
        /// </code></example>
        IOSRoot OpenSTAADFile(string filePath);

        /// <summary>Requests that the captured STAAD application become visible.</summary>
        /// <returns>This root adapter after the native ShowApplication call returns without throwing; visibility and foreground focus are not independently verified.</returns>
        /// <remarks>
        /// <para>Calls ShowApplication once with no arguments and ignores any native return.
        /// Does not launch another application, open/save/close a model, start analysis, or
        /// explicitly maximize, restore or activate a window through Windows APIs.</para>
        /// <para>Call on the session's appropriate COM thread and coordinate shared access.
        /// Native failures propagate unchanged; no retry, timeout or rollback is supplied.
        /// A successful dispatch does not certify that a model is ready for subsequent operations.</para>
        /// </remarks>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native visibility request fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible ShowApplication signature.</exception>
        /// <example><code>
        /// root.ShowApplication();
        /// </code></example>
        IOSRoot ShowApplication();

        /// <summary>Polls this root until its reported full model path matches the requested path or the elapsed-time limit expires.</summary>
        /// <param name="fileFullPath">Non-null, non-whitespace Windows model path, normalized before polling; relative input uses the process working directory.</param>
        /// <param name="timeoutDurationInSeconds">Finite positive timeout in seconds within TimeSpan limits; default 60. Counts native polling time.</param>
        /// <param name="sleepIntervalInSeconds">Finite positive polling interval in seconds within TimeSpan limits; default 0.1. Sleeps are capped by the remaining budget.</param>
        /// <returns>True when a matching path is observed within the logical budget; false on timeout. This does not verify Geometry/Load readiness or analysis results.</returns>
        /// <remarks>
        /// <para>Checks immediately, then reads one native filename per poll with ordinal
        /// case-insensitive full-path comparison. An explicitly blank filename is a nonmatch.
        /// No open/save/close or application launch is performed. Native/read/path errors throw
        /// instead of being treated as a blank session or timeout.</para>
        /// <para>Uses a monotonic elapsed-time budget and synchronous waits on the caller's COM
        /// thread. It does not pump UI messages, offer cancellation or prevent native model changes
        /// after a match. A synchronous native call can exceed the timeout; the budget cannot abort it.
        /// Thread.Sleep resolution can exceed the last remaining interval. Serialize session access.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">fileFullPath is null.</exception>
        /// <exception cref="ArgumentException">The requested path is empty/whitespace, or requested/native text has invalid Windows syntax.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A duration is nonpositive, NaN, infinite or outside supported limits; rejected before native access.</exception>
        /// <exception cref="NotSupportedException">A path format is unsupported.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds runtime limits.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native polling fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible filename API.</exception>
        /// <example><code>
        /// bool identified = root.AwaitModelHandle(@"C:\Models\Frame.v2.STD", 1, 0.01);
        /// Console.WriteLine(identified);
        /// </code></example>
        bool AwaitModelHandle(string fileFullPath, double timeoutDurationInSeconds = 60, double sleepIntervalInSeconds = 0.1);

        /// <summary>Requests opening a model once, then waits for this root to report that exact normalized model path.</summary>
        /// <param name="staadFileFullPath">Non-null, non-whitespace Windows model path, normalized before the native request.</param>
        /// <param name="sleepInterval">Positive polling interval in milliseconds, capped by the remaining budget.</param>
        /// <param name="maxWaitTime">Positive total logical limit in seconds, including the native open call; default 600.</param>
        /// <returns>True when the exact normalized path is observed within the budget; false on timeout. A false result does not undo the open request or prove that the model stayed closed.</returns>
        /// <remarks>
        /// <para>Validates all input before one OpenSTAADFile request, then checks immediately and
        /// polls native full paths with ordinal case-insensitive comparison. Corrects the reference's
        /// basename substring comparison and iteration-count timeout. A similarly named model in
        /// another folder is not a match. It requests opening even if the target was already open.</para>
        /// <para>Model switching can affect unsaved work and captured interfaces; native dialogs may
        /// block. No auto-save, rollback, new process creation or Geometry/Load readiness check occurs.
        /// Uses synchronous caller-thread sleeps without UI pumping or cancellation. A monotonic
        /// budget cannot interrupt a blocked COM call. Native and path failures propagate unchanged.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">staadFileFullPath is null.</exception>
        /// <exception cref="ArgumentException">The requested path is empty/whitespace, or requested/native text has invalid Windows syntax.</exception>
        /// <exception cref="ArgumentOutOfRangeException">sleepInterval or maxWaitTime is nonpositive; no native call occurs.</exception>
        /// <exception cref="NotSupportedException">A path format is unsupported.</exception>
        /// <exception cref="System.IO.PathTooLongException">A path exceeds runtime limits.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native open or polling fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible open or filename API.</exception>
        /// <example><code>
        /// bool identified = root.OpenStaadModel(@"C:\Models\Frame.v2.STD", 10, 1);
        /// Console.WriteLine(identified);
        /// </code></example>
        bool OpenStaadModel(string staadFileFullPath, int sleepInterval, int maxWaitTime = 600);

        /// <summary>Requests a silent close only when this root reports a non-whitespace current model filename.</summary>
        /// <remarks>
        /// <para>Reads one full native filename. Explicit empty/whitespace text causes no mutation.
        /// Otherwise calls native SetSilentMode with integer 1, then CloseSTAADFile once. Corrects
        /// the reference's null full-path flag. The previous silent setting is ignored and not restored.</para>
        /// <para>Does not explicitly save unsaved work, verify closing, terminate STAAD or dispose
        /// the wrapper. Native dialogs can block despite silent mode, and captured model interfaces
        /// can become unusable after closing. A filename can change after the read; callers must
        /// coordinate UI/session access on the correct COM thread. Native failures propagate,
        /// and a failure after setting silent mode can leave that setting changed.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Filename, silent-mode or close dispatch fails; the original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible filename, silent-mode or close API.</exception>
        /// <example><code>
        /// root.CloseCurrentStaadModel(); // explicitly requests closing; does not auto-save
        /// </code></example>
        void CloseCurrentStaadModel();

        /// <summary>Closes the matching current model, replaces its existing file contents and requests reopening after bounded close/lock-release waiting.</summary>
        /// <param name="filePath">Non-null, non-whitespace Windows path to the existing current model; normalized before dispatch.</param>
        /// <param name="fileContents">Non-null list snapshotted before mutation. Lines are written as UTF-8 without BOM using platform newlines; null entries become empty lines.</param>
        /// <param name="sleepTimeMilliseconds">Positive retry interval in milliseconds; capped by the remaining close/lock-release budget. Default 200.</param>
        /// <param name="maxTimeMilliseconds">Nonnegative close/lock-release wait limit in milliseconds after the close request. Default 5000; zero permits only an immediate check.</param>
        /// <returns>True when replacement completed and the target was identified or its reopen request returned; false for a nonmatching model, wait expiry or an intervening model switch. False can follow native side effects or a completed file replacement.</returns>
        /// <remarks>
        /// <para>Validates input, snapshots lines and requires the current normalized path to match
        /// the target before closing. Rejects missing, directory or read-only targets before mutation.
        /// Sets native silent mode to 1 without restoring it, requests closing, then waits for an
        /// explicitly empty filename and exclusive read/write availability. Only Windows sharing/
        /// lock violations are retried; other filesystem errors propagate.</para>
        /// <para>Publishes a complete exclusively created sibling temporary file using File.Replace,
        /// without truncation fallback. Only owned temporary cleanup is attempted. Rechecks blank
        /// identity before writing and avoids reopening over a different intervening model. The
        /// exclusive probe is a snapshot, not a transaction with external writers or the STAAD UI.
        /// Native and filesystem races remain possible; replacement does not preserve original encoding.</para>
        /// <para>The budget does not bound close/write/open native calls or force-abort COM. No
        /// explicit save, backup, rollback, directory creation, process launch or reopen-readiness
        /// verification occurs. Errors propagate; a failure can leave silent mode enabled, the model
        /// closed or the replacement published. Serialize access on the correct session COM thread.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">filePath or fileContents is null.</exception>
        /// <exception cref="ArgumentException">A requested path is empty/whitespace, or requested/native path syntax is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">sleepTimeMilliseconds is nonpositive or maxTimeMilliseconds is negative.</exception>
        /// <exception cref="NotSupportedException">A path or filesystem operation is unsupported.</exception>
        /// <exception cref="System.IO.IOException">The target is missing/a directory or a non-lock filesystem operation fails; original failures propagate.</exception>
        /// <exception cref="UnauthorizedAccessException">The target is read-only or permissions prevent required access.</exception>
        /// <exception cref="InvalidOperationException">Native filename output is unwritten or non-string.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native identity, silent-mode, close or open dispatch fails.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a required native signature.</exception>
        /// <example><code>
        /// var editedLines = new System.Collections.Generic.List&lt;string&gt; { "STAAD SPACE", "UNIT METER KN", "FINISH" };
        /// bool requested = root.CloseWriteReopenStaadFile(@"C:\Models\Frame.v2.STD", editedLines);
        /// Console.WriteLine(requested); // explicit file replacement; no reopen-readiness guarantee
        /// </code></example>
        bool CloseWriteReopenStaadFile(string filePath, List<string> fileContents, int sleepTimeMilliseconds = 200, int maxTimeMilliseconds = 5000);

        /// <summary>Requests minimizing the main window reported by this OpenSTAAD root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads GetMainWindowHandle and GetProcessId once each, accepts supported integral
        /// native values, checks a nonzero pointer-sized handle, existing window and matching process,
        /// then posts Windows ShowWindowAsync command 6. Rejects malformed, unavailable or foreign
        /// windows before posting. No model opening/saving/closing or application launch occurs.</para>
        /// <para>The void return means the state request was accepted, not that the final window
        /// state, visibility or focus was observed. Uses asynchronous posting rather than blocking
        /// ShowWindow. HWND/process identity checks are snapshots: windows can be destroyed/recycled
        /// after validation. Native COM reads can still block. Use the correct COM thread and
        /// coordinate UI/session access; no retries, rollback or completion timeout are provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native handle/process output is malformed, the window is absent/foreign or Windows does not accept the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native handle or process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.MinimizeStaadMainWindow();
        /// </code></example>
        void MinimizeStaadMainWindow();

        /// <summary>Requests maximizing the main window reported by this OpenSTAAD root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads GetMainWindowHandle and GetProcessId once each, accepts supported integral
        /// native values, checks a nonzero pointer-sized handle, existing window and matching process,
        /// then posts Windows ShowWindowAsync command 3. Rejects malformed, unavailable or foreign
        /// windows before posting. No model opening/saving/closing or application launch occurs.</para>
        /// <para>The void return means the state request was accepted, not that the final window
        /// state, visibility or focus was observed. Uses asynchronous posting rather than blocking
        /// ShowWindow. HWND/process identity checks are snapshots: windows can be destroyed/recycled
        /// after validation. Native COM reads can still block. Use the correct COM thread and
        /// coordinate UI/session access; no retries, rollback or completion timeout are provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Native handle/process output is malformed, the window is absent/foreign or Windows does not accept the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native handle or process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.MaximizeStaadMainWindow();
        /// </code></example>
        void MaximizeStaadMainWindow();

        /// <summary>Requests restoring the main window reported by this root after validating its process association.</summary>
        /// <remarks>
        /// <para>Reads native GetMainWindowHandle and GetProcessId once each, validates supported
        /// integral/pointer values and matching window ownership, then posts ShowWindowAsync command
        /// 9. An absent, malformed or foreign window is rejected before posting. No model is changed.</para>
        /// <para>The void return means the restore request was accepted, not that normal window state
        /// or focus was observed. HWND/PID checks are snapshots and handles can be destroyed/recycled
        /// after checking. Native identity reads may block. Coordinate UI/session access on the correct
        /// COM thread; no launch, retry, rollback or completion timeout is provided.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">Handle/process output is malformed, the window is absent/foreign or Windows rejects the request.</exception>
        /// <exception cref="PlatformNotSupportedException">The default window API is invoked outside Windows.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native window/process lookup fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible handle/process API.</exception>
        /// <example><code>
        /// bool changeWindowState = false; // set true when an actual STAAD window should be changed
        /// if (changeWindowState) root.RestoreStaadMainWindow();
        /// </code></example>
        void RestoreStaadMainWindow();

        /// <summary>Requests analysis of the currently opened STAAD model with explicit engine-dialog and waiting options.</summary>
        /// <param name="silentMode">Defined SilentMode code 0/1 controlling supported engine warnings; no separate application SetSilentMode call is made.</param>
        /// <param name="hiddenMode">Defined HiddenMode code 0/1 controlling the analysis-engine dialog.</param>
        /// <param name="waitMode">Defined WaitAnalysisMode: Return=0 requests immediate return; Wait=1 requests native completion waiting.</param>
        /// <returns>The native integer analysis status as AnalysisResult: -1 terminated, 0 general error, 1 in progress, 2 completed successfully, 3 warnings, 4 errors or 5 not performed. Unrecognized Int32 codes are preserved as undefined enum values.</returns>
        /// <remarks>
        /// <para>Validates all flags before one AnalyzeEx call with integer silent/hidden/wait codes
        /// in that order. Accepts only supported CLR integer status values within Int32 range; null,
        /// Boolean, floating-point, decimal and string responses throw rather than being coerced
        /// into success. Defined native failure/in-progress codes are returned, not converted to exceptions.</para>
        /// <para>This starts native analysis and may generate/change analysis output files and display
        /// dialogs. No managed pre-save, target/readiness check, extra SetSilentMode call, result-file
        /// retrieval, retry, cancellation, timeout or rollback is supplied. Return mode does not mean
        /// completion; wait mode can block. Silent/hidden flags do not guarantee unattended execution.
        /// Serialize calls on the correct COM thread and explicitly inspect the returned status.</para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Any flag is undefined; rejected before analysis dispatch.</exception>
        /// <exception cref="InvalidOperationException">Native status is not a supported integer or cannot fit Int32.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native analysis dispatch fails; its original exception propagates.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The root lacks a compatible AnalyzeEx signature.</exception>
        /// <example><code>
        /// AnalysisResult status = root.AnalyzeEx(SilentMode.Enable, HiddenMode.Enable, WaitAnalysisMode.Wait);
        /// Console.WriteLine(status); // inspect errors/warnings; this requests real analysis
        /// </code></example>
        AnalysisResult AnalyzeEx(SilentMode silentMode, HiddenMode hiddenMode, WaitAnalysisMode waitMode);
    }
}
