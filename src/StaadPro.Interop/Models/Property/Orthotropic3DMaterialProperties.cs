namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains all eighteen native 3D orthotropic constants as immutable named fields.</summary>
    /// <remarks>First/second/third retain native array order. The reviewed help does not establish physical
    /// axis/plane ordering, including for density, alpha and damping. No isotropic G relation, defaults,
    /// unit conversion or engineering certification is inferred.</remarks>
    public sealed class Orthotropic3DMaterialProperties
    {
        internal Orthotropic3DMaterialProperties(int materialNumber, string name, double[][] values)
        {
            NativeMaterialNumber = materialNumber; Name = name;
            ElasticModulusFirst = values[0][0]; ElasticModulusSecond = values[0][1]; ElasticModulusThird = values[0][2];
            PoissonRatioFirst = values[1][0]; PoissonRatioSecond = values[1][1]; PoissonRatioThird = values[1][2];
            ShearModulusFirst = values[2][0]; ShearModulusSecond = values[2][1]; ShearModulusThird = values[2][2];
            WeightDensityFirst = values[3][0]; WeightDensitySecond = values[3][1]; WeightDensityThird = values[3][2];
            ThermalExpansionFirst = values[4][0]; ThermalExpansionSecond = values[4][1]; ThermalExpansionThird = values[4][2];
            DampingRatioFirst = values[5][0]; DampingRatioSecond = values[5][1]; DampingRatioThird = values[5][2];
        }
        /// <summary>Gets the requested nonnegative native material number, not a material name or inferred inventory index.</summary>
        public int NativeMaterialNumber { get; }
        /// <summary>Gets the exact material name returned by STAAD.</summary>
        public string Name { get; }
        /// <summary>Gets native E[0] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ElasticModulusFirst { get; }
        /// <summary>Gets native E[1] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ElasticModulusSecond { get; }
        /// <summary>Gets native E[2] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ElasticModulusThird { get; }
        /// <summary>Gets native Poisson[0] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double PoissonRatioFirst { get; }
        /// <summary>Gets native Poisson[1] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double PoissonRatioSecond { get; }
        /// <summary>Gets native Poisson[2] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double PoissonRatioThird { get; }
        /// <summary>Gets native G[0] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ShearModulusFirst { get; }
        /// <summary>Gets native G[1] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ShearModulusSecond { get; }
        /// <summary>Gets native G[2] in native stress units, preserving native order without assigning a physical direction.</summary>
        public double ShearModulusThird { get; }
        /// <summary>Gets native Density[0] in native force/length-cubed units, preserving native order without assigning a physical direction.</summary>
        public double WeightDensityFirst { get; }
        /// <summary>Gets native Density[1] in native force/length-cubed units, preserving native order without assigning a physical direction.</summary>
        public double WeightDensitySecond { get; }
        /// <summary>Gets native Density[2] in native force/length-cubed units, preserving native order without assigning a physical direction.</summary>
        public double WeightDensityThird { get; }
        /// <summary>Gets native Alpha[0] in the native temperature convention, preserving native order without assigning a physical direction.</summary>
        public double ThermalExpansionFirst { get; }
        /// <summary>Gets native Alpha[1] in the native temperature convention, preserving native order without assigning a physical direction.</summary>
        public double ThermalExpansionSecond { get; }
        /// <summary>Gets native Alpha[2] in the native temperature convention, preserving native order without assigning a physical direction.</summary>
        public double ThermalExpansionThird { get; }
        /// <summary>Gets native Damping[0] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double DampingRatioFirst { get; }
        /// <summary>Gets native Damping[1] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double DampingRatioSecond { get; }
        /// <summary>Gets native Damping[2] in dimensionless native units, preserving native order without assigning a physical direction.</summary>
        public double DampingRatioThird { get; }
    }
}
