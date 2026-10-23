namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains the thirteen native 2D orthotropic material values as named immutable fields.</summary>
    /// <remarks>The reviewed help specifies buffer sizes but not physical axis/plane ordering. First/second/third
    /// preserve native order and must not be assumed to mean global X/Y/Z or XY/YZ/ZX. No isotropic
    /// E/Poisson-to-G relation, unit conversion or material defaults are applied.</remarks>
    public sealed class Orthotropic2DMaterialProperties
    {
        internal Orthotropic2DMaterialProperties(string name, double[][] values)
        { Name = name; ElasticModulusFirstDirection = values[0][0]; ElasticModulusSecondDirection = values[0][1]; PoissonRatioFirstDirection = values[1][0]; PoissonRatioSecondDirection = values[1][1]; ShearModulusFirstPlane = values[2][0]; ShearModulusSecondPlane = values[2][1]; ShearModulusThirdPlane = values[2][2]; WeightDensityFirstDirection = values[3][0]; WeightDensitySecondDirection = values[3][1]; ThermalExpansionFirstDirection = values[4][0]; ThermalExpansionSecondDirection = values[4][1]; DampingRatioFirstDirection = values[5][0]; DampingRatioSecondDirection = values[5][1]; }
        /// <summary>Gets the exact native material name.</summary>
        public string Name { get; }
        /// <summary>Gets native E[0] without relabeling its physical direction or converting its units.</summary>
        public double ElasticModulusFirstDirection { get; }
        /// <summary>Gets native E[1] without relabeling its physical direction or converting its units.</summary>
        public double ElasticModulusSecondDirection { get; }
        /// <summary>Gets native Poisson[0] without relabeling its physical direction or converting its units.</summary>
        public double PoissonRatioFirstDirection { get; }
        /// <summary>Gets native Poisson[1] without relabeling its physical direction or converting its units.</summary>
        public double PoissonRatioSecondDirection { get; }
        /// <summary>Gets native G[0] without relabeling its physical direction or converting its units.</summary>
        public double ShearModulusFirstPlane { get; }
        /// <summary>Gets native G[1] without relabeling its physical direction or converting its units.</summary>
        public double ShearModulusSecondPlane { get; }
        /// <summary>Gets native G[2] without relabeling its physical direction or converting its units.</summary>
        public double ShearModulusThirdPlane { get; }
        /// <summary>Gets native Density[0] without relabeling its physical direction or converting its units.</summary>
        public double WeightDensityFirstDirection { get; }
        /// <summary>Gets native Density[1] without relabeling its physical direction or converting its units.</summary>
        public double WeightDensitySecondDirection { get; }
        /// <summary>Gets native Alpha[0] without relabeling its physical direction or converting its units.</summary>
        public double ThermalExpansionFirstDirection { get; }
        /// <summary>Gets native Alpha[1] without relabeling its physical direction or converting its units.</summary>
        public double ThermalExpansionSecondDirection { get; }
        /// <summary>Gets native Damping[0] without relabeling its physical direction or converting its units.</summary>
        public double DampingRatioFirstDirection { get; }
        /// <summary>Gets native Damping[1] without relabeling its physical direction or converting its units.</summary>
        public double DampingRatioSecondDirection { get; }
    }
}
