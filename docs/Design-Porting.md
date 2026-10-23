# Design implementation status and porting guide

**Reviewed:** 23 October 2026. IOSDesign / OSDesignAdapter remain binding-only placeholders. No typed Design functions were implemented by this review.

The installed 2024/2025/2026 manuals contain 31 / 45 / 45 native names respectively. The main source contains 7 interface overloads backed by 4 distinct native names; this does not establish live compatibility.

- [Cross-version architecture and ordered implementation plan](reports/OpenSTAAD-Design-Multi-Version-Architecture-and-Plan.md).
- [Every native contract, parameter, return and managed proposal](reports/OpenSTAAD-Design-API-Coverage-and-Contract-Inventory.md).
- [Machine-readable evidence](reports/data/OpenSTAAD-Design-2024-2026-Comparison.json).
- [Remaining-domain review index](reports/OpenSTAAD-Remaining-Adapters-Review-Index.md).

Implement the foundations and domain workstreams in the plan after approval. Preserve wrapper.StaadVersion routing, status/data result classes, descriptive parameters, complete XML IntelliSense documentation, internal-only COM references and per-version native qualification. No public ref/out or tuples are planned. Raw binding presence is not an operation-readiness guarantee.
