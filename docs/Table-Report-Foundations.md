# Report and table foundations

**Completion update:** all 29/29 main-source Table overloads and all 24/24 reviewed native names are implemented. The checkpoint below records an earlier increment; [current contracts](Table-Completion-and-View-Foundations.md) include color, alignment and table-wide sizing.

**Current coverage:** after the [lifecycle/typography increment](Table-Lifecycle-and-Formatting.md), twenty of 29 source overloads and twenty of 24 native names are implemented. Nine source overloads remain. The foundation checkpoint below records the original ten-method increment.

Implemented 28 October 2026. These **ten methods** adapt existing functions in the read-only main `IOSTable` / `OSTableAdapter`; all ten have a matching main-source implementation. They reach **10/24 documented Table native names**. Nineteen of the main interface's 29 overloads remain pending. Missing native-only APIs follow completion of the main-source porting work.

## Contracts and differences

Rows and columns are **one-based**: 1 is the first row/column. The main interface's zero-based XML wording conflicts with all three reviewed Bentley references; the implementation forwards coordinates directly and the new documentation follows the native contract. The source parameter called `cellId` actually identifies a table and is exposed as `tableId`. No public reference parameters, tuples, proprietary interop assemblies or partial adapters were added.

Counts and cell strings return data directly: these native functions supply no ordinary failure flag to retain. `GetCellValue` fills an internal reference output; an explicit empty string is valid, while an untouched/null/non-string output throws. This avoids treating an unchanged initial empty buffer as successful native data. Counts accept only nonnegative integral Int32-range responses; no string/Boolean/floating-point coercion occurs.

Creation and mutation methods return the shared immutable `TableOperationResult`. Positive creation IDs mean `Succeeded`; zero is `NativeFailure`, with no fabricated created ID. Negative/malformed IDs throw. Void edit/save calls return `InvokedWithoutAcknowledgement`, with `IsSuccessful == false` and `NativeReturnCode == null`. Normal return does not prove application effects or persistence.

| Managed contract | When to use and differences |
| --- | --- |
| `TableOperationResult CreateReport(string title)` | Unlike AddTable, this creates a report, not a table. Native zero is failure and yields ReportId null; a positive ID is retained without deriving it from the report count. Does not add tables or save. Use the returned ReportId for subsequent operations. |
| `TableOperationResult AddTable(int reportId, string tableName, int rowCount, int columnCount)` | Unlike CreateReport, this creates only a table within the supplied report. Native zero is failure and yields TableId null; a positive ID is retained, scoped to ReportId. Row/column counts are positive dimensions, not maximum indices. No headers, values or persistence are added automatically. |
| `int GetReportCount()` | Unlike GetTableCount, this counts reports rather than tables within a report. Zero is a valid empty observation, not a failure flag. A count does not enumerate report IDs or establish that IDs are contiguous. No report is created, selected or saved. |
| `int GetTableCount(int reportId)` | Unlike GetReportCount, this is scoped to the supplied native report ID. Zero is accepted as the returned count; it does not independently prove that the report exists. No undocumented not-found status is inferred. Table IDs must come from creation or another native observation, not count arithmetic. |
| `string GetCellValue(int reportId, int tableId, int rowNumber, int columnNumber)` | Unlike SetCellValue, this observes rather than writes. Native GetCellValue returns void and fills an internal reference output; the public API returns its validated string directly. An explicitly empty native string is valid and is distinct from a null, untouched or non-string output, which throws. Coordinates are one-based, correcting the reference interface's zero-based XML wording; no offset is added internally. No bounds, report existence or numeric/unit conversion is inferred. |
| `TableOperationResult SetCellValue(int reportId, int tableId, int rowNumber, int columnNumber, string value)` | Unlike SetColumnHeader and SetRowHeader, this writes a body cell. Empty text is allowed for clearing. Exact text is forwarded with no numeric parsing, trimming or unit conversion. Coordinates are one-based; no offset or automatic resizing occurs. Native void completion produces InvokedWithoutAcknowledgement, not successful read-back or persistence evidence. |
| `TableOperationResult SetColumnHeader(int reportId, int tableId, int columnNumber, string headerText)` | Unlike SetCellValue, this addresses header metadata for the column, not a body row. Unlike SetColumnUnitString, this supplies the column title rather than its unit label. Empty text clears the title. No automatic value formatting or save occurs; native void completion is unacknowledged. |
| `TableOperationResult SetColumnUnitString(int reportId, int tableId, int columnNumber, string unitString)` | Unlike SetColumnHeader, this supplies the unit label. It does not change model input units, convert stored values or validate dimensional consistency. Empty text clears the label. Use Root unit setters for native model input-unit changes. Native void completion does not prove persistence. |
| `TableOperationResult SetRowHeader(int reportId, int tableId, int rowNumber, string headerText)` | Unlike SetColumnHeader, this labels a row rather than a column. Unlike SetCellValue, it does not address a body column or replace a body cell. Empty text clears the label. No automatic value formatting, resizing or save occurs; native void completion is unacknowledged. |
| `TableOperationResult SaveReport(int reportId)` | This is the main-source SaveReport workflow, scoped to one report and its contained tables. It does not save the STAAD model through Root, save all reports or promise file-system durability. Native void completion records InvokedWithoutAcknowledgement, with no read-back or success flag; native dialogs and storage behavior remain STAAD responsibilities. |

