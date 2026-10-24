using System;
using System.ComponentModel;
using System.Linq;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Models.Property;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries main-project release flags/springs plus lossless native per-DOF evidence when populated.</summary>
    /// <remarks>The whole-packet classification cannot describe independent mixed modes. NativeSpecification retains them. Basic creation rejects partial-moment modes instead of reducing them to Boolean flags. Editing flags/springs/factors invalidates the retained observation; it does not change STAAD.</remarks>
    public class MemberReleases : ValidatablePropertyStore, IEntity
    {
        private bool hasDirectionalPartialMode, hasCommonPartialMode;
        /// <summary>Initializes independent flags, zero spring values and directional factors with no native binding.</summary>
        public MemberReleases()
        {
            Releases = new Releases(); Springs = new Springs(); MpFactors = new MpFactors();
            Releases.PropertyChanged += ProjectionChanged;
            Springs.PropertyChanged += ProjectionChanged;
            MpFactors.PropertyChanged += ProjectionChanged;
        }
        private void ProjectionChanged(object sender, PropertyChangedEventArgs e)
        {
            NativeSpecification = null;
            OnPropertyChanged(nameof(NativeSpecification)); OnPropertyChanged(nameof(ReleaseType)); OnPropertyChanged(nameof(ContainsPartialMomentModes));
        }
        /// <summary>Initializes a packet with the supplied specification ID.</summary>
        /// <param name="id">Native specification ID; zero means not resolved or created.</param>
        public MemberReleases(int id) : this() { Id = id; }
        /// <summary>Gets or sets the native specification ID; member-end read-back does not resolve it.</summary>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets the legacy aggregate classification, with directional/common partial modes taking precedence.</summary>
        /// <remarks>Inspect NativeSpecification for independent mixed modes. This getter derives current state without the source's duplicated change handlers.</remarks>
        public ReleaseType ReleaseType
        {
            get
            {
                var modes = NativeSpecification == null ? new MemberDofReleaseMode[0] : Dofs(NativeSpecification).Select(d => d.Mode).ToArray();
                if (hasDirectionalPartialMode || modes.Contains(MemberDofReleaseMode.DirectionalPartialMoment) || MpFactors.MPx != 0 || MpFactors.MPy != 0 || MpFactors.MPz != 0) return ReleaseType.MpAll;
                if (hasCommonPartialMode || modes.Contains(MemberDofReleaseMode.CommonPartialMoment) || MpFactor != 0) return ReleaseType.MpOnly;
                if (modes.Contains(MemberDofReleaseMode.Spring) || !Springs.IsAllZeroOrNull()) return ReleaseType.ReleaseWithSprings;
                return Releases.IsReleased ? ReleaseType.Release : ReleaseType.NoRelease;
            }
        }
        /// <summary>Gets the mutable Boolean full-release projection; -1/-2/-3 are not represented by these flags.</summary>
        public Releases Releases { get; private set; }
        /// <summary>Gets mutable spring slots; unreturned/inapplicable native slots project to null.</summary>
        public Springs Springs { get; private set; }
        /// <summary>Gets the common partial factor when supplied, or the legacy zero placeholder when absent.</summary>
        /// <remarks>Use NativeSpecification.CommonPartialMomentFactor to distinguish unreturned data from a native zero.</remarks>
        public double MpFactor { get; private set; }
        /// <summary>Gets mutable directional factors; zero is a compatibility placeholder for unavailable/inapplicable outputs.</summary>
        public MpFactors MpFactors { get; private set; }
        /// <summary>Gets lossless typed native evidence until the managed flag/spring/factor projection is edited.</summary>
        /// <remarks>Null for manually constructed packets. Basic observations do not supply separate extended MP outputs.</remarks>
        public MemberReleaseSpecification NativeSpecification { get; private set; }
        /// <summary>Gets whether native partial-mode evidence or nonzero partial factors remain in this packet.</summary>
        /// <remarks>Native mode provenance survives edits to Boolean/spring projections, including zero-valued or unreturned factors. Parse a new basic mode packet or construct a new packet to explicitly replace that provenance. Basic creation refuses these packets.</remarks>
        public bool ContainsPartialMomentModes => hasDirectionalPartialMode || hasCommonPartialMode || MpFactor != 0 || MpFactors.MPx != 0 || MpFactors.MPy != 0 || MpFactors.MPz != 0;
        internal static DegreeOfFreedomRelease[] Dofs(MemberReleaseSpecification value) => new[] { value.TranslationX, value.TranslationY, value.TranslationZ, value.RotationX, value.RotationY, value.RotationZ };
        internal static MemberReleases FromNative(MemberReleaseSpecification value)
        {
            var packet = new MemberReleases(); var dofs = Dofs(value);
            packet.Releases.Parse(dofs.Select(d => (int)d.Mode).ToArray());
            var k = dofs.Select(d => d.Mode == MemberDofReleaseMode.Spring ? d.NativeParameter : null).ToArray();
            packet.Springs.KFx = k[0]; packet.Springs.KFy = k[1]; packet.Springs.KFz = k[2]; packet.Springs.KMx = k[3]; packet.Springs.KMy = k[4]; packet.Springs.KMz = k[5];
            packet.MpFactor = value.CommonPartialMomentFactor ?? 0;
            packet.MpFactors.MPx = value.DirectionalPartialMomentFactors?.MomentX ?? 0;
            packet.MpFactors.MPy = value.DirectionalPartialMomentFactors?.MomentY ?? 0;
            packet.MpFactors.MPz = value.DirectionalPartialMomentFactors?.MomentZ ?? 0;
            packet.hasDirectionalPartialMode = dofs.Any(d => d.Mode == MemberDofReleaseMode.DirectionalPartialMoment);
            packet.hasCommonPartialMode = dofs.Any(d => d.Mode == MemberDofReleaseMode.CommonPartialMoment);
            packet.NativeSpecification = value; return packet;
        }
        /// <summary>Parses six native modes and spring/partial slots without separate extended MP outputs.</summary>
        /// <param name="releaseArrayObj">Exactly six Int32 native modes 0, 1, -1, -2 or -3.</param>
        /// <param name="springArrayObj">Exactly six floating-point slots. Applicable spring/partial values must be finite.</param>
        /// <remarks>Retains native modes independently of Boolean projections; absent extended factors stay unavailable in NativeSpecification.</remarks>
        public void Parse(object releaseArrayObj, object springArrayObj) => ParseCore(releaseArrayObj, springArrayObj, null, null, false);
        /// <summary>Parses six modes, applicable spring values and extended partial-moment outputs.</summary>
        /// <param name="releaseArrayObj">Six Int32 native modes in FX/FY/FZ/MX/MY/MZ order.</param>
        /// <param name="springArrayObj">Six floating-point slots; applicable slots must be finite.</param>
        /// <param name="mpFactorObj">Finite common partial factor when a -3 mode applies; ignored otherwise.</param>
        /// <param name="mpFactorArrayObj">Three directional factors when -2 applies; only selected directions are consumed.</param>
        /// <exception cref="InvalidOperationException">The shape, mode or applicable output violates the native contract.</exception>
        public void Parse(object releaseArrayObj, object springArrayObj, object mpFactorObj, object mpFactorArrayObj) => ParseCore(releaseArrayObj, springArrayObj, mpFactorObj, mpFactorArrayObj, true);
        private void ParseCore(object modesObject, object valuesObject, object common, object directional, bool extended)
        {
            var modes = IntegerArray(modesObject, 6, "MemberReleases.Parse"); var values = FloatingArray(valuesObject, 6, "MemberReleases.Parse", false);
            if (modes.Any(m => !Enum.IsDefined(typeof(MemberDofReleaseMode), m)) || modes.Take(3).Any(m => m < -1)) throw new InvalidOperationException("Unknown or invalid native release mode.");
            var dofs = modes.Select((mode, i) => new DegreeOfFreedomRelease((MemberDofReleaseMode)mode,
                mode == -1 || mode == -2 && (!extended || !double.IsNaN(values[i])) ? (double?)Number(values[i], "MemberReleases.Parse") : null)).ToArray();
            double? commonValue = extended && modes.Contains(-3) ? (double?)Number(common, "MemberReleases.Parse") : null;
            PartialMomentReleaseFactors factors = null;
            if (extended && modes.Contains(-2))
            {
                var a = FloatingArray(directional, 3, "MemberReleases.Parse", false);
                factors = new PartialMomentReleaseFactors(modes[3] == -2 ? (double?)Number(a[0], "MemberReleases.Parse") : null,
                    modes[4] == -2 ? (double?)Number(a[1], "MemberReleases.Parse") : null, modes[5] == -2 ? (double?)Number(a[2], "MemberReleases.Parse") : null);
            }
            var packet = FromNative(new MemberReleaseSpecification(dofs, extended, commonValue, factors));
            Releases.Assign(packet.Releases); Springs.Assign(packet.Springs); MpFactors.Parse(new[] { packet.MpFactors.MPx, packet.MpFactors.MPy, packet.MpFactors.MPz });
            MpFactor = packet.MpFactor; NativeSpecification = packet.NativeSpecification;
            hasDirectionalPartialMode = packet.hasDirectionalPartialMode; hasCommonPartialMode = packet.hasCommonPartialMode;
            OnPropertyChanged(nameof(MpFactor)); OnPropertyChanged(nameof(NativeSpecification)); OnPropertyChanged(nameof(ReleaseType)); OnPropertyChanged(nameof(ContainsPartialMomentModes));
        }
    }
}
