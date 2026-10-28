# Table lifecycle and cell typography

Implemented 28 October 2026. This increment ports **ten existing main-project methods** after the first ten [report/table foundations](Table-Report-Foundations.md). The read-only main source is unchanged. `IOSTable` and the single non-partial `OSTableAdapter` now expose **20 of 29 main-interface overloads**, reaching **20 of 24 documented native names**. Nine source overloads and four native names remain.

## Exact contracts and differences

All new functions return the existing immutable `TableOperationResult`. Each native function is void, so normal completion gives `Status == InvokedWithoutAcknowledgement`, `IsSuccessful == false`, and `NativeReturnCode == null`. This does not mean failure; it means STAAD provided no success flag. Post-call binding failure or a native exception can follow actual changes. No automatic save, retry, rollback, new STAAD instance, or inferred effect is added.

| Managed signature | Native call and usage distinction |
| --- | --- |
| `TableOperationResult DeleteReport(int reportId)` | `DeleteReport`: Unlike DeleteTable, this targets the entire report and its contained tables. This is destructive: no backup, confirmation dialog, replacement report or rollback is provided by this library. The supplied ReportId remains request identity in the receipt and does not prove the report still exists. No implicit save or model-file deletion occurs. |
| `TableOperationResult DeleteTable(int reportId, int tableId)` | `DeleteTable`: Unlike DeleteReport, this targets a single table and does not request deletion of the containing report. The library does not infer success from disappearance, recycle IDs, create a replacement table or save automatically. A void-completion receipt retains the requested IDs, not a confirmed surviving object. |
| `TableOperationResult RenameTable(int reportId, int tableId, string newName)` | `RenameTable`: Unlike AddTable, this requests renaming rather than creating a table or changing its dimensions. Exact nonblank text is forwarded without trimming; duplicate-name and length rules remain native. IDs are not recomputed or inferred. Unlike SetColumnHeader and SetRowHeader, this changes the table name, not header text. |
| `TableOperationResult ResizeTable(int reportId, int tableId, int rowCount, int columnCount)` | `ResizeTable`: Unlike AddTable, this targets an existing table. Counts are requested total dimensions, not increments or cell coordinates. Bentley documents increasing dimensions; shrinking, cell preservation and native capacity limits are not guaranteed. No dimension getter exists in this managed increment, so positivity is validated but growth against current dimensions is not independently checked. No clamping, automatic table recreation or save occurs. |
| `TableOperationResult SaveAllReports()` | `SaveReportAll`: Unlike SaveReport, this is not scoped to one report; unlike SaveTable, it is not scoped to one table. Calls the main-source native target SaveReportAll exactly once. ReportId and TableId are null because no single identity was supplied. This does not enumerate or save reports in other STAAD application instances, save the model through Root, or establish file-system durability. |
| `TableOperationResult SaveTable(int reportId, int tableId)` | `SaveTable`: Unlike SaveReport, this requests one table rather than the whole report and all of its tables; unlike SaveAllReports, it does not request saving every report. The actual native storage scope and dialogs remain STAAD responsibilities. No Root model save, report enumeration or persistence read-back is added. |
| `TableOperationResult SetCellTextBold(int reportId, int tableId, int rowNumber, int columnNumber)` | `SetCellTextBold`: Unlike SetCellValue, this changes formatting rather than writing text. Unlike SetCellTextItalic or SetCellTextUnderline, it requests bold. The native operation exposes no Boolean toggle or documented inverse: this method requests bold, not a toggle or clearing of the style. It does not resize the table, set a numeric value or save automatically. |
| `TableOperationResult SetCellTextItalic(int reportId, int tableId, int rowNumber, int columnNumber)` | `SetCellTextItalic`: Unlike SetCellTextBold and SetCellTextUnderline, this requests italic. It changes cell formatting rather than body text, headers, unit labels or model geometry. No Boolean disable/toggle option is exposed by the native call. No cell-selection side effect is requested by this wrapper, and no automatic save or formatting read-back occurs. |
| `TableOperationResult SetCellTextUnderline(int reportId, int tableId, int rowNumber, int columnNumber)` | `SetCellTextUnderline`: Unlike SetCellTextBold and SetCellTextItalic, this requests underlining rather than a different emphasis. The native operation supplies no Boolean toggle or documented inverse; it does not offer style removal. Body text is not rewritten and no automatic save, resize, unit conversion or formatting read-back occurs. |
| `TableOperationResult SetCellTextSize(int reportId, int tableId, int rowNumber, int columnNumber, double fontSize)` | `SetCellTextSize`: Unlike ResizeTable, this changes font sizing rather than row/column counts. Unlike the main-source SetCellTextSizeForTable workflow, which remains a separate port, this targets one cell only. A finite positive Double is forwarded unchanged using the native Microsoft Word-equivalent size convention, not model input length units. No string formatting, rounding, clamping or automatic save occurs; native size/capacity restrictions remain authoritative. |

