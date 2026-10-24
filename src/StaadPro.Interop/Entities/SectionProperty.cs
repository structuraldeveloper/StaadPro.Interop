using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Common;
using StaadPro.Interop.Models.Sections;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries a native section ID, an explicit creation definition and candidate member references.</summary>
    /// <remarks>Creation/assignment updates Id but does not save. Equality compares the selected profile identity, modifier, additional specification type and values, excluding Id/Beams. Do not mutate definition values while this packet belongs to a HashSet. Private catalog selection/UI state is omitted.</remarks>
    public sealed class SectionProperty : ValidatablePropertyStore, IEntity, IEquatable<SectionProperty>
    {
        /// <summary>Initializes an empty candidate set and no creation definition.</summary>
        public SectionProperty() { Beams = new HashSet<Beam>(); }
        /// <summary>Gets or sets the native section definition ID; creation stores only a positive result.</summary>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets mutable member references used for explicit assignment.</summary>
        public HashSet<Beam> Beams { get => Get<HashSet<Beam>>(); private set => Set(value); }
        /// <summary>Gets or sets the creation graph; assignment by an already resolved Id does not require it.</summary>
        public SectionDefinition SectionDefinition { get => Get<SectionDefinition>(); set => Set(value); }
        /// <summary>Compares profile identity and additional creation values.</summary><param name="other">Other packet.</param><returns>True for equal complete definitions or the same reference.</returns>
        public bool Equals(SectionProperty other)
        {
            if (ReferenceEquals(this, other)) return true;
            var a = SectionDefinition; var b = other?.SectionDefinition; var x = a?.SectionProfile?.SelectedSection; var y = b?.SectionProfile?.SelectedSection;
            return x != null && y != null && x.GetType() == y.GetType() && x.Country == y.Country && x.StaadName == y.StaadName && x.Designation == y.Designation &&
                a.TypeSpecification == b.TypeSpecification && a.AdditionalSpecifications?.GetType() == b.AdditionalSpecifications?.GetType() &&
                (a.AdditionalSpecifications?.ToArray() ?? new double[0]).SequenceEqual(b.AdditionalSpecifications?.ToArray() ?? new double[0]);
        }
        /// <summary>Compares creation definitions.</summary><param name="obj">Other packet.</param><returns>Whether complete definitions match.</returns>
        public override bool Equals(object obj) => obj is SectionProperty other && Equals(other);
        /// <summary>Hashes the same definition values used by equality.</summary><returns>A value hash excluding native ID and candidates.</returns>
        public override int GetHashCode()
        {
            var d = SectionDefinition; var s = d?.SectionProfile?.SelectedSection; if (s == null) return 0;
            unchecked
            {
                int h = s.GetType().GetHashCode(); h = h * 31 + (int)s.Country; h = h * 31 + (s.StaadName?.GetHashCode() ?? 0); h = h * 31 + (s.Designation?.GetHashCode() ?? 0);
                h = h * 31 + (int)d.TypeSpecification; h = h * 31 + (d.AdditionalSpecifications?.GetType().GetHashCode() ?? 0);
                foreach (double value in d.AdditionalSpecifications?.ToArray() ?? new double[0]) h = h * 31 + value.GetHashCode(); return h;
            }
        }
        /// <summary>Compares two definitions, accepting two null references.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether definitions match.</returns>
        public static bool operator ==(SectionProperty a, SectionProperty b) => ReferenceEquals(a, b) || !(a is null) && a.Equals(b);
        /// <summary>Compares definitions for inequality.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether definitions differ.</returns>
        public static bool operator !=(SectionProperty a, SectionProperty b) => !(a == b);
    }
}
