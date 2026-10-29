# Table completion and View foundation

Implemented 28 October 2026. This increment ports **nine remaining main-project Table overloads** and **one main-project View function**, `ShowFront`. Table now adapts **all 29/29 main-interface overloads** and dispatches **all 24/24 reviewed native Table names**. View has **1/70 main-source overloads** and **1/67 reviewed native names**; 69 source overloads remain. The main project is strictly read-only. Source coverage is not native runtime or storage certification.

## Table signatures and differences

### SetCellTextColor(int, int, int, int, int, int, int)

```csharp
TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int red, int green, int blue);
```

Native target: `SetCellTextColor`. Unlike the packed ARGB, Color and text overloads, this accepts three separate RGB channels, each 0–255. It performs no alpha handling or color parsing; invalid channels are rejected before native work. HSL inputs belong in SetCellTextColorHsl. Exactly one native SetCellTextColor call receives Int32 RGB values.

### SetCellTextColor(int, int, int, int, Color)

```csharp
TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, Color color);
```

Native target: `SetCellTextColor`. Unlike the string overload, this performs no parsing or malformed-text fallback. Uses the existing System.Windows.Media.Color type; its R/G/B byte properties become native Int32 channels. Alpha is ignored, including for transparent colors: no background blending or premultiplication is performed. The original RGB channels are retained.

### SetCellTextColor(int, int, int, int, string)

```csharp
TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, string colorText);
```

Native target: `SetCellTextColor`. Unlike the typed Color/RGB overloads, this parses WPF ColorConverter text, such as a named color, #RGB, #RRGGBB or #AARRGGBB. A FormatException from parsing alone selects black, matching the main source; empty/malformed text can therefore write black. Null is rejected. Other parser exceptions propagate. Alpha is ignored. Parsing is completed before native dispatch: native exceptions, including FormatException, propagate unchanged without retry or a second black write.

### SetCellTextColor(int, int, int, int, int)

```csharp
TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, int argbValue);
```

Native target: `SetCellTextColor`. Unlike the three-integer overload, this accepts one packed signed Int32. Extracts R/G/B from bits 16–23, 8–15 and 0–7. Alpha in the high byte is ignored, so a negative Int32 or zero alpha does not imply invalid or invisible text. No parsing or compositing occurs.

### SetCellTextColor(int, int, int, int, byte, byte, byte, byte)

```csharp
TableOperationResult SetCellTextColor(int reportId, int tableId, int rowNumber, int columnNumber, byte alpha, byte red, byte green, byte blue);
```

Native target: `SetCellTextColor`. Unlike the packed Int32 overload, channel bytes are supplied separately in alpha/red/green/blue order. The alpha argument is retained for source workflow parity but is ignored because native SetCellTextColor accepts RGB only. No premultiplication, transparency or background blending is applied. Native channel arguments are Int32 values.

### SetCellTextColorHsl(int, int, int, int, float, float, float)

```csharp
TableOperationResult SetCellTextColorHsl(int reportId, int tableId, int rowNumber, int columnNumber, float hue, float saturation, float lightness);
```

Native target: `SetCellTextColor`. Unlike the RGB overloads, this accepts Single-precision HSL components. Finite hue is clamped to 0–360 degrees, and finite saturation/lightness to 0–1, matching the main source. Hue is clamped rather than wrapped; 360 maps to red. Nonfinite values are rejected. Source-compatible Single arithmetic and midpoint-to-even Math.Round produce RGB bytes; conversion makes one native RGB call. HSL has no alpha or model-unit conversion.

### SetCellTextHorizontalAlignment(int, int, int, int, TableHorizontalAlignment)

```csharp
TableOperationResult SetCellTextHorizontalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableHorizontalAlignment alignment);
```

Native target: `SetCellTextHorzAlignment`. Unlike SetCellTextVerticalAlignment, this uses horizontal codes Left=0, Center=1 and Right=2. The enum adapts the main-project vocabulary with an explicit Table prefix; it is not WPF HorizontalAlignment and offers no Stretch option. Undefined values are rejected. It does not change text, units, column sizing or save the report.

### SetCellTextVerticalAlignment(int, int, int, int, TableVerticalAlignment)

```csharp
TableOperationResult SetCellTextVerticalAlignment(int reportId, int tableId, int rowNumber, int columnNumber, TableVerticalAlignment alignment);
```

Native target: `SetCellTextVertAlignment`. Unlike horizontal alignment, native vertical codes are Top=0, Middle=4 and Bottom=8. The enum preserves the main-project code mapping and deliberately avoids WPF VerticalAlignment numeric values. Undefined values are rejected. No row-height change, text rewrite or implicit save occurs.

### SetCellTextSizeForTable(int, int, double)

```csharp
TableOperationResult SetCellTextSizeForTable(int reportId, int tableId, double fontSize);
```