Report/table IDs are positive native identities, not collection indices. Cell rows and columns are **one-based**. Row/column dimensions are positive requested totals, not increments. Rename text is non-null/nonblank and rejects embedded NUL; otherwise exact text is preserved. Font sizes must be finite positive Double values using the native Word-equivalent convention, independent of model input length units. Native font and table limits remain authoritative; the wrapper does not invent an upper bound or guarantee table shrink support.

`SaveAllReports` deliberately records null ReportId and TableId and calls native `SaveReportAll` once. `SaveReport` retains one report ID; `SaveTable` retains report and table IDs. Deletion receipts retain requested IDs for diagnostics, not proof that the objects remain valid. Requery native data after destructive changes.

## Example

Use only against the acquired report context on the established COM apartment. The deletion calls are independently guarded so a formatting workflow does not automatically destroy its report.

```csharp
// With an acquired wrapper and report/table IDs obtained through creation:
var table = wrapper.Table;
if (table == null) throw new System.InvalidOperationException("Table interface unavailable.");
int reportId = 11; // replace with an actual native report ID
int tableId = 7;   // replace with an actual table ID in that report
table.RenameTable(reportId, tableId, "Design forces");
table.ResizeTable(reportId, tableId, 6, 4); // requested total dimensions; choose growth
table.SetCellTextBold(reportId, tableId, 1, 2);
table.SetCellTextItalic(reportId, tableId, 1, 2);
table.SetCellTextUnderline(reportId, tableId, 1, 2);
table.SetCellTextSize(reportId, tableId, 1, 2, 12.5);
var savedTable = table.SaveTable(reportId, tableId);
var savedAll = table.SaveAllReports();
System.Console.WriteLine(savedAll.Status); // InvokedWithoutAcknowledgement

bool removeReportData = false;
if (removeReportData)
{
    table.DeleteTable(reportId, tableId);
    table.DeleteReport(reportId);
}
```

Bold/italic/underline methods request one native style each. They expose no Boolean toggle or inverse because the inspected native functions provide none. They do not write body text or provide a formatting read-back. Save requests do not call Root model-save operations or enumerate other STAAD application instances.

## Reuse, versions and verification

Reused `TableOperationResult`, `TableOperationStatus`, `TableService`, positive-ID/cell/text validation, the existing captured-binding gate and permanent native-signature fixture. No new public data type, enum, service family, partial adapter, tuple or public ref/out parameter was needed. Source signatures, call sites, source hashes and all three native contracts are recorded in the [expanded contract index](contracts/TableFoundationContractIndex.json).

Installed 2024/2025/2026 help declarations match for all ten calls; passive 2025/2026 metadata corroborates argument order and by-value slots. The common implementation remains shared. A 2024 vendor ABI was not inspected, and source coverage does not certify native UI readiness, licensing, COM marshaling, storage persistence or every servicing build.

Passed **6,780 offline tests**, including **332 Table cases** (145 additional cases in this increment). Compiled and executed **1,262 installed-package XML examples**, including all **forty Table examples**, and verified an isolated lifecycle/formatting consumer workflow. All twenty Table methods have matching interface/concrete XML documentation and guarded examples. Regression checks exercise packets, one-based coordinates, scoped receipts, void evidence, disposal/replacement, caller thread and original exceptions; new font checks cover nonfinite/nonpositive inputs and culture-independent Double forwarding. Native model/report mutation was not performed by offline verification.

## Remaining main-source ports

Nine overloads remain: five `SetCellTextColor` overloads (RGB integers, WPF Color, text, packed ARGB and alpha/R/G/B bytes), `SetCellTextColorHsl`, `SetCellTextHorizontalAlignment`, `SetCellTextVerticalAlignment` and `SetCellTextSizeForTable`. They reach the remaining four native names: SetCellTextColor, SetCellTextHorzAlignment, SetCellTextVertAlignment and SetCellTextSizeAll.

Finish existing main-source methods before adding missing native-only APIs. Parked Load and supplemental Property review/approval requirements remain in force.
