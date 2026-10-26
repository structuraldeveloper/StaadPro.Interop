using System.Windows.Media.Media3D;
using StaadPro.Interop.Enums.Output;
using StaadPro.Interop.Entities;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns six global nodal force/moment components for one identified plate corner.</summary>
    /// <remarks>Adapted from the main-project result using the existing immutable ForceComponents. The six-value layout follows all three installed VBA examples. Global axes follow the main adapter and read-only equilibrium observations on 2026.0.0.340; these are not plate-center stresses or moments per unit width. Other native builds still require qualification.</remarks>
    public sealed class PlateCornerForces
    {
        internal PlateCornerForces(PlateCornerForcesKey key, OutputLoadCaseIdentity loadCase, Point3D corner, ForceComponents components)
        {
            Key = key;
            LoadCase = loadCase;
            CornerPosition = corner;
            Components = components;
        }
        /// <summary>Gets the captured family/case/plate/node identity.</summary>
        public PlateCornerForcesKey Key
        {
            get;
        }
        /// <summary>Gets owned case metadata; no mutable case is retained.</summary>
        public OutputLoadCaseIdentity LoadCase
        {
            get;
        }
        /// <summary>Gets the captured plate ID.</summary>
        public int PlateId => Key.PlateId;
        /// <summary>Gets the captured incidence-node ID.</summary>
        public int CornerNodeId => Key.CornerNodeId;
        /// <summary>Gets the canonical incidence point in native length units.</summary>
        public Point3D CornerPosition
        {
            get;
        }
        /// <summary>Gets immutable Fx/Fy/Fz/Mx/My/Mz in global axes and native force/moment units.</summary>
        public ForceComponents Components
        {
            get;
        }
        /// <summary>Gets Global; this packet is never relabeled as a plate-local or member-local packet.</summary>
        public ResultCoordinateSystem CoordinateSystem => ResultCoordinateSystem.Global;
        /// <summary>Creates a fresh editable copy using the existing force algebra model.</summary><returns>An independent mutable copy; retain Key, CornerPosition and CoordinateSystem separately.</returns>
        public Forces ToForces() => Components.ToForces();
    }
}