Native target: `SetCellTextSizeAll`. Unlike SetCellTextSize, this targets the whole table and takes no row/column coordinates. Uses the same finite, positive native Microsoft Word-equivalent size convention, independent of model input length units. The Double is forwarded unchanged; no cell enumeration, string conversion, invented upper bound or implicit save occurs.

## Managed behavior and reuse

All Table methods retain the existing immutable `TableOperationResult`. New requests are native void calls: normal return means `InvokedWithoutAcknowledgement`, with `IsSuccessful == false` and `NativeReturnCode == null`. This is not a failure flag or independent effect/persistence evidence. Positive report/table identities and one-based cell coordinates use existing shared validation.

Reused the existing WPF `Color`, `ColorConverter`, Table results/statuses, validation, fixture and per-adapter gate. The main alignment enum values were ported as `TableHorizontalAlignment` (0/1/2) and `TableVerticalAlignment` (0/4/8), keeping domain meaning clear alongside WPF types. Conversion helpers live in `Services/Table`; no partial adapters, tuples, public ref/out or duplicate public color/HSL model was introduced.

The text overload deliberately preserves the main-source malformed-format fallback to black. Only parsing FormatException is handled; native exceptions are outside that catch and never trigger a second write. Null text rejects before native work. RGB ranges, alignment enums, finite positive font size and nonfinite HSL components are validated. Finite HSL components retain source clamping and Single arithmetic with midpoint-to-even rounding. Alpha is ignored without blending in Color, packed ARGB and explicit-byte overloads.

## Example

With an acquired wrapper and real report/table IDs, on the established COM apartment:

```csharp
using StaadPro.Interop.Enums.Table;

var table = wrapper.Table;
int reportId = 11; // replace with an acquired report identity
int tableId = 7;   // replace with a table in that report
table.SetCellTextColor(reportId, tableId, 1, 2, 17, 34, 51);
table.SetCellTextColor(reportId, tableId, 1, 2, "#80112233"); // same RGB; alpha ignored
table.SetCellTextColorHsl(reportId, tableId, 1, 2, 210f, 0.5f, 0.25f);
table.SetCellTextHorizontalAlignment(reportId, tableId, 1, 2, TableHorizontalAlignment.Center);
table.SetCellTextVerticalAlignment(reportId, tableId, 1, 2, TableVerticalAlignment.Middle);
table.SetCellTextSizeForTable(reportId, tableId, 12.5);
var save = table.SaveTable(reportId, tableId); // explicit; still no acknowledgement
```

## View: ShowFront

```csharp
ViewOperationResult ShowFront();
```

`wrapper.View.ShowFront()` calls native `ShowFront` once on the captured RawView object. It requests the active graphical view's front orientation. It does not show a hidden STAAD application, create a window, transform geometry, run analysis or save. The native front convention belongs to STAAD.

The named `ViewOperationResult` and `ViewOperationStatus` record `InvokedWithoutAcknowledgement`, with no integer return and no success flag. A Table receipt was not reused because report/table identities would misrepresent a View request. Signature and implementation remain in `IOSView` and the single `OSViewAdapter`; internal dispatch resides in `Services/View`. The other 69 source View overloads remain pending, and no native-only View operation was added.

## Versions, ownership and verification

The remaining four Table contracts and ShowFront match across installed 2024/2025/2026 help. Passive 2025/2026 metadata corroborates argument order/by-value slots. Common contracts share internal services; no artificial year-specific adapters were created. A 2024 vendor ABI was not inspected. Live COM, active-window/report context, licensing, persistence and every servicing build require native acceptance evidence.

All calls use captured interfaces on the caller's established COM apartment with per-adapter gates and binding checks before/after dispatch. External UI/raw-object edits and separate adapters are not locked out. Reacquire after wrapper refresh. No alternate STAAD root, automatic retry, rollback or implicit save is introduced. Native exceptions propagate unchanged; post-call failures may follow actual changes.

Passed **6,944 offline tests**, including **496 Table/View cases** (164 additional cases in this increment), and **1,282 installed-package XML examples**, including all 58 Table examples and two View examples. Managed fixture workflows cover the current Table routes and ShowFront; live native COM/UI/storage qualification remains separate.

Tests cover all overload signatures/packets, integer channel transport, color parsing/fallback, ignored alpha, HSL sector boundaries/clamping/nonfinite inputs, alignment code maps, font/coordinate validation, caller thread, stale/replaced/disposed bindings, original exceptions and no retry after a native FormatException. Interface and adapter XML match; packaged examples compile and execute with mutation guards, alongside managed native-signature workflow checks. No real model or report was mutated.

Source hashes and contracts: [Table index](contracts/TableFoundationContractIndex.json) and [View index](contracts/ViewFoundationContractIndex.json). Earlier guides: [report foundations](Table-Report-Foundations.md), [lifecycle/typography](Table-Lifecycle-and-Formatting.md), [View roadmap](reports/OpenSTAAD-View-Multi-Version-Architecture-and-Plan.md). Complete remaining main-source domains before missing native-only expansion; parked Load and supplemental Property approval requirements remain unchanged.
