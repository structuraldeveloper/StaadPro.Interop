# Support implementation status

**Updated:** 26 October 2026

Implemented **19 common documented Support routes**, leaving **16/35 pending**. The new surface covers supported-node discovery, typed support information and GUIDs, foundation counts/readback, explicit fixed/pinned/fixed-but creation, assignment, unassignment, deletion and void GUID invocation. One internal shared service validates operation-specific counts/types/flags, owned bounded buffers and binding lifetime. Reused existing Releases/Springs packets and ported the main-project enums without private UI dependencies. See [all 19 contracts](Support-Foundations.md).

Release build passed with zero warnings/errors and **5,523 offline tests** (170 new cases). Isolated documentation/package verification covers **1,052 selected XML examples**, including all 92 Output and 38 Support examples. Managed fixtures do not certify live 2024–2026 COM or engineering behavior.

The private main interface has 33 overloads backed by 18 native targets. This increment follows documented native contracts and reuses compatible source models; it does not claim all private convenience overloads are ported. Native-name coverage and managed overload parity remain separate counts.

## Shared service and reuse

One non-partial adapter delegates to Services/Support/SupportService. Existing Releases/Springs packets and strict transport readers are reused; main-project SupportType, FoundationSupportDirection and FoundationSpringType vocabulary is preserved without private UI dependencies. Named immutable snapshots retain DOF states, foundation parameters and request identity. No public ref/out or tuple is introduced.

## Native qualification and remaining work

Sixteen documented routes remain, including later foundation mutations and the reviewed name/extended-info/plate-mat/inclined contracts. Matching declarations do not certify live marshaling. The native RemovePlateMatFromPlate method has two metadata arguments versus one documented argument and remains unimplemented. No adapter is marked complete merely because RawSupport exists.

The prepared 1.0.0-preview.20261026.1 distribution archive is unchanged; this source increment is validated separately. The main project, wrapper/providers and target framework are unchanged.

- [All 19 public contracts and comparative usage](Support-Foundations.md)
- [Cross-version architecture and route matrix](reports/OpenSTAAD-Support-Multi-Version-Architecture-and-Plan.md)
- [Complete native contracts and source reuse inventory](reports/OpenSTAAD-Support-API-Coverage-and-Contract-Inventory.md)
- [Machine-readable comparison](reports/data/OpenSTAAD-Support-2024-2026-Comparison.json)
