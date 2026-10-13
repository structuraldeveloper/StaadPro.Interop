using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StaadPro.Interop.Models
{
    /// <summary>Controls fallback when running applications cannot all be mapped to readable roots.</summary>
    public enum OpenStaadDiscoveryPolicy
    {
        /// <summary>After a discovery grace period, reuse an exposed blank or launch with incomplete-discovery diagnostics.</summary>
        BestEffort,
        /// <summary>Requires complete discovery before blank reuse or launch; an independently verified matching target can still be returned.</summary>
        RequireCompleteDiscovery
    }

    /// <summary>Describes what a successful acquisition actually attached to.</summary>
    public enum OpenStaadAcquisitionOutcome
    {
        /// <summary>No wrapper was acquired; inspect Failure and Diagnostics.</summary>
        Failure,
        /// <summary>Attached to an existing named session without a requested target.</summary>
        ExistingSession,
        /// <summary>Attached to the requested target already open in an existing application.</summary>
        ExistingTarget,
        /// <summary>Attached to a verified unnamed session; the requested target was not opened into it.</summary>
        ExistingBlank,
        /// <summary>Opened the target in the verified newly launched process.</summary>
        OpenedTarget
    }

    /// <summary>Identifies a failed acquisition without concealing native or discovery uncertainty.</summary>
    public enum OpenStaadAcquisitionFailure
    {
        /// <summary>The acquisition succeeded.</summary>
        None,
        /// <summary>No ready running session exists for an attachment-only request.</summary>
        NoRunningSession,
        /// <summary>The logical request budget expired between native calls.</summary>
        Timeout,
        /// <summary>The caller cancelled the request.</summary>
        Cancelled,
        /// <summary>Required process or root discovery failed.</summary>
        DiscoveryFailed,
        /// <summary>Strict policy could not establish complete discovery.</summary>
        IncompleteDiscovery,
        /// <summary>A reachable root's native model identity remained unreadable.</summary>
        IdentityUnavailable,
        /// <summary>Target creation or access failed.</summary>
        FilePreparationFailed,
        /// <summary>The configured executable could not be launched.</summary>
        LaunchFailed,
        /// <summary>The target appeared in a process other than the verified newly launched process.</summary>
        LaunchRedirected,
        /// <summary>The launched process exited before a ready target wrapper was acquired.</summary>
        LaunchedProcessExited
    }

    /// <summary>Configures the independent prefer-existing provider. Values are copied at construction.</summary>
    public sealed class OpenStaadAcquisitionOptions
    {
        /// <summary>Gets or sets the absolute STAAD executable path used only when a target must be launched.</summary>
        /// <value>Null disables launching; attachment to running sessions remains available.</value>
        public string StaadExecutablePath { get; set; }
        /// <summary>Gets or sets the positive logical retry budget for a request.</summary>
        /// <value>Two minutes by default. This cannot interrupt an in-progress synchronous COM call.</value>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(2);
        /// <summary>Gets or sets the positive interval between discovery and readiness retries.</summary>
        /// <value>250 milliseconds by default.</value>
        public TimeSpan PollInterval { get; set; } = TimeSpan.FromMilliseconds(250);
        /// <summary>Gets or sets the initial time allowed for discovery and unusable blank candidates before best-effort fallback.</summary>
        /// <value>One second by default; must be nonnegative and no greater than Timeout.</value>
        public TimeSpan DiscoveryGracePeriod { get; set; } = TimeSpan.FromSeconds(1);
        /// <summary>Gets or sets the policy for incomplete root coverage.</summary>
        /// <value>BestEffort by default. Known matching targets and unreadable mapped identities are still awaited.</value>
        public OpenStaadDiscoveryPolicy DiscoveryPolicy { get; set; } = OpenStaadDiscoveryPolicy.BestEffort;
        /// <summary>Gets or sets whether a missing requested target may be created.</summary>
        /// <value>True by default. Existing targets are never overwritten by this provider.</value>
        public bool CreateMissingTarget { get; set; } = true;
    }

    /// <summary>Reports acquisition identity and diagnostics alongside the unchanged wrapper.</summary>
    /// <remarks>The caller owns a successful Wrapper's managed lifetime. Disposal of the provider does not dispose it or close STAAD.</remarks>
    public sealed class OpenStaadAcquisitionResult
    {
        internal OpenStaadAcquisitionResult(OpenStaadWrapper wrapper, OpenStaadAcquisitionOutcome outcome,
            OpenStaadAcquisitionFailure failure, string requestedPath, string actualPath, int? pid,
            DateTime? startedUtc, bool complete, IList<string> diagnostics, int? launchedPid = null, DateTime? launchedUtc = null)
        {
            Wrapper = wrapper; Outcome = outcome; Failure = failure; RequestedPath = requestedPath;
            ActualPath = actualPath; ProcessId = pid; ProcessStartTimeUtc = startedUtc;
            DiscoveryComplete = complete;
            LaunchedProcessId = launchedPid; LaunchedProcessStartTimeUtc = launchedUtc;
            Diagnostics = new ReadOnlyCollection<string>(new List<string>(diagnostics));
        }
        /// <summary>Gets the verified wrapper, or null on failure.</summary>
        public OpenStaadWrapper Wrapper { get; }
        /// <summary>Gets whether a wrapper was acquired.</summary>
        public bool Succeeded => Wrapper != null;
        /// <summary>Gets the actual attachment or opening outcome.</summary>
        public OpenStaadAcquisitionOutcome Outcome { get; }
        /// <summary>Gets the failure reason, or None for success.</summary>
        public OpenStaadAcquisitionFailure Failure { get; }
        /// <summary>Gets the normalized requested path, or null for attachment-only acquisition.</summary>
        public string RequestedPath { get; }
        /// <summary>Gets the final native path; empty for a blank, null on failure.</summary>
        public string ActualPath { get; }
        /// <summary>Gets the verified hosting process ID, or null on failure.</summary>
        public int? ProcessId { get; }
        /// <summary>Gets the verified process start time in UTC, or null on failure.</summary>
        public DateTime? ProcessStartTimeUtc { get; }
        /// <summary>Gets the process ID returned by this request's startup attempt, even if model verification later failed.</summary>
        /// <remarks>This is startup evidence, not permission to close the process or proof that it hosts the target. Validate start time and ownership before cleanup.</remarks>
        public int? LaunchedProcessId { get; }
        /// <summary>Gets the UTC start time observed for this request's startup process, or null if no identity was obtained.</summary>
        public DateTime? LaunchedProcessStartTimeUtc { get; }
        /// <summary>Gets whether all inventoried applications had readable roots in the final discovery snapshot.</summary>
        /// <remarks>This is a snapshot observation, not a guarantee that no application changes immediately afterwards.</remarks>
        public bool DiscoveryComplete { get; }
        /// <summary>Gets an immutable copy of discovery limitations and failure messages.</summary>
        public IReadOnlyList<string> Diagnostics { get; }
    }
}
