# Output steel-design parameter blocks and failure ratios

**Updated:** 26 October 2026

Added six common documented Output native routes, bringing typed Output coverage to **46 of 60**. The 14 remaining documented routes retain their own transport, layout or optional-capability work. These are source additions after prepared preview 1.0.0-preview.20261026.1.

Import `StaadPro.Interop.Enums.Output` and `StaadPro.Interop.Models.Output` as needed. Examples assume `output = wrapper.Output` on its established COM apartment.

## Implemented contracts

### GetSteelDesignParameterBlockName

~~~csharp
OutputQueryResult<string> GetSteelDesignParameterBlockName(int parameterBlockIndex);
~~~

Native: `GetSteelDesignParameterBlockNameByIndex`.

When to use: Use this name with the parameter-block overloads of GetMemberSteelUtilizationRatio and GetMemberSteelDesignResults. GetSteelDesignParameterBlockCount is a count, not a last index. This query passes the nonnegative index unchanged; it does not enumerate blocks, select a design code, run design or verify that all blocks are supported. Native false is failure; unwritten or empty successful names are rejected rather than used as a default block.

| Parameter | Meaning |
| --- | --- |
| `parameterBlockIndex` | Nonnegative zero-based native design-parameter-block index. Count and block availability remain native result-context decisions. |

~~~csharp
var result = output.GetSteelDesignParameterBlockName(0);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetMemberSteelUtilizationRatio with parameterBlockName

~~~csharp
OutputQueryResult<double> GetMemberSteelUtilizationRatio(int memberId, string parameterBlockName);
~~~

Native: `GetMultipleMemberSteelDesignRatio`.

When to use: Unlike the member-only overload, this calls the multiple-block native API and retains the exact requested ParameterBlockName in the result. The reviewed help describes AISC 360-16 multiple-design results; native availability and applicable code/context remain STAAD decisions. It does not select a block or take a maximum across blocks. Only finite nonnegative ratios are data; the single-block -999/-1 sentinels are not reused here. Native false takes precedence over any ratio buffer.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive native analytical member ID; not a physical member, design-block index or load-case number. |
| `parameterBlockName` | Exact nonblank native parameter-block name, without NUL characters. Case and surrounding whitespace are preserved; obtain it with GetSteelDesignParameterBlockName when discovering names. |

~~~csharp
var result = output.GetMemberSteelUtilizationRatio(17, "PARAMETER 1");
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetMaximumMemberSteelUtilizationRatio

~~~csharp
OutputQueryResult<double> GetMaximumMemberSteelUtilizationRatio(int memberId);
~~~

Native: `GetMultipleMemberSteelDesignMaxRatio`.

When to use: Choose this to ask STAAD for its maximum across parameter blocks, rather than the member-only or explicitly named-block ratio query. The native output does not identify a governing block, so ParameterBlockName is null and no identity is guessed. A finite nonnegative ratio is required on native true; it is not converted to PASS/FAIL and no managed envelope, single-block sentinel interpretation or design execution occurs.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive native analytical member ID; not a physical member, design-block index or load-case number. |

~~~csharp
var result = output.GetMaximumMemberSteelUtilizationRatio(17);
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetMemberSteelDesignResults with parameterBlockName

~~~csharp
OutputQueryResult<ParameterBlockSteelDesignResults> GetMemberSteelDesignResults(int memberId, string parameterBlockName);
~~~

Native: `GetMultipleMemberSteelDesignResults`.

When to use: Unlike GetMemberSteelDesignResults(memberId), this native packet supplies code, PASS/FAIL text, critical/allowable ratios, critical load case, clause and section for an explicit block. It supplies no critical station, FX/MY/MZ or slenderness; those fields are absent from ParameterBlockSteelDesignResults, never synthesized as zero. PASS/FAIL text is classified explicitly; other written text remains Unknown. Exact requested block and native status text are preserved. Native false prevents publication of any packet; no block selection, case activation or design is performed.

| Parameter | Meaning |
| --- | --- |
| `memberId` | Positive native analytical member ID; not a physical member, design-block index or load-case number. |
| `parameterBlockName` | Exact nonblank native parameter-block name, without NUL characters. Case and surrounding whitespace are preserved; obtain it with GetSteelDesignParameterBlockName when discovering names. |

