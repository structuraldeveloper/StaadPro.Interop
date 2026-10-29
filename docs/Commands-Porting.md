# Commands implementation status and porting guide

**Implementation checkpoint, 27 October 2026:** IOSCommands / OSCommandsAdapter now expose seventeen analysis-command methods reaching sixteen active native names. Steel-command creation/catalog remains pending. See [current contracts and verification](Commands-and-Support-Porting.md). The original review below is historical evidence.


**Reviewed:** 23 October 2026. IOSCommands / OSCommandsAdapter remain binding-only placeholders. No typed Commands functions were implemented by this review.

The installed 2024/2025/2026 manuals contain 18 / 18 / 18 native names respectively. The main source contains 21 interface overloads backed by 18 distinct native names; this does not establish live compatibility.

- [Cross-version architecture and ordered implementation plan](reports/OpenSTAAD-Commands-Multi-Version-Architecture-and-Plan.md).
- [Every native contract, parameter, return and managed proposal](reports/OpenSTAAD-Commands-API-Coverage-and-Contract-Inventory.md).
- [Machine-readable evidence](reports/data/OpenSTAAD-Commands-2024-2026-Comparison.json).
- [Remaining-domain review index](reports/OpenSTAAD-Remaining-Adapters-Review-Index.md).

Implement the foundations and domain workstreams in the plan after approval. Preserve wrapper.StaadVersion routing, status/data result classes, descriptive parameters, complete XML IntelliSense documentation, internal-only COM references and per-version native qualification. No public ref/out or tuples are planned. Raw binding presence is not an operation-readiness guarantee.
