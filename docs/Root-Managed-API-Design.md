# Root managed API design rules

These rules apply to new Root APIs and refine the [coverage plan](reports/OpenSTAAD-Root-API-Coverage-and-Implementation-Plan.md) and [multi-version architecture](reports/OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md). Proposed result types below are contracts to implement with their functions, not APIs already shipped in a package.

## Public contracts

- Declare operations in their applicable interfaces and implement them in a single, non-partial adapter. Put native marshaling, compatibility selection and shared helpers in internal services.
- Use descriptive parameter names such as `modelFilePath`, `projectId`, `projectName`, `viewName`, `entityIds` and `modificationMode`. Preserve native positional order internally; public names explain the caller's task.
- Expose no new public `ref` or `out` parameters, including capability discovery. Return data and native operation status together in a named class when the native function supplies both.
- Prefer classes with read-only properties for result snapshots. Avoid tuples, mutable output buffers and public native dispatch services. Add a struct only when value semantics and its size justify it.
- Use existing suitable enums and classes. Add enums for documented value sets, preserve the raw native status and handle unknown future codes explicitly. Do not invent meanings for undocumented flags or status codes.
- Use `IEnumerable<T>` inputs when only enumeration is required; enumerate once into a private snapshot before validation and native mutation. Return materialized, read-only collections such as `IReadOnlyList<string>` for message results. These support `IEnumerable<string>` consumers while preserving count, ordering and indexing. Do not defer COM calls until enumeration.
- Validate complete inputs before native mutation. Native expected failure statuses belong in results; argument errors, disconnected COM objects, incompatible dispatch and malformed successful outputs remain exceptions. Do not silently turn them into ordinary native failures.

## Return semantics

| Native contract | Managed return policy |
| --- | --- |
| Direct scalar/predicate with no separate operation status | A validated enum, scalar or predicate. Preserve unavailable/unknown semantics in XML. |
| Multiple output values with a success/failure flag | A named result containing status, `IsSuccessful` and a data object available only on success. |
| Status code plus an output collection | A named result containing the raw status, a nullable recognized status enum, a documented success predicate and an immutable collection. Distinguish successful empty results from failures. |
| Void plus an output error code | A named result with raw error code and verified status meaning. Do not ship a guessed success predicate before the code meanings are established. |
| Void with no status or output | `void`; returning normally establishes dispatch completion only. Do not manufacture a native success flag. |
| Wrapper metadata projection | Reuse the existing cached type. Never invent native status or output provenance absent from the wrapper. Clearly document cache refresh and compatibility fallbacks. |

`IsSuccessful` must describe the operation in question. For an analysis-status query, successful retrieval does not mean the analysis completed successfully. A dedicated analysis state must distinguish running, terminated, completed with warnings/errors and not performed. Unknown status values must not default to success; raw values remain inspectable. Nullable fields distinguish unavailable outputs from a real count or time of zero.

The existing wrapper's unit fallback may provide a useful enum when a native unit read fails. The new projection methods preserve that compatibility contract and explicitly document it; their enum return does not certify an observed unit. A later diagnostic API would need to capture provenance during the original native read rather than reconstruct it from a cached value.

## Planned result classes

| Class | Data and status contract |
| --- | --- |
| `AnalysisStatusResult` | Raw native status, nullable recognized analysis state, explicitly defined query success, and nullable warning count, error count and CPU time. Output validity is checked against the verified native status contract. |
| `AnalysisMessagesResult` | Raw native status, nullable recognized retrieval status, `IsSuccessful`, read-only `Messages` and queried model path. Native 1 and 0 represent retrieved messages and successful absence respectively; expected native failures expose no valid messages. Allocate and marshal arrays internally. |
| `ConnectedProjectResult` | Verified native success flag, `IsSuccessful` and a `ConnectedProjectInfo` only on success. Preserve supplied project ID/name text. |
| `NamedViewResult` | Raw error code and verified recognized status/success semantics. Release remains gated on flag/error documentation and native validation. |
| `AdinaExportResult` | Raw status, nullable recognized export status, `IsSuccessful` including documented warning success, and a separate warning indication. Do not fabricate an output path absent from the native response. |
| `RootCapabilityResult<TCapability>` | Availability status/reason and a non-null bound interface only when granted. Unsupported capabilities return no interface; discovery/connection failures retain their explicit exception contract. No public output parameter. |

Keep operation-specific result classes where statuses differ. A single generic result with an arbitrary Boolean and an untyped payload would lose useful contract information. Reuse a small generic capability result only for the same discovery semantics.

## Useful overloads

- Analysis status and message retrieval can offer `(string modelFilePath)` and parameterless overloads for the currently bound model. The parameterless form obtains the current path once and delegates to the explicit-path implementation; an unnamed model needs a documented unavailable-result or argument policy before release.
- Short/full job updates can offer a named information-object overload alongside explicit arguments. Snapshot editable object properties before validation; map the native order internally. Reuse suitable existing job information types after checking their field/date semantics.
- Named-view modification should accept entity IDs and a typed entity kind, derive the count internally, and expose a typed modification mode only after its values are established.
- Do not add Boolean freshness overloads to the five metadata projections. Refresh the owning wrapper explicitly. Constructors and method XML explain the standalone native-read path.
- Add overloads with their operation, tests and XML only when they provide a concrete caller benefit. Do not create unused overloads or speculative source files.

## Organization

```text
Adapters/Interfaces/IOSRoot.cs            common public operations
Adapters/Interfaces/Capabilities/         optional public operation contracts
Adapters/Models/OSRootAdapter.cs          single common adapter implementation
Adapters/Models/Capabilities/            internal specialized adapters when needed
Models/Root/                              operation-specific result classes when needed
Enums/Root/                               documented operation statuses/options when needed
Services/Root/Metadata/                    internal wrapper/raw metadata access
Services/Root/                            shared/version-specific native operations as needed
Services/Wrapper/                         shared metadata reading and normalization
```

Create folders and abstractions as implementation requires them. Do not generate empty per-version service trees. Share unchanged contracts; isolate differences only where supported by evidence. Keep existing public behavior intact; adding interface members requires updates to third-party `IOSRoot` implementations and test doubles.

## First implemented increment

The five metadata queries are implemented: `GetApplicationVersion`, `GetBaseUnit`, `GetInputUnitForForce`, `GetInputUnitForLength` and `GetMainWindowHandle`.

Wrapper-owned adapters project cached wrapper properties without additional native reads. Standalone adapters retain a capture-only constructor and use the same internal readers against their supplied root on each query. Standalone unit queries read version and base system before the selected unit; standalone window queries read version and then the handle only for the source-compatible Connect family. Reads are sequential on the caller's COM thread and are not an atomic model snapshot. Metadata methods captured from a disposed wrapper throw `ObjectDisposedException`.

These additions do not implement the remaining native operations or the version-profile/capability engine. Their tests establish managed behavior with stand-ins; actual 2024/2025/2026 compatibility still requires native validation. No NuGet package has been rebuilt for this increment.
