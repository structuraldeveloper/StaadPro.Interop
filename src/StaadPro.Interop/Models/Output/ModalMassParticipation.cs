using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the native X/Y/Z modal mass participation factors for an explicitly requested mode.</summary>
    /// <remarks>Factors are retained without converting between fractions and percentages or inferring their normalization.</remarks>
    public sealed class ModalMassParticipation
    {
        internal ModalMassParticipation(int nativeModeNumber, Vector3D factors) { NativeModeNumber = nativeModeNumber; Factors = factors; }
        /// <summary>Gets the exact native mode number/index passed by the caller, without rebasing.</summary>
        public int NativeModeNumber { get; }
        /// <summary>Gets the three native X/Y/Z factors as an independent value vector.</summary>
        public Vector3D Factors { get; }
    }
}
