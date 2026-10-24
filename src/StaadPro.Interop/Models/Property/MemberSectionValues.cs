using System;
using System.Collections.Generic;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains the validated applicable fields from a member's 24-slot native section-value buffer.</summary>
    /// <remarks>The member ID is not a property ID. Field names and order follow the acquired native STAAD
    /// 24/25/26 layout; inertia labels are not reinterpreted. Unused padding is excluded rather than fabricated.
    /// No catalog identity, country code, area fallback, unit conversion or geometry hydration is implied.</remarks>
    public sealed class MemberSectionValues
    {
        internal MemberSectionValues(int memberId, int nativeMajor, int type, string[] fields, double[] values)
        {
            MemberId = memberId; NativeMajorVersion = nativeMajor; NativePropertyType = type;
            FieldNames = Array.AsReadOnly((string[])fields.Clone());
            var copy = new double[fields.Length]; Array.Copy(values, copy, copy.Length); Values = Array.AsReadOnly(copy);
        }
        /// <summary>Gets the requested positive analytical member ID.</summary>
        public int MemberId { get; }
        /// <summary>Gets the native STAAD major version used to select the reviewed layout; never the host application's version.</summary>
        public int NativeMajorVersion { get; }
        /// <summary>Gets the native section type code returned by the member query.</summary>
        public int NativePropertyType { get; }
        /// <summary>Gets an owned read-only sequence of native field labels, in slot order.</summary>
        public IReadOnlyList<string> FieldNames { get; }
        /// <summary>Gets only the applicable finite native values, in matching FieldNames order and native units.</summary>
        public IReadOnlyList<double> Values { get; }
        /// <summary>Gets the documented native buffer capacity; unused slots are not exposed as engineering data.</summary>
        public int NativeBufferCapacity => 24;
    }
}
