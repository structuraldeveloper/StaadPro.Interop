namespace StaadPro.Interop.Enums.Table
{
    /// <summary>Distinguishes Table creation acknowledgements from unacknowledged native void requests.</summary>
    public enum TableOperationStatus
    {
        /// <summary>The native creation operation returned a positive report or table ID.</summary>
        Succeeded,
        /// <summary>The native creation operation returned its documented zero failure value.</summary>
        NativeFailure,
        /// <summary>The native void request returned normally; its effect or persistence was not independently confirmed.</summary>
        InvokedWithoutAcknowledgement
    }
}
