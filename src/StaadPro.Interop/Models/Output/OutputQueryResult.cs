using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Output;

namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains one Output query's status, requested identifiers and owned managed data.</summary>
    /// <typeparam name="T">The documented scalar or named managed payload type.</typeparam>
    /// <remarks>A successful availability observation can contain false. Inspect IsSuccessful before
    /// consuming Data. Transport exceptions and malformed successful responses are thrown, not converted
    /// into ordinary native failure. Values are unconverted native results; this type does not certify
    /// analysis freshness, engineering correctness or native output units.</remarks>
    public sealed class OutputQueryResult<T>
    {
        private readonly T data;
        internal OutputQueryResult(string nativeFunction, OutputQueryStatus status, T data,
            int? entityId = null, int? loadCaseId = null, MemberEnd? memberEnd = null,
            int? nativeReturnCode = null, bool? nativeBooleanReturn = null, string diagnostic = null)
        {
            NativeFunction = nativeFunction;
            Status = status;
            this.data = status == OutputQueryStatus.Success ? data : default;
            EntityId = entityId;
            LoadCaseId = loadCaseId;
            MemberEnd = memberEnd;
            NativeReturnCode = nativeReturnCode; NativeBooleanReturn = nativeBooleanReturn; Diagnostic = diagnostic;
        }

        /// <summary>Gets the native function that produced this observation.</summary>
        /// <value>The exact OpenSTAAD method name, for diagnostics.</value>
        public string NativeFunction { get; }
        /// <summary>Gets the qualified outcome of this query.</summary>
        /// <value>Success or an operation-specific failure, absence, steel-design sentinel, allocation-limit or changed-state outcome.</value>
        public OutputQueryStatus Status { get; }
        /// <summary>Gets whether qualified data is available from this query.</summary>
        /// <value>True only for Success; this is not the Boolean availability payload or a design pass/fail flag.</value>
        public bool IsSuccessful => Status == OutputQueryStatus.Success;
        /// <summary>Gets the requested entity identifier when the function takes one.</summary>
        /// <value>The positive node, analytical/physical member, plate or solid ID, including on native failure; null for model-wide queries.</value>
        public int? EntityId { get; }
        /// <summary>Gets the requested load-case identifier when applicable.</summary>
        /// <value>The positive native case ID, including on native failure; otherwise null.</value>
        public int? LoadCaseId { get; }
        /// <summary>Gets the requested analytical or physical member end for an end-force query.</summary>
        /// <value>Start or End, including on native failure; null for other operations.</value>
        public MemberEnd? MemberEnd { get; }
        /// <summary>Gets the actual integral native response recorded by an expanded query; null for Boolean returns and older query paths.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets an actual Boolean native response recorded by an expanded query; no integer true/count conversion is fabricated.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets a native-contract, preflight or result-state explanation when applicable.</summary>
        public string Diagnostic { get; }
        /// <summary>Gets the owned, validated result data.</summary>
        /// <value>The documented payload, which can be false for a successful availability observation.</value>
        /// <exception cref="InvalidOperationException">The query did not succeed; inspect Status instead.</exception>
        public T Data => IsSuccessful ? data :
            throw new InvalidOperationException("This Output query has no qualified data. Status: " + Status + ".");
    }
}
