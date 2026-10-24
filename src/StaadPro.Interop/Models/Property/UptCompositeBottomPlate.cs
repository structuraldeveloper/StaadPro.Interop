using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains explicit immutable CompositeBottomPlate inputs in native order.</summary>
    /// <remarks>Native order: BPR, BPL, BPT. No defaults, unit conversion, derived geometry,
    /// catalog lookup or engineering consistency check occurs. Signed native reference distances retain
    /// their sign; other finite/range checks are managed preconditions, not engineering certification.</remarks>
    public sealed class UptCompositeBottomPlate
    {
        /// <summary>Validates and copies all explicit native inputs.</summary>
        /// <param name="nativeBprWidth">Finite nonnegative Native BPR additional plate width (BPR) in native length units.</param>
        /// <param name="nativeBplWidth">Finite nonnegative Native BPL additional plate width; the help duplicates a side description, so no physical side is inferred (BPL) in native length units.</param>
        /// <param name="thickness">Finite positive Additional bottom plate thickness (BPT) in native length units.</param>
        /// <exception cref="ArgumentException">An explicit value violates the stated finite/range or pipe-diameter relationship.</exception>
        public UptCompositeBottomPlate(double nativeBprWidth, double nativeBplWidth, double thickness)
        {
            IsotropicMaterialDefinition.Finite(nativeBprWidth, nameof(nativeBprWidth), positive: false, nonnegative: true); NativeBprWidth = nativeBprWidth;
            IsotropicMaterialDefinition.Finite(nativeBplWidth, nameof(nativeBplWidth), positive: false, nonnegative: true); NativeBplWidth = nativeBplWidth;
            IsotropicMaterialDefinition.Finite(thickness, nameof(thickness), positive: true, nonnegative: true); Thickness = thickness;
        }
        internal double[] NativeValues() => new[] { NativeBprWidth, NativeBplWidth, Thickness };
        /// <summary>Gets explicit Native BPR additional plate width (BPR) in native length units.</summary>
        public double NativeBprWidth { get; }
        /// <summary>Gets explicit Native BPL additional plate width; the help duplicates a side description, so no physical side is inferred (BPL) in native length units.</summary>
        public double NativeBplWidth { get; }
        /// <summary>Gets explicit Additional bottom plate thickness (BPT) in native length units.</summary>
        public double Thickness { get; }
    }
}
