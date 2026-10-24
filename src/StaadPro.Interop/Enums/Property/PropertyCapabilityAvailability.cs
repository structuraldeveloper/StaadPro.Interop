namespace StaadPro.Interop.Enums.Property
{
    /// <summary>Identifies the qualification result for separately acquired newer Property APIs.</summary>
    public enum PropertyCapabilityAvailability
    {
        /// <summary>The exact native build and all required member signatures permit acquisition; live behavior remains unverified.</summary>
        Available,
        /// <summary>The acquired native version is unavailable or incomplete.</summary>
        UnknownVersion,
        /// <summary>The native product family is outside the reviewed 2025/2026 policy.</summary>
        DisabledByVersionPolicy,
        /// <summary>The servicing build or required signature metadata is unreviewed or incompatible.</summary>
        ContractUnverified,
        /// <summary>A required native catalog member is absent.</summary>
        NativeMemberAbsent
    }
}
