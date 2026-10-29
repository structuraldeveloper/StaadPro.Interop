using System;
using System.Windows.Media;
using StaadPro.Interop.Enums.Table;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Table;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Table
{
    internal sealed class TableService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate = new object();
        internal TableService(OpenStaadWrapper wrapper, object native) { this.wrapper = wrapper; this.native = native; }
        private void CheckBinding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawTable, native))
                throw new InvalidOperationException("The Table binding is disposed or replaced. Reacquire wrapper.Table.");
        }
        private T Run<T>(Func<dynamic, T> operation)
        {
            lock (gate)
            {
                CheckBinding();
                T result = operation(native);
                CheckBinding();
                return result;
            }
        }
        private static void Positive(int value, string name)
        { if (value <= 0) throw new ArgumentOutOfRangeException(name, "Use a positive native identity, dimension or one-based coordinate."); }
        private static void Text(string value, string name, bool allowEmpty = true)
        {
            if (value == null) throw new ArgumentNullException(name);
            if ((!allowEmpty && string.IsNullOrWhiteSpace(value)) || value.IndexOf('\0') >= 0)
                throw new ArgumentException("Supply text without embedded NUL characters; titles and table names must be nonblank.", name);
        }
        private static int Nonnegative(object raw, string function)
        {
            int value = NativeValues.ReadInt32(raw, function);
            if (value < 0) throw new InvalidOperationException(function + " returned an undocumented negative count or creation ID.");
            return value;
        }
        private static TableOperationResult Completed(string function, int? reportId, int? tableId = null) =>
            new TableOperationResult(function, TableOperationStatus.InvokedWithoutAcknowledgement, null, reportId, tableId);

        internal TableOperationResult CreateReport(string title)
        {
            Text(title, nameof(title), false);
            return Run(p =>
            {
                int id = Nonnegative((object)p.CreateReport(title), "CreateReport");
                return new TableOperationResult("CreateReport", id == 0 ? TableOperationStatus.NativeFailure : TableOperationStatus.Succeeded, id, id == 0 ? (int?)null : id);
            });
        }
        internal TableOperationResult AddTable(int reportId, string tableName, int rowCount, int columnCount)
        {
            Positive(reportId, nameof(reportId)); Text(tableName, nameof(tableName), false);
            Positive(rowCount, nameof(rowCount)); Positive(columnCount, nameof(columnCount));
            return Run(p =>
            {
                int id = Nonnegative((object)p.AddTable(reportId, tableName, rowCount, columnCount), "AddTable");
                return new TableOperationResult("AddTable", id == 0 ? TableOperationStatus.NativeFailure : TableOperationStatus.Succeeded, id, reportId, id == 0 ? (int?)null : id);
            });
        }
        internal int GetReportCount() => Run(p => Nonnegative((object)p.GetReportCount(), "GetReportCount"));
        internal int GetTableCount(int reportId)
        {
            Positive(reportId, nameof(reportId));
            return Run(p => Nonnegative((object)p.GetTableCount(reportId), "GetTableCount"));
        }
        private static void Cell(int reportId, int tableId, int rowNumber, int columnNumber)
        { Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); Positive(rowNumber, nameof(rowNumber)); Positive(columnNumber, nameof(columnNumber)); }
        internal string GetCellValue(int reportId, int tableId, int rowNumber, int columnNumber)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            return Run(p =>
            {
                object value = null;
                p.GetCellValue(reportId, tableId, rowNumber, columnNumber, ref value);
                return NativeValues.ReadText(value, "GetCellValue output");
            });
        }
        internal TableOperationResult SetCellValue(int reportId, int tableId, int rowNumber, int columnNumber, string value)
        {
            Cell(reportId, tableId, rowNumber, columnNumber); Text(value, nameof(value));
            return Run(p => { p.SetCellValue(reportId, tableId, rowNumber, columnNumber, value); return Completed("SetCellValue", reportId, tableId); });
        }
        internal TableOperationResult SetColumnHeader(int reportId, int tableId, int columnNumber, string headerText)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); Positive(columnNumber, nameof(columnNumber)); Text(headerText, nameof(headerText));
            return Run(p => { p.SetColumnHeader(reportId, tableId, columnNumber, headerText); return Completed("SetColumnHeader", reportId, tableId); });
        }
        internal TableOperationResult SetColumnUnitString(int reportId, int tableId, int columnNumber, string unitString)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); Positive(columnNumber, nameof(columnNumber)); Text(unitString, nameof(unitString));
            return Run(p => { p.SetColumnUnitString(reportId, tableId, columnNumber, unitString); return Completed("SetColumnUnitString", reportId, tableId); });
        }
        internal TableOperationResult SetRowHeader(int reportId, int tableId, int rowNumber, string headerText)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); Positive(rowNumber, nameof(rowNumber)); Text(headerText, nameof(headerText));
            return Run(p => { p.SetRowHeader(reportId, tableId, rowNumber, headerText); return Completed("SetRowHeader", reportId, tableId); });
        }
        internal TableOperationResult SaveReport(int reportId)
        {
            Positive(reportId, nameof(reportId));
            return Run(p => { p.SaveReport(reportId); return Completed("SaveReport", reportId); });
        }

        internal TableOperationResult DeleteReport(int reportId)
        {
            Positive(reportId, nameof(reportId));
            return Run(p => { p.DeleteReport(reportId); return Completed("DeleteReport", reportId); });
        }
        internal TableOperationResult DeleteTable(int reportId, int tableId)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId));
            return Run(p => { p.DeleteTable(reportId, tableId); return Completed("DeleteTable", reportId, tableId); });
        }
        internal TableOperationResult RenameTable(int reportId, int tableId, string newName)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); Text(newName, nameof(newName), false);
            return Run(p => { p.RenameTable(reportId, tableId, newName); return Completed("RenameTable", reportId, tableId); });
        }
        internal TableOperationResult ResizeTable(int reportId, int tableId, int rowCount, int columnCount)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId));
            Positive(rowCount, nameof(rowCount)); Positive(columnCount, nameof(columnCount));
            return Run(p => { p.ResizeTable(reportId, tableId, rowCount, columnCount); return Completed("ResizeTable", reportId, tableId); });
        }
        internal TableOperationResult SaveAllReports() => Run(p => { p.SaveReportAll(); return Completed("SaveReportAll", null); });
        internal TableOperationResult SaveTable(int reportId, int tableId)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId));
            return Run(p => { p.SaveTable(reportId, tableId); return Completed("SaveTable", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextBold(int reportId, int tableId, int rowNumber, int columnNumber)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            return Run(p => { p.SetCellTextBold(reportId, tableId, rowNumber, columnNumber); return Completed("SetCellTextBold", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextItalic(int reportId, int tableId, int rowNumber, int columnNumber)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            return Run(p => { p.SetCellTextItalic(reportId, tableId, rowNumber, columnNumber); return Completed("SetCellTextItalic", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextUnderline(int reportId, int tableId, int rowNumber, int columnNumber)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            return Run(p => { p.SetCellTextUnderline(reportId, tableId, rowNumber, columnNumber); return Completed("SetCellTextUnderline", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextSize(int reportId, int tableId, int rowNumber, int columnNumber, double fontSize)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            FontSize(fontSize);
            return Run(p => { p.SetCellTextSize(reportId, tableId, rowNumber, columnNumber, fontSize); return Completed("SetCellTextSize", reportId, tableId); });
        }

        private static void FontSize(double fontSize)
        {
            if (double.IsNaN(fontSize) || double.IsInfinity(fontSize) || fontSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(fontSize), "Supply a finite, positive native Word-equivalent font size.");
        }
        private static void Channel(int value, string name)
        {
            if (value < 0 || value > 255) throw new ArgumentOutOfRangeException(name, "Supply a color channel from 0 through 255.");
        }
        internal TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int red, int green, int blue)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            Channel(red, nameof(red)); Channel(green, nameof(green)); Channel(blue, nameof(blue));
            return Run(p => { p.SetCellTextColor(reportId, tableId, rowNumber, columnNumber, red, green, blue); return Completed("SetCellTextColor", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, Color color) =>
            SetCellTextColor(reportId, tableId, rowNumber, columnNumber, (int)color.R, (int)color.G, (int)color.B);
        internal TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, string colorText)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            Color color = TableColorConversion.Parse(colorText);
            return SetCellTextColor(reportId, tableId, rowNumber, columnNumber, color);
        }
        internal TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int argbValue) =>
            SetCellTextColor(reportId, tableId, rowNumber, columnNumber, (argbValue >> 16) & 255, (argbValue >> 8) & 255, argbValue & 255);
        internal TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, byte alpha, byte red, byte green, byte blue) =>
            SetCellTextColor(reportId, tableId, rowNumber, columnNumber, (int)red, (int)green, (int)blue);
        internal TableOperationResult SetCellTextColorHsl(int reportId, int tableId, int rowNumber, int columnNumber, float hue, float saturation, float lightness)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            Color color = TableColorConversion.FromHsl(hue, saturation, lightness);
            return SetCellTextColor(reportId, tableId, rowNumber, columnNumber, color);
        }
        internal TableOperationResult SetCellTextHorizontalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableHorizontalAlignment alignment)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            if (!Enum.IsDefined(typeof(TableHorizontalAlignment), alignment)) throw new ArgumentOutOfRangeException(nameof(alignment));
            return Run(p => { p.SetCellTextHorzAlignment(reportId, tableId, rowNumber, columnNumber, (int)alignment); return Completed("SetCellTextHorzAlignment", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextVerticalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableVerticalAlignment alignment)
        {
            Cell(reportId, tableId, rowNumber, columnNumber);
            if (!Enum.IsDefined(typeof(TableVerticalAlignment), alignment)) throw new ArgumentOutOfRangeException(nameof(alignment));
            return Run(p => { p.SetCellTextVertAlignment(reportId, tableId, rowNumber, columnNumber, (int)alignment); return Completed("SetCellTextVertAlignment", reportId, tableId); });
        }
        internal TableOperationResult SetCellTextSizeForTable(int reportId, int tableId, double fontSize)
        {
            Positive(reportId, nameof(reportId)); Positive(tableId, nameof(tableId)); FontSize(fontSize);
            return Run(p => { p.SetCellTextSizeAll(reportId, tableId, fontSize); return Completed("SetCellTextSizeAll", reportId, tableId); });
        }
    }
}
