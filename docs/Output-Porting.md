# Output review and implementation status

**Updated:** 22 October 2026

Output currently has a real native binding and a binding-only IOSOutput / OSOutputAdapter placeholder. It exposes ComObject and the owning Wrapper, with **zero typed domain methods**. No Output implementation was added by this review.

The existing Root member-force service separately calls two native Output names: AreResultsAvailable and GetMemberEndForces. These are reusable internal functionality, not completed IOSOutput methods.

## Cross-version review

- [Output comparison, architecture and implementation sequence](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md)
- [Complete native contracts, private-source overloads and reuse inventory](reports/OpenSTAAD-Output-API-Coverage-and-Contract-Inventory.md)
- [Machine-readable comparison and fingerprints](reports/data/OpenSTAAD-Output-2024-2026-Comparison.json)

Reviewed all installed 2024/2025/2026 Output help sections and Output-owned steel-result functions filed under Design. Counts are **59 / 60 / 60 native names**, with 59 common and 60 in the union. GetMaxBeamStresses is newly documented in 2025; four common contracts need direction/layout policy verification. Normalized 2025/2026 declarations, contracts and examples match.

The strictly read-only main project exposes **86 managed overloads**, implemented by 86 public adapter methods backed by **17 native targets**. Thirty-three return Task; two expose out outputs that should become named results in this repository. Four offset-aware/physical-joint overloads depend on an unported Property operation. The contract annex records every signature, dependency and required adaptation.

Both installed 2025/2026 vendor IOSOutputUI interfaces contain 72 matching metadata methods. Thirteen additional unit queries appear in all three native class listings without detailed contracts, with a reference/value mismatch against interop metadata; they need contract discovery, and GetMissingMassParticipationFactors is documented but absent from those interfaces. These evidence gaps require acquired-server verification, not version guesses.

## Recommended first increment

Approve the managed status/data contracts, then implement the minimum shared binding/decoder foundation and five native names in O2: qualified analysis availability, nodal displacement, support reaction, static check and member-end forces. Reuse existing force models and Root validation. Later phases add plate, buckling/design, member extrema, solid/foundation, modal/nonlinear, time-history and optional operations.

Use the acquired wrapper.StaadVersion internally. Keep one non-partial adapter, no new public ref/out or tuple outputs, and complete XML documentation for interfaces, adapters and result models. Native ambiguity must remain explicitly unverified until resolved.

This documentation review did not acquire STAAD objects, execute native queries, change the target framework, run library tests or build/publish a package. The current net481 package and previously published preview are unchanged.
