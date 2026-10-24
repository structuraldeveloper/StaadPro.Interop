using System.Collections.Generic;
using StaadPro.Interop.Common;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries a uniform native plate thickness definition and assignment candidates.</summary>
    /// <remarks>Thickness retains the caller's native length convention. Unlike PlateElementThickness this packet has one uniform value; creation sends that value unchanged to the native scalar overload.</remarks>
    public class PlateThickness : ValidatablePropertyStore
    {
        /// <summary>Initializes an empty plate candidate set and an unresolved definition ID.</summary>
        public PlateThickness() { Plates = new HashSet<Plate>(); }
        /// <summary>Gets or sets the positive native thickness definition ID, or zero before creation/resolution.</summary>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets or sets uniform thickness in the acquired native creation length convention.</summary>
        public double Thickness { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the mutable plate candidates for explicit assignment; null means no candidates.</summary>
        public HashSet<Plate> Plates { get => Get<HashSet<Plate>>(); set => Set(value); }
    }
}
