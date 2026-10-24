using StaadPro.Interop.Common;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Models.Sections
{
    /// <summary>Carries no additional native creation values.</summary>
    public sealed class NoAdditionalSpecifications : IAdditionalSpecifications
    {
        /// <summary>Copies the empty specification.</summary><returns>A new empty array.</returns>
        public double[] ToArray() => new double[0];
    }
    /// <summary>Carries clear spacing for double/back-to-back/flange-reversed profiles in native length units.</summary>
    public sealed class SpacedDoubleSectionsSpecifications : ValidatablePropertyStore, IAdditionalSpecifications
    {
        /// <summary>Gets or sets finite nonnegative clear spacing in the native creation length convention.</summary>
        public double Spacing { get => Get<double>(); set => Set(value); }
        /// <summary>Copies spacing.</summary><returns>A new single-entry array.</returns>
        public double[] ToArray() => new[] { Spacing };
    }
    /// <summary>Carries cover-plate width and thickness in native creation length units.</summary>
    public sealed class CoverPlateSpecifications : ValidatablePropertyStore, IAdditionalSpecifications
    {
        /// <summary>Gets or sets positive plate width.</summary>
        public double Width { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets positive plate thickness.</summary>
        public double Thickness { get => Get<double>(); set => Set(value); }
        /// <summary>Copies width and thickness.</summary><returns>A new two-entry array.</returns>
        public double[] ToArray() => new[] { Width, Thickness };
    }
    /// <summary>Carries separate top and bottom cover plates; TC uses Top, BC uses Bottom, TB uses both.</summary>
    public sealed class TopAndBottomCoverPlateSpecifications : IAdditionalSpecifications
    {
        /// <summary>Initializes independent top and bottom packets.</summary>
        public TopAndBottomCoverPlateSpecifications() { Top = new CoverPlateSpecifications(); Bottom = new CoverPlateSpecifications(); }
        /// <summary>Gets top cover dimensions in the native creation length convention.</summary>
        public CoverPlateSpecifications Top { get; private set; }
        /// <summary>Gets bottom cover dimensions in the native creation length convention.</summary>
        public CoverPlateSpecifications Bottom { get; private set; }
        /// <summary>Copies top width/thickness then bottom width/thickness.</summary><returns>A new four-entry array.</returns>
        public double[] ToArray() => new[] { Top.Width, Top.Thickness, Bottom.Width, Bottom.Thickness };
    }
    /// <summary>Carries concrete-composite thickness, strength, width and weight density without unit conversion.</summary>
    public sealed class ConcreteCompositeSpecifications : ValidatablePropertyStore, IAdditionalSpecifications
    {
        /// <summary>Gets or sets concrete thickness in native length units.</summary>
        public double Thickness { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets concrete strength in native force/area units.</summary>
        public double CharacteristicStrength { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets concrete width in native length units.</summary>
        public double Width { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets concrete weight per volume, not mass density.</summary>
        public double Density { get => Get<double>(); set => Set(value); }
        /// <summary>Copies CT, FC, CW and CD, matching the source creation call.</summary><returns>A new four-entry array.</returns>
        public double[] ToArray() => new[] { Thickness, CharacteristicStrength, Width, Density };
    }
    /// <summary>Carries user-defined pipe outside and inside diameters in native length units.</summary>
    public sealed class PipeDefineSpecifications : ValidatablePropertyStore, IAdditionalSpecifications
    {
        /// <summary>Gets or sets positive outside diameter.</summary>
        public double OuterDiameter { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets nonnegative inside diameter, smaller than outside diameter.</summary>
        public double InnerDiameter { get => Get<double>(); set => Set(value); }
        /// <summary>Copies outside then inside diameter.</summary><returns>A new two-entry array.</returns>
        public double[] ToArray() => new[] { OuterDiameter, InnerDiameter };
    }
    /// <summary>Carries user-defined tube wall thickness, width and depth in native length units.</summary>
    public sealed class TubeDefineSpecifications : ValidatablePropertyStore, IAdditionalSpecifications
    {
        /// <summary>Gets or sets positive wall thickness.</summary>
        public double Thickness { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets positive outside width.</summary>
        public double Width { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets positive outside depth.</summary>
        public double Depth { get => Get<double>(); set => Set(value); }
        /// <summary>Copies thickness, width and depth.</summary><returns>A new three-entry array.</returns>
        public double[] ToArray() => new[] { Thickness, Width, Depth };
    }
}
