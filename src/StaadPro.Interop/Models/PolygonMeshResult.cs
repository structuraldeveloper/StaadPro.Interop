using StaadPro.Interop.Entities;

using System.Collections.Generic;

namespace StaadPro.Interop.Models
{
    /// <summary>
    /// Result of generating a 2D triangular mesh over a simple polygon.
    /// </summary>
    /// <remarks>Source-compatible mutable result model. Collections are newly allocated; contained structural entities remain mutable.
    /// Generated results own their list containers, not the lifetime of the referenced native application.</remarks>
    public class PolygonMeshResult
    {
        /// <summary>
        /// The polygon's original boundary nodes, exactly as supplied to the mesh generator, in
        /// their original order and with their original identifiers. No new nodes are ever added
        /// to this collection, and none of these nodes are ever moved, renumbered, or removed.
        /// </summary>
        public List<Node> BoundaryNodes { get; set; } = new List<Node>();

        /// <summary>
        /// The nodes created inside the polygon boundary while refining the mesh to the requested
        /// quality. Every node here lies strictly inside the polygon, except for the case
        /// where a boundary segment itself was locally split to resolve an encroachment near
        /// a very sharp or thin region of the polygon; such split points still lie exactly on the
        /// original boundary line and never introduce a new corner.
        /// </summary>
        public List<Node> InteriorNodes { get; set; } = new List<Node>();

        /// <summary>
        /// The triangular plates that make up the mesh.
        /// </summary>
        public List<Plate> Plates { get; set; } = new List<Plate>();

        /// <summary>
        /// The smallest interior angle, in degrees, found across every triangle in <see cref="Plates"/>.
        /// This reflects the actual quality achieved, which may fall short of the requested minimum
        /// angle if a progress limit or the 256-insertion safety cap was reached.
        /// </summary>
        public double AchievedMinimumAngleDegrees { get; set; }

        /// <summary>
        /// <c>true</c> when both the angle and any enabled edge-length target were met;
        /// <c>false</c> when numerical progress or the insertion budget prevented this.
        /// A false value still returns the valid best-effort mesh. Inspect both this flag and
        /// <see cref="AchievedMinimumAngleDegrees"/> before using the mutable result.
        /// </summary>
        public bool ReachedTargetQuality { get; set; }
    }
}
