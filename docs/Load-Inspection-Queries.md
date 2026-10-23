# Load inspection and contribution queries

## Implemented surface

Added 15 managed operations beyond the completed private-source reference port. IOSLoad and OSLoadAdapter expose **135 overloads**: 115 reference signatures, five existing conveniences and these 15 native-expansion queries. The single adapter delegates to an internal shared LoadInspectionService; no adapter partial classes, public ref/out parameters or tuples were introduced.

| Native function | Managed method | Data on success |
| --- | --- | --- |
| `GetActiveLoad` | `LoadQueryResult<int> GetActiveLoadCaseNumber()` | `int` |
| `GetLoadListCount` | `LoadQueryResult<int> GetLoadListCount()` | `int` |
| `GetLoadCountInLoadList` | `LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex)` | `int` |
| `GetLoadsInLoadList` | `LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults = 100000)` | `IEnumerable<int>` |
| `GetLoadTypeCount` | `LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind)` | `int` |
| `GetListSizeForLoadType` | `LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex)` | `int` |
| `GetLoadItemsCount` | `LoadQueryResult<int> GetLoadItemCount(int loadCaseId)` | `int` |
| `GetLoadItemType` | `LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex)` | `LoadItemKind` |
| `IsDynamicLoadIncluded` | `LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId)` | `bool` |
| `GetNoLoadFactorInRepeatLoad` | `LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex)` | `int` |
| `GetRepeatLoadByIndex` | `LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults = 100000)` | `IEnumerable<LoadFactorContribution>` |
| `GetNotionalLoadCount` | `LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId)` | `int` |
| `GetNoLoadFactorDirectionInNotionalLoad` | `LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex)` | `int` |
| `GetNotionalLoadByIndex` | `LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults = 100000)` | `IEnumerable<NotionalLoadContribution>` |
| `GetReferenceLoadCount` | `LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId)` | `int` |

## Result contract

Every operation returns `LoadQueryResult<T>`. Inspect `IsSuccessful` before `Data`; accessing Data for any other status throws InvalidOperationException. Native transport/binding errors propagate, and malformed responses throw rather than becoming invented success/failure codes.

| Status | Meaning |
| --- | --- |
| Success | Data is available, including valid false predicates and documented zero counts/empty snapshots. |
| NativeFailure | A documented native error was observed. Dynamic -1 is an error, not Boolean true. |
| NotFound | A list/item/repeat index was absent according to the documented/preflight evidence. |
| ActivationFailed | SetLoadActive rejected the primary-case selection; no requested query followed. |
| StateChanged | Observed activation/counts changed during retrieval; no partial payload is exposed. |
| LimitExceeded | Count exceeded maximumResults before output allocation; no silent truncation. |
| EmptyOrUnavailable | GetLoadTypeCount returned zero, whose documented meaning cannot prove an empty inventory. |

RequestedNativeFunction identifies the request. NativeFunction and NativeReturnCode describe the decisive operation, including preflight/activation failures. NativeReturnCode is null for actual Boolean returns; it is not an invented normalized code. Empty paired readers skip native fetch and identify their successful count operation. Diagnostic carries additional failure or ambiguity context.

## Case scope, indices and native codes

- GetActiveLoadCaseNumber returns only the observed positive number. It cannot distinguish primary/reference families with overlapping numbers; the wrapper has no cached active-case identity.
- Native LOAD LIST indices are one-based. GetLoadListCaseCount/GetLoadListCases preflight the list count and detect changed list/member counts. LOAD LIST records are distinct from envelopes. Returned IDs retain order/duplicates and do not acquire an inferred family.
- Item and command-kind indices are zero-based. LoadItemKind retains unknown positive codes; use Enum.IsDefined to test recognition. Code 3312 explicitly retains Bentley’s ambiguous region-nodal/partial-plate label. LoadCommandKind is a separate table: codes 3260 and 3310 cover multiple command forms. Neither enum is the engineering LoadType or coarse managed LoadItemType.
- Repeat indices are explicitly one-based in the manual. Notional examples address command 1; the managed convention is positive, one-based. The notional count text also uses the word factor. Its interpretation as the paired output size and the acquired build’s index behavior remain live qualification requirements, not offline-test certification.
- Active-case methods require a positive primaryLoadCaseId. They use SetLoadActive and check GetActiveLoad before and after inspection. They do not activate a reference case. The requested target remains active; prior activation is not restored on success or failure. Direct case-number queries do not activate cases.
- Repeat contributions retain duplicate positive primary case numbers and finite dimensionless signed/zero factors. Notional contributions retain their original signed ID: positive means primary and negative means reference; LoadCaseId is positive and CaseType carries the family separately. Zero and Int32.MinValue IDs are rejected. Notional direction uses a dedicated global X/Y/Z enum with codes 1/2/3.

