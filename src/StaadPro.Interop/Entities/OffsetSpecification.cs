using System;
using System.Collections.Generic;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries a member-end offset definition, native ID and assignment candidates.</summary>
    /// <remarks>Components retain native length units in Axis. Do not mutate Location, Axis or components while this value is in a hash-based collection: equality excludes Id and Beams and compares those five definition values. The private units-library helper is omitted.</remarks>
    public class OffsetSpecification : ValidatablePropertyStore
    {
        /// <summary>Initializes an empty member assignment set.</summary>
        public OffsetSpecification() { Beams = new HashSet<Beam>(); }
        /// <summary>Gets or sets the native specification ID; queries of a member's components do not resolve it.</summary>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets or sets the start/end selector, Start=0 or End=1.</summary>
        public OffsetLocation Location { get => Get<OffsetLocation>(); set => Set(value); }
        /// <summary>Gets or sets the component frame, Global=0 or Local=1.</summary>
        public OffsetAxis Axis { get => Get<OffsetAxis>(); set => Set(value); }
        /// <summary>Gets or sets the signed X component in native length units.</summary>
        public double X { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the signed Y component in native length units.</summary>
        public double Y { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the signed Z component in native length units.</summary>
        public double Z { get => Get<double>(); set => Set(value); }
        /// <summary>Gets the mutable candidate member references for explicit assignment.</summary>
        public HashSet<Beam> Beams { get => Get<HashSet<Beam>>(); private set => Set(value); }
        /// <summary>Gets whether every component has absolute magnitude below 1e-9 native length units.</summary>
        public bool IsZero => Math.Abs(X) < 1e-9 && Math.Abs(Y) < 1e-9 && Math.Abs(Z) < 1e-9;
        /// <summary>Provides the legacy mutable global-start zero definition.</summary>
        /// <remarks>Do not modify this shared instance. Queries return independent packets, including for zero offsets, so their requested end/frame remain available.</remarks>
        public static readonly OffsetSpecification None = new OffsetSpecification { Axis = OffsetAxis.Global };
        /// <summary>Creates a global-Y start offset packet without creating native state.</summary>
        /// <param name="offset">Signed native length.</param><returns>A new unnumbered packet.</returns>
        public static OffsetSpecification GlobalYStart(double offset) => new OffsetSpecification { Y = offset, Axis = OffsetAxis.Global, Location = OffsetLocation.Start };
        /// <summary>Creates a global-Y end offset packet without creating native state.</summary>
        /// <param name="offset">Signed native length.</param><returns>A new unnumbered packet.</returns>
        public static OffsetSpecification GlobalYEnd(double offset) => new OffsetSpecification { Y = offset, Axis = OffsetAxis.Global, Location = OffsetLocation.End };
        /// <summary>Creates a local-Y start offset packet without creating native state.</summary>
        /// <param name="offset">Signed native length.</param><returns>A new unnumbered packet.</returns>
        public static OffsetSpecification LocalYStart(double offset) => new OffsetSpecification { Y = offset, Axis = OffsetAxis.Local, Location = OffsetLocation.Start };
        /// <summary>Creates a local-Y end offset packet without creating native state.</summary>
        /// <param name="offset">Signed native length.</param><returns>A new unnumbered packet.</returns>
        public static OffsetSpecification LocalYEnd(double offset) => new OffsetSpecification { Y = offset, Axis = OffsetAxis.Local, Location = OffsetLocation.End };
        /// <summary>Hashes the definition values used by equality.</summary><returns>A hash excluding native ID and assignments.</returns>
        public override int GetHashCode() => $"{Location}{Axis}{X}{Y}{Z}".GetHashCode();
        /// <summary>Compares exact location, frame and component values.</summary><param name="obj">Other packet.</param><returns>True for equal definition values.</returns>
        public override bool Equals(object obj) => obj is OffsetSpecification other && this == other;
        /// <summary>Compares exact definition values, treating two null references as equal.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether values match.</returns>
        public static bool operator ==(OffsetSpecification a, OffsetSpecification b) => ReferenceEquals(a, b) || !(a is null) && !(b is null) && a.Location == b.Location && a.Axis == b.Axis && a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        /// <summary>Compares definition values for inequality.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether values differ.</returns>
        public static bool operator !=(OffsetSpecification a, OffsetSpecification b) => !(a == b);
    }
}
