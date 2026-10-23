using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains directly observed material constants without defaults or derived substitutes.</summary>
    /// <remarks>Reuses main MaterialProperty field semantics. Its computed G and default engineering values
    /// are deliberately not used: ShearModulus is a native observation only, or null when not returned.</remarks>
    public sealed class MaterialConstants
    {
        internal MaterialConstants(double[] values, double? shearModulus = null)
        { ElasticModulus = values[0]; PoissonRatio = values[1]; WeightDensity = values[2]; ThermalExpansionCoefficient = values[3]; DampingRatio = values[4]; ShearModulus = shearModulus; }
        /// <summary>Gets native elastic modulus in native force/area units.</summary>
        public double ElasticModulus { get; }
        /// <summary>Gets native dimensionless Poisson ratio.</summary>
        public double PoissonRatio { get; }
        /// <summary>Gets native weight per volume, not mass density.</summary>
        public double WeightDensity { get; }
        /// <summary>Gets native coefficient per temperature unit.</summary>
        public double ThermalExpansionCoefficient { get; }
        /// <summary>Gets native dimensionless damping ratio.</summary>
        public double DampingRatio { get; }
        /// <summary>Gets the directly returned native G modulus, or null when the selected function omits it.</summary>
        public double? ShearModulus { get; }
    }

    /// <summary>Contains strength fields directly returned by an extended material observation.</summary>
    /// <remarks>Stress fields retain native force/area units. These values are not a design-code check.</remarks>
    public sealed class MaterialStrengthProperties
    {
        internal MaterialStrengthProperties(double[] values)
        { YieldStress = values[0]; UltimateTensileStrength = values[1]; YieldStrengthRatio = values[2]; TensileStrengthRatio = values[3]; CompressiveStrength = values[4]; }
        /// <summary>Gets native Fy yield stress.</summary>
        public double YieldStress { get; }
        /// <summary>Gets native Fu tensile strength.</summary>
        public double UltimateTensileStrength { get; }
        /// <summary>Gets native dimensionless Ry ratio.</summary>
        public double YieldStrengthRatio { get; }
        /// <summary>Gets native dimensionless Rt ratio.</summary>
        public double TensileStrengthRatio { get; }
        /// <summary>Gets native Fcu compressive strength.</summary>
        public double CompressiveStrength { get; }
    }

    /// <summary>Contains a material name, observed constants and optional native strength/assignment outputs.</summary>
    /// <remarks>Null optional blocks indicate outputs absent from the chosen function, not native zero.
    /// Reuses the main material field groups without catalog dependencies, mutable entity sets, assumed IDs or default values.</remarks>
    public sealed class MaterialProperties
    {
        internal MaterialProperties(string name, MaterialConstants constants, MaterialStrengthProperties strengths = null, MaterialAssignmentState? assignment = null)
        { Name = name; Constants = constants; Strengths = strengths; AssignmentState = assignment; }
        /// <summary>Gets the exact requested or native-returned material name, preserving case and whitespace.</summary>
        public string Name { get; }
        /// <summary>Gets directly observed constants, including G only where returned.</summary>
        public MaterialConstants Constants { get; }
        /// <summary>Gets extended strength outputs, or null for a basic query.</summary>
        public MaterialStrengthProperties Strengths { get; }
        /// <summary>Gets native unassigned/assigned state only for the assignment-aware query; otherwise null.</summary>
        public MaterialAssignmentState? AssignmentState { get; }
    }
}
