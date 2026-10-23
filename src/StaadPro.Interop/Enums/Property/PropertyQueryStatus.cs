namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Describes the outcome of a Property observation without confusing absence with a zero value.</summary>
    public enum PropertyQueryStatus
    {
        /// <summary>The native observation succeeded and validated Data is available.</summary>
        Success,
        /// <summary>The operation returned its documented failure flag.</summary>
        NativeFailure,
        /// <summary>The native operation reported that the requested entity or material was not found.</summary>
        NotFound,
        /// <summary>No property is assigned, as explicitly reported by the native operation.</summary>
        NotAssigned,
        /// <summary>No offset specification exists; this is distinct from a successful zero offset.</summary>
        NoSpecification,
        /// <summary>The entity or material reference was rejected by STAAD.</summary>
        InvalidReference,
        /// <summary>Native counts changed during retrieval; no snapshot is exposed.</summary>
        StateChanged,
        /// <summary>The requested output exceeds the caller's allocation limit.</summary>
        LimitExceeded,
        /// <summary>An empty/native-null name cannot distinguish no assignment from an unavailable entity.</summary>
        EmptyOrUnavailable,
        /// <summary>The acquired native receiver does not expose the documented optional dispatch member.</summary>
        Unsupported
    }

    /// <summary>Preserves each documented native degree-of-freedom release code.</summary>
    /// <remarks>These are per-DOF states, not the main source's whole-member ReleaseType classification.
    /// Partial-moment modes must not be collapsed to a Boolean released flag.</remarks>
    public enum MemberDofReleaseMode
    {
        /// <summary>No release or spring (native 0).</summary>
        Restrained = 0,
        /// <summary>Released degree of freedom (native 1).</summary>
        Released = 1,
        /// <summary>Spring stiffness defined (native -1).</summary>
        Spring = -1,
        /// <summary>Directional MPX/MPY/MPZ release defined (native -2).</summary>
        DirectionalPartialMoment = -2,
        /// <summary>Common MP partial-moment release defined (native -3).</summary>
        CommonPartialMoment = -3
    }

    /// <summary>Identifies the native assignment observation returned with isotropic material properties.</summary>
    public enum MaterialAssignmentState
    {
        /// <summary>Native value 1: the material is unassigned.</summary>
        Unassigned = 1,
        /// <summary>Native value 2: the material is assigned to one or more elements.</summary>
        Assigned = 2
    }

    /// <summary>Identifies a plate-node offset's stored native coordinate convention.</summary>
    /// <remarks>Ported from the main project's ElementNodeOffsetAxis. Codes differ from its member OffsetAxis.
    /// Z is the native Z-offset selector; no global/local transformation is inferred.</remarks>
    public enum ElementNodeOffsetAxis
    {
        /// <summary>Stored local-coordinate offset (native 0).</summary>
        Local = 0,
        /// <summary>Stored global-coordinate offset (native 1).</summary>
        Global = 1,
        /// <summary>Stored Z-offset convention (native 2).</summary>
        Z = 2
    }
}
