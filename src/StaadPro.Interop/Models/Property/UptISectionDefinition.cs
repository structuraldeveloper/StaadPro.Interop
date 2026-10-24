using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable ISection inputs in native order.</summary>
    /// <remarks>Native order: DWW, TWW, DWW1, BFF, TFF, BFF1, TFF1, AYF, AZF, XIF. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptISectionDefinition
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="startDepth">Finite positive Section depth at start node (DWW) in native length units.</param>
        /// <param name="webThickness">Finite positive Explicit web thickness (TWW) in native length units.</param>
        /// <param name="endDepth">Finite positive Section depth at end node (DWW1) in native length units.</param>
        /// <param name="topFlangeWidth">Finite positive Top flange width (BFF) in native length units.</param>
        /// <param name="topFlangeThickness">Finite positive Top flange thickness (TFF) in native length units.</param>
        /// <param name="bottomFlangeWidth">Finite positive Bottom flange width (BFF1) in native length units.</param>
        /// <param name="bottomFlangeThickness">Finite positive Bottom flange thickness (TFF1) in native length units.</param>
        /// <param name="shearAreaY">Finite nonnegative Explicit shear area for local Y (AYF) in native length-squared units.</param>
        /// <param name="shearAreaZ">Finite nonnegative Explicit shear area for local Z (AZF) in native length-squared units.</param>
        /// <param name="torsionalConstant">Finite nonnegative Explicit torsional constant (XIF) in native length-to-fourth-power units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptISectionDefinition(double startDepth, double webThickness, double endDepth, double topFlangeWidth, double topFlangeThickness, double bottomFlangeWidth, double bottomFlangeThickness, double shearAreaY, double shearAreaZ, double torsionalConstant)
        {
            IsotropicMaterialDefinition.Finite(startDepth, nameof(startDepth), positive: true, nonnegative: true); StartDepth = startDepth;
            IsotropicMaterialDefinition.Finite(webThickness, nameof(webThickness), positive: true, nonnegative: true); WebThickness = webThickness;
            IsotropicMaterialDefinition.Finite(endDepth, nameof(endDepth), positive: true, nonnegative: true); EndDepth = endDepth;
            IsotropicMaterialDefinition.Finite(topFlangeWidth, nameof(topFlangeWidth), positive: true, nonnegative: true); TopFlangeWidth = topFlangeWidth;
            IsotropicMaterialDefinition.Finite(topFlangeThickness, nameof(topFlangeThickness), positive: true, nonnegative: true); TopFlangeThickness = topFlangeThickness;
            IsotropicMaterialDefinition.Finite(bottomFlangeWidth, nameof(bottomFlangeWidth), positive: true, nonnegative: true); BottomFlangeWidth = bottomFlangeWidth;
            IsotropicMaterialDefinition.Finite(bottomFlangeThickness, nameof(bottomFlangeThickness), positive: true, nonnegative: true); BottomFlangeThickness = bottomFlangeThickness;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false, nonnegative: true); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false, nonnegative: true); ShearAreaZ = shearAreaZ;
            IsotropicMaterialDefinition.Finite(torsionalConstant, nameof(torsionalConstant), positive: false, nonnegative: true); TorsionalConstant = torsionalConstant;
        }
        internal double[] NativeValues() => new[] { StartDepth, WebThickness, EndDepth, TopFlangeWidth, TopFlangeThickness, BottomFlangeWidth, BottomFlangeThickness, ShearAreaY, ShearAreaZ, TorsionalConstant };
        /// <summary>Gets explicit Section depth at start node (DWW) in native length units.</summary>
        public double StartDepth { get; }
        /// <summary>Gets explicit Explicit web thickness (TWW) in native length units.</summary>
        public double WebThickness { get; }
        /// <summary>Gets explicit Section depth at end node (DWW1) in native length units.</summary>
        public double EndDepth { get; }
        /// <summary>Gets explicit Top flange width (BFF) in native length units.</summary>
        public double TopFlangeWidth { get; }
        /// <summary>Gets explicit Top flange thickness (TFF) in native length units.</summary>
        public double TopFlangeThickness { get; }
        /// <summary>Gets explicit Bottom flange width (BFF1) in native length units.</summary>
        public double BottomFlangeWidth { get; }
        /// <summary>Gets explicit Bottom flange thickness (TFF1) in native length units.</summary>
        public double BottomFlangeThickness { get; }
        /// <summary>Gets explicit Explicit shear area for local Y (AYF) in native length-squared units.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit Explicit shear area for local Z (AZF) in native length-squared units.</summary>
        public double ShearAreaZ { get; }
        /// <summary>Gets explicit Explicit torsional constant (XIF) in native length-to-fourth-power units.</summary>
        public double TorsionalConstant { get; }
    }
}
