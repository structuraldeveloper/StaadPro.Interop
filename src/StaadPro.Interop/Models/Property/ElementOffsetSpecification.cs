using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains the stored plate-corner offset selector and its unconverted vector.</summary>
    /// <remarks>Reuses main ElementNodeOffsetAxis values and the assembly's existing Vector3D value type.
    /// A successful zero vector is distinct from PropertyQueryStatus.NoSpecification.</remarks>
    public sealed class ElementOffsetSpecification
    {
        internal ElementOffsetSpecification(ElementNodeOffsetAxis axis, Vector3D offset) { Axis = axis; Offset = offset; }
        /// <summary>Gets the stored local/global/Z-offset convention, without inferring a coordinate transform.</summary>
        public ElementNodeOffsetAxis Axis { get; }
        /// <summary>Gets an independent value vector in the stored convention and native length units.</summary>
        public Vector3D Offset { get; }
    }
}
