# OpenStaadWrapper implementation and source parity

**Updated:** 23 October 2026
**Scope approved:** Complete wrapper members, raw objects, application/model metadata and adapter bindings. Unported domain adapters are placeholders; their functions will be ported separately.

The [main project wrapper](<D:/04.%20ReInvented/MainProjects/SRi.XamlUIThickenerApp/src/ReInvented.StaadPro.Interop/Models/OpenStaadWrapper.cs>) was treated as **strictly read-only**. All implementation changes are in this open-source repository. The wrapper now includes all **30 source properties**, both constructor forms and all three public methods. The existing IsConnected property is retained as an additional convenience.

The public wrapper surface is complete for this approved scope. Current domain coverage includes 96 Output methods, 249 common/five optional Property methods, 27 Support routes, seventeen Commands methods and ten Design methods. Table now exposes twenty existing main-source report/lifecycle/typography operations; View and IdeaStatiCa remain explicitly documented placeholders. Their native bindings are real; no missing domain operation is disguised as implemented or replaced by a throwing stub.

**Table update, 28 October 2026:** Table exposes twenty existing main-source report/lifecycle/typography operations; [contracts and remaining ports](Table-Report-Foundations.md). The original wrapper-surface parity record remains below.

## 1. Source/member parity

The source's Bentley COM types are represented by object/dynamic, matching this repository's late-bound architecture. Consumers do not need a proprietary Bentley interop assembly to build against the package. Signatures and behavior involving cached metadata are documented below.

