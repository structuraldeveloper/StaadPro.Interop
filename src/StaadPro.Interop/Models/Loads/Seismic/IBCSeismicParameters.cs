using System;
using System.Collections.Generic;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Provides strongly-typed parameter definitions for International Building Code (IBC 2006/2012/2015/2018) seismic definitions.
    /// </summary>
    public class IBCSeismicParameters
    {
        /// <summary>Mapped MCE spectral response acceleration at short periods SS (g).</summary>
        public double? SS { get; set; }

        /// <summary>Mapped MCE spectral response acceleration at 1-sec period S1 (g).</summary>
        public double? S1 { get; set; }

        /// <summary>Postal Zip Code (alternative to SS/S1).</summary>
        public int? ZipCode { get; set; }

        /// <summary>Latitude in decimal degrees (alternative to ZipCode/SS).</summary>
        public double? Latitude { get; set; }

        /// <summary>Longitude in decimal degrees (alternative to ZipCode/SS).</summary>
        public double? Longitude { get; set; }

        /// <summary>Occupancy Importance Factor I (e.g. 1.0, 1.25, 1.5).</summary>
        public double ImportanceFactor { get; set; } = 1.0;

        /// <summary>Response modification coefficient in X direction RX (R value).</summary>
        public double ResponseModificationFactorX { get; set; } = 3.0;

        /// <summary>Response modification coefficient in Z direction RZ (R value).</summary>
        public double ResponseModificationFactorZ { get; set; } = 3.0;

        /// <summary>Site Soil Class SCLASS (1=A, 2=B, 3=C, 4=D, 5=E, 6=F).</summary>
        public int SiteClass { get; set; } = 4;

        /// <summary>Period calculation parameter CT in X direction CTX.</summary>
        public double? CTX { get; set; }

        /// <summary>Period calculation parameter CT in Z direction CTZ.</summary>
        public double? CTZ { get; set; }

        /// <summary>Period in X direction PX (seconds, 0 for code formula).</summary>
        public double PeriodX { get; set; } = 0.0;

        /// <summary>Period in Z direction PZ (seconds, 0 for code formula).</summary>
        public double PeriodZ { get; set; } = 0.0;

        /// <summary>Long-period transition period TL (seconds).</summary>
        public double? TL { get; set; }

        /// <summary>Site coefficient Fa.</summary>
        public double? FA { get; set; }

        /// <summary>Site coefficient Fv.</summary>
        public double? FV { get; set; }

        /// <summary>Period exponent in X direction XX.</summary>
        public double? ExponentX { get; set; }

        /// <summary>Period exponent in Z direction XZ.</summary>
        public double? ExponentZ { get; set; }

        /// <summary>
        /// Converts the parameters to a dictionary of STAAD keyword-value pairs.
        /// </summary>
        /// <returns>A new case-insensitive dictionary of native keywords and finite values; no native calls are made.</returns>
        /// <remarks>Only explicitly supplied nullable values are included. This mapping does not select or verify the active seismic definition. Values use the native code-specific units.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A numeric value is not finite.</exception>
        public Dictionary<string, double> ToParameterDictionary()
        {
            if (SS.HasValue) LoadArguments.Finite(SS.Value, nameof(SS));
            if (S1.HasValue) LoadArguments.Finite(S1.Value, nameof(S1));
            if (Latitude.HasValue) LoadArguments.Finite(Latitude.Value, nameof(Latitude));
            if (Longitude.HasValue) LoadArguments.Finite(Longitude.Value, nameof(Longitude));
            LoadArguments.Finite(ImportanceFactor, nameof(ImportanceFactor));
            LoadArguments.Finite(ResponseModificationFactorX, nameof(ResponseModificationFactorX));
            LoadArguments.Finite(ResponseModificationFactorZ, nameof(ResponseModificationFactorZ));
            if (CTX.HasValue) LoadArguments.Finite(CTX.Value, nameof(CTX));
            if (CTZ.HasValue) LoadArguments.Finite(CTZ.Value, nameof(CTZ));
            LoadArguments.Finite(PeriodX, nameof(PeriodX));
            LoadArguments.Finite(PeriodZ, nameof(PeriodZ));
            if (TL.HasValue) LoadArguments.Finite(TL.Value, nameof(TL));
            if (FA.HasValue) LoadArguments.Finite(FA.Value, nameof(FA));
            if (FV.HasValue) LoadArguments.Finite(FV.Value, nameof(FV));
            if (ExponentX.HasValue) LoadArguments.Finite(ExponentX.Value, nameof(ExponentX));
            if (ExponentZ.HasValue) LoadArguments.Finite(ExponentZ.Value, nameof(ExponentZ));
            var dict = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
            {
                ["I"] = ImportanceFactor,
                ["RX"] = ResponseModificationFactorX,
                ["RZ"] = ResponseModificationFactorZ,
                ["SCLASS"] = SiteClass,
                ["PX"] = PeriodX,
                ["PZ"] = PeriodZ
            };

            if (SS.HasValue) dict["SS"] = SS.Value;
            if (S1.HasValue) dict["S1"] = S1.Value;
            if (ZipCode.HasValue) dict["ZIP"] = ZipCode.Value;
            if (Latitude.HasValue) dict["LAT"] = Latitude.Value;
            if (Longitude.HasValue) dict["LONG"] = Longitude.Value;
            if (CTX.HasValue) dict["CTX"] = CTX.Value;
            if (CTZ.HasValue) dict["CTZ"] = CTZ.Value;
            if (TL.HasValue) dict["TL"] = TL.Value;
            if (FA.HasValue) dict["FA"] = FA.Value;
            if (FV.HasValue) dict["FV"] = FV.Value;
            if (ExponentX.HasValue) dict["XX"] = ExponentX.Value;
            if (ExponentZ.HasValue) dict["XZ"] = ExponentZ.Value;

            return dict;
        }
    }
}
