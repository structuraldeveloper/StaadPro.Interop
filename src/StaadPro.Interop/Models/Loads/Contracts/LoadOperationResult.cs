using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Loads;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Contains operation-specific status and native evidence for a proposed Load mutation.</summary>
    /// <remarks>Contract prepared for future implementations. No new mutation returns this type yet.
    /// Success requires the particular operation's documented contract; void invocation evidence alone
    /// is InvocationCompleted. Transport exceptions propagate; combined body/cleanup failures must be preserved.</remarks>
    public class LoadOperationResult
    {
        internal LoadOperationResult(LoadOperationStatus status, IEnumerable<LoadOperationStep> steps,
            bool mayHaveChangedModel, string diagnostic = null)
        {
            Status = status; Steps = Array.AsReadOnly((steps ?? throw new ArgumentNullException(nameof(steps))).ToArray());
            MayHaveChangedModel = mayHaveChangedModel; Diagnostic = diagnostic;
        }
        /// <summary>Gets the qualified completion status.</summary>
        public LoadOperationStatus Status { get; }
        /// <summary>Gets whether documented success was established; InvocationCompleted is not success.</summary>
        public bool IsSuccessful => Status == LoadOperationStatus.Success;
        /// <summary>Gets ordered, materialized evidence including cleanup calls, without exposing mutable arrays.</summary>
        public IEnumerable<LoadOperationStep> Steps { get; }
        /// <summary>Gets whether invocation may have affected model state, including when it later failed.</summary>
        public bool MayHaveChangedModel { get; }
        /// <summary>Gets an explanation of failure, unsupported choices or partial completion.</summary>
        public string Diagnostic { get; }
    }

    /// <summary>Contains qualified mutation status and successful returned data.</summary>
    /// <typeparam name="T">The named immutable output data model.</typeparam>
    public sealed class LoadOperationResult<T> : LoadOperationResult
    {
        private readonly T data;
        internal LoadOperationResult(LoadOperationStatus status, T data, IEnumerable<LoadOperationStep> steps,
            bool mayHaveChangedModel, string diagnostic = null) : base(status, steps, mayHaveChangedModel, diagnostic) { this.data = data; }
        /// <summary>Gets validated output data only when IsSuccessful is true.</summary>
        /// <exception cref="InvalidOperationException">The operation did not establish success.</exception>
        public T Data => IsSuccessful ? data : throw new InvalidOperationException("No successful Load operation data is available: " + Status);
    }

    /// <summary>Records one native operation's actual response without applying a universal status conversion.</summary>
    public sealed class LoadOperationStep
    {
        internal LoadOperationStep(string nativeFunction, int? nativeReturnCode, bool? nativeBooleanReturn, LoadOperationStatus status)
        { NativeFunction = nativeFunction; NativeReturnCode = nativeReturnCode; NativeBooleanReturn = nativeBooleanReturn; Status = status; }
        /// <summary>Gets the invoked native member.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets its actual integral return, or null for a Boolean/void response.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets its actual Boolean response, or null for an integral/void response.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets the operation-specific interpreted outcome.</summary>
        public LoadOperationStatus Status { get; }
    }
}
