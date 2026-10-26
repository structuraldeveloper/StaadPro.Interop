using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains the native global base pressures for one requested support node.</summary>
    /// <remarks>The YZ/ZX/XY native arrays correspond to global X/Y/Z pressure directions. Signed native force-per-area values are preserved without soil/contact reinterpretation.</remarks>
    public sealed class NodeBasePressure
    {
        internal NodeBasePressure(int nodeId, double globalX, double globalY, double globalZ) { NodeId = nodeId; GlobalX = globalX; GlobalY = globalY; GlobalZ = globalZ; }
        /// <summary>Gets the requested positive support-node ID at this position.</summary>
        public int NodeId { get; }
        /// <summary>Gets the signed global X base pressure from the native YZ array.</summary>
        public double GlobalX { get; }
        /// <summary>Gets the signed global Y base pressure from the native ZX array.</summary>
        public double GlobalY { get; }
        /// <summary>Gets the signed global Z base pressure from the native XY array.</summary>
        public double GlobalZ { get; }
    }
}
