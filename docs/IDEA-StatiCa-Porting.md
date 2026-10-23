# IDEA StatiCa integration status and porting guide

**Reviewed:** 23 October 2026. IOpenStaadIdeaStatiCaAdapter remains a placeholder. No extraction, transformation, mapping, verification or export API is implemented.

The main project contains 24 public method overloads plus options, mapping/force models and artifact services. The integration depends on Geometry, Load, Output and Property, with Support required for requested reaction-aware paths. A native binding alone does not satisfy those dependencies.

- [Architecture, compatibility and ordered porting plan](reports/OpenSTAAD-IDEA-StatiCa-Architecture-and-Porting-Plan.md).
- [Complete source overload/model/option inventory](reports/OpenSTAAD-IDEA-StatiCa-API-Coverage-and-Contract-Inventory.md).
- [Machine-readable source evidence](reports/data/OpenSTAAD-IDEA-StatiCa-Source-Inventory.json).
- [Remaining-domain review index](reports/OpenSTAAD-Remaining-Adapters-Review-Index.md).

STAAD year profiles and IDEA mapping/import-format profiles are separate qualifications. Pure transforms and data models can be started after API approval; useful live extraction requires the pending upstream domains first. No wrapper or NuGet target change is proposed by this review.
