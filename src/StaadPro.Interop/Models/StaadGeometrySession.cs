using System;
using System.Runtime.InteropServices;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Encapsulates an active connection session to a Bentley STAAD.Pro instance.
    /// </summary>
    public class StaadGeometrySession : IDisposable
    {
        public StaadGeometrySession(object openStaad)
        {
            RawOpenStaad = openStaad ?? throw new ArgumentNullException(nameof(openStaad));
            dynamic dynStaad = openStaad;
            try { RawGeometry = dynStaad.Geometry; } catch { RawGeometry = openStaad; }
            try { RawLoad = dynStaad.Load; } catch { RawLoad = null; }
            Geometry = new OSGeometryAdapter(this);
            Load = new OSLoadAdapter(this);
        }

        public StaadGeometrySession(object geometryComObject, object loadComObject)
        {
            RawGeometry = geometryComObject;
            RawLoad = loadComObject;
            if (geometryComObject != null) Geometry = new OSGeometryAdapter(this);
            if (loadComObject != null) Load = new OSLoadAdapter(this);
        }

        /// <summary>
        /// Gets the raw root OpenSTAAD COM object.
        /// </summary>
        public object RawOpenStaad { get; }

        /// <summary>
        /// Gets the underlying OpenSTAAD Geometry COM object.
        /// </summary>
        public dynamic RawGeometry { get; }

        /// <summary>
        /// Gets the underlying OpenSTAAD Load COM object.
        /// </summary>
        public dynamic RawLoad { get; }

        /// <summary>
        /// Gets the strongly-typed managed geometry adapter interface.
        /// </summary>
        public IOSGeometry Geometry { get; }

        /// <summary>
        /// Gets the strongly-typed managed load adapter interface.
        /// </summary>
        public IOSLoad Load { get; }

        /// <summary>
        /// Connects to the currently running STAAD.Pro application instance.
        /// </summary>
        /// <returns>A new <see cref="StaadGeometrySession"/> bound to the active STAAD instance.</returns>
        public static StaadGeometrySession ConnectActiveInstance()
        {
            try
            {
                object comObject = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                return new StaadGeometrySession(comObject);
            }
            catch (COMException ex)
            {
                throw new InvalidOperationException("No active STAAD.Pro instance was found via COM.", ex);
            }
        }

        public void Dispose()
        {
            // Optional COM release
        }
    }
}
