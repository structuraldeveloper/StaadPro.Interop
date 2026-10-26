using System;
using StaadPro.Interop.Entities;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Describes one sparse plate-corner extraction using existing entity types.</summary>
    /// <remarks>Entity references remain caller-owned and mutable. Query methods capture metadata and supplied coordinates before native dispatch; do not mutate inputs during enumeration. An ID-only plate can be hydrated by a single/sparse query; all-corner expansions require supplied incidence.</remarks>
    public sealed class PlateCornerForcesRequest
    {
        /// <summary>Creates a sparse request without invoking STAAD.</summary>
        /// <param name="loadCase">Non-null case metadata.</param><param name="plate">Non-null plate; ID must later be positive.</param><param name="cornerNode">Non-null corner selector; its ID selects the plate's canonical incidence point.</param>
        /// <exception cref="ArgumentNullException">An entity is null.</exception>
        public PlateCornerForcesRequest(ILoadCase loadCase, Plate plate, Node cornerNode)
        {
            LoadCase = loadCase ?? throw new ArgumentNullException(nameof(loadCase));
            Plate = plate ?? throw new ArgumentNullException(nameof(plate));
            CornerNode = cornerNode ?? throw new ArgumentNullException(nameof(cornerNode));
        }
        /// <summary>Gets the caller-owned case metadata reference.</summary>
        public ILoadCase LoadCase
        {
            get;
        }
        /// <summary>Gets the caller-owned plate reference.</summary>
        public Plate Plate
        {
            get;
        }
        /// <summary>Gets the caller-owned corner selector reference.</summary>
        public Node CornerNode
        {
            get;
        }
        /// <summary>Gets a key from the current entity identities; queries retain their own captured key.</summary>
        /// <exception cref="ArgumentOutOfRangeException">A current ID or family is invalid.</exception>
        public PlateCornerForcesKey Key => new PlateCornerForcesKey(LoadCase.Id, Plate.Id, CornerNode.Id, LoadCase.CaseType);
    }
}
