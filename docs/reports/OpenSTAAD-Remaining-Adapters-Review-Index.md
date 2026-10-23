# Remaining OpenSTAAD adapter reviews and implementation sequence

**Reviewed:** 23 October 2026<br>
**Baseline:** main at 1d8285ce66c95c60da55530d4c721fb7274893da<br>
**Scope:** Completed the remaining domain comparisons and implementation plans; no domain functions were implemented or packaged by this review.

## 1. Review coverage

| Domain | Installed help names, 2024 / 2025 / 2026 | Main managed interface overloads | Current status / report |
| --- | --- | --- | --- |
| Root | 39 reviewed native functions | 23 reference methods | Reviewed managed coverage complete; native qualification remains. [Root plan](OpenSTAAD-Root-Multi-Version-Architecture-and-Plan.md) |
| Geometry | 135 / 135 / 135, including one deprecated summary | 143 | Reference parity complete; wider native and version-policy work remains. [Geometry plan](OpenSTAAD-Geometry-Multi-Version-Architecture-and-Plan.md) |
| Load | 129 / 129 / 129 | 115 | Reference parity complete, with five additional conveniences; wider native coverage remains. [Load plan](OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md) |
| Output | 59 / 60 / 60 | 86 | Binding-only placeholder. [Output plan](OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md) |
| Property | 204 / 209 / 209 | 103 | Binding-only placeholder; deprecated/reserved/private entries distinguished in its report. [Property plan](OpenSTAAD-Property-Multi-Version-Architecture-and-Plan.md) |
| Support | 35 / 35 / 35 | 33 | Binding-only placeholder. [Support plan](OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md) |
| Design | 31 / 45 / 45 | 7 | Binding-only placeholder. [Design plan](OpenSTAAD-Design-Multi-Version-Architecture-and-Plan.md) |
| Commands | 18 / 18 / 18, including one deprecated summary | 21 | Binding-only placeholder. [Commands plan](OpenSTAAD-Commands-Multi-Version-Architecture-and-Plan.md) |
| Table | 24 / 24 / 24 | 29 | Binding-only placeholder. [Table plan](OpenSTAAD-Table-Multi-Version-Architecture-and-Plan.md) |
| View | 67 / 67 / 67 | 70 | Binding-only placeholder. [View plan](OpenSTAAD-View-Multi-Version-Architecture-and-Plan.md) |
| IDEA StatiCa | Not a native OpenSTAAD group | 24 | Managed integration placeholder. [Integration plan](OpenSTAAD-IDEA-StatiCa-Architecture-and-Porting-Plan.md) |

These are domain report scopes, not interchangeable measures. Managed overloads, native names, class helpers, reserved entries and metadata-only functions must not be summed as a single “implemented API” total. In particular, Output-owned functions filed under the Design steel help page are counted in Output.

All remaining placeholder domains now have comparison/source inventories and architecture plans. That completes the **review series**, not implementation of those domains or native qualification of existing ports.

## 2. Findings from the remaining review

| Area | Finding | Planned response |
| --- | --- | --- |
| Design additions | Fourteen functions added in 2025, unchanged in 2026 | Acquired optional capability interfaces; no cast bypass through the common adapter. |
| Design legacy arity | Four Chinese steel result/report calls lose the explicit code argument in 2025 | Narrow internal legacy/later policies after acquired-interface verification. |
| Design selectors | 107 parameter selectors become 136; fields shift and a label is duplicated | Logical descriptors plus verified profile maps; no blanket integer offset. |
| Design assignment grammar | GROUP documentation differs between 2024 and later references | Isolate and qualify grammar; preserve native failure codes. |
| Commands | Matching contracts across all years; one deprecated entry | One common service, exclude the deprecated entry from new APIs. |
| Steel command catalog | 49 codes and 1,066 keyword/number occurrences; operand schemas incomplete | Reuse catalog/validator/marshaller; keep unqualified tuples provisional. |
| Table | Matching contracts; one-based rows/columns contradict source XML | Shared service with explicit one-based contracts and named table IDs. |
| View | Matching declarations; five editorial scale-text corrections | Shared service; no artificial year split. |
| View source defect | Scale model omits Bending Z and shifts later values | Full 14-slot mapping and validated status/count/fill behavior. |
| View export ABI | Manuals specify four arguments; inspected 2025/2026 metadata exposes three | Verify acquired dispatch shape and overwrite behavior; never retry with a different arity after an ambiguous write. |
| View missing private target | SetModeSectionPage absent from the main adapter | Plan a native-contract implementation beyond private parity. |
| IDEA StatiCa | Depends on unported Output/Property and conditional Support operations | Port prerequisites first; separate STAAD and IDEA-format qualifications. |