| Source property | Current implementation |
| --- | --- |
| `BaseUnitSystem` | Cached native base system; refreshed explicitly. |
| `InputForceUnit` | Cached source-compatible parsed/fallback force unit. |
| `InputLengthUnit` | Cached source-compatible parsed/fallback length unit. |
| `IsDedicated` | Caller-supplied dedication flag; no process ownership. |
| `RawOpenStaad` | Original acquired root as object; null after disposal. |
| `Root` | Root binding with 53 common methods and a capability gateway, including all 23 private-source functions; three optional operation interfaces complete the 39-function vendor inventory. |
| `RawGeometry` | Captures native Geometry interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Geometry` | Existing implemented Geometry adapter. |
| `RawLoad` | Captures native Load interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Load` | Existing implemented Load adapter. |
| `RawOutput` | Captures native Output interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Output` | `IOSOutput` / `OSOutputAdapter` with 15 typed status/data queries; 45 native mappings remain pending. See the [Output guide](Output-Porting.md). |
| `RawProperty` | Captures native Property interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Property` | `IOSProperty` / `OSPropertyAdapter` exposes 25 typed observation queries with internal native calls and no public reference outputs. See the [Property guide](Property-Porting.md). |
| `RawSupport` | Captures native Support interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Support` | 27 typed methods over 27/35 native routes. See [Support foundation operations](Commands-and-Support-Porting.md). |
| `RawDesign` | Captures native Design interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Design` | Ten common methods reaching five native names; extended GROUP and Chinese steel remain pending. See [Design porting](Design-Porting.md). |
| `RawCommands` | Captures native Command (singular) interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Commands` | Seventeen typed methods reaching sixteen analysis-command native names; steel catalog work remains pending. See [analysis command operations](Commands-and-Support-Porting.md). |
| `RawTable` | Captures native Table interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `RawView` | Captures native View interface; null when absent, except retained legacy Geometry/Load direct-root fallback. |
| `Table` | Twenty existing main-source report/table/lifecycle/typography operations through `IOSTable` / `OSTableAdapter`; [contracts and examples](Table-Report-Foundations.md). Nine reference overloads remain. |
| `View` | Binding-only `IOSView` / `OSViewAdapter` placeholder; no domain functions ported. See the [View comparison and plan](View-Porting.md). |
| `IdeaStatiCa` | Placeholder created only when Geometry, Load and Output bindings exist. See the [integration source review and plan](IDEA-StatiCa-Porting.md) for the additional operation prerequisites. |
| `StaadVersion` | Native STAAD version; independent copy on access; null if version member absent. |
| `ApplicationVersion` | Entry/calling host assembly version; never native STAAD version selection. |
| `StaadEdition` | None / V8i / Connect using the source major-version rule. |
| `StaadApplicationWindowHandle` | Cached non-owning handle; default service reads the acquired root for Connect. |
| `StaadInteropInteractionService` | Captured host service; noninteractive default; not disposed by wrapper. |

### Constructors and public functions

| Member | Implemented behavior |
| --- | --- |
| `OpenStaadWrapper(object openStaad, bool isDedicated = false)` | Captures the root, binds domains, queries metadata and captures the current default host service. The bool default preserves the existing open-source call form. |
| `OpenStaadWrapper(object openStaad, bool isDedicated, IStaadModelServices staadModelServices, IStaadInteropInteractionService staadInteropInteractionService)` | Ports the main source's injected-service constructor using late-bound native objects. Null dependencies are rejected before native queries. |
| `RefreshComObjects()` | Rebinds all raw/managed objects and refreshes both version values, edition, units and the applicable window handle. Does not open/reopen a file, sleep or certify database readiness. |
| `RefreshUnits()` | Refreshes only base/force/length snapshots. Does not replace adapters, reread versions/handles, save or mutate the model. |
| `Dispose()` | Clears all raw and managed bindings, including placeholders, and prevents refresh resurrection. Does not close STAAD, save a model or final-release shared RCWs. Repeated disposal is harmless. |

Source private helpers are ported directly or extracted into internal services. SetPrivateProperties, SetUnmanagedObjects, SetManagedObjects and host-version/edition helpers remain wrapper methods. Native base-unit, input-unit and STAAD-version reads now live in WrapperMetadataReader and serve both wrapper caches and standalone Root adapters. Unit conversion/parsing helpers live in WrapperUnitParser; shared strict native integer validation lives under Services/Interop/NativeValues. The source's private model-service field is retained.

### Placeholder boundary

| Binding | Implemented now | Deferred |
| --- | --- | --- |
| Output | Binding and 15 typed queries with named results, immutable payloads and XML. | Remaining 45 documented native mappings, supplemental contracts and live qualification. |
| Support / Design / Commands / Table / View | Named public interface and sealed non-partial adapter; captured ComObject and owning Wrapper; correct native raw object. | Every domain-specific function. |
| IDEA StatiCa | Named public placeholder interface/adapter and owning wrapper; required Geometry/Load/Output binding precondition. | Options, extraction, mapping, equilibrium checks, diagrams and export functions from the main integration. |
| Geometry / Load / Root | Existing implementations retained, with rebuilt bindings on refresh. | Broader native API coverage and version qualification. Main-interface Load reference parity and the reviewed Root/Geometry mappings are complete. |

No adapter partial classes were created. No Bentley DLL, private Sections/Units/Shared project reference or main-project assembly was added. The source's broad IStaadModelServices implementation was not imported: only its window-handle operation required by this wrapper is included. File creation/model coordination/editing services remain outside this approved wrapper port.

## 2. Metadata semantics and deliberate adaptations

### Native versus host application version

`StaadVersion` comes from GetApplicationVersion on the captured native root. `ApplicationVersion` comes from the host's CLR assembly version. This distinction is present in the main source itself. A Root GetApplicationVersion projection or native version selector must use **StaadVersion**.

Native version querying uses the documented four reference outputs and version-text return, improving on the source's by-value call. Written numeric parts are checked against the returned text. Missing optional member yields null; malformed/non-string/contradictory supplied metadata throws instead of inventing a version. A return-text-only automation shim can be accepted when all numeric buffers remain their initial sentinels. A returned StaadVersion value is a defensive copy, so editing the mutable model cannot change the cached observation used for future routing.

The source-compatible ApplicationVersion model contains Major, Update, Revision, Build, Display and both Parse overloads. CLR Major/Minor/Build/Revision map to those fields in that order. Text parsing is deliberately stricter than the source helper: exactly four nonnegative integer parts are required. The source helper silently ignored extra components and allowed negative values. Display uses invariant formatting. Edition values remain None=0, V8i=1, Connect=2; obsolete machine-specific installation paths are not embedded in enum descriptions.

### Unit parsing, fallback and caching

The wrapper now uses reference-output unit calls. It prefers a parsed native output over a usable return, then falls back to Kip/Feet for an Imperial base system and kN/Meter otherwise. This fallback, including on transition exceptions, is retained from the main wrapper and explicitly documented; it does not prove the actual current input unit.

The port handles all 0–7 numeric code values, the length code-1 feet alias, standard source names/abbreviations, and the documented lb/MTon/DN forms missing from the source parser. Boolean values and floating/decimal coercions cannot turn into unit codes through Convert.ToInt32 rounding. The 2024 help's documented success return is conservatively not treated as a returned unit when reference output is unusable. That guard is not a native-verified contract registry. A missing native version retains the source's compatible numeric-return heuristic; it must not be advertised as strict unit provenance.

The [2024–2026 comparison](reports/OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md) still requires controlled native evidence to decide whether the 2024 contract genuinely differs or its documentation is wrong. A future strategy must operate beneath the wrapper's metadata reader so Root does not repeat or contradict the same native unit query.

Unit properties are **cached snapshots**, matching the main project. This changes the earlier open-source behavior that queried on each property access. Call RefreshUnits after native unit changes; call RefreshComObjects after reopening a model once its database is ready. Existing Root unit setters are unchanged and do not silently refresh the cache.

### Window identity and lifecycle

The source default window helper scans process names and chooses the first match. The port deliberately reads GetMainWindowHandle on the exact captured root, preventing accidental selection of another STAAD instance. The injected IStaadModelServices hook is retained; its implementation must target the same instance. The source edition rule is retained: Connect requests the handle; other/unknown editions expose zero.

The handle is non-owning and cached. Zero is allowed; invalid sentinel/coercible noninteger/overflow shapes are rejected. The wrapper does not call IsWindow or promise that a previously observed handle remains live. Existing Root window-control functions continue their independent fresh HWND/PID ownership checks.

Both refresh methods reject a disposed wrapper. Disposal clears managed bindings as well as raw bindings, improving on the source's retained managed adapters. Cached metadata remains readable. An external reference to an old adapter is not revoked; callers must reacquire adapters after RefreshComObjects. Refresh itself is synchronous and in place, so a propagated error may leave partial refreshed state. Call on the correct COM thread, serialize access, and recover deliberately.

For compatibility, missing Geometry/Load properties retain the repository's existing direct-root dispatch fallback. Other missing native domains remain null. Unexpected native interface/metadata failures propagate rather than masquerading as missing optional members. Missing metadata on legacy/test roots is tolerated; it is not evidence of version-specific support.

## 3. Root property reuse (implemented)

| Implemented Root function | Implemented wrapper value | Important meaning |
| --- | --- | --- |
| GetApplicationVersion | `StaadVersion` | Native STAAD version, defensive copy, not host ApplicationVersion. |
| GetBaseUnit | `BaseUnitSystem` | Cached normalized base-system value. |
| GetInputUnitForForce | `InputForceUnit` | Cached enum, possibly source-compatible fallback. |
| GetInputUnitForLength | `InputLengthUnit` | Cached enum with normalized feet aliases. |
| GetMainWindowHandle | `StaadApplicationWindowHandle` | Cached, non-owning, Connect-family lookup. |
| Version/family metadata | `StaadVersion`, `StaadEdition` | Select native policy from the observed version; edition alone is insufficient. |

Wrapper-bound projections now reuse the internal metadata accessor without native version/unit/window reads. Native text, raw-code aliases and output provenance are not retained by these public cache properties; do not fabricate them. GetProcessId has no wrapper property and remains a separate native query. Standalone raw-root adapters use the shared WrapperMetadataReader. The implemented metadata-access seam is described in the architecture plan.

## 4. Usage

```csharp
using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;

