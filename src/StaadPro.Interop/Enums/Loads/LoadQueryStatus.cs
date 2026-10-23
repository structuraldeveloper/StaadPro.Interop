namespace StaadPro.Interop.Enums.Loads
{
    /// <summary>Describes the outcome of a managed Load inspection request.</summary>
    public enum LoadQueryStatus
    {
        /// <summary>The observation succeeded and Data is available.</summary>
        Success,
        /// <summary>A documented native error was returned.</summary>
        NativeFailure,
        /// <summary>The requested native list, item or repeat index was not found.</summary>
        NotFound,
        /// <summary>The requested primary case could not be activated.</summary>
        ActivationFailed,
        /// <summary>Observed activation or counts changed during retrieval; no payload is exposed.</summary>
        StateChanged,
        /// <summary>The required output exceeds the caller's retrieval limit.</summary>
        LimitExceeded,
        /// <summary>A native zero cannot distinguish no matching commands from an unavailable case.</summary>
        EmptyOrUnavailable,
        /// <summary>The requested command family or operation is not supported by the native contract.</summary>
        Unsupported
    }
}
