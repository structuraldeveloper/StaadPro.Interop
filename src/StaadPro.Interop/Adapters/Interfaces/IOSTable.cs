using System.Windows.Media;
using StaadPro.Interop.Enums.Table;
using System;
using StaadPro.Interop.Models.Table;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides typed report lifecycle, table editing, observation and basic cell typography operations.</summary>
    /// <remarks>Rows and columns are one-based. Creation acknowledgements and native void completion have distinct results. All 29 reviewed main-project Table overloads are ported. Creation acknowledgements and native void completion remain distinct from live native qualification.</remarks>
    public interface IOSTable : IOSBase
    {
        /// <summary>Gets the exact captured native Table object; callers must obey session lifetime and COM-thread rules.</summary>
        dynamic ComObject { get; }

        /// <summary>Creates a report container and returns its native creation evidence.</summary>
        /// <param name="title">Non-null, nonblank report title without embedded NUL characters. The exact text is forwarded; duplicate names and native length limits remain native responsibilities.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike AddTable, this creates a report, not a table. Native zero is failure and yields ReportId null; a positive ID is retained without deriving it from the report count. Does not add tables or save. Use the returned ReportId for subsequent operations.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.CreateReport("Design summary");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult CreateReport(string title);

        /// <summary>Adds a named, sized table to an existing report.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableName">Non-null, nonblank table name without embedded NUL characters; exact text is forwarded.</param>
        /// <param name="rowCount">Positive initial number of body rows. Native capacity and allocation limits remain authoritative.</param>
        /// <param name="columnCount">Positive initial number of body columns. Native capacity and allocation limits remain authoritative.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike CreateReport, this creates only a table within the supplied report. Native zero is failure and yields TableId null; a positive ID is retained, scoped to ReportId. Row/column counts are positive dimensions, not maximum indices. No headers, values or persistence are added automatically.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.AddTable(11, "Members", 3, 2);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult AddTable(int reportId, string tableName, int rowCount, int columnCount);

        /// <summary>Reads the current number of STAAD report containers.</summary>
        /// <returns>The validated nonnegative native count, including zero; IDs are not derived from this value.</returns>
        /// <remarks><para>Unlike GetTableCount, this counts reports rather than tables within a report. Zero is a valid empty observation, not a failure flag. A count does not enumerate report IDs or establish that IDs are contiguous. No report is created, selected or saved.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>var value = table.GetReportCount();
        /// System.Console.WriteLine(value);</code></example>
        int GetReportCount();

        /// <summary>Reads the current number of tables in one native report.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <returns>The validated nonnegative native count, including zero; IDs are not derived from this value.</returns>
        /// <remarks><para>Unlike GetReportCount, this is scoped to the supplied native report ID. Zero is accepted as the returned count; it does not independently prove that the report exists. No undocumented not-found status is inferred. Table IDs must come from creation or another native observation, not count arithmetic.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>var value = table.GetTableCount(11);
        /// System.Console.WriteLine(value);</code></example>
        int GetTableCount(int reportId);

        /// <summary>Reads the exact textual value of a one-based report-table cell.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableId">Positive native table ID within the specified report, obtained from AddTable; this parameter identifies a table, not a cell.</param>
        /// <param name="rowNumber">One-based body row/header number: 1 is the first row. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based body column/header number: 1 is the first column. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <returns>The exact native string, including an explicit empty string; no public reference parameter is required.</returns>
        /// <remarks><para>Unlike SetCellValue, this observes rather than writes. Native GetCellValue returns void and fills an internal reference output; the public API returns its validated string directly. An explicitly empty native string is valid and is distinct from a null, untouched or non-string output, which throws. Coordinates are one-based, correcting the reference interface's zero-based XML wording; no offset is added internally. No bounds, report existence or numeric/unit conversion is inferred.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>var value = table.GetCellValue(11, 7, 1, 2);
        /// System.Console.WriteLine(value);</code></example>
        string GetCellValue(int reportId, int tableId, int rowNumber, int columnNumber);

        /// <summary>Writes text to a one-based report-table cell.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableId">Positive native table ID within the specified report, obtained from AddTable; this parameter identifies a table, not a cell.</param>
        /// <param name="rowNumber">One-based body row/header number: 1 is the first row. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based body column/header number: 1 is the first column. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="value">Non-null exact cell text without embedded NUL characters. Empty or whitespace-only text is allowed; no numeric conversion is performed.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike SetColumnHeader and SetRowHeader, this writes a body cell. Empty text is allowed for clearing. Exact text is forwarded with no numeric parsing, trimming or unit conversion. Coordinates are one-based; no offset or automatic resizing occurs. Native void completion produces InvokedWithoutAcknowledgement, not successful read-back or persistence evidence.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellValue(11, 7, 1, 2, "42.5");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellValue(int reportId, int tableId, int rowNumber, int columnNumber, string value);

        /// <summary>Sets the text of a one-based column header.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableId">Positive native table ID within the specified report, obtained from AddTable; this parameter identifies a table, not a cell.</param>
        /// <param name="columnNumber">One-based body column/header number: 1 is the first column. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="headerText">Non-null header text without embedded NUL characters. Empty/whitespace text is forwarded unchanged.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike SetCellValue, this addresses header metadata for the column, not a body row. Unlike SetColumnUnitString, this supplies the column title rather than its unit label. Empty text clears the title. No automatic value formatting or save occurs; native void completion is unacknowledged.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetColumnHeader(11, 7, 2, "Axial force");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetColumnHeader(int reportId, int tableId, int columnNumber, string headerText);

        /// <summary>Sets a display-only unit label on a one-based table column.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableId">Positive native table ID within the specified report, obtained from AddTable; this parameter identifies a table, not a cell.</param>
        /// <param name="columnNumber">One-based body column/header number: 1 is the first column. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="unitString">Non-null display-unit text without embedded NUL characters. Empty text removes the label; no physical unit conversion is performed.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike SetColumnHeader, this supplies the unit label. It does not change model input units, convert stored values or validate dimensional consistency. Empty text clears the label. Use Root unit setters for native model input-unit changes. Native void completion does not prove persistence.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetColumnUnitString(11, 7, 2, "kN");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetColumnUnitString(int reportId, int tableId, int columnNumber, string unitString);

        /// <summary>Sets the text of a one-based row header.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <param name="tableId">Positive native table ID within the specified report, obtained from AddTable; this parameter identifies a table, not a cell.</param>
        /// <param name="rowNumber">One-based body row/header number: 1 is the first row. Zero/negative values are rejected; no offset is applied and upper bounds remain native responsibilities.</param>
        /// <param name="headerText">Non-null header text without embedded NUL characters. Empty/whitespace text is forwarded unchanged.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>Unlike SetColumnHeader, this labels a row rather than a column. Unlike SetCellValue, it does not address a body column or replace a body cell. Empty text clears the label. No automatic value formatting, resizing or save occurs; native void completion is unacknowledged.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetRowHeader(11, 7, 1, "Member 17");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetRowHeader(int reportId, int tableId, int rowNumber, string headerText);

        /// <summary>Requests saving of the specified native report and all of its tables.</summary>
        /// <param name="reportId">Positive native report ID obtained from CreateReport; not a zero-based position or the report count.</param>
        /// <returns>An immutable creation acknowledgement or void-completion receipt with the requested/created identities. IsSuccessful is true only for positive-ID creation; inspect Status for void requests.</returns>
        /// <remarks><para>This is the main-source SaveReport workflow, scoped to one report and its contained tables. It does not save the STAAD model through Root, save all reports or promise file-system durability. Native void completion records InvokedWithoutAcknowledgement, with no read-back or success flag; native dialogs and storage behavior remain STAAD responsibilities.</para><para>Ported from the read-only main Table adapter, using a shared internal implementation for matching reviewed 2024/2025/2026 contracts. This does not certify every STAAD build or license context. Runs synchronously on the caller's established COM apartment against the captured Table object. Uses a per-adapter gate and validates binding before and after dispatch; it does not lock out external application/raw-object edits. Reacquire wrapper.Table after refresh. No automatic retry, model launch, alternate root acquisition, rollback or implicit save occurs. Native exceptions propagate unchanged; a post-call validation exception can follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A required ID/dimension/one-based coordinate or text input is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">Binding was disposed/replaced, or native creation/count/cell output has an undocumented type or value, including an untouched cell output.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, report context or storage rejected the request; earlier native effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the expected native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SaveReport(11);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SaveReport(int reportId);

        /// <summary>Deletes a native report and its contained tables.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <returns>An immutable receipt with NativeFunction DeleteReport and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike DeleteTable, this targets the entire report and its contained tables. This is destructive: no backup, confirmation dialog, replacement report or rollback is provided by this library. The supplied ReportId remains request identity in the receipt and does not prove the report still exists. No implicit save or model-file deletion occurs.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.DeleteReport(11);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult DeleteReport(int reportId);

        /// <summary>Deletes one native table from the specified report.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <returns>An immutable receipt with NativeFunction DeleteTable and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike DeleteReport, this targets a single table and does not request deletion of the containing report. The library does not infer success from disappearance, recycle IDs, create a replacement table or save automatically. A void-completion receipt retains the requested IDs, not a confirmed surviving object.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.DeleteTable(11, 7);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult DeleteTable(int reportId, int tableId);

        /// <summary>Changes the name of an existing report table.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="newName">Non-null, nonblank new table name without embedded NUL characters. Exact whitespace is preserved; native duplicate-name and length rules apply.</param>
        /// <returns>An immutable receipt with NativeFunction RenameTable and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike AddTable, this requests renaming rather than creating a table or changing its dimensions. Exact nonblank text is forwarded without trimming; duplicate-name and length rules remain native. IDs are not recomputed or inferred. Unlike SetColumnHeader and SetRowHeader, this changes the table name, not header text.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.RenameTable(11, 7, "Design forces");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult RenameTable(int reportId, int tableId, string newName);

        /// <summary>Requests new row and column counts for an existing report table.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="rowCount">Positive requested total row count, not an increment. The native help describes growing the table; current dimensions and shrink behavior are not inferred.</param>
        /// <param name="columnCount">Positive requested total column count, not an increment. Native capacity and current dimensions are not independently validated.</param>
        /// <returns>An immutable receipt with NativeFunction ResizeTable and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike AddTable, this targets an existing table. Counts are requested total dimensions, not increments or cell coordinates. Bentley documents increasing dimensions; shrinking, cell preservation and native capacity limits are not guaranteed. No dimension getter exists in this managed increment, so positivity is validated but growth against current dimensions is not independently checked. No clamping, automatic table recreation or save occurs.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.ResizeTable(11, 7, 6, 4);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult ResizeTable(int reportId, int tableId, int rowCount, int columnCount);

        /// <summary>Requests saving all reports maintained by the acquired native Table instance.</summary>
        /// <returns>An immutable receipt with NativeFunction SaveReportAll and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike SaveReport, this is not scoped to one report; unlike SaveTable, it is not scoped to one table. Calls the main-source native target SaveReportAll exactly once. ReportId and TableId are null because no single identity was supplied. This does not enumerate or save reports in other STAAD application instances, save the model through Root, or establish file-system durability.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SaveAllReports();
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SaveAllReports();

        /// <summary>Requests saving one table within its native report.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <returns>An immutable receipt with NativeFunction SaveTable and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike SaveReport, this requests one table rather than the whole report and all of its tables; unlike SaveAllReports, it does not request saving every report. The actual native storage scope and dialogs remain STAAD responsibilities. No Root model save, report enumeration or persistence read-back is added.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SaveTable(11, 7);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SaveTable(int reportId, int tableId);

        /// <summary>Requests bold formatting for one one-based table cell.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <returns>An immutable receipt with NativeFunction SetCellTextBold and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike SetCellValue, this changes formatting rather than writing text. Unlike SetCellTextItalic or SetCellTextUnderline, it requests bold. The native operation exposes no Boolean toggle or documented inverse: this method requests bold, not a toggle or clearing of the style. It does not resize the table, set a numeric value or save automatically.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextBold(11, 7, 1, 2);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextBold(int reportId, int tableId, int rowNumber, int columnNumber);

        /// <summary>Requests italic formatting for one one-based table cell.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <returns>An immutable receipt with NativeFunction SetCellTextItalic and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike SetCellTextBold and SetCellTextUnderline, this requests italic. It changes cell formatting rather than body text, headers, unit labels or model geometry. No Boolean disable/toggle option is exposed by the native call. No cell-selection side effect is requested by this wrapper, and no automatic save or formatting read-back occurs.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextItalic(11, 7, 1, 2);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextItalic(int reportId, int tableId, int rowNumber, int columnNumber);

        /// <summary>Requests underline formatting for one one-based table cell.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <returns>An immutable receipt with NativeFunction SetCellTextUnderline and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike SetCellTextBold and SetCellTextItalic, this requests underlining rather than a different emphasis. The native operation supplies no Boolean toggle or documented inverse; it does not offer style removal. Body text is not rewritten and no automatic save, resize, unit conversion or formatting read-back occurs.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextUnderline(11, 7, 1, 2);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextUnderline(int reportId, int tableId, int rowNumber, int columnNumber);

        /// <summary>Requests a Word-equivalent native font size for one one-based table cell.</summary>
        /// <param name="reportId">Positive native report ID returned by CreateReport or observed from the acquired native instance; not a report count or collection position.</param>
        /// <param name="tableId">Positive native table ID within the supplied report, not a cell ID or collection position.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Zero and negative values are rejected; upper bounds remain native responsibilities.</param>
        /// <param name="fontSize">Finite, strictly positive Double using the native Microsoft Word-equivalent font-size convention. Not model input length units; forwarded without conversion or an invented maximum.</param>
        /// <returns>An immutable receipt with NativeFunction SetCellTextSize and Status InvokedWithoutAcknowledgement after normal native void completion. NativeReturnCode is null and IsSuccessful is false; this is not native effect or persistence verification.</returns>
        /// <remarks><para>Unlike ResizeTable, this changes font sizing rather than row/column counts. Unlike the main-source SetCellTextSizeForTable workflow, which remains a separate port, this targets one cell only. A finite positive Double is forwarded unchanged using the native Microsoft Word-equivalent size convention, not model input length units. No string formatting, rounding, clamping or automatic save occurs; native size/capacity restrictions remain authoritative.</para><para>Adapted from an existing implementation in the read-only main Table adapter. The reviewed 2024/2025/2026 help contracts match, so one internal service is shared; this does not certify every STAAD build, license or UI context. Uses the captured Table object on the caller's established COM apartment and the same per-adapter gate as report creation and cell queries. Binding is checked before and after dispatch; external UI/raw-object edits and other adapter instances are not locked out. Reacquire wrapper.Table after refresh. No retry, alternate STAAD acquisition, implicit save, rollback or invented success flag is added. Native exceptions propagate unchanged, and an exception after dispatch may follow native changes.</para></remarks>
        /// <exception cref="ArgumentException">A positive identity/dimension/one-based coordinate, finite font size or required name is invalid; no native call occurs.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding was disposed or replaced; a post-call exception can follow a native mutation.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport, UI context or storage rejected the request; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object does not expose the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextSize(11, 7, 1, 2, 12.5);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextSize(int reportId, int tableId, int rowNumber, int columnNumber, double fontSize);

        /// <summary>Sets report-cell text color from explicit RGB channel integers.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="red">Red channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <param name="green">Green channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <param name="blue">Blue channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the packed ARGB, Color and text overloads, this accepts three separate RGB channels, each 0–255. It performs no alpha handling or color parsing; invalid channels are rejected before native work. HSL inputs belong in SetCellTextColorHsl. Exactly one native SetCellTextColor call receives Int32 RGB values.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColor(11, 7, 1, 2, 17, 34, 51);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int red, int green, int blue);

        /// <summary>Sets report-cell text color using an existing WPF Color value.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="color">Existing WPF Color; its R/G/B properties are used and alpha is ignored.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the string overload, this performs no parsing or malformed-text fallback. Uses the existing System.Windows.Media.Color type; its R/G/B byte properties become native Int32 channels. Alpha is ignored, including for transparent colors: no background blending or premultiplication is performed. The original RGB channels are retained.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColor(11, 7, 1, 2, System.Windows.Media.Color.FromArgb(12, 17, 34, 51));
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, Color color);

        /// <summary>Sets report-cell text color from WPF-supported color text with the main-source black fallback.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="colorText">Non-null WPF-supported color text. Parsing FormatException selects black, including malformed/empty text; no native error is converted to fallback.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the typed Color/RGB overloads, this parses WPF ColorConverter text, such as a named color, #RGB, #RRGGBB or #AARRGGBB. A FormatException from parsing alone selects black, matching the main source; empty/malformed text can therefore write black. Null is rejected. Other parser exceptions propagate. Alpha is ignored. Parsing is completed before native dispatch: native exceptions, including FormatException, propagate unchanged without retry or a second black write.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColor(11, 7, 1, 2, "#0C112233");
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, string colorText);

        /// <summary>Sets report-cell text color from a packed 0xAARRGGBB integer.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="argbValue">Packed signed Int32 in 0xAARRGGBB order. The high alpha byte is ignored.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the three-integer overload, this accepts one packed signed Int32. Extracts R/G/B from bits 16–23, 8–15 and 0–7. Alpha in the high byte is ignored, so a negative Int32 or zero alpha does not imply invalid or invisible text. No parsing or compositing occurs.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColor(11, 7, 1, 2, unchecked((int)0x80112233));
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int argbValue);

        /// <summary>Sets report-cell text color from explicit ARGB bytes while preserving the source alpha parameter.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="alpha">Source-compatible alpha byte; intentionally ignored by the RGB-only native operation.</param>
        /// <param name="red">Red channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <param name="green">Green channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <param name="blue">Blue channel, 0 through 255 inclusive. Passed as a native Int32.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the packed Int32 overload, channel bytes are supplied separately in alpha/red/green/blue order. The alpha argument is retained for source workflow parity but is ignored because native SetCellTextColor accepts RGB only. No premultiplication, transparency or background blending is applied. Native channel arguments are Int32 values.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColor(11, 7, 1, 2, (byte)12, (byte)17, (byte)34, (byte)51);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, byte alpha, byte red, byte green, byte blue);

        /// <summary>Sets report-cell text color by converting finite HSL inputs to native RGB.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="hue">Finite Single hue in degrees; clamped to 0–360, not wrapped. NaN/infinity are rejected.</param>
        /// <param name="saturation">Finite Single saturation, clamped to 0–1. NaN/infinity are rejected.</param>
        /// <param name="lightness">Finite Single lightness, clamped to 0–1. NaN/infinity are rejected.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextColor and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike the RGB overloads, this accepts Single-precision HSL components. Finite hue is clamped to 0–360 degrees, and finite saturation/lightness to 0–1, matching the main source. Hue is clamped rather than wrapped; 360 maps to red. Nonfinite values are rejected. Source-compatible Single arithmetic and midpoint-to-even Math.Round produce RGB bytes; conversion makes one native RGB call. HSL has no alpha or model-unit conversion.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextColorHsl(11, 7, 1, 2, 210f, 0.5f, 0.25f);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextColorHsl(int reportId, int tableId, int rowNumber, int columnNumber, float hue, float saturation, float lightness);

        /// <summary>Sets the horizontal text alignment of one one-based report cell.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="alignment">Defined Table alignment enum value; horizontal uses 0/1/2 and vertical uses 0/4/8.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextHorzAlignment and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike SetCellTextVerticalAlignment, this uses horizontal codes Left=0, Center=1 and Right=2. The enum adapts the main-project vocabulary with an explicit Table prefix; it is not WPF HorizontalAlignment and offers no Stretch option. Undefined values are rejected. It does not change text, units, column sizing or save the report.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextHorizontalAlignment(11, 7, 1, 2, TableHorizontalAlignment.Center);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextHorizontalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableHorizontalAlignment alignment);

        /// <summary>Sets the vertical text alignment of one one-based report cell.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="rowNumber">One-based row number: 1 is the first row. Upper bounds remain native responsibilities.</param>
        /// <param name="columnNumber">One-based column number: 1 is the first column. Upper bounds remain native responsibilities.</param>
        /// <param name="alignment">Defined Table alignment enum value; horizontal uses 0/1/2 and vertical uses 0/4/8.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextVertAlignment and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike horizontal alignment, native vertical codes are Top=0, Middle=4 and Bottom=8. The enum preserves the main-project code mapping and deliberately avoids WPF VerticalAlignment numeric values. Undefined values are rejected. No row-height change, text rewrite or implicit save occurs.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextVerticalAlignment(11, 7, 1, 2, TableVerticalAlignment.Middle);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextVerticalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableVerticalAlignment alignment);

        /// <summary>Requests one native font size for every cell of a report table.</summary>
        /// <param name="reportId">Positive acquired native report ID, not a report count or collection index.</param>
        /// <param name="tableId">Positive native table ID within the specified report; not a cell identifier.</param>
        /// <param name="fontSize">Finite, positive native Word-equivalent Double font size; no conversion from model units is applied.</param>
        /// <returns>A TableOperationResult with NativeFunction SetCellTextSizeAll and InvokedWithoutAcknowledgement after normal native void completion. IsSuccessful is false and NativeReturnCode is null; no effect/read-back success flag is invented.</returns>
        /// <remarks><para>Unlike SetCellTextSize, this targets the whole table and takes no row/column coordinates. Uses the same finite, positive native Microsoft Word-equivalent size convention, independent of model input length units. The Double is forwarded unchanged; no cell enumeration, string conversion, invented upper bound or implicit save occurs.</para><para>Adapted from an existing main-project function; original sources remain read-only. Uses shared Table validation/result types and the same captured-binding gate. Native 2024/2025/2026 help contracts match; 2025/2026 metadata corroborates the by-value call shape, not runtime compatibility. Runs on the caller's established COM apartment. Reacquire after wrapper refresh. External UI/raw-object edits are not locked out. No implicit save, native retry, model launch, alternate instance or rollback occurs. Native errors propagate unchanged and post-call binding errors may follow actual changes.</para></remarks>
        /// <exception cref="ArgumentException">A required identity, coordinate, channel, enum, finite component, font size or text input is invalid; no native call occurs. Malformed color text has the documented black fallback.</exception>
        /// <exception cref="InvalidOperationException">The captured Table binding is disposed or replaced; after-dispatch errors may follow changes.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">The native request failed; earlier effects may remain.</exception>
        /// <exception cref="Microsoft.CSharp.RuntimeBinder.RuntimeBinderException">The captured Table object lacks the required native operation.</exception>
        /// <example><code>bool modifyReport = false;
        /// if (modifyReport)
        /// {
        ///     var result = table.SetCellTextSizeForTable(11, 7, 12.5);
        ///     System.Console.WriteLine(result.Status);
        /// }</code></example>
        TableOperationResult SetCellTextSizeForTable(int reportId, int tableId, double fontSize);
    }
}
