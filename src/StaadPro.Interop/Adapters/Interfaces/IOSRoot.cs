using System;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Exposes the model-specific OpenSTAAD root binding and session filename queries.</summary>
    /// <remarks>The root port now includes model path, filename and derived analysis-file path reads. Other IOSRoot operations are ported separately.
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
        /// to request the full path. No numeric success status is expected. The output must be
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
    }
}
