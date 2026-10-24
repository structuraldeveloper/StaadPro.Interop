using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains an exact immutable native attribute name/value pair without normalization.</summary>
    public sealed class PropertyAttribute
    {
        /// <summary>Copies explicit native attribute text, including empty strings and whitespace.</summary>
        /// <param name="name">Non-null exact attribute name without NUL characters; no trimming or case folding.</param>
        /// <param name="value">Non-null exact string value without NUL characters; empty/whitespace is forwarded unchanged.</param>
        /// <exception cref="ArgumentNullException">Either text input is null.</exception>
        /// <exception cref="ArgumentException">Either text input contains NUL.</exception>
        public PropertyAttribute(string name, string value)
        { Text(name, nameof(name)); Text(value, nameof(value)); Name = name; Value = value; }
        internal static void Text(string value, string name)
        { if (value == null) throw new ArgumentNullException(name); if (value.IndexOf('\0') >= 0) throw new ArgumentException("Attribute text must not contain NUL.", name); }
        /// <summary>Gets the exact native attribute name, including empty/whitespace text.</summary>
        public string Name { get; }
        /// <summary>Gets the exact native string value, including empty/whitespace text.</summary>
        public string Value { get; }
    }
    /// <summary>Contains a qualified native entity count for one exact attribute pair and domain.</summary>
    public sealed class PropertyAttributeMatchCount
    {
        internal PropertyAttributeMatchCount(PropertyAttribute attribute, PropertyAttributeScope scope, int count)
        { Attribute = attribute; Scope = scope; Count = count; }
        /// <summary>Gets the immutable exact requested pair; by-reference native input rewriting is not accepted as this identity.</summary>
        public PropertyAttribute Attribute { get; }
        /// <summary>Gets the explicitly requested native member or element domain.</summary>
        public PropertyAttributeScope Scope { get; }
        /// <summary>Gets the nonnegative count, including zero; zero does not prove the attribute definition exists.</summary>
        public int Count { get; }
    }
    /// <summary>Contains a bounded owned entity-ID snapshot for an exact native attribute pair.</summary>
    public sealed class PropertyAttributeEntityIds
    {
        internal PropertyAttributeEntityIds(PropertyAttribute attribute, PropertyAttributeScope scope, int[] ids)
        { Attribute = attribute; Scope = scope; EntityIds = Array.AsReadOnly((int[])ids.Clone()); }
        /// <summary>Gets the exact immutable pair used for count/list/count observations.</summary>
        public PropertyAttribute Attribute { get; }
        /// <summary>Gets the explicit native member or element domain without geometry hydration.</summary>
        public PropertyAttributeScope Scope { get; }
        /// <summary>Gets owned read-only positive IDs in native order, retaining duplicates. Equal counts cannot exclude same-count external edits.</summary>
        public IReadOnlyList<int> EntityIds { get; }
    }
    /// <summary>Contains the count for a requested integer ID resolved by native member-first/plate-fallback rules.</summary>
    public sealed class AssignedAttributeSummary
    {
        internal AssignedAttributeSummary(int memberOrPlateId, int count) { MemberOrPlateId = memberOrPlateId; Count = count; }
        /// <summary>Gets the requested positive integer identity; native uses a member if it exists and a plate otherwise. The resolved kind is not inferred.</summary>
        public int MemberOrPlateId { get; }
        /// <summary>Gets the nonnegative native assignment count. No assigned-index context or index base is certified.</summary>
        public int Count { get; }
    }
}
