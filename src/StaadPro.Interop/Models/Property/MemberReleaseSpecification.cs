using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Preserves one DOF's native release mode and its applicable spring or partial-moment parameter.</summary>
    /// <remarks>The main source's Boolean Releases representation cannot retain -1/-2/-3 modes. This
    /// snapshot preserves them independently. Unused/unavailable parameters are null, never invented zero.</remarks>
    public sealed class DegreeOfFreedomRelease
    {
        internal DegreeOfFreedomRelease(MemberDofReleaseMode mode, double? parameter)
        { Mode = mode; NativeParameter = parameter; }
        /// <summary>Gets the exact documented native per-DOF release mode.</summary>
        public MemberDofReleaseMode Mode { get; }
        /// <summary>Gets a written native spring/partial-moment slot when applicable; null for unused or unavailable slots.</summary>
        /// <remarks>For Spring this is stiffness in the native translational or rotational spring units.
        /// For DirectionalPartialMoment it is a dimensionless native slot; use the extended directional outputs when supplied.</remarks>
        public double? NativeParameter { get; }
    }

    /// <summary>Contains applicable MPX/MPY/MPZ outputs from the extended release query.</summary>
    public sealed class PartialMomentReleaseFactors
    {
        internal PartialMomentReleaseFactors(double? x, double? y, double? z) { MomentX = x; MomentY = y; MomentZ = z; }
        /// <summary>Gets the dimensionless MPX factor, or null when that directional mode is not selected.</summary>
        public double? MomentX { get; }
        /// <summary>Gets the dimensionless MPY factor, or null when that directional mode is not selected.</summary>
        public double? MomentY { get; }
        /// <summary>Gets the dimensionless MPZ factor, or null when that directional mode is not selected.</summary>
        public double? MomentZ { get; }
    }

    /// <summary>Contains an immutable member-end release snapshot with six independent native modes.</summary>
    /// <remarks>Reuses the main MemberReleases DOF and MP concepts without its mutable parser, shared
    /// event state, Boolean loss of partial-release flags or zero defaults for unreturned MP data.</remarks>
    public sealed class MemberReleaseSpecification
    {
        internal MemberReleaseSpecification(DegreeOfFreedomRelease[] releases, bool extended, double? common, PartialMomentReleaseFactors directional)
        { TranslationX = releases[0]; TranslationY = releases[1]; TranslationZ = releases[2]; RotationX = releases[3]; RotationY = releases[4]; RotationZ = releases[5];
          IncludesExtendedOutputs = extended; CommonPartialMomentFactor = common; DirectionalPartialMomentFactors = directional; }
        /// <summary>Gets native FX release/spring state.</summary>
        public DegreeOfFreedomRelease TranslationX { get; }
        /// <summary>Gets native FY release/spring state.</summary>
        public DegreeOfFreedomRelease TranslationY { get; }
        /// <summary>Gets native FZ release/spring state.</summary>
        public DegreeOfFreedomRelease TranslationZ { get; }
        /// <summary>Gets native MX release/spring/partial-moment state.</summary>
        public DegreeOfFreedomRelease RotationX { get; }
        /// <summary>Gets native MY release/spring/partial-moment state.</summary>
        public DegreeOfFreedomRelease RotationY { get; }
        /// <summary>Gets native MZ release/spring/partial-moment state.</summary>
        public DegreeOfFreedomRelease RotationZ { get; }
        /// <summary>Gets whether the extended native release function was used.</summary>
        public bool IncludesExtendedOutputs { get; }
        /// <summary>Gets the dimensionless common MP factor when its extended output applies; otherwise null.</summary>
        public double? CommonPartialMomentFactor { get; }
        /// <summary>Gets applicable extended directional factors, or null for the basic query or when no directional mode applies.</summary>
        public PartialMomentReleaseFactors DirectionalPartialMomentFactors { get; }
    }
}