~~~csharp
var result = output.GetMemberSteelDesignResults(17, "PARAMETER 1");
if (result.IsSuccessful) System.Console.WriteLine(result.Data.DesignStatus);
~~~

### GetMinimumSteelDesignFailureRatio

~~~csharp
OutputQueryResult<double> GetMinimumSteelDesignFailureRatio();
~~~

Native: `GetMemberSteelDesignMinFailureRatio`.

When to use: This function has no member ID or parameter-block input. It is a model-wide native failure-ratio observation, not the minimum utilization of a requested member or a managed envelope. GetMaximumSteelDesignFailureRatio is a separate observation with its own success flag; the two are not combined into an allegedly atomic range. Native false means failure or unavailable data without proving the absence of failing members. A finite nonnegative ratio is required on native success.

~~~csharp
var result = output.GetMinimumSteelDesignFailureRatio();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

### GetMaximumSteelDesignFailureRatio

~~~csharp
OutputQueryResult<double> GetMaximumSteelDesignFailureRatio();
~~~

Native: `GetMemberSteelDesignMaxFailureRatio`.

When to use: This function has no member ID or parameter-block input. It is independent from GetMinimumSteelDesignFailureRatio and from GetMaximumMemberSteelUtilizationRatio, which targets a member across blocks. No governing member/block is returned or invented. Native false prevents consumption of its output; the method does not infer failure thresholds, result freshness or an engineering PASS/FAIL decision.

~~~csharp
var result = output.GetMaximumSteelDesignFailureRatio();
if (result.IsSuccessful) System.Console.WriteLine(result.Data);
~~~

## Exact differences from existing queries

The member-only utilization ratio keeps its earlier native function and -999/-1 sentinel handling. The new named-block overload calls GetMultipleMemberSteelDesignRatio and accepts only a finite nonnegative ratio on true. It returns the exact requested ParameterBlockName, including on native failure. MaximumMemberSteelUtilizationRatio does not claim a governing block because the native output supplies none.

The single-block complete-design packet remains MemberSteelDesignResults with critical station, FX/MY/MZ and slenderness. The named-block native packet returns fewer fields, so its overload uses ParameterBlockSteelDesignResults. Both preserve status text and classify explicit PASS/FAIL only. No missing action, station, governing block or slenderness is manufactured.

The failure minimum and maximum functions are independent model-wide observations without a member ID. Calling both does not make an atomic range or prove that a failing member exists. Their native flags remain separate and no cross-call success is inferred.

## Reuse, version and execution

Reused OutputQueryResult<T>, SteelDesignStatus, the established Output binding gate and strict native scalar/text readers. Added only the named-block packet needed to distinguish the native field sets and an optional ParameterBlockName diagnostic property on the existing envelope. Existing members and their native dispatch remain unchanged.

Common reviewed 2024–2026 contract, implemented by the shared internal Output reader. Calls run synchronously on the caller’s established COM apartment under the existing expanded-query gate. Reacquire wrapper.Output after refresh; disposed or replaced captured receivers are rejected before and after work. Coordinate raw access and external edits separately. Native units, code/result context and text are retained without conversion. No analysis, design, save, mutation, automatic retry or live engineering certification occurs.

The read-only main adapter was reviewed for its steel-design extraction and native field meanings. Its lack of named-block routes does not make those six documented native APIs unavailable; this port follows the common installed 2024/2025/2026 help and vendor signatures. No private source, wrapper/provider or target framework was changed.

## Validation and remaining work

Matched interface/adapter XML includes all six examples, descriptive parameters, failures and comparative usage. Tests cover exact native arguments, caller thread, strict flags, malformed/unwritten text and ratios, negative single-block sentinel rejection, preserved unknown status text, disposed/replaced bindings and transport identity without retry.

[The complete Output matrix](reports/OpenSTAAD-Output-Multi-Version-Architecture-and-Plan.md#3-complete-cross-version-function-matrix) retains 14 pending routes. They include member-end displacement layout, three direction policies, plate corner/cut/point buffers, solid Von Mises shape, scalar-by-value nonlinear/buckling outputs, missing-mass dispatch, time-history extrema and the newer beam-stress acquired capability. Supplemental unit candidates remain separate. Managed fixtures do not certify live STAAD marshaling or engineering behavior.

See [the contract index](contracts/OutputSteelBlockContractIndex.json), [existing Output guide](Output-Porting.md) and [earlier extended queries](Output-Extended-Queries.md).
