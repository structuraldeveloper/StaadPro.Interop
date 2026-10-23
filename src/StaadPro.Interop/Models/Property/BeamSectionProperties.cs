namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains the short or extended native member-section parameter packet.</summary>
    /// <remarks>Native Ix/Iz prose conflicts with conventional axis labels. These slots retain their native names
    /// and order; they do not hydrate a catalog section or assert a principal/torsion mapping. No zero is
    /// substituted for thicknesses absent from the short query.</remarks>
    public sealed class BeamSectionProperties
    {
        internal BeamSectionProperties(double[] values, bool extended)
        { Width = values[0]; Depth = values[1]; AxialArea = values[2]; ShearAreaLocalY = values[3]; ShearAreaLocalZ = values[4]; NativeIx = values[5]; NativeIy = values[6]; NativeIz = values[7]; FlangeThickness = extended ? values[8] : (double?)null; WebThickness = extended ? values[9] : (double?)null; }
        /// <summary>Gets native section width in length units.</summary>
        public double Width { get; }
        /// <summary>Gets native section depth in length units.</summary>
        public double Depth { get; }
        /// <summary>Gets native Ax area in squared length units.</summary>
        public double AxialArea { get; }
        /// <summary>Gets native Ay effective shear area; zero retains the native ignore-shear-deformation meaning.</summary>
        public double ShearAreaLocalY { get; }
        /// <summary>Gets native Az effective shear area; zero retains the native ignore-shear-deformation meaning.</summary>
        public double ShearAreaLocalZ { get; }
        /// <summary>Gets unconverted native Ix output in fourth-power length units; the help labels its physical axis inconsistently.</summary>
        public double NativeIx { get; }
        /// <summary>Gets unconverted native Iy output in fourth-power length units.</summary>
        public double NativeIy { get; }
        /// <summary>Gets unconverted native Iz output in fourth-power length units; no torsion/principal-axis reinterpretation is inferred.</summary>
        public double NativeIz { get; }
        /// <summary>Gets native top-flange thickness in length units; null for the short query.</summary>
        public double? FlangeThickness { get; }
        /// <summary>Gets native web thickness in length units; null for the short query.</summary>
        public double? WebThickness { get; }
    }
}
