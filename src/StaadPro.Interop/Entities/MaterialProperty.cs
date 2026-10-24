using System;
using System.Collections.Generic;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries main-project isotropic material constants and managed assignment candidates.</summary>
    /// <remarks>This mutable compatibility type retains the source's assumed creation defaults and computed G/K. Those values are not native observations. Set creation inputs in the intended native unit convention explicitly; use the existing qualified material queries for directly observed G and optional strength data. Private catalog/unit-converter helpers are outside this port. Name determines case-insensitive equality; do not rename a packet while it belongs to a hash-based collection.</remarks>
    public sealed class MaterialProperty : ValidatablePropertyStore, IEntity
    {
        /// <summary>Initializes empty assignment sets and the source's legacy creation defaults.</summary>
        /// <remarks>E=200000, Poisson=0.3, weight density=77.0085, Alpha=16e-6 and Damping=0.02. These are assumed values without an established unit context, not values retrieved from STAAD.</remarks>
        public MaterialProperty() { E = 200000.0; Poisson = .3; Density = 77.0085; Alpha = 16e-6; Damping = .02; Beams = new HashSet<Beam>(); Plates = new HashSet<Plate>(); }
        /// <summary>Initializes a named packet with legacy defaults and empty assignments.</summary>
        /// <param name="name">Native material name. Creation/assignment methods validate it before dispatch.</param>
        public MaterialProperty(string name) : this() { Name = name; }
        /// <summary>Gets or sets the zero-based native list index when read by index; named reads leave it zero.</summary>
        /// <remarks>This is not a creation result. Creation leaves Id unchanged because native 0/1 means Created/Updated.</remarks>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets or sets the exact native material name; assignment and creation preserve spelling and whitespace.</summary>
        public string Name { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets Fy in the packet's force/area units; basic reads leave it zero and basic creation does not send it.</summary>
        public double Fy { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets Fu in the packet's force/area units; basic reads leave it zero and basic creation does not send it.</summary>
        public double Fu { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets dimensionless Ry; basic reads leave it zero and basic creation does not send it.</summary>
        public double Ry { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets dimensionless Rt; basic reads leave it zero and basic creation does not send it.</summary>
        public double Rt { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets Fcu in the packet's force/area units; basic creation does not send it.</summary>
        public double Fcu { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets elastic modulus; its unit convention must agree with the selected creation method.</summary>
        public double E { get => Get<double>(); set { Set(value); OnPropertyChanged(nameof(G)); OnPropertyChanged(nameof(K)); } }
        /// <summary>Gets or sets dimensionless Poisson ratio. Native creation validates -1 &lt; Poisson &lt; 0.5.</summary>
        public double Poisson { get => Get<double>(); set { Set(value); OnPropertyChanged(nameof(G)); OnPropertyChanged(nameof(K)); } }
        /// <summary>Gets or sets weight per volume, not mass density. Values pass unchanged during basic creation.</summary>
        public double Density { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets thermal expansion coefficient per native temperature unit.</summary>
        public double Alpha { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets the dimensionless damping ratio.</summary>
        public double Damping { get => Get<double>(); set => Set(value); }
        /// <summary>Gets or sets managed member assignment candidates; changing this set alone does not modify STAAD.</summary>
        public HashSet<Beam> Beams { get => Get<HashSet<Beam>>(); set => Set(value); }
        /// <summary>Gets or sets managed plate assignment candidates; changing this set alone does not modify STAAD.</summary>
        public HashSet<Plate> Plates { get => Get<HashSet<Plate>>(); set => Set(value); }
        /// <summary>Gets E / (2 * (1 + Poisson)) in E's units; this is derived and is never an observed native G value.</summary>
        public double G => E / (2 * (1 + Poisson));
        /// <summary>Gets E / (3 * (1 - 2 * Poisson)) in E's units; this is derived, not a native bulk-modulus observation.</summary>
        public double K => E / (3 * (1 - 2 * Poisson));
        /// <summary>Gets the latest successful creation/update status, or null before a successful managed creation call.</summary>
        /// <remarks>Retains native 0=Created or 1=Updated independently of Id. Failure leaves both the prior status and Id unchanged.</remarks>
        public MaterialCreationStatus? LastCreationStatus { get; internal set; }
        /// <summary>Compares names using the current culture, ignoring case, as in the source compatibility type.</summary>
        /// <param name="obj">Other packet.</param><returns>True for matching names, including two null names.</returns>
        public override bool Equals(object obj) => obj is MaterialProperty material && Equals(material);
        /// <summary>Compares material names without inspecting constants or assignments.</summary>
        /// <param name="material">Other packet.</param><returns>Whether names match, ignoring case in the current culture.</returns>
        public bool Equals(MaterialProperty material) => !(material is null) && StringComparer.CurrentCultureIgnoreCase.Equals(Name, material.Name);
        /// <summary>Hashes names using the same comparer as equality, correcting the source's case-sensitive hash mismatch.</summary>
        /// <returns>A name hash; no native ID or assignment data participates.</returns>
        public override int GetHashCode() => StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name ?? string.Empty);
        /// <summary>Compares packet names or two null references.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether names match.</returns>
        public static bool operator ==(MaterialProperty a, MaterialProperty b) => ReferenceEquals(a, b) || !(a is null) && a.Equals(b);
        /// <summary>Compares packet names for inequality.</summary><param name="a">First packet.</param><param name="b">Second packet.</param><returns>Whether names differ.</returns>
        public static bool operator !=(MaterialProperty a, MaterialProperty b) => !(a == b);
    }
}
