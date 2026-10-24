namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Describes the native acknowledgement of a Property mutation, independently of entity IDs.</summary>
    public enum PropertyMutationStatus
    {
        /// <summary>The operation acknowledged success; persistence and engineering validity were not read back.</summary>
        Succeeded,
        /// <summary>The material or section definition was created; material status zero is not an ID.</summary>
        Created,
        /// <summary>An existing named material was updated (native material-creation status one).</summary>
        Updated,
        /// <summary>The operation returned its documented failure code or false flag; partial changes may remain.</summary>
        NativeFailure,
        /// <summary>The native operation rejected the profile selector or array representation.</summary>
        InvalidInput
    }

    /// <summary>Identifies the scope of a requested Property mutation.</summary>
    public enum PropertyMutationTargetKind
    {
        /// <summary>The operation affects the acquired model's section assignments.</summary>
        Model,
        /// <summary>The operation affects a named material definition.</summary>
        Material,
        /// <summary>The operation affects the listed analytical members.</summary>
        Member,
        /// <summary>The operation affects the listed analytical plates.</summary>
        Plate,
        /// <summary>The operation affects the listed analytical solids.</summary>
        Solid,
        /// <summary>The operation creates an assigned-profile section definition.</summary>
        SectionDefinition
    }

    /// <summary>Identifies the five native ASSIGN profile selectors; these are not section property reference IDs.</summary>
    public enum AssignedSectionProfile
    {
        /// <summary>ASSIGN ANGLE (native zero).</summary>
        Angle = 0,
        /// <summary>ASSIGN ANGLE DOUBLE (native one).</summary>
        DoubleAngle = 1,
        /// <summary>ASSIGN BEAM (native two).</summary>
        Beam = 2,
        /// <summary>ASSIGN COLUMN (native three).</summary>
        Column = 3,
        /// <summary>ASSIGN CHANNEL (native four).</summary>
        Channel = 4
    }

    /// <summary>Identifies the native classification of an isotropic material without supplying its constants.</summary>
    public enum IsotropicMaterialType
    {
        /// <summary>No material type is specified (native zero).</summary>
        NotSpecified = 0,
        /// <summary>Steel (native one).</summary>
        Steel = 1,
        /// <summary>Concrete (native two).</summary>
        Concrete = 2,
        /// <summary>Aluminum (native three).</summary>
        Aluminum = 3,
        /// <summary>Timber (native four).</summary>
        Timber = 4
    }
}
