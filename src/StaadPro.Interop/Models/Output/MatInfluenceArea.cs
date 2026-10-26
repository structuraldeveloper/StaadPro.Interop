using System.Windows.Media.Media3D;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains one requested support node and its corresponding native influence areas.</summary>
    /// <remarks>Native YZ/ZX/XY plane labels are retained. Duplicate requested node IDs remain duplicate records in input order; values use native length-squared units.</remarks>
    public sealed class MatInfluenceArea
    {
        internal MatInfluenceArea(int nodeId, double areaYZ, double areaZX, double areaXY) { NodeId = nodeId; AreaYZ = areaYZ; AreaZX = areaZX; AreaXY = areaXY; }
        /// <summary>Gets the requested positive support-node ID at this position.</summary>
        public int NodeId { get; }
        /// <summary>Gets the native area associated with the YZ plane.</summary>
        public double AreaYZ { get; }
        /// <summary>Gets the native area associated with the ZX plane.</summary>
        public double AreaZX { get; }
        /// <summary>Gets the native area associated with the XY plane.</summary>
        public double AreaXY { get; }
    }
}
