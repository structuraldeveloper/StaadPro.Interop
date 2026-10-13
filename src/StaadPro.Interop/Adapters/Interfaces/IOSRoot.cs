using System;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Exposes the model-specific OpenSTAAD root binding and session filename queries.</summary>
    /// <remarks>The root port includes model/analysis filenames, normalized opened-model paths and full-path identity checks. Other IOSRoot operations are ported separately.
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
    }
}
