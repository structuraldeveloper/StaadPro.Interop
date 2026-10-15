namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains an opaque numeric value reported as a process handle by OpenSTAAD.</summary>
    /// <remarks>This immutable snapshot owns no operating-system resource. The server's handle context,
    /// ownership and actual pointer width are unspecified. Do not pass this value to Windows handle APIs,
    /// close it, equate it to a process ID or infer connection liveness from it.</remarks>
    public sealed class NativeProcessHandleValue
    {
        internal NativeProcessHandleValue(ulong rawValue) { RawValue = rawValue; }

        /// <summary>Gets the native numeric value represented as an unsigned 64-bit bit pattern.</summary>
        /// <value>Unsigned native values are preserved; signed negative values are sign-extended to
        /// 64 bits. Zero and all-one values are retained without interpreting validity or pseudo-handle
        /// semantics. This representation does not establish the native server's pointer width.</value>
        public ulong RawValue { get; }
    }
}
