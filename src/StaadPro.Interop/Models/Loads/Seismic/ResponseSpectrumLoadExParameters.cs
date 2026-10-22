using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Encapsulates all options and data for OpenSTAAD's <c>AddResponseSpectrumLoadEx</c> API.
    /// </summary>
    /// <remarks>Mutable source-compatible request. Uses extended modal codes 0–6, Double parameter/data packets and the currently active load case. Validate does not select a case or certify code-specific engineering keywords. Keep inputs stable during submission.</remarks>
    public class ResponseSpectrumLoadExParameters
    {
        /// <summary>
        /// Gets or sets the Response Spectrum Code (0 for generic custom, 1 for IS1893-2002, 2 for IS1893-2016, 6-8 for IBC, 14 for GB50011, etc.).
        /// </summary>
        public ResponseSpectrumCode Code { get; set; } = ResponseSpectrumCode.GenericCustom;

        /// <summary>
        /// Gets or sets the modal combination rule (SRSS = 0, ABS = 1, CQC = 2, ASCE = 3, TEN = 4, CSM = 5, GRP = 6).
        /// </summary>
        public ModalCombinationMethod CombinationMethod { get; set; } = ModalCombinationMethod.CQC;

        /// <summary>
        /// Gets or sets primary parameters (Set 1): directional factors (X, Y, Z), damping (DAM/CDA/MDA), scale (SCA), etc.
        /// </summary>
        public Dictionary<string, double> PrimaryParameters { get; set; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets optional spectrum generation parameters (Set 2): e.g. SOI, CHE, RF, Q, ALP, etc.
        /// Note: Set 2 and CustomSpectrumData are mutually exclusive in STAAD.
        /// </summary>
        public Dictionary<string, double> SpectrumGenerationParameters { get; set; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets custom spectrum period-acceleration/displacement data curve pairs.
        /// Note: CustomSpectrumData and SpectrumGenerationParameters are mutually exclusive.
        /// </summary>
        public List<SpectrumCurvePoint> CustomSpectrumData { get; set; } = new List<SpectrumCurvePoint>();

        /// <summary>
        /// Adds a directional factor to the primary parameter set (X, Y, or Z).
        /// </summary>
        /// <param name="x">Finite X factor; values with magnitude at most 1e-9 are skipped without removing any existing X entry.</param>
        /// <param name="y">Finite Y factor with the same skip rule.</param>
        /// <param name="z">Finite Z factor with the same skip rule.</param>
        /// <returns>This editable parameter object.</returns>
        /// <remarks>Validates all three inputs before updating the dictionary. Does not clear earlier directions or dispatch native calls.</remarks>
        /// <exception cref="ArgumentException">A numeric input is nonfinite or the primary dictionary is null.</exception>
        public ResponseSpectrumLoadExParameters WithDirection(double x = 0, double y = 0, double z = 0)
        {
            LoadArguments.Finite(x, nameof(x)); LoadArguments.Finite(y, nameof(y)); LoadArguments.Finite(z, nameof(z));
            if (PrimaryParameters == null) throw new ArgumentNullException(nameof(PrimaryParameters));
            if (Math.Abs(x) > 1e-9) PrimaryParameters["X"] = x;
            if (Math.Abs(y) > 1e-9) PrimaryParameters["Y"] = y;
            if (Math.Abs(z) > 1e-9) PrimaryParameters["Z"] = z;
            return this;
        }

        /// <summary>
        /// Adds modal damping ratio to the primary parameter set (e.g. DAM = 0.05).
        /// </summary>
        /// <param name="dampingRatio">Finite DAM scalar, conventionally a fraction; engineering suitability is the caller’s responsibility.</param>
        /// <returns>This object after replacing the DAM entry.</returns>
        /// <exception cref="ArgumentException">The scalar is nonfinite or the primary dictionary is null.</exception>
        public ResponseSpectrumLoadExParameters WithDamping(double dampingRatio)
        {
            LoadArguments.Finite(dampingRatio, nameof(dampingRatio));
            if (PrimaryParameters == null) throw new ArgumentNullException(nameof(PrimaryParameters));
            PrimaryParameters["DAM"] = dampingRatio;
            return this;
        }

        /// <summary>
        /// Adds scale factor to the primary parameter set.
        /// </summary>
        /// <param name="scale">Finite SCA multiplier, forwarded without unit conversion.</param>
        /// <returns>This object after replacing the SCA entry.</returns>
        /// <exception cref="ArgumentException">The scalar is nonfinite or the primary dictionary is null.</exception>
        public ResponseSpectrumLoadExParameters WithScale(double scale)
        {
            LoadArguments.Finite(scale, nameof(scale));
            if (PrimaryParameters == null) throw new ArgumentNullException(nameof(PrimaryParameters));
            PrimaryParameters["SCA"] = scale;
            return this;
        }

        /// <summary>
        /// Validates parameters before dispatching to OpenSTAAD.
        /// </summary>
        /// <remarks>Validates a request without issuing native commands. Keep mutable inputs stable during validation and dispatch.</remarks>
        /// <exception cref="ArgumentException">A required field, classification, numeric value or input packet is invalid.</exception>
        public void Validate()
        {
            SpectrumNativePacket.Extended(this);
        }
    }
}
