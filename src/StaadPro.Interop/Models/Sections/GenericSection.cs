using System;
using System.Collections.Generic;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Models.Sections
{
    /// <summary>Owns a version-decoded native section packet without importing the private shape/catalog hierarchy.</summary>
    /// <remarks>Identity/value observations are fixed. Id and AssignedBeams are mutable compatibility fields. No catalog substitution, inertia-axis relabeling, engineering defaults or native writes occur in this model.</remarks>
    public sealed class GenericSection : IGenericSection
    {
        private readonly double[] values;
        private readonly string[] fields;
        private readonly double? area;
        internal GenericSection(int id, int type, int country, int record, string name, int major, double[] values, string[] fields, double? area)
        { Id = id; SectionPropertyType = (SectionPropertyType)type; Country = (Country)country; SectionRecNo = record; StaadName = PropertyName = name; NativeMajorVersion = major; this.values = values; this.fields = fields; this.area = area; AssignedBeams = new HashSet<Beam>(); }
        /// <summary>Gets or sets the native definition ID; assignment requires a positive existing ID.</summary>
        public int Id { get; set; }
        /// <summary>Gets the native database record, including zero, or -1 when unavailable/not applicable.</summary>
        public int SectionRecNo { get; }
        /// <summary>Gets the exact native section designation.</summary>
        public string StaadName { get; }
        /// <summary>Gets the native property label without catalog-generated display text.</summary>
        public string PropertyName { get; }
        /// <summary>Gets the native country code cast to the preserved enum; zero may mean a non-table definition.</summary>
        public Country Country { get; }
        /// <summary>Gets the native section type selecting the buffer layout.</summary>
        public SectionPropertyType SectionPropertyType { get; }
        /// <summary>Gets the acquired STAAD major version used for layout selection.</summary>
        public int NativeMajorVersion { get; }
        /// <summary>Gets whether Ax is observed or can be obtained from the source tapered-I plate-area formula.</summary>
        public bool HasSectionalArea => area.HasValue;
        /// <summary>Gets observed Ax or the mean start/end tapered-I area in native length-squared units.</summary>
        /// <exception cref="NotSupportedException">BeamD and UPT ISECTION omit Ax; no catalog or approximate replacement is available.</exception>
        public double Ax => area ?? throw new NotSupportedException("This native layout does not provide Ax. GetSectionalArea returns null for this type.");
        /// <summary>Gets an owned native-capacity buffer copy, with unused unwritten padding replaced by zero.</summary>
        public double[] PropertyValues => (double[])values.Clone();
        /// <summary>Gets an owned copy of the applicable native field names in their version-specific order.</summary>
        /// <remarks>These are native labels, including Ix/Iy/Iz; they are not a reinterpreted physical-axis mapping.</remarks>
        public string[] PropertyValueNames => (string[])fields.Clone();
        /// <summary>Gets the mutable candidate member set; editing it does not assign native properties.</summary>
        public HashSet<Beam> AssignedBeams { get; }
    }
}
