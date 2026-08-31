using System;
using System.Runtime.InteropServices;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Wrapper class used to encapsulate the interfaces required to interact with an active or dedicated STAAD.Pro model instance.
    /// </summary>
    public class OpenStaadWrapper : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenStaadWrapper"/> class wrapping the root OpenSTAAD COM object.
        /// </summary>
        /// <param name="openStaad">The root OpenSTAAD COM object retrieved from STAAD.Pro.</param>
        /// <param name="isDedicated">Indicates whether the STAAD.Pro process is dedicated to this wrapper instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="openStaad"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="openStaad"/> does not expose valid STAAD COM interfaces.</exception>
        public OpenStaadWrapper(object openStaad, bool isDedicated = false)
        {
            RawOpenStaad = openStaad ?? throw new ArgumentNullException(nameof(openStaad), "OpenSTAAD COM instance cannot be null.");
            IsDedicated = isDedicated;

            SetUnmanagedObjects();
            SetManagedObjects();

            if (Geometry == null && Load == null)
            {
                throw new ArgumentException("Provided OpenSTAAD instance is invalid or does not expose required Geometry or Load interfaces.", nameof(openStaad));
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the OpenSTAAD object wrapped in this instance is dedicated for operations and not associated with any other model.
        /// </summary>
        public bool IsDedicated { get; private set; }

        /// <summary>
        /// Raw OpenSTAAD root object retrieved from the STAAD.Pro application.
        /// </summary>
        public object RawOpenStaad { get; private set; }

        /// <summary>
        /// Raw Geometry COM interface retrieved from OpenSTAAD.
        /// </summary>
        public dynamic RawGeometry { get; private set; }

        /// <summary>
        /// Managed interface exposing functions for performing geometry operations on nodes, beams, plates, and surfaces.
        /// </summary>
        public IOSGeometry Geometry { get; private set; }

        /// <summary>
        /// Raw Load COM interface retrieved from OpenSTAAD.
        /// </summary>
        public dynamic RawLoad { get; private set; }

        /// <summary>
        /// Managed interface exposing functions for performing load operations and load case management.
        /// </summary>
        public IOSLoad Load { get; private set; }

        /// <summary>
        /// Gets the active base unit system of the STAAD model.
        /// </summary>
        public BaseUnitSystem BaseUnitSystem => GetBaseUnitSystem();

        /// <summary>
        /// Gets the input force unit of the STAAD model.
        /// </summary>
        public ForceInputUnit InputForceUnit => GetInputForceUnit();

        /// <summary>
        /// Gets the input length unit of the STAAD model.
        /// </summary>
        public LengthInputUnit InputLengthUnit => GetInputLengthUnit();

        /// <summary>
        /// Indicates whether this wrapper is connected to a valid STAAD COM instance.
        /// </summary>
        public bool IsConnected => RawOpenStaad != null && (Geometry != null || Load != null);

        #endregion

        #region Private Setup Methods

        private void SetUnmanagedObjects()
        {
            if (RawOpenStaad == null) return;
            dynamic dynStaad = RawOpenStaad;
            try { RawGeometry = dynStaad.Geometry; } catch { RawGeometry = RawOpenStaad; }
            try { RawLoad = dynStaad.Load; } catch { RawLoad = RawOpenStaad; }
        }

        private void SetManagedObjects()
        {
            try { Geometry = RawGeometry != null ? new OSGeometryAdapter(this) : null; } catch { Geometry = null; }
            try { Load = RawLoad != null ? new OSLoadAdapter(this) : null; } catch { Load = null; }
        }

        private BaseUnitSystem GetBaseUnitSystem()
        {
            if (RawOpenStaad == null) return BaseUnitSystem.Unknown;
            try
            {
                dynamic dyn = RawOpenStaad;
                object unit = dyn.GetBaseUnit();
                return unit != null ? (BaseUnitSystem)Convert.ToInt32(unit) : BaseUnitSystem.Unknown;
            }
            catch
            {
                return BaseUnitSystem.Unknown;
            }
        }

        private ForceInputUnit GetInputForceUnit()
        {
            if (RawOpenStaad == null) return default;
            try
            {
                dynamic dyn = RawOpenStaad;
                object unit = dyn.GetInputUnitForForce(string.Empty);
                return unit != null ? (ForceInputUnit)Convert.ToInt32(unit) : default;
            }
            catch
            {
                return default;
            }
        }

        private LengthInputUnit GetInputLengthUnit()
        {
            if (RawOpenStaad == null) return default;
            try
            {
                dynamic dyn = RawOpenStaad;
                object unit = dyn.GetInputUnitForLength(string.Empty);
                return unit != null ? (LengthInputUnit)Convert.ToInt32(unit) : default;
            }
            catch
            {
                return default;
            }
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            RawGeometry = null;
            RawLoad = null;
            RawOpenStaad = null;
            Geometry = null;
            Load = null;
        }

        #endregion
    }
}
