using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable CompositeSlab inputs in native order.</summary>
    /// <remarks>Native order: CFR, CFL, CFT, MR. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptCompositeSlab
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="rightWidth">Finite nonnegative Composite slab width to the right of web centerline (CFR) in native length units.</param>
        /// <param name="leftWidth">Finite nonnegative Composite slab width to the left of web centerline (CFL) in native length units.</param>
        /// <param name="thickness">Finite positive Composite slab thickness (CFT) in native length units.</param>
        /// <param name="modularRatio">Finite positive Explicit concrete modular ratio (MR) in dimensionless native units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptCompositeSlab(double rightWidth, double leftWidth, double thickness, double modularRatio)
        {
            IsotropicMaterialDefinition.Finite(rightWidth, nameof(rightWidth), positive: false, nonnegative: true); RightWidth = rightWidth;
            IsotropicMaterialDefinition.Finite(leftWidth, nameof(leftWidth), positive: false, nonnegative: true); LeftWidth = leftWidth;
            IsotropicMaterialDefinition.Finite(thickness, nameof(thickness), positive: true, nonnegative: true); Thickness = thickness;
            IsotropicMaterialDefinition.Finite(modularRatio, nameof(modularRatio), positive: true, nonnegative: true); ModularRatio = modularRatio;
        }
        internal double[] NativeValues() => new[] { RightWidth, LeftWidth, Thickness, ModularRatio };
        /// <summary>Gets explicit Composite slab width to the right of web centerline (CFR) in native length units.</summary>
        public double RightWidth { get; }
        /// <summary>Gets explicit Composite slab width to the left of web centerline (CFL) in native length units.</summary>
        public double LeftWidth { get; }
        /// <summary>Gets explicit Composite slab thickness (CFT) in native length units.</summary>
        public double Thickness { get; }
        /// <summary>Gets explicit Explicit concrete modular ratio (MR) in dimensionless native units.</summary>
        public double ModularRatio { get; }
    }
}
