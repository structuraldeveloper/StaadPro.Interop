using System;
using StaadPro.Interop.Models.Table;

namespace StaadPro.Interop.Adapters.Interfaces
{
    /// <summary>Provides typed report creation, observation, cell/header editing and report-saving operations.</summary>
    /// <remarks>Rows and columns are one-based. Creation acknowledgements and native void completion have distinct results. Formatting and other main-source operations remain separate ports.</remarks>
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
    }
}
