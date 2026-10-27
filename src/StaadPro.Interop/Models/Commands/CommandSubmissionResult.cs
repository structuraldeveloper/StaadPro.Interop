using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Models.Commands
{
    /// <summary>Distinguishes acknowledged command updates, native failure, void completion and an empty command inventory.</summary>
    public enum CommandSubmissionStatus
    {
        /// <summary>Native 1/true explicitly acknowledged the requested command change.</summary>
        Succeeded,
        /// <summary>Native 0/false reported failure for this command.</summary>
        NativeFailure,
        /// <summary>The documented void call completed; no command-effect acknowledgement exists.</summary>
        InvokedWithoutAcknowledgement,
        /// <summary>DeleteAllAnalysisCommands returned 0/false: no perform-analysis commands were present.</summary>
        NoCommandsPresent
    }

    /// <summary>Records native command-submission evidence without claiming that analysis ran or converged.</summary>
    /// <remarks>These methods configure model instructions. No automatic analysis, read-back, save, retry or rollback occurs. Failures and post-call binding exceptions may follow native changes.</remarks>
    public sealed class CommandSubmissionResult
    {
        internal CommandSubmissionResult(string function, CommandSubmissionStatus status, object raw = null)
        {
            NativeFunction = function;
            Status = status;
            if (raw is bool flag)
                NativeBooleanReturn = flag;
            else if (raw != null)
                NativeReturnCode = NativeValues.ReadInt32(raw, function);
        }

        /// <summary>Gets the exact invoked native member, including the Ex target of a typed convenience.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the explicit acknowledgement or unacknowledged completion state.</summary>
        public CommandSubmissionStatus Status { get; }
        /// <summary>Gets true only for an explicit successful native acknowledgement; void completion is false.</summary>
        public bool IsSuccessful => Status == CommandSubmissionStatus.Succeeded;
        /// <summary>Gets actual integral evidence; null for Boolean or void returns.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets actual Boolean evidence; null for integral or void returns.</summary>
        public bool? NativeBooleanReturn { get; }
    }
}
