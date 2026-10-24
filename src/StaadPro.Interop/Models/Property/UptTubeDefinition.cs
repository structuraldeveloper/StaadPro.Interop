using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable Tube inputs in native order.</summary>
    /// <remarks>Native order: AX, D, WF, TF, IZ, IY, IX, AY, AZ. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptTubeDefinition
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="axialArea">Finite positive Explicit cross-sectional area (AX) in native length-squared units.</param>
        /// <param name="depth">Finite positive Explicit section depth (D) in native length units.</param>
        /// <param name="flangeWidth">Finite positive Explicit native flange width (WF) in native length units.</param>
        /// <param name="flangeThickness">Finite positive Explicit native flange thickness (TF) in native length units.</param>
        /// <param name="nativeIz">Finite nonnegative Native IZ torsional constant (IZ) in native length-to-fourth-power units.</param>
        /// <param name="nativeIy">Finite nonnegative Native IY about local Y (IY) in native length-to-fourth-power units.</param>
        /// <param name="nativeIx">Finite nonnegative Native IX about local Z (IX) in native length-to-fourth-power units.</param>
        /// <param name="shearAreaY">Finite nonnegative Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</param>
        /// <param name="shearAreaZ">Finite nonnegative Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptTubeDefinition(double axialArea, double depth, double flangeWidth, double flangeThickness, double nativeIz, double nativeIy, double nativeIx, double shearAreaY, double shearAreaZ)
        {
            IsotropicMaterialDefinition.Finite(axialArea, nameof(axialArea), positive: true, nonnegative: true); AxialArea = axialArea;
            IsotropicMaterialDefinition.Finite(depth, nameof(depth), positive: true, nonnegative: true); Depth = depth;
            IsotropicMaterialDefinition.Finite(flangeWidth, nameof(flangeWidth), positive: true, nonnegative: true); FlangeWidth = flangeWidth;
            IsotropicMaterialDefinition.Finite(flangeThickness, nameof(flangeThickness), positive: true, nonnegative: true); FlangeThickness = flangeThickness;
            IsotropicMaterialDefinition.Finite(nativeIz, nameof(nativeIz), positive: false, nonnegative: true); NativeIz = nativeIz;
            IsotropicMaterialDefinition.Finite(nativeIy, nameof(nativeIy), positive: false, nonnegative: true); NativeIy = nativeIy;
            IsotropicMaterialDefinition.Finite(nativeIx, nameof(nativeIx), positive: false, nonnegative: true); NativeIx = nativeIx;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false, nonnegative: true); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false, nonnegative: true); ShearAreaZ = shearAreaZ;
        }
        internal double[] NativeValues() => new[] { AxialArea, Depth, FlangeWidth, FlangeThickness, NativeIz, NativeIy, NativeIx, ShearAreaY, ShearAreaZ };
        /// <summary>Gets explicit Explicit cross-sectional area (AX) in native length-squared units.</summary>
        public double AxialArea { get; }
        /// <summary>Gets explicit Explicit section depth (D) in native length units.</summary>
        public double Depth { get; }
        /// <summary>Gets explicit Explicit native flange width (WF) in native length units.</summary>
        public double FlangeWidth { get; }
        /// <summary>Gets explicit Explicit native flange thickness (TF) in native length units.</summary>
        public double FlangeThickness { get; }
        /// <summary>Gets explicit Native IZ torsional constant (IZ) in native length-to-fourth-power units.</summary>
        public double NativeIz { get; }
        /// <summary>Gets explicit Native IY about local Y (IY) in native length-to-fourth-power units.</summary>
        public double NativeIy { get; }
        /// <summary>Gets explicit Native IX about local Z (IX) in native length-to-fourth-power units.</summary>
        public double NativeIx { get; }
        /// <summary>Gets explicit Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</summary>
        public double ShearAreaZ { get; }
    }
}
