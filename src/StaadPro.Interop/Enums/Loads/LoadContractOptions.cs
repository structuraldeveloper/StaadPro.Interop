using System;

namespace StaadPro.Interop.Enums.Loads
{
    /// <summary>Identifies the native ASD strength-of-forces option; unrelated to case LoadType.</summary>
    public enum LoadStrengthType
    {
        /// <summary>No strength-of-forces attribute (native 0).</summary>
        None = 0,
        /// <summary>Normal ASD working-stress forces without P-delta (native 1).</summary>
        WorkingStressWithoutPDelta = 1,
        /// <summary>Normal ASD working-stress forces with P-delta (native 2).</summary>
        WorkingStressWithPDelta = 2,
        /// <summary>Strength-type forces without P-delta (native 3).</summary>
        StrengthWithoutPDelta = 3,
        /// <summary>Strength-type forces with P-delta (native 4).</summary>
        StrengthWithPDelta = 4,
        /// <summary>Column-only strength-type forces without P-delta (native 5).</summary>
        ColumnStrengthWithoutPDelta = 5,
        /// <summary>Column-only strength-type forces with P-delta (native 6).</summary>
        ColumnStrengthWithPDelta = 6
    }

    /// <summary>Specifies how a plate trapezoidal pressure definition varies.</summary>
    public enum PlatePressureVariation
    {
        /// <summary>Two endpoint pressures varying in local X (native 1).</summary>
        LocalX = 1,
        /// <summary>Two endpoint pressures varying in local Y (native 2).</summary>
        LocalY = 2,
        /// <summary>Four pressures in plate joint order (native 3).</summary>
        Joints = 3
    }

    /// <summary>Specifies a directed global axis supported by native wind-load application.</summary>
    public enum WindLoadDirection
    {
        /// <summary>Positive global X (native 1).</summary>
        PositiveX = 1,
        /// <summary>Positive global Z (native 3).</summary>
        PositiveZ = 3,
        /// <summary>Negative global X (native 4).</summary>
        NegativeX = 4,
        /// <summary>Negative global Z (native 6).</summary>
        NegativeZ = 6
    }

    /// <summary>Selects explicit directions for automatic notional-load generation.</summary>
    /// <remarks>Managed flags, not native direction codes. Each flag maps to a separate native Boolean.</remarks>
    [Flags]
    public enum AutomaticNotionalDirections
    {
        /// <summary>No direction; rejected when notional generation is enabled.</summary>
        None = 0,
        /// <summary>Generate positive X contributions.</summary>
        PositiveX = 1,
        /// <summary>Generate negative X contributions.</summary>
        NegativeX = 2,
        /// <summary>Generate positive Z contributions.</summary>
        PositiveZ = 4,
        /// <summary>Generate negative Z contributions.</summary>
        NegativeZ = 8
    }

    /// <summary>Describes observed completion without promising native transactional rollback.</summary>
    public enum LoadOperationStatus
    {
        /// <summary>The documented native success contract was satisfied.</summary>
        Success,
        /// <summary>A documented native failure was returned.</summary>
        NativeFailure,
        /// <summary>The target case could not be selected.</summary>
        ActivationFailed,
        /// <summary>The requested choice is unsupported by the acquired native version.</summary>
        Unsupported,
        /// <summary>Required native semantics are unresolved; no mutation may be attempted.</summary>
        UnverifiedContract,
        /// <summary>Some earlier operations completed before a later failure.</summary>
        PartialCompletion,
        /// <summary>A void native call returned; this does not certify mutation success.</summary>
        InvocationCompleted
    }
}
