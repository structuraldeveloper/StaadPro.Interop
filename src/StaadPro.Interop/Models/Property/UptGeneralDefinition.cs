using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable General inputs in native order.</summary>
    /// <remarks>Native order: AX, D, TD, B, TB, IZ, IY, IX, SZ, SY, AY, AZ, PZ, PY, HSS, DEE. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptGeneralDefinition
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="axialArea">Finite positive Explicit cross-sectional area (AX) in native length-squared units.</param>
        /// <param name="depth">Finite positive Explicit section depth (D) in native length units.</param>
        /// <param name="depthElementThickness">Finite nonnegative Thickness associated with an element parallel to depth (TD) in native length units.</param>
        /// <param name="width">Finite positive Explicit section width (B) in native length units.</param>
        /// <param name="flangeElementThickness">Finite nonnegative Thickness associated with a flange-parallel element (TB) in native length units.</param>
        /// <param name="nativeIz">Finite nonnegative Native IZ torsional constant (IZ) in native length-to-fourth-power units.</param>
        /// <param name="nativeIy">Finite nonnegative Native IY about local Y (IY) in native length-to-fourth-power units.</param>
        /// <param name="nativeIx">Finite nonnegative Native IX about local Z (IX) in native length-to-fourth-power units.</param>
        /// <param name="sectionModulusZ">Finite nonnegative Section modulus about local Z (SZ) in native length-cubed units.</param>
        /// <param name="sectionModulusY">Finite nonnegative Section modulus about local Y (SY) in native length-cubed units.</param>
        /// <param name="shearAreaY">Finite nonnegative Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</param>
        /// <param name="shearAreaZ">Finite nonnegative Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</param>
        /// <param name="plasticModulusZ">Finite nonnegative Plastic modulus about local Z (PZ) in native length-cubed units.</param>
        /// <param name="plasticModulusY">Finite nonnegative Plastic modulus about local Y (PY) in native length-cubed units.</param>
        /// <param name="warpingConstant">Finite nonnegative Native warping constant for lateral-torsional buckling (HSS) in the native HSS convention.</param>
        /// <param name="webDepth">Finite nonnegative Explicit web depth; rolled-section distance between fillets (DEE) in native length units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptGeneralDefinition(double axialArea, double depth, double depthElementThickness, double width, double flangeElementThickness, double nativeIz, double nativeIy, double nativeIx, double sectionModulusZ, double sectionModulusY, double shearAreaY, double shearAreaZ, double plasticModulusZ, double plasticModulusY, double warpingConstant, double webDepth)
        {
            IsotropicMaterialDefinition.Finite(axialArea, nameof(axialArea), positive: true, nonnegative: true); AxialArea = axialArea;
            IsotropicMaterialDefinition.Finite(depth, nameof(depth), positive: true, nonnegative: true); Depth = depth;
            IsotropicMaterialDefinition.Finite(depthElementThickness, nameof(depthElementThickness), positive: false, nonnegative: true); DepthElementThickness = depthElementThickness;
            IsotropicMaterialDefinition.Finite(width, nameof(width), positive: true, nonnegative: true); Width = width;
            IsotropicMaterialDefinition.Finite(flangeElementThickness, nameof(flangeElementThickness), positive: false, nonnegative: true); FlangeElementThickness = flangeElementThickness;
            IsotropicMaterialDefinition.Finite(nativeIz, nameof(nativeIz), positive: false, nonnegative: true); NativeIz = nativeIz;
            IsotropicMaterialDefinition.Finite(nativeIy, nameof(nativeIy), positive: false, nonnegative: true); NativeIy = nativeIy;
            IsotropicMaterialDefinition.Finite(nativeIx, nameof(nativeIx), positive: false, nonnegative: true); NativeIx = nativeIx;
            IsotropicMaterialDefinition.Finite(sectionModulusZ, nameof(sectionModulusZ), positive: false, nonnegative: true); SectionModulusZ = sectionModulusZ;
            IsotropicMaterialDefinition.Finite(sectionModulusY, nameof(sectionModulusY), positive: false, nonnegative: true); SectionModulusY = sectionModulusY;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false, nonnegative: true); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false, nonnegative: true); ShearAreaZ = shearAreaZ;
            IsotropicMaterialDefinition.Finite(plasticModulusZ, nameof(plasticModulusZ), positive: false, nonnegative: true); PlasticModulusZ = plasticModulusZ;
            IsotropicMaterialDefinition.Finite(plasticModulusY, nameof(plasticModulusY), positive: false, nonnegative: true); PlasticModulusY = plasticModulusY;
            IsotropicMaterialDefinition.Finite(warpingConstant, nameof(warpingConstant), positive: false, nonnegative: true); WarpingConstant = warpingConstant;
            IsotropicMaterialDefinition.Finite(webDepth, nameof(webDepth), positive: false, nonnegative: true); WebDepth = webDepth;
        }
        internal double[] NativeValues() => new[] { AxialArea, Depth, DepthElementThickness, Width, FlangeElementThickness, NativeIz, NativeIy, NativeIx, SectionModulusZ, SectionModulusY, ShearAreaY, ShearAreaZ, PlasticModulusZ, PlasticModulusY, WarpingConstant, WebDepth };
        /// <summary>Gets explicit Explicit cross-sectional area (AX) in native length-squared units.</summary>
        public double AxialArea { get; }
        /// <summary>Gets explicit Explicit section depth (D) in native length units.</summary>
        public double Depth { get; }
        /// <summary>Gets explicit Thickness associated with an element parallel to depth (TD) in native length units.</summary>
        public double DepthElementThickness { get; }
        /// <summary>Gets explicit Explicit section width (B) in native length units.</summary>
        public double Width { get; }
        /// <summary>Gets explicit Thickness associated with a flange-parallel element (TB) in native length units.</summary>
        public double FlangeElementThickness { get; }
        /// <summary>Gets explicit Native IZ torsional constant (IZ) in native length-to-fourth-power units.</summary>
        public double NativeIz { get; }
        /// <summary>Gets explicit Native IY about local Y (IY) in native length-to-fourth-power units.</summary>
        public double NativeIy { get; }
        /// <summary>Gets explicit Native IX about local Z (IX) in native length-to-fourth-power units.</summary>
        public double NativeIx { get; }
        /// <summary>Gets explicit Section modulus about local Z (SZ) in native length-cubed units.</summary>
        public double SectionModulusZ { get; }
        /// <summary>Gets explicit Section modulus about local Y (SY) in native length-cubed units.</summary>
        public double SectionModulusY { get; }
        /// <summary>Gets explicit Effective shear area for local Y; explicit zero retains native ignore-shear semantics (AY) in native length-squared units.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit Effective shear area for local Z; explicit zero retains native ignore-shear semantics (AZ) in native length-squared units.</summary>
        public double ShearAreaZ { get; }
        /// <summary>Gets explicit Plastic modulus about local Z (PZ) in native length-cubed units.</summary>
        public double PlasticModulusZ { get; }
        /// <summary>Gets explicit Plastic modulus about local Y (PY) in native length-cubed units.</summary>
        public double PlasticModulusY { get; }
        /// <summary>Gets explicit Native warping constant for lateral-torsional buckling (HSS) in the native HSS convention.</summary>
        public double WarpingConstant { get; }
        /// <summary>Gets explicit Explicit web depth; rolled-section distance between fillets (DEE) in native length units.</summary>
        public double WebDepth { get; }
    }
}
