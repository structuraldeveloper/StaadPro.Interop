using System;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Contains a qualified Support observation and owned managed payload.</summary>
    /// <typeparam name="T">The named result or owned read-only collection type.</typeparam>
    /// <remarks>Native counts, IDs and type codes are data, not Boolean flags. No payload is exposed on failure. A successful type observation can be NoSupport or an unknown raw code. Transport/malformed-success errors throw.</remarks>
    public sealed class SupportQueryResult<T>
    {
        private readonly T data;
        internal SupportQueryResult(string function, SupportQueryStatus status, T data = default, int? nodeId = null, int? supportId = null, object raw = null, string diagnostic = null)
        { NativeFunction=function; Status=status; this.data=data; NodeId=nodeId; SupportId=supportId; Diagnostic=diagnostic;
          if(raw is bool flag)NativeBooleanReturn=flag;else if(raw!=null)NativeReturnCode=Services.Interop.NativeValues.ReadInt32(raw,function); }
        /// <summary>Gets the exact native function identifying the observation, including preflight failures.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the qualified operation outcome.</summary>
        public SupportQueryStatus Status { get; }
        /// <summary>Gets whether Data is qualified and readable.</summary>
        public bool IsSuccessful => Status==SupportQueryStatus.Success;
        /// <summary>Gets the requested analytical node ID when applicable, including on failure.</summary>
        public int? NodeId { get; }
        /// <summary>Gets the requested support definition/item ID when applicable; it is not a node ID.</summary>
        public int? SupportId { get; }
        /// <summary>Gets actual integral native evidence; null for Boolean or uninvoked paths.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets actual Boolean native evidence; no integral count or status is converted into it.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets a preflight/state explanation when the requested native retrieval could not be completed coherently.</summary>
        public string Diagnostic { get; }
        /// <summary>Gets the owned payload after successful validation.</summary>
        /// <exception cref="InvalidOperationException">The query did not succeed; inspect Status first.</exception>
        public T Data => IsSuccessful ? data : throw new InvalidOperationException("No qualified Support data: "+Status);
    }
}
