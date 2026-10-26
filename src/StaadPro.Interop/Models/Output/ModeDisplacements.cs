using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains an owned six-component eigenmode shape at one node.</summary>
    /// <remarks>This is a normalized native mode shape, not a physical static displacement. Translation and rotation vectors are independent value copies; no normalization or angular conversion is inferred.</remarks>
    public sealed class ModeDisplacements
    {
        internal ModeDisplacements(int nativeModeNumber, int nodeId, Vector3D translation, Vector3D rotation) { NativeModeNumber = nativeModeNumber; NodeId = nodeId; Translation = translation; Rotation = rotation; }
        /// <summary>Gets the exact caller-supplied nonnegative native mode number/index, without rebasing.</summary>
        public int NativeModeNumber { get; }
        /// <summary>Gets the requested positive analytical node ID.</summary>
        public int NodeId { get; }
        /// <summary>Gets the native X/Y/Z mode-shape translation slots; normalization-dependent units are preserved.</summary>
        public Vector3D Translation { get; }
        /// <summary>Gets the native Rx/Ry/Rz mode-shape rotation slots; no degrees/radians conversion is applied.</summary>
        public Vector3D Rotation { get; }
    }
}
