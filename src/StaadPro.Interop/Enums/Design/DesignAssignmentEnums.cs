namespace StaadPro.Interop.Enums.Design
{
    /// <summary>Identifies the documented common design-command strings.</summary>
    /// <remarks>Concrete commands are unsupported by this native route. Aluminum supports CheckCode, Select and TakeOff; timber supports CheckCode. Native licensing and design-code acceptance remain authoritative.</remarks>
    public enum DesignCommandKind
    {
        /// <summary>CHECK CODE.</summary>
        CheckCode,
        /// <summary>FIXED GROUP.</summary>
        FixedGroup,
        /// <summary>GROUP with a single property selector; qualified here only for STAAD.Pro 2025/2026.</summary>
        Group,
        /// <summary>MEMBER TAKE OFF.</summary>
        MemberTakeOff,
        /// <summary>SELECT.</summary>
        Select,
        /// <summary>SELECT OPTIMIZED.</summary>
        SelectOptimized,
        /// <summary>SELECT WELD.</summary>
        SelectWeld,
        /// <summary>SELECT WELD TRUSS.</summary>
        SelectWeldTruss,
        /// <summary>TAKE OFF.</summary>
        TakeOff
    }

    /// <summary>Selects the single-property form of GROUP, not the extended SAME AS native route.</summary>
    public enum DesignGroupProperty
    {
        /// <summary>Native option 0: no property selector.</summary>
        None = 0,
        /// <summary>Native option 1: Ax.</summary>
        Area = 1,
        /// <summary>Native option 2: Sy.</summary>
        SectionModulusY = 2,
        /// <summary>Native option 3: Sz.</summary>
        SectionModulusZ = 3
    }
}