## Retrieval and lifetime rules

The three collection readers eagerly return read-only owned snapshots as IEnumerable, with immutable contribution records. Internal buffers use Int32 IDs/directions and Double factors; factors start as NaN, IDs/directions as invalid zero. Successful unwritten, nonfinite, wrong-rank, wrong-element-type or wrong-length outputs are rejected. Valid one-dimensional nonzero lower bounds are copied correctly.

Paired reads compare sizing counts with returned counts and a post-fetch count. Collection readers accept a positive maximumResults (default 100000), checked before output buffers are allocated. A zero contribution count returns a successful empty snapshot without fetch; this does not erase the separate ambiguous-zero contract of GetLoadTypeCount.

Calls run synchronously on the caller’s owning COM apartment. New queries on one adapter serialize with each other; legacy methods, other adapters, raw calls and external clients are not globally locked. Detected count/activation changes produce StateChanged, but same-count replacements, change-away-and-back races and an external family switch sharing the same case number cannot be excluded. These observations are not atomic native transactions.

The service checks the captured RawLoad/root binding before and after each request and at native access boundaries. Reacquire wrapper.Load after refresh/rebind or disposal; stale queries throw. No solver, load assignment, implicit save, automatic retry, rollback or units conversion occurs. Count/index operations are dimensionless and factors remain dimensionless.

## Usage

~~~csharp
using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Enums.Loads;

// load is an acquired IOSLoad on its owning COM thread.
var commandCount = load.GetLoadCommandCount(17, LoadCommandKind.Nodal);
if (commandCount.IsSuccessful) Console.WriteLine(commandCount.Data);
else Console.WriteLine(commandCount.Status + ": " + commandCount.Diagnostic);

var contributions = load.GetNotionalContributions(17, 1, maximumResults: 10000);
if (contributions.IsSuccessful)
{
    foreach (var item in contributions.Data)
        Console.WriteLine(item.CaseType + " " + item.LoadCaseId + " " + item.Factor + " " + item.Direction);
}
else Console.WriteLine(contributions.Status);
~~~

## Coverage and verification

Source native coverage is **88/129** documented names: 67 direct adapter calls, 15 additional common-service targets and six member-entity targets. Of the remaining **41** names, one has an existing creation alternative, one is derivable, four are partial and **35 have no managed equivalent**. Three metadata-only candidates remain separate. Thus 39 documented native contracts retain missing scope, without implying 39 required public methods.

Passed **2,448 offline tests**, including **189 new inspection cases**. Compiled and executed all **30 new XML examples** on interface and concrete adapter consumers against managed stand-ins. Existing API inventory checks were updated to 135 overloads. A validation-only CI archive passed isolated PackageReference restore/build/runtime verification with all 370 selected XML examples, including the 30 new examples. The verifier also retains the prepared 120-overload archive path (340 examples). The validation archive is separate from the prepared distribution preview.

The shared service follows matching installed 2024/2025/2026 contracts; no artificial per-year implementation was added. Live native marshaling, index semantics, active-family behavior and engineering qualification remain pending. This source increment does not replace or rebuild the prepared 1.0.0-preview.20261023 archive, which predates these 15 queries.

See the [cross-version plan](reports/OpenSTAAD-Load-Multi-Version-Architecture-and-Plan.md), [full contract inventory](reports/OpenSTAAD-Load-API-Coverage-and-Contract-Inventory.md) and [semantic coverage audit](reports/OpenSTAAD-Load-Managed-Capability-Audit.md).
