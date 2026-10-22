using StaadPro.Interop.Services.Load;
namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates ASCE 7 wind calculation factor overrides and manual inputs for OpenSTAAD.
    /// </summary>
    public class WindFactorsConfiguration
    {
        // User Input flags (true = User Input (1), false = Calculated by STAAD (0))

        /// <summary>
        /// Gets or sets whether velocity pressure exposure coefficient Kz is user-specified (Index 0).
        /// </summary>
        public bool IsKzUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether topographic factor Kzt is user-specified (Index 1).
        /// </summary>
        public bool IsKztUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether importance factor I is user-specified (Index 2).
        /// </summary>
        public bool IsIUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether wind directionality factor Kd is user-specified (Index 3).
        /// </summary>
        public bool IsKdUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether ground elevation factor Ke is user-specified (Index 4, ASCE 7-2016).
        /// </summary>
        public bool IsKeUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether gust effect factor G is user-specified (Index 5).
        /// </summary>
        public bool IsGUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether external pressure coefficient Cp is user-specified (Index 6).
        /// </summary>
        public bool IsCpUserInput { get; set; }

        /// <summary>
        /// Gets or sets whether internal pressure coefficient Gcpi is user-specified (Index 7).
        /// </summary>
        public bool IsGcpiUserInput { get; set; }

        // Factor values

        /// <summary>
        /// Gets or sets the custom Kz factor value.
        /// </summary>
        public double Kz { get; set; }

        /// <summary>
        /// Gets or sets the custom Kzt factor value.
        /// </summary>
        public double Kzt { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the custom Importance factor I value.
        /// </summary>
        public double I { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the custom Kd factor value.
        /// </summary>
        public double Kd { get; set; } = 0.85;

        /// <summary>
        /// Gets or sets the custom Ke factor value (ASCE 7-2016).
        /// </summary>
        public double Ke { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the custom Gust effect factor G value.
        /// </summary>
        public double G { get; set; } = 0.85;

        /// <summary>
        /// Gets or sets the custom External pressure coefficient Cp value.
        /// </summary>
        public double Cp { get; set; } = 0.8;

        /// <summary>
        /// Gets or sets the custom Internal pressure coefficient Gcpi value.
        /// </summary>
        public double Gcpi { get; set; } = 0.18;

        /// <summary>
        /// Marshals the user-input flags into the exact 8-element integer array expected by OpenSTAAD COM.
        /// </summary>
        /// <returns>An integer array of size 8 (1 for user input, 0 for calculated).</returns>
        public int[] ToUserInputFlagsArray()
        {
            return new int[8]
            {
                IsKzUserInput ? 1 : 0,
                IsKztUserInput ? 1 : 0,
                IsIUserInput ? 1 : 0,
                IsKdUserInput ? 1 : 0,
                IsKeUserInput ? 1 : 0,
                IsGUserInput ? 1 : 0,
                IsCpUserInput ? 1 : 0,
                IsGcpiUserInput ? 1 : 0
            };
        }

        /// <summary>
        /// Marshals the factor values into the exact 8-element double array expected by OpenSTAAD COM.
        /// </summary>
        /// <returns>A double array of size 8.</returns>
        public double[] ToFactorValuesArray()
        {
            LoadArguments.Finite(Kz, nameof(Kz));
            LoadArguments.Finite(Kzt, nameof(Kzt));
            LoadArguments.Finite(I, nameof(I));
            LoadArguments.Finite(Kd, nameof(Kd));
            LoadArguments.Finite(Ke, nameof(Ke));
            LoadArguments.Finite(G, nameof(G));
            LoadArguments.Finite(Cp, nameof(Cp));
            LoadArguments.Finite(Gcpi, nameof(Gcpi));
            return new double[8]
            {
                Kz,
                Kzt,
                I,
                Kd,
                Ke,
                G,
                Cp,
                Gcpi
            };
        }
    }
}
