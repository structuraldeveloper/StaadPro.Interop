# Porting continuation audit

**Checked:** 27 October 2026. **Active branch:** main, based on cf46e65.

The interrupted parent-branch increment contains all 25 selected functions: 17 Commands methods reaching sixteen native analysis-command names and eight Support foundation/assignment methods. No function from that selected scope remains a placeholder. Support reaches 27/35 native routes; Commands reaches 16/17 active documented routes, excluding the deprecated eighteenth entry.

## Completed verification

- Passed all 6,320 offline tests; 284 cases cover this increment.
- Rebuilt and reran all 284 targeted cases after reconciling documentation, including matching interface/adapter XML and contract-index descriptions.
- Compiled and executed all 50 new XML examples with mutations disabled. The consumer build reported zero warnings/errors and no native fixture calls.
- Reviewed native packet ordering, by-value array transport, acknowledgement decoding, source defaults, input validation, binding replacement/disposal and original exception propagation.
- Reconciled the guide, roadmap, coverage matrices and machine-readable counts. Corrected stale negative-flag XML wording and previous validation/pending-name records. Parsed the updated package-verification script and checked whitespace.
- Kept the main reference read-only; recorded inspected source hashes in the [contract index](../contracts/CommandsAndSupportPortContractIndex.json).

Managed fixtures establish the managed contracts. Live COM marshaling, native model effects and engineering qualification remain separate acceptance work. No real model was mutated or solver invoked.

## Work preserved and work still pending

The API-usability branch remains at 5382f52. Its Property migration is committed, with its recorded 6,199-test and 640-example validation; this audit did not rerun that separate branch. Its subsequent Output, Support/Root, Geometry and Load return-contract migrations are still pending. Returning to main did not merge or discard that work.

The existing NuGet archive/version remains unchanged. Yesterday's parent-branch additions were not yet distributed in a new package or committed/pushed. Neither action is claimed as complete by this audit.

Extended Commands steel-catalog work, eight Support contract gates, thirteen Output gates, nine Property gates and fifteen supplemental Property candidates remain explicitly distinguished from the completed increment. Parked Load work continues to require separate explicit approval.

## Next authorized increment

Following completion of this audit, implement ten Design methods: seven adaptations of the main source's common brief/assignment/parameter-query workflows, one design-code query and two ordered query-batch overloads. The extended GROUP route and Chinese steel/version-exclusive functions remain outside this increment. See [Design porting](../Design-Porting.md).
