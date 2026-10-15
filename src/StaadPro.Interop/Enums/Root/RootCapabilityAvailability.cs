namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Describes the result of a non-mutating Root capability acquisition.</summary>
    public enum RootCapabilityAvailability
    {
        /// <summary>The exact candidate build and compatible member metadata permit acquisition; runtime behavior is not certified. Value 0.</summary>
        Available = 0,

        /// <summary>The acquired native version is unavailable or incomplete. Value 1.</summary>
        UnknownVersion = 1,

        /// <summary>The observed product family is not a candidate for this feature. Value 2.</summary>
        DisabledByVersionPolicy = 2,

        /// <summary>The servicing build or native signature is not covered by the conservative policy. Value 3.</summary>
        ContractUnverified = 3,

        /// <summary>A required native member is absent. Value 4.</summary>
        NativeMemberAbsent = 4
    }
}
