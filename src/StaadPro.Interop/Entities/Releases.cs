using System;
using System.Linq;
using StaadPro.Interop.Common;
using static StaadPro.Interop.Services.Property.PropertyValueReader;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries six Boolean full-release flags in native FX/FY/FZ/MX/MY/MZ order.</summary>
    /// <remarks>Boolean flags cannot encode native -1/-2/-3 modes. A populated MemberReleases.NativeSpecification retains those modes separately. This type performs no native calls.</remarks>
    public sealed class Releases : ValidatablePropertyStore
    {
        /// <summary>Initializes six false release flags.</summary>
        public Releases() { }
        /// <summary>Copies Boolean flags from another packet, or leaves them false for null.</summary>
        /// <param name="other">Source packet; null is accepted.</param>
        public Releases(Releases other) { Assign(other); }
        /// <summary>Gets or sets whether FX is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool Fx { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets or sets whether FY is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool Fy { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets or sets whether FZ is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool Fz { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets or sets whether MX is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool Mx { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets or sets whether MY is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool My { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets or sets whether MZ is fully released. This does not select a spring or a partial-moment mode.</summary>
        public bool Mz { get => Get<bool>(); set => Set(value); }
        /// <summary>Gets whether any full-release flag is true.</summary>
        public bool IsReleased => Fx || Fy || Fz || Mx || My || Mz;
        /// <summary>Gets the FX full-release flag.</summary>
        public bool TranslationalX => Fx;
        /// <summary>Gets the FY full-release flag.</summary>
        public bool TranslationalY => Fy;
        /// <summary>Gets the FZ full-release flag.</summary>
        public bool TranslationalZ => Fz;
        /// <summary>Gets the MX full-release flag.</summary>
        public bool RotationalX => Mx;
        /// <summary>Gets the MY full-release flag.</summary>
        public bool RotationalY => My;
        /// <summary>Gets the MZ full-release flag.</summary>
        public bool RotationalZ => Mz;
        /// <summary>Provides the legacy mutable FullyReleased flag packet; do not modify this shared instance.</summary>
        public static readonly Releases FullyReleased = new Releases { Fx = true, Fy = true, Fz = true, Mx = true, My = true, Mz = true };
        /// <summary>Provides the legacy mutable FullyFixed flag packet; do not modify this shared instance.</summary>
        public static readonly Releases FullyFixed = new Releases {  };
        /// <summary>Provides the legacy mutable None flag packet; do not modify this shared instance.</summary>
        public static readonly Releases None = new Releases {  };
        /// <summary>Provides the legacy mutable OfPinnedSupport flag packet; do not modify this shared instance.</summary>
        public static readonly Releases OfPinnedSupport = new Releases { Mx = true, My = true, Mz = true };
        /// <summary>Projects six native release codes into Boolean full-release flags.</summary>
        /// <param name="releasesArrayObj">Exactly six native Int32 modes. Only code 1 becomes true; other documented modes remain represented by MemberReleases.NativeSpecification.</param>
        public void Parse(object releasesArrayObj)
        {
            int[] values = IntegerArray(releasesArrayObj, 6, "Releases.Parse");
            Fx = values[0] == 1;
            Fy = values[1] == 1;
            Fz = values[2] == 1;
            Mx = values[3] == 1;
            My = values[4] == 1;
            Mz = values[5] == 1;
        }
        /// <summary>Copies six full-release flags into native 0/1 order.</summary>
        /// <returns>A new six-entry Int32 array; no spring/MP codes are encoded.</returns>
        public int[] ToIntArray() => new[] { Fx ? 1 : 0, Fy ? 1 : 0, Fz ? 1 : 0, Mx ? 1 : 0, My ? 1 : 0, Mz ? 1 : 0 };
        /// <summary>Copies six Boolean flags as floating-point 0/1 values.</summary>
        /// <returns>A new six-entry array in FX/FY/FZ/MX/MY/MZ order.</returns>
        public double[] ToDoubleArray() => ToIntArray().Select(v => (double)v).ToArray();
        /// <summary>Copies flags from another packet without changing native state.</summary>
        /// <param name="from">Source packet; null is a no-op.</param>
        public void Assign(Releases from) { if (from == null) return; Fx = from.Fx; Fy = from.Fy; Fz = from.Fz; Mx = from.Mx; My = from.My; Mz = from.Mz; }
        /// <summary>Copies flags from another packet.</summary>
        /// <param name="source">Source packet; null is a no-op.</param>
        public void MapFrom(Releases source) => Assign(source);
        /// <summary>Tests whether a full-release flag is active.</summary>
        /// <returns>True when any Boolean flag is true.</returns>
        public bool HasAnyActive() => IsReleased;
        /// <summary>Tests whether all Boolean flags are false.</summary>
        /// <returns>True when no full-release flag is selected; this does not describe springs or partial moments.</returns>
        public bool IsFullyFixed() => !IsReleased;
        /// <summary>Selects MZ when no Boolean flag is active; preserves the source convenience behavior.</summary>
        public void SetAtLeastOneActive() { if (!IsReleased) Mz = true; }
        /// <summary>Selects MY/MZ releases and clears FX/FY/FZ/MX; changes only this managed packet.</summary>
        public void SetMemberEndReleases() { Fx = Fy = Fz = Mx = false; My = Mz = true; }
        /// <summary>Hashes the six Boolean values.</summary>
        /// <returns>A value hash excluding native IDs.</returns>
        public override int GetHashCode() => string.Join(",", ToIntArray()).GetHashCode();
        /// <summary>Compares the six Boolean values.</summary>
        /// <param name="obj">Other flag packet.</param>
        /// <returns>True when all flags match.</returns>
        public override bool Equals(object obj) => obj is Releases other && ToIntArray().SequenceEqual(other.ToIntArray());
    }
}
