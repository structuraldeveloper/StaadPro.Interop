namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains all ten explicit immutable values for native UPT wide-flange insertion.</summary>
    /// <remarks>Native order is AX, D, TW, WF, TF, IZ, IY, IX, AY, AZ. Inertia slot names and this
    /// operation's documented meanings are retained; no reuse of a differently ordered section packet,
    /// private catalog lookup, unit conversion, defaults or engineering consistency check occurs.</remarks>
    public sealed class UptWideFlangeDefinition
    {
        /// <summary>Validates and copies all ten explicit native values.</summary>
        /// <param name="axialArea">Finite positive AX area in native length-squared units.</param>
        /// <param name="depth">Finite positive D total depth in native length units.</param>
        /// <param name="webThickness">Finite positive TW web thickness in native length units.</param>
        /// <param name="topFlangeWidth">Finite positive WF top flange width in native length units.</param>
        /// <param name="topFlangeThickness">Finite positive TF top flange thickness in native length units.</param>
        /// <param name="nativeIz">Finite nonnegative native IZ, documented here as torsional constant, in length-to-fourth-power units.</param>
        /// <param name="nativeIy">Finite nonnegative native IY about local Y in length-to-fourth-power units.</param>
        /// <param name="nativeIx">Finite nonnegative native IX, documented here about local Z, in length-to-fourth-power units.</param>
        /// <param name="shearAreaY">Finite nonnegative AY shear area in native length-squared units; zero retains ignore-shear semantics.</param>
        /// <param name="shearAreaZ">Finite nonnegative AZ shear area in native length-squared units; zero retains ignore-shear semantics.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">An input violates its documented finite/range requirement.</exception>
        public UptWideFlangeDefinition(double axialArea, double depth, double webThickness, double topFlangeWidth, double topFlangeThickness, double nativeIz, double nativeIy, double nativeIx, double shearAreaY, double shearAreaZ)
        {
            IsotropicMaterialDefinition.Finite(axialArea, nameof(axialArea), positive: true); AxialArea = axialArea;
            IsotropicMaterialDefinition.Finite(depth, nameof(depth), positive: true); Depth = depth;
            IsotropicMaterialDefinition.Finite(webThickness, nameof(webThickness), positive: true); WebThickness = webThickness;
            IsotropicMaterialDefinition.Finite(topFlangeWidth, nameof(topFlangeWidth), positive: true); TopFlangeWidth = topFlangeWidth;
            IsotropicMaterialDefinition.Finite(topFlangeThickness, nameof(topFlangeThickness), positive: true); TopFlangeThickness = topFlangeThickness;
            IsotropicMaterialDefinition.Finite(nativeIz, nameof(nativeIz), positive: false); NativeIz = nativeIz;
            IsotropicMaterialDefinition.Finite(nativeIy, nameof(nativeIy), positive: false); NativeIy = nativeIy;
            IsotropicMaterialDefinition.Finite(nativeIx, nameof(nativeIx), positive: false); NativeIx = nativeIx;
            IsotropicMaterialDefinition.Finite(shearAreaY, nameof(shearAreaY), positive: false); ShearAreaY = shearAreaY;
            IsotropicMaterialDefinition.Finite(shearAreaZ, nameof(shearAreaZ), positive: false); ShearAreaZ = shearAreaZ;
        }
        internal double[] NativeValues() => new[] { AxialArea, Depth, WebThickness, TopFlangeWidth, TopFlangeThickness, NativeIz, NativeIy, NativeIx, ShearAreaY, ShearAreaZ };
        /// <summary>Gets explicit AX area in native length-squared units.</summary>
        public double AxialArea { get; }
        /// <summary>Gets explicit D total depth in native length units.</summary>
        public double Depth { get; }
        /// <summary>Gets explicit TW web thickness in native length units.</summary>
        public double WebThickness { get; }
        /// <summary>Gets explicit WF top flange width in native length units.</summary>
        public double TopFlangeWidth { get; }
        /// <summary>Gets explicit TF top flange thickness in native length units.</summary>
        public double TopFlangeThickness { get; }
        /// <summary>Gets explicit native IZ, documented here as torsional constant, in length-to-fourth-power units.</summary>
        public double NativeIz { get; }
        /// <summary>Gets explicit native IY about local Y in length-to-fourth-power units.</summary>
        public double NativeIy { get; }
        /// <summary>Gets explicit native IX, documented here about local Z, in length-to-fourth-power units.</summary>
        public double NativeIx { get; }
        /// <summary>Gets explicit AY shear area in native length-squared units; zero retains ignore-shear semantics.</summary>
        public double ShearAreaY { get; }
        /// <summary>Gets explicit AZ shear area in native length-squared units; zero retains ignore-shear semantics.</summary>
        public double ShearAreaZ { get; }
    }
}
