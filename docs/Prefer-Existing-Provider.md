# PreferExistingOpenStaadWrapperProvider

The independent provider prefers an already-open target, then a reachable verified unnamed session, then a newly opened target. It does not replace `OpenStaadWrapperProvider.Resolver`, edit the legacy provider, or change `OpenStaadWrapper` or its properties.

Current source builds for `net481` and `net8.0-windows`. Modern .NET support is **Windows only** and uses the built-in COM runtime. The library retains its WPF geometry types, so modern consumers require the Windows Desktop/WPF framework even if their application is a console program. Earlier published previews do not acquire these capabilities merely by installing them; build/install a package containing this implementation.

## Usage

Create, call and dispose a provider on the same STA thread. For a modern console application, use a conventional entry point with `[STAThread]`; do not use an asynchronous entry point that resumes elsewhere.

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net8.0-windows</TargetFramework>
  <UseWPF>true</UseWPF>
</PropertyGroup>
```

```csharp
using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using (var provider = new PreferExistingOpenStaadWrapperProvider(
            new OpenStaadAcquisitionOptions
            {
                // Configure the actual installed executable. No version is assumed by the provider.
                StaadExecutablePath = @"C:\Program Files\Bentley\Engineering\STAAD.Pro 2026\STAAD\Bentley.Staad.exe",
                DiscoveryPolicy = OpenStaadDiscoveryPolicy.BestEffort,
                Timeout = TimeSpan.FromMinutes(2)
            }))
        {
            var result = provider.Acquire(@"C:\Models\Structure.std");
            foreach (string diagnostic in result.Diagnostics) Console.WriteLine(diagnostic);
            if (!result.Succeeded)
            {
                Console.WriteLine(result.Failure);
                return;
            }
            using (result.Wrapper)
            {
                Console.WriteLine($"{result.Outcome}: PID {result.ProcessId}, {result.ActualPath}");
                // Use the wrapper synchronously on this same STA thread.
            }
        }
    }
}
```

`Get(path)` returns only the wrapper, or null on operational failure. Prefer `Acquire(path, cancellationToken)` when an application needs to distinguish outcomes and report failures. Invalid arguments, use on another thread, reentrancy and use after disposal throw; those are caller-contract errors rather than successful acquisitions.

`Get()` and `Acquire()` with no path are **attachment only**. They do not create files or launch applications. A provider with default options can attach; configure `StaadExecutablePath` only if fallback may launch. Options are validated and copied at construction. The logical timeout must be positive and no greater than one day; polling must be positive and no greater than the timeout; discovery grace must be nonnegative and no greater than the timeout.

## Selection and readiness

1. Inventory supported `Bentley.Staad` processes in the caller's interactive Windows session, including PID and UTC start time. An access failure is reported, not silently treated as zero applications.
2. Combine retained roots with existing ROT candidates and the registered active root. Native discovery uses `ole32`/`oleaut32` and existing registrations; it does not activate a COM server or open a file merely to discover it.
3. Read each candidate's native PID and BSTR filename output. Deduplicate readable aliases for the same live process. Conflicting filenames cause a rescan; an unwritten, malformed or unreadable mapped filename blocks fallback.
4. Prefer a matching full path. If identified but not ready, wait within the request budget. Do not replace it with a blank or reopen it.
5. Apply incomplete-discovery policy, then test reachable unnamed sessions. A blank requires an explicitly written empty filename and zero nodes, members, plates, solids, primary load cases, reference load cases and combination cases. Both interfaces must be present and every count must be a supported nonnegative integer. Unsupported/negative counts do not mean empty content.
6. Construct the unchanged wrapper, test its captured interfaces again, and recheck PID/start time/path and cancellation/time before returning it. Strict blank reuse also rechecks discovery coverage.
7. If no usable session remains, coordinate cooperating callers for the normalized target and rediscover before preparing or launching it.

The blank checks establish only this content contract. They do not prove that every native setting or definition is absent, that analysis/write access is safe, or that the application cannot change after the last read. A returned `ExistingBlank` has **not opened the requested file**. Existing unsaved models are never overwritten by a provider COM file-open call.

## Discovery policies

| Policy | Unmapped applications | Verified matching target |
|---|---|---|
| `BestEffort` (default) | Retry for the discovery grace period, report incomplete discovery, then allow reachable-blank reuse or target fallback | Return if ready; await it if not ready |
| `RequireCompleteDiscovery` | Wait; fail if complete readable coverage is not established before blank reuse or fallback | An independently verified target can be returned even if other applications remain unmapped |

Best effort cannot exclude a target already open in an inaccessible application. Strict mode prefers failure to that risk, but even strict coverage is a snapshot and does not prevent a different program opening a model just afterwards. Case-insensitive full-path normalization does not resolve all symbolic links, drive mappings or filesystem aliases. Use the same canonical target path across cooperating callers.

## Retained roots

The provider retains roots it has successfully identified. A named application that later becomes blank can be retried through that retained root rather than the ambiguous global class registration. Native transitions can invalidate a root or leave particular functions unavailable; retention does not turn those states into a ready blank.

`RegisterRoot(object)` accepts a root the caller actually possesses, validates its native process identity and readable filename, and retains it. For example, call `provider.RegisterRoot(existingWrapper.RawOpenStaad)` on the provider's owning STA thread. This does not implement PID-to-root binding for an inaccessible application. `ClearRetainedRoots()` clears references without closing STAAD or final-releasing shared proxies.

The provider is scoped to its STA thread. Do not put it in a global singleton used by arbitrary task/thread-pool threads. Services should run acquisition and all wrapper operations on a deliberately managed STA context. Existing adapter asynchronous methods do not become thread safe merely because acquisition uses this provider.

## File preparation, launch and coordination

Missing targets are written completely to an exclusively created sibling temporary file, then published using a non-overwriting rename. A competing creator's target wins and its bytes are preserved. Cancellation before publication removes only the owned temporary file. Existing targets are never truncated or replaced. A published target is retained after later failure or cancellation because another application may already be using it.

The configured launcher is checked before file preparation. With no launcher, attachment remains available but fallback reports `LaunchFailed` without preparing a target. The native backend supports the `Bentley.Staad.exe` inventory/launcher name; missing or mismatched executables fail before target creation.

Coordination uses a session-local named mutex keyed by the normalized, case-insensitive path. It coordinates callers using this provider; it does not lock the customer's UI, other programs or alternate path aliases. Discovery is rechecked after acquisition of the mutex. Every exit releases coordination.

Startup uses the configured executable directly with the target argument and runs once per request. A returned process handle is not enough: PID/start time, the native opened path and captured interfaces must agree before `OpenedTarget` is returned. Redirection to a different process is reported. This provider does not issue `OpenSTAADFile` or `NewSTAADFile` to an existing root; behaviour of the STAAD executable itself, including redirection or licence dialogs, cannot be rolled back by the provider.

## Results and failures

Successful outcomes are `ExistingSession` (attachment-only named model), `ExistingTarget`, `ExistingBlank` and `OpenedTarget`. `ProcessId`/`ProcessStartTimeUtc` identify the verified hosting application. Existing attachments set `IsDedicated` false; only a verified newly launched hosting process sets it true. A later request attaching to that same application again is an existing attachment.

Failures provide a null wrapper and a typed reason: no session, timeout, cancellation, incomplete discovery, unreadable identity, discovery failure, file-preparation failure, launch failure, redirection or premature process exit. `Diagnostics` is an immutable, bounded copy. `LaunchedProcessId` and `LaunchedProcessStartTimeUtc` preserve startup evidence even if later verification fails; they are **not permission to close a process** or proof of target ownership.

Disposing a provider clears retained references and balances its COM initialization. It does not close applications, dispose successful wrappers or delete targets. The caller disposes returned wrappers on the same STA thread. Failure/cancellation does not automatically close a newly launched application: that application may already have user work. Any cleanup must independently verify process identity and ownership.

## Timeout and deployment limits

One monotonic budget covers retries, discovery, target coordination, readiness and launch. Cancellation is checked between native operations and before file publication/startup. STA waiting pumps Windows messages, and nested acquisition/registry/lifecycle calls are rejected to protect state.

**A synchronous COM call may block beyond the budget.** Neither cancellation nor a stopwatch safely aborts that call. A hard wall-clock deadline would require a separately designed worker-process boundary, and raw COM wrappers cannot simply be passed out of that process. This implementation intentionally does not promise an absolute absence of STAAD, Windows, licence or COM failures.

The modern asset targets Windows .NET 8; .NET Core 3.1 and non-Windows applications are not targeted. Use the Windows Desktop runtime and ordinary JIT deployment. Trimming and Native AOT are not validated for this dynamic COM/WPF wrapper.

## Verification

The automated suite runs on both targets and exercises matching/blank preference, busy targets, unreadable identities, duplicate aliases, PID reuse, retained/registered roots, changed models, strict coverage changes, malformed counts, cancellation, coordination timeouts, one-shot startup, redirection, process exit and atomic file races. Expanded public XML documentation is checked by the existing API documentation suite.

Read-only native checks attached to the customer's existing named model on both runtimes without reopening it. Controlled native creation tests used new temporary STD targets and closed only verified test-created processes. STAAD.Pro 2026 `26.00.00.340` returned a negative reference-load count after closing an owned empty model; that state did not meet the blank contract and was handled by fallback rather than interpreting the negative result as zero. Broader STAAD-version/licensing coverage and native ready-blank configurations remain outside this local validation.

See the [flow verification report](reports/OpenStaadWrapper-Provider-Flow-Verification.md) for diagrams, native evidence and edge cases.
