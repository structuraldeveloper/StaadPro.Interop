using System;
using StaadPro.Interop.Models.Root;
namespace StaadPro.Interop.Adapters.Interfaces.Capabilities
{
    /// <summary>Acquires optional Root interfaces after native-version and member-metadata checks.</summary>
    /// <remarks>Each method checks independently without executing an optional operation. Unknown/future versions,
    /// uncovered servicing builds, absent members and incompatible metadata grant no interface. Public declarations
    /// remain inspectable; this boundary does not restrict deliberate raw COM access.</remarks>
    public interface IOSRootCapabilities
    {
        /// <summary>Attempts to acquire IOSRootAdinaExport for the captured native root.</summary>
        /// <returns>A typed availability result; only an Available outcome contains a non-null adapter.</returns>
        /// <remarks>Uses the owning wrapper's cached native StaadVersion, or a same-root native read for a standalone
        /// binding. Exact candidate profiles are 25.0.1.424 (export only) and 26.0.0.340 (export and messages).
        /// Requires compatible non-mutating native member metadata; message grants also inspect GetAnalysisStatus.
        /// The profiles are backed by installed help/managed declarations, not certified native runtime tests.
        /// Inspection never exports, retrieves messages, mutates models or guesses from installed directories.
        /// No grant is cached globally. Reacquire after a version change; calls through a grant validate its snapshot.
        /// Use the session's COM thread. Busy, disconnected, wrong-thread and locale failures remain exceptions.</remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper was disposed.</exception>
        /// <exception cref="InvalidOperationException">Native version information is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Version or metadata inspection fails; native failures are not converted to unsupported results.</exception>
        /// <example><code>
        /// var grant = root.Capabilities.GetAdinaExport();
        /// Console.WriteLine(grant.Availability);
        /// </code></example>
        RootCapabilityResult<IOSRootAdinaExport> GetAdinaExport();

        /// <summary>Attempts to acquire IOSRootAnalysisErrorMessages for the captured native root.</summary>
        /// <returns>A typed availability result; only an Available outcome contains a non-null adapter.</returns>
        /// <remarks>Uses the owning wrapper's cached native StaadVersion, or a same-root native read for a standalone
        /// binding. Exact candidate profiles are 25.0.1.424 (export only) and 26.0.0.340 (export and messages).
        /// Requires compatible non-mutating native member metadata; message grants also inspect GetAnalysisStatus.
        /// The profiles are backed by installed help/managed declarations, not certified native runtime tests.
        /// Inspection never exports, retrieves messages, mutates models or guesses from installed directories.
        /// No grant is cached globally. Reacquire after a version change; calls through a grant validate its snapshot.
        /// Use the session's COM thread. Busy, disconnected, wrong-thread and locale failures remain exceptions.</remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper was disposed.</exception>
        /// <exception cref="InvalidOperationException">Native version information is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Version or metadata inspection fails; native failures are not converted to unsupported results.</exception>
        /// <example><code>
        /// var grant = root.Capabilities.GetAnalysisErrorMessages();
        /// Console.WriteLine(grant.Availability);
        /// </code></example>
        RootCapabilityResult<IOSRootAnalysisErrorMessages> GetAnalysisErrorMessages();

        /// <summary>Attempts to acquire IOSRootAnalysisWarningMessages for the captured native root.</summary>
        /// <returns>A typed availability result; only an Available outcome contains a non-null adapter.</returns>
        /// <remarks>Uses the owning wrapper's cached native StaadVersion, or a same-root native read for a standalone
        /// binding. Exact candidate profiles are 25.0.1.424 (export only) and 26.0.0.340 (export and messages).
        /// Requires compatible non-mutating native member metadata; message grants also inspect GetAnalysisStatus.
        /// The profiles are backed by installed help/managed declarations, not certified native runtime tests.
        /// Inspection never exports, retrieves messages, mutates models or guesses from installed directories.
        /// No grant is cached globally. Reacquire after a version change; calls through a grant validate its snapshot.
        /// Use the session's COM thread. Busy, disconnected, wrong-thread and locale failures remain exceptions.</remarks>
        /// <exception cref="ObjectDisposedException">The owning wrapper was disposed.</exception>
        /// <exception cref="InvalidOperationException">Native version information is malformed.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Version or metadata inspection fails; native failures are not converted to unsupported results.</exception>
        /// <example><code>
        /// var grant = root.Capabilities.GetAnalysisWarningMessages();
        /// Console.WriteLine(grant.Availability);
        /// </code></example>
        RootCapabilityResult<IOSRootAnalysisWarningMessages> GetAnalysisWarningMessages();
    }
}
