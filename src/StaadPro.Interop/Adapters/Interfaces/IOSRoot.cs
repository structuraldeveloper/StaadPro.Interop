using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Exposes the model-specific OpenSTAAD root binding, filenames, identity, job metadata, input units and save/close operations.</summary>
    /// <remarks>The root port includes read-only filename/identity/job queries and native input-unit, silent-mode, save and close operations. Other IOSRoot operations are ported separately.
    /// Native access uses runtime dispatch and requires an undisposed connection on the correct COM thread.</remarks>
    public interface IOSRoot
    {
        /// <summary>Gets the root OpenSTAAD automation object associated with this binding.</summary>
        /// <value>The raw root object; its Geometry property identifies the model modified by extended operations.</value>
        dynamic ComObject { get; }

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
    }
}