The four native domains reviewed here contain 140 names in 2024 and 154 in 2025/2026, with one deprecated Commands entry included. All their current typed domain methods remain pending. IDEA's 24 managed overloads are a separate integration surface.

## 3. Recommended implementation sequence

1. **Approve the managed contracts and shared conventions.** Reuse compatible result/validation/value readers, use descriptive parameters, named status/data classes and owned collections. Keep native references internal and document lifecycle/units. Avoid public ref/out and tuples.
2. **Implement Output foundations and Property orientation/offset prerequisites.** These enable reliable result extraction and coordinate transforms, including the later integration. Keep documented version differences isolated.
3. **Implement Support foundations and Commands.** Support enables supported-node/reaction workflows. Commands adds explicit analysis-command configuration; Root remains responsible for actual analysis execution.
4. **Implement Design common operations, then Chinese steel profiles and optional additions.** Start with audited private-source conveniences, but include every missing native operation in the approved workstreams.
5. **Implement Table and View.** Correct the one-based coordinate and 14-slot scale contracts before porting conveniences. Qualify UI/clipboard/export behavior independently.
6. **Implement IDEA models and pure transforms, then live extraction and mapping.** Proceed only after the feature-specific upstream operations are ready. Add equilibrium/closure audit and artifact delivery in the documented order.
7. **Complete controlled native acceptance and package release work for the implemented scope.** Reconcile coverage, compile XML examples, check package assets/dependencies, update README/release notes, build/install the preview, and publish only with accurate compatibility claims.

These are dependency priorities; independent domains can be approved separately. Do not wait for all native APIs to exist before delivering a tested useful subset, and do not describe a private-reference milestone as complete vendor API coverage.

## 4. Common architecture rules

Public function signatures belong in their interface, implementations in a single non-partial adapter, and coherent internal services/models in domain folders. Do not duplicate shared logic or create one service/result class for every scalar operation.

Use wrapper.StaadVersion for native profiles and preserve the acquired raw object's identity. ApplicationVersion is the host CLR assembly version. A raw binding, native member presence, successful invocation, license/context readiness and engineering correctness are separate observations.

Identical contracts use shared implementations. Only evidenced signature/semantic changes justify narrow profile policies. Runtime selection cannot remove statically declared C# members from IntelliSense; genuinely exclusive features require acquired optional interfaces.

Native void means dispatch-completion evidence unless a separate read-back establishes the effect. Mutation retries and rollback require explicit semantics; never guess a new model instance or manufacture a success result from untouched outputs.

## 5. Evidence and remaining gates

Each native report has a complete contract annex, all source overloads, per-year help links, passive 2025/2026 interop metadata and machine-readable fingerprints. The IDEA annex inventories every integration overload, option/model property and helper responsibility.

Installed-help and private-source hyperlinks are specific to this review environment. The contract tables and JSON evidence preserve the reviewed content for readers who do not have those local paths.

The main source project was strictly read-only. No COM activation, STAAD/IDEA modification, production-code edit or NuGet build was performed in this review. Documentation and metadata matching do not qualify marshaling, engineering behavior, every servicing build or modern .NET support.

Before implementation, approve public contracts and resolve the documented ambiguities. Before release, qualify the exact native profiles/features being shipped and update package-facing documentation to match the built artifact.

## 6. Documentation verification

Reconciled all 154 native names in the four remaining native-domain inventories and all 151 main-interface overloads across the five reviewed domains/integrations. Verified the 2024/2025/2026 function sets, native call targets, per-year help fingerprints and unchanged fingerprints of all 66 reviewed main-source/dependency files.

Validated all five JSON artifacts, all 2,335 local document/help/source links and applicable anchors/line references, Markdown table widths, template completion and code-fence balance across 18 Markdown files. Five Mermaid flowcharts are included. No production test run or native runtime certification is implied by these documentation checks.
