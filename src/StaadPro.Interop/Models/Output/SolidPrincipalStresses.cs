using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Preserves the three native solid principal-stress slots without inventing their ordering.</summary>
    /// <remarks>The installed help repeats the S1 label. First/Second/Third identify native array positions, not a verified descending S1/S2/S3 order. Values are never sorted.</remarks>
    public sealed class SolidPrincipalStresses
    {
        internal SolidPrincipalStresses(double firstNativeValue, double secondNativeValue, double thirdNativeValue) { FirstNativeValue = firstNativeValue; SecondNativeValue = secondNativeValue; ThirdNativeValue = thirdNativeValue; }
        /// <summary>Gets the finite first native principal-stress slot, in native stress units.</summary>
        public double FirstNativeValue { get; }
        /// <summary>Gets the finite second native principal-stress slot, in native stress units.</summary>
        public double SecondNativeValue { get; }
        /// <summary>Gets the finite third native principal-stress slot, in native stress units.</summary>
        public double ThirdNativeValue { get; }
    }
}