## Example

The following changes a report in the acquired model. Run on the established STAAD COM apartment with the required native report context. Report/table creation may leave native state even if a later call fails.

```csharp
using StaadPro.Interop.Models;
using StaadPro.Interop.Enums.Table;

// With an already acquired OpenStaadWrapper named wrapper:
var table = wrapper.Table;
if (table == null) throw new System.InvalidOperationException("Table interface unavailable.");
var report = table.CreateReport("Member summary");
if (!report.IsSuccessful) return;
var created = table.AddTable(report.ReportId.Value, "Forces", 3, 2);
if (!created.IsSuccessful) return;
int reportId = report.ReportId.Value;
int tableId = created.TableId.Value;
table.SetColumnHeader(reportId, tableId, 2, "Axial force");
table.SetColumnUnitString(reportId, tableId, 2, "kN");
table.SetRowHeader(reportId, tableId, 1, "Member 17");
table.SetCellValue(reportId, tableId, 1, 2, "42.50");
string observed = table.GetCellValue(reportId, tableId, 1, 2);
var save = table.SaveReport(reportId);
System.Console.WriteLine(observed);
System.Console.WriteLine(save.Status); // InvokedWithoutAcknowledgement
```

The unit string is a display label. It neither changes model input units nor converts values. Native counts do not enumerate IDs; never infer IDs from a count. Names/text are forwarded unchanged: titles/table names must be nonblank, other text may be empty, all text must be non-null and contain no embedded NUL. Report/table IDs, dimensions and coordinates must be positive. No upper-bound, context or existence check is fabricated. Native errors propagate unchanged.

## Architecture and lifecycle

Signatures reside in `IOSTable`; implementations reside in the single non-partial `OSTableAdapter`; a common internal `Services/Table/TableService` performs native calls. Existing `NativeValues` readers handle strict native scalar/text validation. The main source contains no result class suitable for distinguishing creation acknowledgement from void report requests, so one reusable result class and one status enum were added under the Table folders.

The ten reviewed contracts match across installed 2024/2025/2026 help. There is no evidence requiring a duplicate service per year. Passive 2025/2026 metadata corroborates argument order and the internal by-reference final `GetCellValue` slot; no installed 2024 ABI was inspected. All operations use the captured RawTable object and caller thread, serialize within an adapter, and check binding before/after dispatch. External UI edits/raw COM calls and other adapter instances are not locked out. Reacquire after refresh. No automatic model acquisition, thread-pool call, retry, rollback, conversion or save occurs.

## Verification and pending work

Passed **6,635 offline tests**, including **187 Table cases**, and **1,242 installed-package XML examples**, including all twenty Table examples and an isolated report write/read/save workflow. Managed verification does not certify live COM, native UI/licensing or report persistence.

Regression tests cover all ten native packets, identities/index bases, creation failure, void evidence, exact/empty cell strings, untouched outputs, malformed native values, preflight rejection, caller-thread execution, binding replacement/disposal, original exceptions and a complete managed-fixture report workflow. Interface and adapter XML documents match; packaged examples compile and execute in an isolated installed-package consumer with mutation examples guarded. These checks do not qualify live COM, UI/report readiness, license availability, report storage or every servicing build.

The original lifecycle/typography follow-up is now implemented. Remaining main-source operations are the nine color, alignment and table-wide font-size overloads listed in the [current guide](Table-Lifecycle-and-Formatting.md). View and IDEA integration remain placeholders. Load expansion and supplemental Property work retain their earlier approval checkpoints.

See the [Table porting status](Table-Porting.md), [native contract inventory](reports/OpenSTAAD-Table-API-Coverage-and-Contract-Inventory.md), and [source/contract index](contracts/TableFoundationContractIndex.json).
