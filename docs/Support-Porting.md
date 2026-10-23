# Support review and implementation status

**Updated:** 23 October 2026

Support currently has a real RawSupport binding and a binding-only IOSSupport / OSSupportAdapter placeholder, exposing ComObject and the owning Wrapper. It has **zero typed domain methods and zero native Support dispatches**. This review added documentation only.

## Cross-version review

- [Support comparison, architecture and implementation sequence](reports/OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md)
- [Complete contracts and main-source reuse inventory](reports/OpenSTAAD-Support-API-Coverage-and-Contract-Inventory.md)
- [Machine-readable comparison and fingerprints](reports/data/OpenSTAAD-Support-2024-2026-Comparison.json)

The three installed 2024/2025/2026 references contain the same **35 detailed Support functions** across three group pages. Declarations and non-example contracts match. All 35 examples change from 2024 to 2025; 2025/2026 examples match. Both inspected 2025/2026 IOSSupportUI interfaces expose the same 35 methods. Seven extra native-class helpers are excluded from public API work.

There is no evidence requiring different implementations by year. Use shared internal services and polymorphic support/foundation/request handling, keeping acquired native version and contract evidence available for diagnostics and future verified differences.

The strictly read-only main project has **33 matching interface/public-adapter overloads backed by eighteen native targets**, leaving seventeen native names without a target. Two overloads are obsolete(error=true). The reports flag no-op enforced creation, incomplete type discovery, lost plate subgrades, spring-state/units/null handling and source identity/factory limitations.

## Recommended first increment

Approve the result/identity contracts, then implement a minimal shared foundation and qualified supported-node count/list, type and basic information queries. Follow with verified extended identity, foundation details and explicit creation/assignment/removal APIs.

Keep one non-partial adapter, descriptive parameters and named status/data results, with no new public ref/out, tuples or vendor COM types. Preserve node/definition/index distinctions, all native release states and qualified partial effects. All new public methods/models/enums require full XML IntelliSense.

Plate-mat scalar/array input and extended-info reference directions remain native-verification gates. Equal help and metadata do not certify runtime behavior or engineering correctness. No source implementation, library test run, wrapper/target change, NuGet build or publication occurred; package contents remain unchanged.
