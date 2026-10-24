using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains a native UPT section count with its requested table identity.</summary>
    public sealed class UptTableSectionCount
    {
        internal UptTableSectionCount(int tableId, int count) { TableId = tableId; Count = count; }
        /// <summary>Gets the requested positive UPT table ID.</summary>
        public int TableId { get; }
        /// <summary>Gets the nonnegative native section count, including zero, without inferring table existence from zero.</summary>
        public int Count { get; }
    }
    /// <summary>Retains the native named-section count without treating its ambiguous return description as buffer capacity.</summary>
    public sealed class UptNamedSectionCount
    {
        internal UptNamedSectionCount(int tableId, string sectionName, int count) { TableId = tableId; SectionName = sectionName; NativeCount = count; }
        /// <summary>Gets the requested positive UPT table ID.</summary>
        public int TableId { get; }
        /// <summary>Gets the exact requested native section name.</summary>
        public string SectionName { get; }
        /// <summary>Gets the nonnegative integral native observation. The help calls it a section count despite the function name; it is never used to allocate a section-value buffer.</summary>
        public int NativeCount { get; }
    }
    /// <summary>Contains a written scalar native UPT section type and the checked table section count.</summary>
    public sealed class UptTableSectionType
    {
        internal UptTableSectionType(int tableId, int type, int count) { TableId = tableId; NativeTypeCode = type; SectionCount = count; }
        /// <summary>Gets the requested positive UPT table ID.</summary>
        public int TableId { get; }
        /// <summary>Gets the positive raw native type code, retaining unknown codes without implying layout support.</summary>
        public int NativeTypeCode { get; }
        /// <summary>Gets the native return count checked against separate before/after table counts.</summary>
        public int SectionCount { get; }
    }
    /// <summary>Contains an explicitly requested native UPT index and its returned table number, without aliasing it to a property reference.</summary>
    public sealed class UptTableReference
    {
        internal UptTableReference(int index, int tableId) { NativeIndex = index; TableId = tableId; }
        /// <summary>Gets the exact nonnegative native table index passed by the caller. No zero/one-based conversion is inferred.</summary>
        public int NativeIndex { get; }
        /// <summary>Gets the positive native UPT table ID returned for that index.</summary>
        public int TableId { get; }
    }
    /// <summary>Contains owned applicable UPT section values and their exact native labels and identity.</summary>
    public sealed class UptSectionProperties
    {
        internal UptSectionProperties(int tableId, string name, int type, Version version, int capacity, string[] fields, double[] values)
        {
            TableId = tableId; SectionName = name; NativeTypeCode = type; NativeContractVersion = version; NativeBufferCapacity = capacity;
            FieldNames = Array.AsReadOnly((string[])fields.Clone()); var copy = new double[fields.Length]; Array.Copy(values, copy, copy.Length); Values = Array.AsReadOnly(copy);
        }
        /// <summary>Gets the requested positive UPT table ID.</summary>
        public int TableId { get; }
        /// <summary>Gets the exact requested section name, without table-list parsing or catalog normalization.</summary>
        public string SectionName { get; }
        /// <summary>Gets the reviewed native UPT layout code 412..502, distinct from common section-property codes.</summary>
        public int NativeTypeCode { get; }
        /// <summary>Gets the immutable exact acquired native version selecting a reviewed layout profile.</summary>
        public Version NativeContractVersion { get; }
        /// <summary>Gets the bounded native capacity returned by GetCountofSectionPropertyValuesEx; never inferred from the ambiguous named-section count.</summary>
        public int NativeBufferCapacity { get; }
        /// <summary>Gets owned read-only native field labels in applicable slot order; unused padding is excluded.</summary>
        public IReadOnlyList<string> FieldNames { get; }
        /// <summary>Gets owned read-only explicitly written finite native values in matching field order and native units, without inertia/axis reinterpretation.</summary>
        public IReadOnlyList<double> Values { get; }
    }
    /// <summary>Contains a raw country-specific native shape code with its exact requested catalog identity.</summary>
    public sealed class SectionShapeCode
    {
        internal SectionShapeCode(Country country, string name, int code) { Country = country; SectionName = name; NativeCode = code; }
        /// <summary>Gets the requested reviewed country selector.</summary>
        public Country Country { get; }
        /// <summary>Gets the exact requested section name.</summary>
        public string SectionName { get; }
        /// <summary>Gets the nonnegative raw native shape code, including unmapped codes and zero. Codes are country-specific and are not a universal section enum.</summary>
        public int NativeCode { get; }
    }
}
