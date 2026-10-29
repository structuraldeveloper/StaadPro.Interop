using System;
using StaadPro.Interop.Enums.Design;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Design
{
    /// <summary>Records brief-creation evidence independently of zero-success assignment codes.</summary>
    public sealed class DesignBriefResult
    {
        internal DesignBriefResult(StaadDesignCode code, int raw)
        {
            RequestedCode = code;
            NativeReturnCode = raw;
            Status = raw == -1 ? DesignOperationStatus.NativeFailure : DesignOperationStatus.Success;
            DesignBriefId = raw >= 0 ? (int? )raw : null;
        }

        /// <summary>Gets the requested design code; native acceptance is authoritative.</summary>
        public StaadDesignCode RequestedCode { get; }
        /// <summary>Gets the actual native reference ID or documented -1 failure.</summary>
        public int NativeReturnCode { get; }
        /// <summary>Gets the operation-specific outcome.</summary>
        public DesignOperationStatus Status { get; }
        /// <summary>Gets whether native creation returned a nonnegative reference.</summary>
        public bool IsSuccessful => Status == DesignOperationStatus.Success;
        /// <summary>Gets the created reference, including zero if returned; null after -1. Creation does not use zero as a failure sentinel.</summary>
        public int? DesignBriefId { get; }
    }

    /// <summary>Records one assignment acknowledgement and its owned requested identities.</summary>
    public sealed class DesignMutationResult
    {
        internal DesignMutationResult(string nativeFunction, int? brief, string name, IEnumerable<int> ids, int? raw, int? inputIndex = null)
        {
            NativeFunction = nativeFunction;
            DesignBriefId = brief;
            Name = name;
            MemberIds = Array.AsReadOnly(ids.ToArray());
            NativeReturnCode = raw;
            InputIndex = inputIndex;
            Status = raw == null ? DesignOperationStatus.NotAttempted : raw == 0 ? DesignOperationStatus.Success : DesignOperationStatus.NativeFailure;
        }

        /// <summary>Gets the exact intended native assignment member.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the requested/created brief reference, or null when creation failed.</summary>
        public int? DesignBriefId { get; }
        /// <summary>Gets the parameter keyword or native command string.</summary>
        public string Name { get; }
        /// <summary>Gets owned requested member IDs; acknowledgement does not certify per-member effects.</summary>
        public IReadOnlyList<int> MemberIds { get; }
        /// <summary>Gets actual native 0/-1 evidence, or null when this item was not invoked.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the input sequence index for a batch, before ordering; null for a single assignment.</summary>
        public int? InputIndex { get; }
        /// <summary>Gets success, failure or explicitly unattempted state.</summary>
        public DesignOperationStatus Status { get; }
        /// <summary>Gets true only for native zero-success acknowledgement.</summary>
        public bool IsSuccessful => Status == DesignOperationStatus.Success;
    }

    /// <summary>Retains a nontransactional ordered assignment report, including unattempted items.</summary>
    /// <remarks>Stops after the first documented native failure. Transport/malformed-response exceptions propagate and can follow earlier native changes; no retry, save, rollback or synthetic completion receipt is produced for them.</remarks>
    public sealed class DesignBatchResult
    {
        internal DesignBatchResult(int? brief, DesignBriefResult creation, IEnumerable<DesignMutationResult> items)
        {
            DesignBriefId = brief;
            Creation = creation;
            Items = Array.AsReadOnly(items.ToArray());
        }

        /// <summary>Gets the requested or successfully created brief reference.</summary>
        public int? DesignBriefId { get; }
        /// <summary>Gets creation evidence only for the create-and-assign overload; null when no creation was attempted.</summary>
        public DesignBriefResult Creation { get; }
        /// <summary>Gets items in execution priority order, retaining each original InputIndex and all unattempted items.</summary>
        public IReadOnlyList<DesignMutationResult> Items { get; }
        /// <summary>Gets whether the validated input was empty and no native operation was attempted.</summary>
        public bool IsNoOperation => Items.Count == 0;
        /// <summary>Gets true only for a nonempty batch whose creation, if any, and every assignment succeeded.</summary>
        public bool IsSuccessful => Items.Count > 0 && (Creation == null || Creation.IsSuccessful) && Items.All(i => i.IsSuccessful);
        /// <summary>Gets the number of acknowledged successful items.</summary>
        public int SucceededCount => Items.Count(i => i.IsSuccessful);
        /// <summary>Gets the number of actual native calls that reported -1.</summary>
        public int FailedCount => Items.Count(i => i.Status == DesignOperationStatus.NativeFailure);
        /// <summary>Gets the number of explicitly unattempted items.</summary>
        public int NotAttemptedCount => Items.Count(i => i.Status == DesignOperationStatus.NotAttempted);
    }

    /// <summary>Contains an identified, owned Design observation without exposing native reference outputs.</summary>
    /// <typeparam name = "T">The named immutable payload.</typeparam>
    public sealed class DesignQueryResult<T>
    {
        private readonly T data;
        internal DesignQueryResult(string function, int brief, int? member, int raw, DesignOperationStatus status, T data = default)
        {
            NativeFunction = function;
            DesignBriefId = brief;
            MemberId = member;
            NativeReturnCode = raw;
            Status = status;
            this.data = data;
        }

        /// <summary>Gets the decisive native query name.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the requested brief reference, including zero.</summary>
        public int DesignBriefId { get; }
        /// <summary>Gets the requested member ID, or null for a brief-level observation.</summary>
        public int? MemberId { get; }
        /// <summary>Gets the original integral status/code; zero has operation-specific meaning.</summary>
        public int NativeReturnCode { get; }
        /// <summary>Gets the qualified outcome; absence is distinct from native execution failure.</summary>
        public DesignOperationStatus Status { get; }
        /// <summary>Gets true only when output validation passed.</summary>
        public bool IsSuccessful => Status == DesignOperationStatus.Success;
        /// <summary>Gets the validated owned payload after success, including a successful empty parameter list.</summary>
        /// <exception cref = "InvalidOperationException">No successful payload exists; inspect Status.</exception>
        public T Data => IsSuccessful ? data : throw new InvalidOperationException("No qualified Design data is available: " + Status);
    }

    /// <summary>Preserves the actual design code without silently coercing an unknown value into a known enum.</summary>
    public sealed class DesignBriefCodeObservation
    {
        internal DesignBriefCodeObservation(int raw)
        {
            NativeCode = raw;
            KnownCode = Enum.IsDefined(typeof(StaadDesignCode), raw) ? (StaadDesignCode? )raw : null;
        }

        /// <summary>Gets the positive native code, including values absent from the source enum.</summary>
        public int NativeCode { get; }
        /// <summary>Gets a recognized code, or null for an unlisted positive code. Such a code remains valid observed data.</summary>
        public StaadDesignCode? KnownCode { get; }
    }

    /// <summary>Owns one source-compatible member-parameter row copied from the temporary native holder.</summary>
    public sealed class MemberDesignParameterInfo
    {
        internal MemberDesignParameterInfo(int index, string name, string value, string unit, string description, int defaultFlag)
        {
            Index = index;
            Name = name;
            Value = value;
            Unit = unit;
            Description = description;
            NativeDefaultFlag = defaultFlag;
        }

        /// <summary>Gets the zero-based holder row index, not a parameter selector.</summary>
        public int Index { get; }
        /// <summary>Gets the native nonblank parameter name.</summary>
        public string Name { get; }
        /// <summary>Gets the exact native textual value; it is not parsed or converted.</summary>
        public string Value { get; }
        /// <summary>Gets the exact unit label, which may be empty.</summary>
        public string Unit { get; }
        /// <summary>Gets the native description, which may be empty.</summary>
        public string Description { get; }
        /// <summary>Gets the actual integer flag, retained without assuming 1 is the only native true value.</summary>
        public int NativeDefaultFlag { get; }
        /// <summary>Gets whether the native default flag is nonzero, preserving main-source semantics.</summary>
        public bool IsDefault => NativeDefaultFlag != 0;
    }

    /// <summary>Owns all validated rows for the requested brief/member after native success.</summary>
    public sealed class MemberDesignParametersResult
    {
        internal MemberDesignParametersResult(int brief, int member, IEnumerable<MemberDesignParameterInfo> rows)
        {
            DesignBriefId = brief;
            MemberId = member;
            Parameters = Array.AsReadOnly(rows.ToArray());
        }

        /// <summary>Gets the requested design brief reference.</summary>
        public int DesignBriefId { get; }
        /// <summary>Gets the requested positive analytical member ID.</summary>
        public int MemberId { get; }
        /// <summary>Gets owned rows in native zero-based holder order; a successfully populated zero-count holder yields an empty list.</summary>
        public IReadOnlyList<MemberDesignParameterInfo> Parameters { get; }
    }
}
