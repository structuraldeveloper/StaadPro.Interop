using System;
using StaadPro.Interop.Enums.Loads;

namespace StaadPro.Interop.Models.Loads.Queries
{
    /// <summary>Contains the status, native evidence and owned data of a Load inspection.</summary>
    /// <typeparam name="T">The immutable value or read-only snapshot produced on success.</typeparam>
    /// <remarks>Data is unavailable on failure, including count/activation changes and ambiguous zeros.
    /// Successful false predicates and empty contribution lists are valid data. Transport and malformed
    /// response errors throw rather than becoming fabricated native failure codes.</remarks>
    public sealed class LoadQueryResult<T>
    {
        private readonly T data;
        internal LoadQueryResult(string requested, LoadQueryStatus status, T data, int? code = null,
            string observed = null, string diagnostic = null)
        { RequestedNativeFunction = requested; NativeFunction = observed ?? requested; Status = status;
            this.data = data; NativeReturnCode = code; Diagnostic = diagnostic; }
        /// <summary>Gets the native function associated with the requested managed operation.</summary>
        public string RequestedNativeFunction { get; }
        /// <summary>Gets the operation whose return determined the outcome, which may be a preflight or activation call.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets that operation's actual integral return, or null for an actual Boolean return.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the qualified managed outcome.</summary>
        public LoadQueryStatus Status { get; }
        /// <summary>Gets whether Data is available; a successful Boolean Data can still be false.</summary>
        public bool IsSuccessful => Status == LoadQueryStatus.Success;
        /// <summary>Gets the successful value or read-only materialized snapshot.</summary>
        /// <exception cref="InvalidOperationException">The inspection was not successful.</exception>
        public T Data => IsSuccessful ? data : throw new InvalidOperationException("No successful Load query data is available: " + Status);
        /// <summary>Gets contextual failure/ambiguity information, or null when no explanation is needed.</summary>
        public string Diagnostic { get; }
    }
}
