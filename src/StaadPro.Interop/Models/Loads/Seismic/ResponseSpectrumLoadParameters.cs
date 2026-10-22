using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Encapsulates parameters for OpenSTAAD's <c>AddRSLoad</c> API (from OpenSTAAD Load Group).
    /// </summary>
    /// <remarks>Mutable source-compatible request for the legacy nineteen-slot API. Modal codes are 1–6, distinct from the extended API. Validate checks owned Single-precision conversion, exactly three directions and at least one point without invoking native code. Finite values that overflow Single or round from nonzero to zero are rejected; ordinary Single rounding is unavoidable. Caller chooses native quantity/scale units; no conversion, sorting or interpolation is added.</remarks>
    public class ResponseSpectrumLoadParameters
    {
        /// <summary>
        /// Gets or sets modal combination method (1=Srss, 2=Cqc, 3=Absolute, 4=Asce, 5=Ten, 6=Csm).
        /// </summary>
        public ResponseSpectrumLoadType LoadType { get; set; } = ResponseSpectrumLoadType.Cqc;

        /// <summary>
        /// Directional factor array: [X_Factor, Y_Factor, Z_Factor].
        /// </summary>
        public double[] DirectionFactors { get; set; } = new double[] { 1.0, 0.0, 0.0 };

        /// <summary>
        /// Acceleration (1) or Displacement (0) spectrum.
        /// </summary>
        public SpectrumQuantityType QuantityType { get; set; } = SpectrumQuantityType.Acceleration;

        /// <summary>
        /// Scale factor (e.g. 9.80665 or 1.0).
        /// </summary>
        public double Scale { get; set; } = 1.0;

        /// <summary>
        /// Damping type (1=DAMP, 2=CDAMP, 3=MDAMP).
        /// </summary>
        public SpectrumDampingType DampingType { get; set; } = SpectrumDampingType.Damp;

        /// <summary>
        /// Damping factor (e.g. 0.05 for 5%).
        /// </summary>
        public double DampingFactor { get; set; } = 0.05;

        /// <summary>
        /// Interpolation type (0=Linear, 1=Logarithmic).
        /// </summary>
        public SpectrumInterpolationType InterpolationType { get; set; } = SpectrumInterpolationType.Logarithmic;

        /// <summary>
        /// Whether missing mass is enabled (MIS).
        /// </summary>
        public bool ConsiderMissingMass { get; set; } = false;

        /// <summary>
        /// Missing mass factor.
        /// </summary>
        public double MissingMassFactor { get; set; } = 1.0;

        /// <summary>
        /// Whether Zero Period Acceleration (ZPA) is enabled.
        /// </summary>
        public bool ConsiderZPA { get; set; } = false;

        /// <summary>
        /// ZPA frequency or factor value.
        /// </summary>
        public double ZPAFactor { get; set; } = 33.0;

        /// <summary>
        /// Whether rigid mode frequency cutoff 1 (FF1) is enabled.
        /// </summary>
        public bool ConsiderFf1 { get; set; } = false;

        /// <summary>
        /// FF1 frequency value.
        /// </summary>
        public double Ff1Factor { get; set; } = 0.0;

        /// <summary>
        /// Whether rigid mode frequency cutoff 2 (FF2) is enabled.
        /// </summary>
        public bool ConsiderFf2 { get; set; } = false;

        /// <summary>
        /// FF2 frequency value.
        /// </summary>
        public double Ff2Factor { get; set; } = 0.0;

        /// <summary>
        /// Save flag (1 to save modal response values).
        /// </summary>
        public bool SaveFlag { get; set; } = false;

        /// <summary>
        /// Spectrum curve data points (Periods/Frequencies vs Accelerations/Displacements).
        /// </summary>
        public List<SpectrumCurvePoint> SpectrumPoints { get; set; } = new List<SpectrumCurvePoint>();

        /// <summary>
        /// Validates parameters.
        /// </summary>
        /// <remarks>Validates a request without issuing native commands. Keep mutable inputs stable during validation and dispatch.</remarks>
        /// <exception cref="ArgumentException">A required field, classification, numeric value or input packet is invalid.</exception>
        public void Validate()
        {
            SpectrumNativePacket.Legacy(this);
        }
    }
}