// Run on the session's appropriate COM thread.
using (OpenStaadWrapper wrapper = OpenStaadWrapperProvider.GetRunning())
{
    if (wrapper == null) return;
    Console.WriteLine(wrapper.StaadVersion?.Display);       // STAAD.Pro
    Console.WriteLine(wrapper.ApplicationVersion.Display); // consuming application
    Console.WriteLine(wrapper.InputForceUnit);

    // After an explicit native input-unit change:
    wrapper.RefreshUnits();

    // After explicit model reopening AND native database initialization:
    wrapper.RefreshComObjects();
    var geometry = wrapper.Geometry; // acquire the replacement adapter

    // Raw binding remains available; prefer the typed Output queries where implemented.
    object nativeOutput = wrapper.RawOutput;
}
```

Construction and refresh do not show host UI or launch/close STAAD. The default host interaction service cancels selections and ignores notifications; consumers can inject their own service. An already-created wrapper retains the service captured at construction.

## 5. Validation and release boundary

- Release net481 build succeeds, with compiled XML for the new members.
- **1,604 offline tests pass**, including source-property parity, two constructors, all raw bindings/placeholders, native versus host version, cache/refresh behavior, service injection, unit aliases/fallbacks, malformed values, bound-instance handles and disposal. The explicit live-provider test is not run by the normal suite.
- Rechecked the current source on 22 October 2026: all 30 source property names, both constructors and the three source public methods are present. The new integration case uses all three completed Geometry functions through the wrapper without native reads and checks refreshed bindings.
- All 30 source property names and the three source public method names are present. The legacy single-constructor test was updated to require the source's two constructor forms.
- The source wrapper fingerprint was compared before/after the port and is unchanged. The main project was never built or modified.
- The original wrapper-only increment performed no native STAAD compatibility test or additional domain-function port. Later work implemented Root version profiles/capabilities and built the published 1.0.0-preview.20261015 package. Subsequent increments added five Root metadata queries and two common analysis observation functions with a current-model overload; see the [analysis guide](Root-Analysis-Queries.md). Three common model/error reads were added next; shared strict native conversion now lives under Services/Interop/NativeValues. The next model-setup increment added process identity and four explicit native mutation operations with a ShortJobInfo overload.

Source wrapper SHA-256: `83737733D57A672313385F99EA0CA8FFA10F1917FB6A33B89E43CE27E3F37B14`.

The published 1.0.0-preview.20261015 package includes the approved wrapper surface and reviewed Root mappings. The final three Geometry operations were implemented later and require a new package before distribution. The wrapper has no missing approved source property, constructor or public method; this increment rechecks parity and integration rather than duplicating existing members. The original wrapper increment kept eight domain APIs outside its scope. Output subsequently added 15 typed queries and Property added 25 typed observation queries; the other six remain placeholders. See [Geometry completion](Geometry-Porting.md#completed-managed-geometry-operations).
