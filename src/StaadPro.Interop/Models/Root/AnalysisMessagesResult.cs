using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains a bounded message-query outcome, native response and immutable message snapshot.</summary>
    /// <remarks>Managed count/allocation failures do not fabricate a native response. A null NativeStatus
    /// means retrieval was never invoked. Status and message data are qualified independently from analysis success.</remarks>
    public sealed class AnalysisMessagesResult
    {
        internal AnalysisMessagesResult(AnalysisMessageQueryOutcome outcome, int? nativeStatus,
            AnalysisStatusResult analysisState, string[] messages, int retrievalAttempts)
        {
            Outcome = outcome; NativeStatus = nativeStatus; AnalysisState = analysisState; RetrievalAttempts = retrievalAttempts;
            Status = nativeStatus.HasValue && Enum.IsDefined(typeof(AnalysisMessageRetrievalStatus), nativeStatus.Value)
                ? (AnalysisMessageRetrievalStatus?)nativeStatus.Value : null;
            Messages = new ReadOnlyCollection<string>((string[])messages.Clone());
        }
        /// <summary>Gets the managed query outcome.</summary>
        /// <value>Success, native failure, unavailable count or allocation-limit failure.</value>
        public AnalysisMessageQueryOutcome Outcome { get; }
        /// <summary>Gets the last native retrieval response.</summary>
        /// <value>The exact native Int32 code, including unknown codes; null if retrieval was not called.</value>
        public int? NativeStatus { get; }
        /// <summary>Gets the recognized native retrieval response.</summary>
        /// <value>A documented response or null when unrecognized or not invoked.</value>
        public AnalysisMessageRetrievalStatus? Status { get; }
        /// <summary>Gets whether retrieval reported successful data or successful absence.</summary>
        /// <value>True only for Retrieved or NoMessages outcomes; never proof of successful analysis.</value>
        public bool IsSuccessful => Outcome == AnalysisMessageQueryOutcome.Retrieved || Outcome == AnalysisMessageQueryOutcome.NoMessages;
        /// <summary>Gets the last analysis-count snapshot used by this query.</summary>
        /// <value>The immutable status result identifying the queried absolute model path and count availability.</value>
        public AnalysisStatusResult AnalysisState { get; }
        /// <summary>Gets the qualified native text messages.</summary>
        /// <value>An independent read-only collection on native success; empty on failure or successful absence.
        /// Order, empty strings, whitespace, Unicode and line breaks are preserved.</value>
        public IReadOnlyList<string> Messages { get; }
        /// <summary>Gets the number of native retrieval invocations.</summary>
        /// <value>Zero, one or two. A second attempt is allowed only after an explicit -108 response and a fresh count query.</value>
        public int RetrievalAttempts { get; }
    }
}
