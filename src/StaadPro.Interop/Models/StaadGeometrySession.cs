using System;
using System.Runtime.InteropServices;
using OpenSTAADUI;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Encapsulates an active connection session to a Bentley STAAD.Pro instance for geometry operations.
    /// </summary>
    public class StaadGeometrySession : IDisposable
    {
        public StaadGeometrySession(OpenSTAAD openStaad)
        {
            RawOpenStaad = openStaad ?? throw new ArgumentNullException(nameof(openStaad));
            RawGeometry = (OSGeometryUI)openStaad.Geometry;
            Geometry = new OSGeometryAdapter(this);
        }

        public StaadGeometrySession(OSGeometryUI geometryComObject)
        {
            RawGeometry = geometryComObject ?? throw new ArgumentNullException(nameof(geometryComObject));
            Geometry = new OSGeometryAdapter(this);
        }

        /// <summary>
        /// Gets the raw root OpenSTAAD COM object if bound via OpenSTAAD root.
        /// </summary>
        public OpenSTAAD RawOpenStaad { get; }

        /// <summary>
        /// Gets the underlying OpenSTAAD Geometry COM object (OSGeometryUI).
        /// </summary>
        public OSGeometryUI RawGeometry { get; }

        /// <summary>
        /// Gets the strongly-typed managed geometry adapter interface.
        /// </summary>
        public IOSGeometry Geometry { get; }

        /// <summary>
        /// Connects to the currently running STAAD.Pro application instance.
        /// </summary>
        /// <returns>A new <see cref="StaadGeometrySession"/> bound to the active STAAD instance.</returns>
        public static StaadGeometrySession ConnectActiveInstance()
        {
            try
            {
                object comObject = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                return new StaadGeometrySession((OpenSTAAD)comObject);
            }
            catch (COMException ex)
            {
                throw new InvalidOperationException("No active STAAD.Pro instance was found via COM.", ex);
            }
        }

        public void Dispose()
        {
            // Optional COM release if needed
        }
    }
}
