using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains all ten named immutable native general-prismatic creation inputs.</summary>
    /// <remarks>Native array order is AX, AY, AZ, IX, IY, IZ, YD, ZD, YB, ZB. No unit conversion, derived dimensions, area/inertia consistency check or catalog inference occurs; the caller owns engineering adequacy.</remarks>
    public sealed class GeneralPrismaticSectionDefinition
    {
        /// <summary>Validates and copies the complete explicit input packet.</summary>
        /// <param name="axialArea">AX area in native length squared; finite and positive.</param>
        /// <param name="shearAreaY">AY in native length squared; finite and nonnegative; zero retains native ignore-shear meaning.</param>
        /// <param name="shearAreaZ">AZ in native length squared; finite and nonnegative; zero retains native ignore-shear meaning.</param>
        /// <param name="torsionalConstant">IX torsional constant in native length to the fourth power; finite and nonnegative.</param>
        /// <param name="inertiaY">IY about native local Y in length to the fourth power; finite and nonnegative.</param>
        /// <param name="inertiaZ">IZ about native local Z in length to the fourth power; finite and nonnegative.</param>
        /// <param name="depthY">YD local-Y depth in native length units; finite and nonnegative.</param>
        /// <param name="depthZ">ZD local-Z depth in native length units; finite and nonnegative.</param>
        /// <param name="stemDepthOrTopWidth">YB stem depth for tees or top width for trapezoids in native length units; finite and nonnegative.</param>
        /// <param name="stemWidthOrBottomWidth">ZB stem width for tees or bottom width for trapezoids in native length units; finite and nonnegative.</param>
        /// <exception cref="ArgumentOutOfRangeException">A numeric input violates the documented managed preconditions.</exception>
        public GeneralPrismaticSectionDefinition(double axialArea, double shearAreaY, double shearAreaZ, double torsionalConstant, double inertiaY, double inertiaZ, double depthY, double depthZ, double stemDepthOrTopWidth, double stemWidthOrBottomWidth)
        {
            IsotropicMaterialDefinition.Finite(axialArea, nameof(axialArea), positive: true);
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY));
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ));
            IsotropicMaterialDefinition.Finite(torsionalConstant, nameof(torsionalConstant));
            IsotropicMaterialDefinition.Finite(inertiaY, nameof(inertiaY));
            IsotropicMaterialDefinition.Finite(inertiaZ, nameof(inertiaZ));
            IsotropicMaterialDefinition.Finite(depthY, nameof(depthY));
            IsotropicMaterialDefinition.Finite(depthZ, nameof(depthZ));
            IsotropicMaterialDefinition.Finite(stemDepthOrTopWidth, nameof(stemDepthOrTopWidth));
            IsotropicMaterialDefinition.Finite(stemWidthOrBottomWidth, nameof(stemWidthOrBottomWidth));
            AxialArea = axialArea;
            ShearAreaY = shearAreaY;
            ShearAreaZ = shearAreaZ;
            TorsionalConstant = torsionalConstant;
            InertiaY = inertiaY;
            InertiaZ = inertiaZ;
            DepthY = depthY;
            DepthZ = depthZ;
            StemDepthOrTopWidth = stemDepthOrTopWidth;
            StemWidthOrBottomWidth = stemWidthOrBottomWidth;
        }
        /// <summary>Gets explicit AX area in native length squared; finite and positive.</summary>
        public double AxialArea { get; }
        /// <summary>Gets explicit AY in native length squared; finite and nonnegative; zero retains native ignore-shear meaning.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit AZ in native length squared; finite and nonnegative; zero retains native ignore-shear meaning.</summary>
        public double ShearAreaZ { get; }
        /// <summary>Gets explicit IX torsional constant in native length to the fourth power; finite and nonnegative.</summary>
        public double TorsionalConstant { get; }
        /// <summary>Gets explicit IY about native local Y in length to the fourth power; finite and nonnegative.</summary>
        public double InertiaY { get; }
        /// <summary>Gets explicit IZ about native local Z in length to the fourth power; finite and nonnegative.</summary>
        public double InertiaZ { get; }
        /// <summary>Gets explicit YD local-Y depth in native length units; finite and nonnegative.</summary>
        public double DepthY { get; }
        /// <summary>Gets explicit ZD local-Z depth in native length units; finite and nonnegative.</summary>
        public double DepthZ { get; }
        /// <summary>Gets explicit YB stem depth for tees or top width for trapezoids in native length units; finite and nonnegative.</summary>
        public double StemDepthOrTopWidth { get; }
        /// <summary>Gets explicit ZB stem width for tees or bottom width for trapezoids in native length units; finite and nonnegative.</summary>
        public double StemWidthOrBottomWidth { get; }
    }
}
