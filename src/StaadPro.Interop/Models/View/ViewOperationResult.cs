using StaadPro.Interop.Enums.View;

namespace StaadPro.Interop.Models.View
{
    /// <summary>Records normal completion of a View request without claiming that the UI changed.</summary>
    public sealed class ViewOperationResult
    {
        internal ViewOperationResult(string nativeFunction) { NativeFunction = nativeFunction; }
        /// <summary>Gets the invoked native function on the captured View instance.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets normal native void completion; UI orientation/effect was not independently observed.</summary>
        public ViewOperationStatus Status => ViewOperationStatus.InvokedWithoutAcknowledgement;
        /// <summary>Gets false because the current void request supplies no native success acknowledgement; this is not a failure flag.</summary>
        public bool IsSuccessful => false;
        /// <summary>Gets null because the native function returns void rather than an integer status.</summary>
        public int? NativeReturnCode => null;
    }
}
