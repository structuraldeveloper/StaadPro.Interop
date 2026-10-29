using StaadPro.Interop.Enums.Table;

namespace StaadPro.Interop.Models.Table
{
    /// <summary>Records a report/table creation acknowledgement or a native void request's normal completion.</summary>
    /// <remarks>Report and table IDs are native identities, not collection positions. Creation failure publishes no invented ID. Normal completion of a void request does not prove that a cell changed or a report was persisted.</remarks>
    public sealed class TableOperationResult
    {
        internal TableOperationResult(string function, TableOperationStatus status, int? code, int? reportId, int? tableId = null)
        { NativeFunction = function; Status = status; NativeReturnCode = code; ReportId = reportId; TableId = tableId; }
        /// <summary>Gets the exact native operation invoked on the captured Table object.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets acknowledged creation success/failure or unacknowledged void completion.</summary>
        public TableOperationStatus Status { get; }
        /// <summary>Gets true only when creation returned a positive ID; void completion is false.</summary>
        public bool IsSuccessful => Status == TableOperationStatus.Succeeded;
        /// <summary>Gets the actual positive creation ID or zero failure; null for native void functions.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the created or targeted report ID; null after failed report creation or for SaveAllReports, which targets no single report.</summary>
        public int? ReportId { get; }
        /// <summary>Gets the created or targeted table ID; null for report-only operations or failed table creation.</summary>
        public int? TableId { get; }
    }
}
