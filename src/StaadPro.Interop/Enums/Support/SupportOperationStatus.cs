namespace StaadPro.Interop.Enums.Support
{
    /// <summary>Qualifies a Support query without interpreting counts or type codes as flags.</summary>
    public enum SupportQueryStatus
    {
        /// <summary>Native query succeeded and its owned payload passed validation.</summary>
        Success = 0,
        /// <summary>Native operation returned its documented error/failure.</summary>
        NativeFailure = 1,
        /// <summary>Native type zero explicitly reports no assigned support; no information packet is published.</summary>
        NoSupport = 2,
        /// <summary>Native null/empty identity text cannot distinguish absent from unavailable.</summary>
        EmptyOrUnavailable = 3,
        /// <summary>Count or foundation context changed during extraction; no mixed payload is published.</summary>
        StateChanged = 4,
        /// <summary>Observed count exceeds the requested allocation limit; no result buffer was allocated.</summary>
        LimitExceeded = 5,
        /// <summary>Native type is unknown to the reviewed information layout; use GetSupportType for its raw code.</summary>
        UnsupportedNativeType = 6
    }
    /// <summary>Describes a native Support mutation acknowledgement, independently of save/persistence.</summary>
    public enum SupportMutationStatus
    {
        /// <summary>The operation returned its documented successful acknowledgement.</summary>
        Succeeded = 0,
        /// <summary>The operation returned its documented failure.</summary>
        NativeFailure = 1,
        /// <summary>The native void invocation completed; no success acknowledgement or read-back is claimed.</summary>
        InvokedWithoutAcknowledgement = 2,
        /// <summary>An empty node selection caused no native invocation.</summary>
        NoOperation = 3
    }
    /// <summary>Preserves Support per-DOF states; member partial-release modes are not accepted here.</summary>
    public enum SupportDofState
    {
        /// <summary>Native -1: spring state, with its separately recorded stiffness.</summary>
        Spring = -1,
        /// <summary>Native 0: fixed state.</summary>
        Fixed = 0,
        /// <summary>Native 1: released state.</summary>
        Released = 1
    }
}
