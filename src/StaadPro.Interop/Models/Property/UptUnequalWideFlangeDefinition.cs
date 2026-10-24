using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains an immutable twelve-slot UPT unequal wide-flange input without guessing physical second-flange orientation.</summary>
    public sealed class UptUnequalWideFlangeDefinition
    {
        /// <summary>Copies the ten primary values and explicit native WF1/TF1 inputs.</summary>
        /// <param name="primaryValues">Non-null immutable AX/D/TW/WF/TF/IZ/IY/IX/AY/AZ values in that exact order.</param>
        /// <param name="secondFlangeWidth">Finite positive WF1 in native length units, native slot ten; no geometric top/bottom mapping beyond the native label is inferred.</param>
        /// <param name="secondFlangeThickness">Finite positive TF1 in native length units, native slot eleven.</param>
        /// <exception cref="ArgumentNullException">The primary values are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A second-flange value is not finite and positive.</exception>
        public UptUnequalWideFlangeDefinition(UptWideFlangeDefinition primaryValues, double secondFlangeWidth, double secondFlangeThickness)
        {
            if (primaryValues == null) throw new ArgumentNullException(nameof(primaryValues));
            IsotropicMaterialDefinition.Finite(secondFlangeWidth, nameof(secondFlangeWidth), positive: true);
            IsotropicMaterialDefinition.Finite(secondFlangeThickness, nameof(secondFlangeThickness), positive: true);
            PrimaryValues = primaryValues; SecondFlangeWidth = secondFlangeWidth; SecondFlangeThickness = secondFlangeThickness;
        }
        /// <summary>Gets the immutable ten primary values; no mutable buffer is exposed.</summary>
        public UptWideFlangeDefinition PrimaryValues { get; }
        /// <summary>Gets native WF1 in length units, slot ten.</summary>
        public double SecondFlangeWidth { get; }
        /// <summary>Gets native TF1 in length units, slot eleven.</summary>
        public double SecondFlangeThickness { get; }
        internal double[] NativeValues()
        { var output = new double[12]; Array.Copy(PrimaryValues.NativeValues(), output, 10); output[10] = SecondFlangeWidth; output[11] = SecondFlangeThickness; return output; }
    }
}
