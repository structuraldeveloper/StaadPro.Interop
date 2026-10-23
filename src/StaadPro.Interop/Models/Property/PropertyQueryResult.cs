using System;
using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains operation-specific Property status, native evidence and validated owned data.</summary>
    /// <typeparam name="T">The documented scalar, value vector, named snapshot or materialized sequence.</typeparam>
    /// <remarks>No model mutation, unit conversion or engineering certification is implied. Absence is
    /// different from a successful zero. Transport errors and malformed responses propagate as exceptions.</remarks>
    public sealed class PropertyQueryResult<T>
    {
        private readonly T data;
        internal PropertyQueryResult(string nativeFunction, PropertyQueryStatus status, T data = default,
            int? nativeReturnCode = null, bool? nativeBooleanReturn = null, string diagnostic = null)
        { NativeFunction = nativeFunction; Status = status; this.data = status == PropertyQueryStatus.Success ? data : default;
          NativeReturnCode = nativeReturnCode; NativeBooleanReturn = nativeBooleanReturn; Diagnostic = diagnostic; }
        /// <summary>Gets the decisive native member, including a sizing/preflight member when that observation prevented retrieval.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the operation-specific outcome, including separate missing entity, unassigned property and absent offset states.</summary>
        public PropertyQueryStatus Status { get; }
        /// <summary>Gets whether Data passed validation; a successful zero remains valid data.</summary>
        public bool IsSuccessful => Status == PropertyQueryStatus.Success;
        /// <summary>Gets the actual integral native response, when present; Boolean and string returns have no fabricated integer code.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the actual native Boolean response, or null when the response was not Boolean.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets a qualification, ambiguity or failure explanation; null when none is needed.</summary>
        public string Diagnostic { get; }
        /// <summary>Gets the validated owned payload only for a successful observation.</summary>
        /// <exception cref="InvalidOperationException">The query did not supply qualified data; inspect Status.</exception>
        public T Data => IsSuccessful ? data : throw new InvalidOperationException("No qualified Property data is available: " + Status);
    }
}
