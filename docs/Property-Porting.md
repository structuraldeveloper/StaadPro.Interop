# Property review and implementation status

**Updated:** 23 October 2026

Property currently has a real RawProperty binding and a binding-only IOSProperty / OSPropertyAdapter placeholder. It exposes ComObject and the owning Wrapper, with **zero typed domain methods and zero Property native dispatches**. This review did not implement domain functions.

## Cross-version review

- [Property comparison, internal architecture and implementation sequence](reports/OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md)
- [Complete native contracts, section layouts and source-reuse inventory](reports/OpenSTAAD-Property-API-Coverage-and-Contract-Inventory.md)
- [Machine-readable comparison and fingerprints](reports/data/OpenSTAAD-Property-2024-2026-Comparison.json)

Reviewed all eighteen Property group pages per installed version. STAAD.Pro 2024/2025/2026 contain **204/209/209 names**, including **199/207/207 detailed contracts**. The union has 212 names, with 201 common. Five reserved entries, two private native helpers and one deprecated setter are excluded from new public API work, leaving 204 active candidates subject to verification.

Four common contracts need focused policy review: section-value layouts, country-qualified shape codes, section-record identity and database-folder mutation status. Eight detailed APIs appear in 2025/2026. The older three profile functions are explicitly reserved and cannot provide a supported 2024 fallback. Normalized 2025/2026 declarations, contracts and examples match.

The strictly read-only main project has **103 interface overloads**, all represented by its adapter, plus four public adapter conveniences. Its 56 direct and nineteen additional delegated section-model targets form 75 audited native call names. The reports inventory every signature and flag source unit/status/offset, obsolete-wrapper and cover-plate selector issues requiring adaptation.

Both inspected vendor IOSPropertyUI interfaces contain 218 matching methods. Fourteen metadata names outside the group tree and thirteen class-only surface summaries overlap to form fifteen supplemental contract-discovery candidates. The reports preserve surface/plate name discrepancies and exclude native implementation helpers.

## Recommended first implementation

Approve managed contracts and the minimal acquired-version/decoder foundation, then port qualified member offsets, release and beta reads. Property offset reads unblock the separately planned Output offset-aware results without importing the entire private section catalog. Later increments add material/section queries, controlled creation/assignment, specifications, UPT and catalog APIs.

Keep one non-partial adapter, native references internal, and new public APIs free of ref/out and tuples. Use named status/data results, descriptive parameters, reused managed models and complete XML IntelliSense. Select policies from wrapper.StaadVersion; preserve OpenStaadWrapper and published APIs.

No native STAAD calls, library tests, target changes, NuGet build or publication occurred in this documentation-only review. Package contents remain unchanged.
