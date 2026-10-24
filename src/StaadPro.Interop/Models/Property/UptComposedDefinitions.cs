using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains a native channel's ten common wide-flange slots plus explicit CZ.</summary>
    public sealed class UptChannelDefinition
    {
        /// <summary>Copies immutable inputs without assuming a physical meaning or dimension for the help's unexplained CZ label.</summary>
        /// <param name="primaryValues">Non-null explicit AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values.</param>
        /// <param name="nativeCz">Finite signed native CZ, passed unchanged in the acquired native convention between IX and AY.</param>
        /// <exception cref="ArgumentException">Primary values are null or CZ is nonfinite.</exception>
        public UptChannelDefinition(UptWideFlangeDefinition primaryValues, double nativeCz)
        { PrimaryValues = primaryValues ?? throw new ArgumentNullException(nameof(primaryValues)); IsotropicMaterialDefinition.Finite(nativeCz, nameof(nativeCz), nonnegative: false); NativeCz = nativeCz; }
        /// <summary>Gets the immutable ten common values; no mutable packet is exposed.</summary>
        public UptWideFlangeDefinition PrimaryValues { get; }
        /// <summary>Gets finite signed native CZ without centroid/axis reinterpretation.</summary>
        public double NativeCz { get; }
    }
    /// <summary>Contains a native tee's common values plus its explicit native CY reference distance.</summary>
    public sealed class UptTeeDefinition
    {
        /// <summary>Copies the immutable common values and native reference distance.</summary>
        /// <param name="primaryValues">Non-null explicit AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ values; the reader sends the native tee order AX,D,WF,TF,TW,... explicitly.</param>
        /// <param name="nativeCy">Finite signed native CY distance from the back of web to C.G. along local Y, in native length units; no axis transformation.</param>
        /// <exception cref="ArgumentException">Primary values are null or CY is nonfinite.</exception>
        public UptTeeDefinition(UptWideFlangeDefinition primaryValues, double nativeCy)
        { PrimaryValues = primaryValues ?? throw new ArgumentNullException(nameof(primaryValues)); IsotropicMaterialDefinition.Finite(nativeCy, nameof(nativeCy), nonnegative: false); NativeCy = nativeCy; }
        /// <summary>Gets the immutable common values; the native tee route has a distinct parameter order.</summary>
        public UptWideFlangeDefinition PrimaryValues { get; }
        /// <summary>Gets the explicit signed native CY reference distance in length units.</summary>
        public double NativeCy { get; }
    }
    /// <summary>Contains an immutable composite wide-flange packet with exactly 12, 16 or 19 native slots.</summary>
    /// <remarks>The 12 steel slots are required. The optional slab adds CFR,CFL,CFT,MR; a bottom plate
    /// then adds BPR,BPL,BPT. A bottom plate without a slab has no reviewed buffer layout and is rejected.</remarks>
    public sealed class UptCompositeWideFlangeDefinition
    {
        /// <summary>Copies an explicit supported composite layout without filling missing engineering inputs.</summary>
        /// <param name="steelValues">Non-null immutable twelve-slot AX,D,TW,WF,TF,IZ,IY,IX,AY,AZ,WF1,TF1 steel packet.</param>
        /// <param name="slab">Optional immutable four-slot composite slab. Null selects the reviewed twelve-slot steel-only route.</param>
        /// <param name="bottomPlate">Optional immutable three-slot plate; requires slab. Native BPR/BPL labels retain their reviewed ambiguity.</param>
        /// <exception cref="ArgumentException">Steel values are null or a bottom plate is supplied without a slab.</exception>
        public UptCompositeWideFlangeDefinition(UptUnequalWideFlangeDefinition steelValues, UptCompositeSlab slab = null, UptCompositeBottomPlate bottomPlate = null)
        {
            SteelValues = steelValues ?? throw new ArgumentNullException(nameof(steelValues));
            if (bottomPlate != null && slab == null) throw new ArgumentException("A bottom plate requires the reviewed composite-slab layout.", nameof(bottomPlate));
            Slab = slab; BottomPlate = bottomPlate;
        }
        /// <summary>Gets the immutable required twelve steel values.</summary>
        public UptUnequalWideFlangeDefinition SteelValues { get; }
        /// <summary>Gets the optional immutable slab, or null for the twelve-slot route.</summary>
        public UptCompositeSlab Slab { get; }
        /// <summary>Gets the optional immutable additional plate, or null.</summary>
        public UptCompositeBottomPlate BottomPlate { get; }
        /// <summary>Gets the exact native buffer length, 12, 16 or 19.</summary>
        public int NativeValueCount => 12 + (Slab == null ? 0 : 4) + (BottomPlate == null ? 0 : 3);
        internal double[] NativeValues()
        {
            var values = new double[NativeValueCount]; Array.Copy(SteelValues.NativeValues(), values, 12);
            if (Slab != null) Array.Copy(Slab.NativeValues(), 0, values, 12, 4);
            if (BottomPlate != null) Array.Copy(BottomPlate.NativeValues(), 0, values, 16, 3);
            return values;
        }
    }
}
