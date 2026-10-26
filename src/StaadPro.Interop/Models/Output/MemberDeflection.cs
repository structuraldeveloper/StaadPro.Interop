using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the two native Y/Z deflection outputs at the requested member station.</summary>
    /// <remarks>The reviewed help does not establish a complete absolute/relative or coordinate-frame convention. NativeY/NativeZ identify output slots without asserting a managed transformation.</remarks>
    public sealed class MemberDeflection
    {
        internal MemberDeflection(double nativeY, double nativeZ) { NativeY = nativeY; NativeZ = nativeZ; }
        /// <summary>Gets the finite native Y output in native result length units.</summary>
        public double NativeY { get; }
        /// <summary>Gets the finite native Z output in native result length units.</summary>
        public double NativeZ { get; }
    }
}
