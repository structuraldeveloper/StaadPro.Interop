using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable Pipe inputs in native order.</summary>
    /// <remarks>Native order: OD, ID, AY, AZ. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptPipeDefinition
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="outerDiameter">Finite positive Explicit outer diameter (OD) in native length units.</param>
        /// <param name="innerDiameter">Finite nonnegative Explicit inner diameter; must be smaller than outerDiameter (ID) in native length units.</param>
        /// <param name="shearAreaY">Finite nonnegative Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</param>
        /// <param name="shearAreaZ">Finite nonnegative Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptPipeDefinition(double outerDiameter, double innerDiameter, double shearAreaY, double shearAreaZ)
        {
            IsotropicMaterialDefinition.Finite(outerDiameter, nameof(outerDiameter), positive: true, nonnegative: true); OuterDiameter = outerDiameter;
            IsotropicMaterialDefinition.Finite(innerDiameter, nameof(innerDiameter), positive: false, nonnegative: true); InnerDiameter = innerDiameter;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false, nonnegative: true); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false, nonnegative: true); ShearAreaZ = shearAreaZ;
            if (innerDiameter >= outerDiameter) throw new ArgumentException("The inner diameter must be smaller than the outer diameter.", nameof(innerDiameter));
        }
        internal double[] NativeValues() => new[] { OuterDiameter, InnerDiameter, ShearAreaY, ShearAreaZ };
        /// <summary>Gets explicit Explicit outer diameter (OD) in native length units.</summary>
        public double OuterDiameter { get; }
        /// <summary>Gets explicit Explicit inner diameter; must be smaller than outerDiameter (ID) in native length units.</summary>
        public double InnerDiameter { get; }
        /// <summary>Gets explicit Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</summary>
        public double ShearAreaZ { get; }
    }
}
