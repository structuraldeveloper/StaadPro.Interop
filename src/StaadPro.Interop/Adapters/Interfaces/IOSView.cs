using StaadPro.Interop.Models.View;
using System;
namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides the captured View binding and the ported front-orientation request.</summary>
    /// <remarks>ShowFront is implemented; other main-source View functions remain pending.</remarks>
    public interface IOSView : IOSBase
    {
        /// <summary>Gets the native View automation object captured by this adapter.</summary>
        /// <value>The connected raw interface; native calls use the session’s COM-thread and lifetime rules.</value>
        dynamic ComObject { get; }

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
        ViewOperationResult ShowFront();
    }
}
