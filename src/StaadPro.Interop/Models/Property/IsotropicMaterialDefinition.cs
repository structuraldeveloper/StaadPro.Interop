using System;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains all seven explicit common inputs to native isotropic material creation.</summary>
    /// <remarks>No material defaults, derived shear modulus or unit conversion are applied. The caller supplies
    /// constants in the acquired creation convention and qualifies persistence on the intended STAAD build.</remarks>
    public sealed class IsotropicMaterialDefinition
    {
        /// <summary>Copies and validates an explicit immutable material definition.</summary>
        /// <param name="name">Exact nonblank material name without NUL characters; whitespace is preserved.</param>
        /// <param name="elasticModulus">Finite positive E in native force/length-squared units.</param>
        /// <param name="poissonRatio">Finite dimensionless Poisson ratio strictly between -1 and 0.5.</param>
        /// <param name="shearModulus">Finite positive G in native force/length-squared units; not derived from E/Poisson.</param>
        /// <param name="weightDensity">Finite nonnegative weight density in native force/length-cubed units.</param>
        /// <param name="thermalExpansion">Finite thermal expansion coefficient in the native temperature convention.</param>
        /// <param name="dampingRatio">Finite nonnegative dimensionless damping ratio.</param>
        /// <exception cref="ArgumentException">The name is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A numeric input violates its stated range or is nonfinite.</exception>
        public IsotropicMaterialDefinition(string name, double elasticModulus, double poissonRatio, double shearModulus,
            double weightDensity, double thermalExpansion, double dampingRatio)
        {
            MaterialName(name); Finite(elasticModulus, nameof(elasticModulus), true); Finite(shearModulus, nameof(shearModulus), true);
            if (double.IsNaN(poissonRatio) || poissonRatio <= -1 || poissonRatio >= .5) throw new ArgumentOutOfRangeException(nameof(poissonRatio));
            Finite(weightDensity, nameof(weightDensity)); Finite(thermalExpansion, nameof(thermalExpansion), nonnegative: false); Finite(dampingRatio, nameof(dampingRatio));
            Name = name; ElasticModulus = elasticModulus; PoissonRatio = poissonRatio; ShearModulus = shearModulus;
            WeightDensity = weightDensity; ThermalExpansion = thermalExpansion; DampingRatio = dampingRatio;
        }
        internal static void Finite(double value, string name, bool positive = false, bool nonnegative = true)
        { if (double.IsNaN(value) || double.IsInfinity(value) || positive && value <= 0 || nonnegative && value < 0) throw new ArgumentOutOfRangeException(name); }
        /// <summary>Gets the exact requested native material name.</summary>
        public string Name { get; }
        /// <summary>Gets explicit E in native force/length-squared units.</summary>
        public double ElasticModulus { get; }
        /// <summary>Gets the dimensionless Poisson ratio.</summary>
        public double PoissonRatio { get; }
        /// <summary>Gets explicit G in native force/length-squared units, independent of E and Poisson.</summary>
        public double ShearModulus { get; }
        /// <summary>Gets weight density in native force/length-cubed units.</summary>
        public double WeightDensity { get; }
        /// <summary>Gets the thermal expansion coefficient in the native temperature convention.</summary>
        public double ThermalExpansion { get; }
        /// <summary>Gets the dimensionless damping ratio.</summary>
        public double DampingRatio { get; }
    }

    /// <summary>Contains all five explicit strength inputs to CreateIsotropicMaterialPropertiesEx.</summary>
    /// <remarks>Values are forwarded without conversion. Native success does not prove these fields persisted;
    /// the acquired STAAD build must be qualified independently before relying on design strengths.</remarks>
    public sealed class IsotropicMaterialStrengths
    {
        /// <summary>Copies finite nonnegative strengths and dimensionless ratios in extended-creator order.</summary>
        /// <param name="yieldStrength">Fy in native force/length-squared units.</param>
        /// <param name="ultimateStrength">Fu in native force/length-squared units.</param>
        /// <param name="yieldRatio">Dimensionless Ry.</param>
        /// <param name="ultimateRatio">Dimensionless Rt.</param>
        /// <param name="compressiveStrength">Fcu in native force/length-squared units.</param>
        /// <exception cref="ArgumentOutOfRangeException">An input is negative or nonfinite.</exception>
        public IsotropicMaterialStrengths(double yieldStrength, double ultimateStrength, double yieldRatio, double ultimateRatio, double compressiveStrength)
        {
            IsotropicMaterialDefinition.Finite(yieldStrength, nameof(yieldStrength)); IsotropicMaterialDefinition.Finite(ultimateStrength, nameof(ultimateStrength));
            IsotropicMaterialDefinition.Finite(yieldRatio, nameof(yieldRatio)); IsotropicMaterialDefinition.Finite(ultimateRatio, nameof(ultimateRatio));
            IsotropicMaterialDefinition.Finite(compressiveStrength, nameof(compressiveStrength));
            YieldStrength = yieldStrength; UltimateStrength = ultimateStrength; YieldRatio = yieldRatio; UltimateRatio = ultimateRatio; CompressiveStrength = compressiveStrength;
        }
        /// <summary>Gets explicit Fy in native stress units.</summary>
        public double YieldStrength { get; }
        /// <summary>Gets explicit Fu in native stress units.</summary>
        public double UltimateStrength { get; }
        /// <summary>Gets dimensionless Ry.</summary>
        public double YieldRatio { get; }
        /// <summary>Gets dimensionless Rt.</summary>
        public double UltimateRatio { get; }
        /// <summary>Gets explicit Fcu in native stress units.</summary>
        public double CompressiveStrength { get; }
    }

    /// <summary>Contains four explicit steel strength inputs in native Steel-creator order: Fu, Fy, Rt, Ry.</summary>
    public sealed class SteelMaterialStrengths
    {
        /// <summary>Copies finite nonnegative steel strengths and ratios without reordering their meaning.</summary>
        /// <param name="ultimateStrength">Fu in native force/length-squared units, sent before Fy.</param>
        /// <param name="yieldStrength">Fy in native force/length-squared units.</param>
        /// <param name="ultimateRatio">Dimensionless Rt, sent before Ry.</param>
        /// <param name="yieldRatio">Dimensionless Ry.</param>
        /// <exception cref="ArgumentOutOfRangeException">An input is negative or nonfinite.</exception>
        public SteelMaterialStrengths(double ultimateStrength, double yieldStrength, double ultimateRatio, double yieldRatio)
        {
            IsotropicMaterialDefinition.Finite(ultimateStrength, nameof(ultimateStrength)); IsotropicMaterialDefinition.Finite(yieldStrength, nameof(yieldStrength));
            IsotropicMaterialDefinition.Finite(ultimateRatio, nameof(ultimateRatio)); IsotropicMaterialDefinition.Finite(yieldRatio, nameof(yieldRatio));
            UltimateStrength = ultimateStrength; YieldStrength = yieldStrength; UltimateRatio = ultimateRatio; YieldRatio = yieldRatio;
        }
        /// <summary>Gets explicit Fu in native stress units.</summary>
        public double UltimateStrength { get; }
        /// <summary>Gets explicit Fy in native stress units.</summary>
        public double YieldStrength { get; }
        /// <summary>Gets dimensionless Rt.</summary>
        public double UltimateRatio { get; }
        /// <summary>Gets dimensionless Ry.</summary>
        public double YieldRatio { get; }
    }
}
