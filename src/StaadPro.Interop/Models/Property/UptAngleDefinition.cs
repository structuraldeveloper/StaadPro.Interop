using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable Angle inputs in native order.</summary>
    /// <remarks>Native order: D, WF, TF, R, AY, AZ. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptAngleDefinition
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="depth">Finite positive Explicit section depth (D) in native length units.</param>
        /// <param name="flangeWidth">Finite positive Explicit native flange width (WF) in native length units.</param>
        /// <param name="flangeThickness">Finite positive Explicit native flange thickness (TF) in native length units.</param>
        /// <param name="principalRadiusOfGyration">Finite nonnegative Radius of gyration about the principal axis (R) in native length units.</param>
        /// <param name="shearAreaY">Finite nonnegative Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</param>
        /// <param name="shearAreaZ">Finite nonnegative Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptAngleDefinition(double depth, double flangeWidth, double flangeThickness, double principalRadiusOfGyration, double shearAreaY, double shearAreaZ)
        {
            IsotropicMaterialDefinition.Finite(depth, nameof(depth), positive: true, nonnegative: true); Depth = depth;
            IsotropicMaterialDefinition.Finite(flangeWidth, nameof(flangeWidth), positive: true, nonnegative: true); FlangeWidth = flangeWidth;
            IsotropicMaterialDefinition.Finite(flangeThickness, nameof(flangeThickness), positive: true, nonnegative: true); FlangeThickness = flangeThickness;
            IsotropicMaterialDefinition.Finite(principalRadiusOfGyration, nameof(principalRadiusOfGyration), positive: false, nonnegative: true); PrincipalRadiusOfGyration = principalRadiusOfGyration;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false, nonnegative: true); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false, nonnegative: true); ShearAreaZ = shearAreaZ;
        }
        internal double[] NativeValues() => new[] { Depth, FlangeWidth, FlangeThickness, PrincipalRadiusOfGyration, ShearAreaY, ShearAreaZ };
        /// <summary>Gets explicit Explicit section depth (D) in native length units.</summary>
        public double Depth { get; }
        /// <summary>Gets explicit Explicit native flange width (WF) in native length units.</summary>
        public double FlangeWidth { get; }
        /// <summary>Gets explicit Explicit native flange thickness (TF) in native length units.</summary>
        public double FlangeThickness { get; }
        /// <summary>Gets explicit Radius of gyration about the principal axis (R) in native length units.</summary>
        public double PrincipalRadiusOfGyration { get; }
        /// <summary>Gets explicit Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</summary>
        public double ShearAreaZ { get; }
    }
}
