using StaadPro.Interop.Models.View;
using StaadPro.Interop.Services.View;
using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>Implements front-view orientation on the wrapper's captured native View object.</summary>
    /// <remarks>ShowFront records unacknowledged native void completion. Remaining View functions are separate ports.</remarks>
    public sealed class OSViewAdapter : OSBaseAdapter, IOSView
    {
        private readonly ViewService service;
        /// <summary>Captures the supplied wrapper’s View interface without calling its native functions.</summary>
        /// <param name="wrapper">The connected wrapper whose RawView must be non-null.</param>
        /// <exception cref="ArgumentNullException">The wrapper or its RawView binding is null.</exception>
        public OSViewAdapter(OpenStaadWrapper wrapper) : base(wrapper)
        {
            ComObject = wrapper.RawView ?? throw new ArgumentNullException(nameof(wrapper.RawView));
            service = new ViewService(wrapper, ComObject);
        }

        /// <summary>Gets the captured native View automation object.</summary>
        /// <value>The same object that RawView exposed at construction; refresh the wrapper after reopening a model.</value>
        public dynamic ComObject { get; }

        /// <summary>Requests the front orientation in the acquired instance's active graphical view.</summary>
        /// <returns>A ViewOperationResult with NativeFunction ShowFront and InvokedWithoutAcknowledgement. NativeReturnCode is null and IsSuccessful is false because native ShowFront returns void.</returns>
        /// <remarks><para>Ported from the main OSViewAdapter. Unlike Root application-window controls, this requests a graphical view orientation; it does not show a hidden application, create a window, run analysis, extract geometry or save. The native front convention belongs to STAAD. No orientation read-back or effect certification is provided.</para><para>Shared reviewed 2024/2025/2026 contract, corroborated by passive 2025/2026 metadata. Uses captured RawView on the caller's established COM apartment with a per-adapter gate and binding checks before/after dispatch. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No automatic retry, fallback instance, rollback, model launch or implicit save occurs. Native errors propagate unchanged; post-call errors may follow a UI change. Native readiness/licensing and live compatibility remain separate acceptance work.</para></remarks>
        /// <exception cref="InvalidOperationException">The acquired View binding was disposed or replaced.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native UI request failed; effects may precede an error.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The acquired object lacks ShowFront.</exception>
        /// <example><code>bool changeView = false;
        /// if (changeView)
        /// {
        ///     var result = view.ShowFront();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        public ViewOperationResult ShowFront() => service.ShowFront();
    }
}
