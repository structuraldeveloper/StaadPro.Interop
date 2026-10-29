# Table implementation status and porting guide

**Current implementation, 28 October 2026:** All 29 main-project Table overloads are adapted and all 24 reviewed native Table names are dispatched. View now ports ShowFront (1/70 source overloads; 1/67 native names). Native-only expansion follows remaining main-source work. See [completion contracts and usage](Table-Completion-and-View-Foundations.md). This coverage is not live COM or report/UI qualification.

**Latest implementation checkpoint, 28 October 2026:** Twenty existing main-source Table overloads are implemented, reaching 20/24 native names. The latest ten cover report/table deletion, renaming/resizing, scoped saves and cell typography. Nine source overloads remain. Passed 6,780 offline tests, including 332 Table cases. See [lifecycle and formatting contracts](Table-Lifecycle-and-Formatting.md). Main-source parity precedes native-only expansion.

**Implementation checkpoint, 28 October 2026:** Ported ten existing main-project Table methods, reaching 10/24 documented native names. Nineteen of 29 main-source overloads remain pending. Main-source parity has priority over native-only expansion. See [report/table contracts and usage](Table-Report-Foundations.md). The review baseline below is historical.

**Reviewed:** 23 October 2026. IOSTable / OSTableAdapter remain binding-only placeholders. No typed Table functions were implemented by this review.

The installed 2024/2025/2026 manuals contain 24 / 24 / 24 native names respectively. The main source contains 29 interface overloads backed by 24 distinct native names; this does not establish live compatibility.

- [Cross-version architecture and ordered implementation plan](reports/OpenSTAAD-Table-Multi-Version-Architecture-and-Plan.md).
- [Every native contract, parameter, return and managed proposal](reports/OpenSTAAD-Table-API-Coverage-and-Contract-Inventory.md).
- [Machine-readable evidence](reports/data/OpenSTAAD-Table-2024-2026-Comparison.json).
- [Remaining-domain review index](reports/OpenSTAAD-Remaining-Adapters-Review-Index.md).

Implement the foundations and domain workstreams in the plan after approval. Preserve wrapper.StaadVersion routing, status/data result classes, descriptive parameters, complete XML IntelliSense documentation, internal-only COM references and per-version native qualification. No public ref/out or tuples are planned. Raw binding presence is not an operation-readiness guarantee.
