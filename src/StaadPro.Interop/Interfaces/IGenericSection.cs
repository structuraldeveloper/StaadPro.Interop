using System.Collections.Generic;
using StaadPro.Interop.Common;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Interfaces
{
    /// <summary>Exposes the main-project section identity, values, area and managed assignment surface.</summary>
    /// <remarks>Native operations belong to the Property adapter. Private catalog display helpers and model-level raw COM creation methods are excluded from this data boundary. Field positions depend on the acquired native STAAD version and type, not on a universal array schema.</remarks>
    public interface IGenericSection : IEntity
    {
        /// <summary>Gets the native section database record, or -1 when no record is available/applicable; zero is retained as a possible native record.</summary>
        int SectionRecNo { get; }
        /// <summary>Gets the exact native section designation.</summary>
        string StaadName { get; }
        /// <summary>Gets the native property label without private catalog substitutions.</summary>
        string PropertyName { get; }
        /// <summary>Gets the native country code cast to the preserved enum; zero/non-enum codes may represent non-table or future definitions.</summary>
        Country Country { get; }
        /// <summary>Gets the directly returned Ax or the documented mean tapered-I plate area in native length-squared units.</summary>
        /// <exception cref="System.NotSupportedException">BeamD and UPT ISECTION omit Ax; no private catalog or rectangular approximation is substituted.</exception>
        double Ax { get; }
        /// <summary>Gets the native section type used to decode this packet.</summary>
        SectionPropertyType SectionPropertyType { get; }
        /// <summary>Gets an owned copy of the native-capacity buffer; unused unwritten padding is zero-filled for compatibility.</summary>
        double[] PropertyValues { get; }
        /// <summary>Gets the mutable set of candidate beam references; changing it alone does not modify STAAD.</summary>
        HashSet<Beam> AssignedBeams { get; }
    }
}
