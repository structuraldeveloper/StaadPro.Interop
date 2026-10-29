# View implementation status and porting guide

**Current implementation, 28 October 2026:** All 29 main-project Table overloads are adapted and all 24 reviewed native Table names are dispatched. View now ports ShowFront (1/70 source overloads; 1/67 native names). Native-only expansion follows remaining main-source work. See [completion contracts and usage](Table-Completion-and-View-Foundations.md). This coverage is not live COM or report/UI qualification.

**Reviewed:** 23 October 2026. IOSView / OSViewAdapter remain binding-only placeholders. No typed View functions were implemented by this review.

The installed 2024/2025/2026 manuals contain 67 / 67 / 67 native names respectively. The main source contains 70 interface overloads backed by 66 distinct native names; this does not establish live compatibility.

- [Cross-version architecture and ordered implementation plan](reports/OpenSTAAD-View-Multi-Version-Architecture-and-Plan.md).
- [Every native contract, parameter, return and managed proposal](reports/OpenSTAAD-View-API-Coverage-and-Contract-Inventory.md).
- [Machine-readable evidence](reports/data/OpenSTAAD-View-2024-2026-Comparison.json).
- [Remaining-domain review index](reports/OpenSTAAD-Remaining-Adapters-Review-Index.md).

Implement the foundations and domain workstreams in the plan after approval. Preserve wrapper.StaadVersion routing, status/data result classes, descriptive parameters, complete XML IntelliSense documentation, internal-only COM references and per-version native qualification. No public ref/out or tuples are planned. Raw binding presence is not an operation-readiness guarantee.
