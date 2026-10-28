using System;
using System.Collections.Generic;
using System.Threading;

namespace PackageVerification
{
    public static class TableFoundationFixture
    {
        public sealed class Root
        {
            public object Table { get; set; }
            public int Major = 26;
            public object GetApplicationVersion(ref object major, ref object update, ref object revision, ref object build)
            { major = Major; update = 0; revision = 0; build = 340; return Major + ".00.00.340"; }
        }
        public sealed class Native
        {
            public readonly List<string> Calls = new List<string>();
            public readonly List<object[]> Arguments = new List<object[]>();
            public readonly List<int> Threads = new List<int>();
            public readonly Dictionary<string, object> Returns = new Dictionary<string, object>();
            public readonly Dictionary<string, string> Cells = new Dictionary<string, string>();
            public Action<string> OnCall;
            public bool WriteCellOutput = true;
            public object CellOutput = "Cell text";
            private object Record(string function, object fallback, params object[] args)
            {
                Calls.Add(function); Arguments.Add(args); Threads.Add(Thread.CurrentThread.ManagedThreadId);
                OnCall?.Invoke(function);
                return Returns.TryGetValue(function, out var value) ? value : fallback;
            }
            private static string Cell(int report, int table, int row, int column) => report + ":" + table + ":" + row + ":" + column;
            public object CreateReport(object title) => Record("CreateReport", 11, title);
            public object AddTable(object report, object name, object rows, object columns) => Record("AddTable", 7, report, name, rows, columns);
            public object GetReportCount() => Record("GetReportCount", 2);
            public object GetTableCount(object report) => Record("GetTableCount", 3, report);
            public void GetCellValue(object report, object table, object row, object column, ref object value)
            {
                Record("GetCellValue", null, report, table, row, column);
                if (WriteCellOutput) value = Cells.TryGetValue(Cell((int)report, (int)table, (int)row, (int)column), out var text) ? text : CellOutput;
            }
            public void SetCellValue(object report, object table, object row, object column, object value)
            {
                Record("SetCellValue", null, report, table, row, column, value);
                Cells[Cell((int)report, (int)table, (int)row, (int)column)] = (string)value;
            }
            public void SetColumnHeader(object report, object table, object column, object header) => Record("SetColumnHeader", null, report, table, column, header);
            public void SetColumnUnitString(object report, object table, object column, object unit) => Record("SetColumnUnitString", null, report, table, column, unit);
            public void SetRowHeader(object report, object table, object row, object header) => Record("SetRowHeader", null, report, table, row, header);
            public void SaveReport(object report) => Record("SaveReport", null, report);
        }
    }
}
