using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Queries;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Load
{
    // Shared installed 2024-2026 contracts. No year-specific mapping was evidenced for these queries.
    internal sealed class LoadInspectionService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate = new object();
        internal LoadInspectionService(OpenStaadWrapper wrapper, object native) { this.wrapper = wrapper; this.native = native; }
        private dynamic Binding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawLoad, native))
                throw new InvalidOperationException("The captured Load binding is disposed or replaced. Reacquire wrapper.Load.");
            return native;
        }
        private LoadQueryResult<T> Run<T>(Func<LoadQueryResult<T>> query)
        {
            lock (gate) { Binding(); var result = query(); Binding(); return result; }
        }
        private static int Integer(object value, string operation) => NativeValues.ReadInt32(value, operation + " return");
        private static int Count(object value, string operation, bool negativeOneAllowed = true)
        {
            int count = Integer(value, operation);
            if (count < 0 && !(negativeOneAllowed && count == -1))
                throw new InvalidOperationException(operation + " returned an undocumented negative count.");
            return count;
        }
        private static void Positive(int value, string name) { if (value <= 0) throw new ArgumentOutOfRangeException(name); }
        private static void Index(int value, string name) { if (value < 0) throw new ArgumentOutOfRangeException(name); }
        private static LoadQueryResult<T> Result<T>(string requested, LoadQueryStatus status, T data = default,
            int? code = null, string observed = null, string diagnostic = null) =>
            new LoadQueryResult<T>(requested, status, data, code, observed, diagnostic);
        private static LoadQueryResult<T> ConvertFailure<T>(string requested, LoadQueryResult<int> failure) =>
            Result<T>(requested, failure.Status, code: failure.NativeReturnCode, observed: failure.NativeFunction, diagnostic: failure.Diagnostic);
        private static LoadQueryResult<int> CountResult(string operation, int count) => count == -1
            ? Result<int>(operation, LoadQueryStatus.NativeFailure, code: count)
            : Result(operation, LoadQueryStatus.Success, count, count);
        private LoadQueryResult<int> CheckActive(string requested, int id)
        {
            int current = Integer((object)Binding().GetActiveLoad(), "GetActiveLoad");
            if (current == -1) return Result<int>(requested, LoadQueryStatus.NativeFailure, code: current, observed: "GetActiveLoad");
            PositiveNativeId(current, "active case");
            return current == id ? null : Result<int>(requested, LoadQueryStatus.StateChanged, code: current,
                observed: "GetActiveLoad", diagnostic: "Expected primary case " + id + ", observed case number " + current + ".");
        }
        private LoadQueryResult<T> Active<T>(int primaryLoadCaseId, string operation, Func<LoadQueryResult<T>> query)
        {
            Positive(primaryLoadCaseId, nameof(primaryLoadCaseId));
            return Run(() =>
            {
                object activation = Binding().SetLoadActive(primaryLoadCaseId);
                int? code = activation is bool ? (int?)null : Integer(activation, "SetLoadActive");
                bool activated = activation is bool flag ? flag : code == 1 || code == -1;
                if (!(activation is bool) && code != 0 && code != 1 && code != -1)
                    throw new InvalidOperationException("SetLoadActive returned an undocumented flag.");
                if (!activated) return Result<T>(operation, LoadQueryStatus.ActivationFailed, code: code, observed: "SetLoadActive");
                var before = CheckActive(operation, primaryLoadCaseId);
                if (before != null) return ConvertFailure<T>(operation, before);
                var result = query();
                var after = CheckActive(operation, primaryLoadCaseId);
                return after == null ? result : ConvertFailure<T>(operation, after);
            });
        }
        internal LoadQueryResult<int> GetActiveLoadCaseNumber() => Run(() =>
        {
            const string op = "GetActiveLoad";
            int id = Integer((object)Binding().GetActiveLoad(), op);
            if (id == -1) return Result<int>(op, LoadQueryStatus.NativeFailure, code: id);
            PositiveNativeId(id, "active case");
            return Result(op, LoadQueryStatus.Success, id, id);
        });
        internal LoadQueryResult<int> GetLoadListCount() => Run(() =>
            CountResult("GetLoadListCount", Count((object)Binding().GetLoadListCount(), "GetLoadListCount")));
        private LoadQueryResult<int> CheckList(string operation, int listIndex, out int total)
        {
            total = Count((object)Binding().GetLoadListCount(), "GetLoadListCount");
            if (total == -1) return Result<int>(operation, LoadQueryStatus.NativeFailure, code: total, observed: "GetLoadListCount");
            return listIndex > total ? Result<int>(operation, LoadQueryStatus.NotFound, code: total,
                observed: "GetLoadListCount", diagnostic: "The one-based load-list index exceeds the observed list count.") : null;
        }
        private int ListCaseCount(int index) => Count((object)Binding().GetLoadCountInLoadList(index), "GetLoadCountInLoadList", false);
        internal LoadQueryResult<int> GetLoadListCaseCount(int loadListIndex)
        {
            Positive(loadListIndex, nameof(loadListIndex));
            return Run(() =>
            {
                const string op = "GetLoadCountInLoadList";
                var failure = CheckList(op, loadListIndex, out int total);
                if (failure != null) return failure;
                int count = ListCaseCount(loadListIndex);
                int after = Count((object)Binding().GetLoadListCount(), "GetLoadListCount");
                return after == total ? Result(op, LoadQueryStatus.Success, count, count) :
                    Result<int>(op, LoadQueryStatus.StateChanged, code: after, observed: "GetLoadListCount");
            });
        }
        internal LoadQueryResult<IEnumerable<int>> GetLoadListCases(int loadListIndex, int maximumResults)
        {
            Positive(loadListIndex, nameof(loadListIndex)); Positive(maximumResults, nameof(maximumResults));
            return Run(() =>
            {
                const string op = "GetLoadsInLoadList";
                var failure = CheckList(op, loadListIndex, out int total);
                if (failure != null) return ConvertFailure<IEnumerable<int>>(op, failure);
                int count = ListCaseCount(loadListIndex);
                if (count > maximumResults) return Result<IEnumerable<int>>(op, LoadQueryStatus.LimitExceeded, code: count, observed: "GetLoadCountInLoadList");
                int[] ids = new int[count];
                int? code = null;
                if (count > 0)
                {
                    object buffer = ids;
                    object status = Binding().GetLoadsInLoadList(loadListIndex, ref buffer);
                    code = status is bool ? (int?)null : Integer(status, op);
                    bool ok = status is bool b ? b : code == 1 || code == -1;
                    if (!(status is bool) && code != 0 && code != 1 && code != -1)
                        throw new InvalidOperationException(op + " returned an undocumented flag.");
                    if (!ok) return Result<IEnumerable<int>>(op, LoadQueryStatus.NativeFailure, code: code);
                    ids = ArrayValues<int>(buffer, count, op);
                    foreach (int id in ids) PositiveNativeId(id, "load-list case");
                }
                int afterTotal = Count((object)Binding().GetLoadListCount(), "GetLoadListCount");
                if (afterTotal != total) return Result<IEnumerable<int>>(op, LoadQueryStatus.StateChanged, code: afterTotal, observed: "GetLoadListCount");
                int after = ListCaseCount(loadListIndex);
                if (after != count) return Result<IEnumerable<int>>(op, LoadQueryStatus.StateChanged, code: after, observed: "GetLoadCountInLoadList");
                return Result<IEnumerable<int>>(op, LoadQueryStatus.Success, Array.AsReadOnly(ids),
                    count == 0 ? 0 : code, count == 0 ? "GetLoadCountInLoadList" : op);
            });
        }
        private static void Command(LoadCommandKind commandKind)
        { if (!Enum.IsDefined(typeof(LoadCommandKind), commandKind)) throw new ArgumentOutOfRangeException(nameof(commandKind)); }
        private int CommandCount(LoadCommandKind kind) => Count((object)Binding().GetLoadTypeCount((int)kind), "GetLoadTypeCount", false);
        internal LoadQueryResult<int> GetLoadCommandCount(int primaryLoadCaseId, LoadCommandKind commandKind)
        {
            Command(commandKind);
            return Active(primaryLoadCaseId, "GetLoadTypeCount", () =>
            {
                int count = CommandCount(commandKind);
                return count == 0 ? Result<int>("GetLoadTypeCount", LoadQueryStatus.EmptyOrUnavailable, code: 0,
                    diagnostic: "Native zero is documented as case not found and cannot establish an empty command inventory.") :
                    Result("GetLoadTypeCount", LoadQueryStatus.Success, count, count);
            });
        }
        internal LoadQueryResult<int> GetAssignedEntityCount(int primaryLoadCaseId, LoadCommandKind commandKind, int commandIndex)
        {
            Command(commandKind); Index(commandIndex, nameof(commandIndex));
            return Active(primaryLoadCaseId, "GetListSizeForLoadType", () =>
            {
                const string op = "GetListSizeForLoadType";
                int commands = CommandCount(commandKind);
                if (commands == 0) return Result<int>(op, LoadQueryStatus.EmptyOrUnavailable, code: 0, observed: "GetLoadTypeCount");
                if (commandIndex >= commands) return Result<int>(op, LoadQueryStatus.NotFound, code: commands, observed: "GetLoadTypeCount");
                int count = Count((object)Binding().GetListSizeForLoadType((int)commandKind, commandIndex), op, false);
                int after = CommandCount(commandKind);
                return after == commands ? Result(op, LoadQueryStatus.Success, count, count) :
                    Result<int>(op, LoadQueryStatus.StateChanged, code: after, observed: "GetLoadTypeCount");
            });
        }
        internal LoadQueryResult<int> GetLoadItemCount(int loadCaseId)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            return Run(() => CountResult("GetLoadItemsCount", Count((object)Binding().GetLoadItemsCount(loadCaseId), "GetLoadItemsCount")));
        }
        internal LoadQueryResult<LoadItemKind> GetLoadItemKind(int loadCaseId, int loadItemIndex)
        {
            Positive(loadCaseId, nameof(loadCaseId)); Index(loadItemIndex, nameof(loadItemIndex));
            return Run(() =>
            {
                const string op = "GetLoadItemType";
                int code = Integer((object)Binding().GetLoadItemType(loadCaseId, loadItemIndex), op);
                if (code == 0) return Result<LoadItemKind>(op, LoadQueryStatus.NotFound, code: code);
                if (code < 0) throw new InvalidOperationException(op + " returned an undocumented negative item code.");
                return Result(op, LoadQueryStatus.Success, (LoadItemKind)code, code);
            });
        }
        internal LoadQueryResult<bool> ContainsDynamicLoad(int loadCaseId)
        {
            Positive(loadCaseId, nameof(loadCaseId));
            return Run(() =>
            {
                const string op = "IsDynamicLoadIncluded";
                object value = Binding().IsDynamicLoadIncluded(loadCaseId);
                if (value is bool flag) return Result(op, LoadQueryStatus.Success, flag);
                int code = Integer(value, op);
                if (code == -1) return Result<bool>(op, LoadQueryStatus.NativeFailure, code: code);
                if (code != 0 && code != 1) throw new InvalidOperationException(op + " returned an undocumented predicate result.");
                return Result(op, LoadQueryStatus.Success, code == 1, code);
            });
        }
        internal LoadQueryResult<int> GetRepeatContributionCount(int primaryLoadCaseId, int repeatItemIndex)
        {
            Positive(repeatItemIndex, nameof(repeatItemIndex));
            return Active(primaryLoadCaseId, "GetNoLoadFactorInRepeatLoad", () =>
            {
                const string op = "GetNoLoadFactorInRepeatLoad";
                int count = ContributionCount(false, repeatItemIndex);
                return count == -1 ? Result<int>(op, LoadQueryStatus.NotFound, code: count) : CountResult(op, count);
            });
        }
        internal LoadQueryResult<int> GetNotionalContributionCount(int primaryLoadCaseId, int notionalItemIndex)
        {
            Positive(notionalItemIndex, nameof(notionalItemIndex));
            return Active(primaryLoadCaseId, "GetNoLoadFactorDirectionInNotionalLoad", () =>
                CountResult("GetNoLoadFactorDirectionInNotionalLoad", ContributionCount(true, notionalItemIndex)));
        }
        internal LoadQueryResult<int> GetNotionalItemCount(int primaryLoadCaseId) =>
            Active(primaryLoadCaseId, "GetNotionalLoadCount", () => CountResult("GetNotionalLoadCount",
                Count((object)Binding().GetNotionalLoadCount(), "GetNotionalLoadCount")));
        internal LoadQueryResult<int> GetReferenceItemCount(int primaryLoadCaseId) =>
            Active(primaryLoadCaseId, "GetReferenceLoadCount", () => CountResult("GetReferenceLoadCount",
                Count((object)Binding().GetReferenceLoadCount(), "GetReferenceLoadCount")));
        private int ContributionCount(bool notional, int index) => notional
            ? Count((object)Binding().GetNoLoadFactorDirectionInNotionalLoad(index), "GetNoLoadFactorDirectionInNotionalLoad")
            : Count((object)Binding().GetNoLoadFactorInRepeatLoad(index), "GetNoLoadFactorInRepeatLoad");
        internal LoadQueryResult<IEnumerable<LoadFactorContribution>> GetRepeatContributions(int primaryLoadCaseId, int repeatItemIndex, int maximumResults)
        {
            Positive(repeatItemIndex, nameof(repeatItemIndex)); Positive(maximumResults, nameof(maximumResults));
            return Active(primaryLoadCaseId, "GetRepeatLoadByIndex", () => ReadContributions(false, repeatItemIndex, maximumResults,
                (id, factor, direction) => new LoadFactorContribution(id, factor)));
        }
        internal LoadQueryResult<IEnumerable<NotionalLoadContribution>> GetNotionalContributions(int primaryLoadCaseId, int notionalItemIndex, int maximumResults)
        {
            Positive(notionalItemIndex, nameof(notionalItemIndex)); Positive(maximumResults, nameof(maximumResults));
            return Active(primaryLoadCaseId, "GetNotionalLoadByIndex", () => ReadContributions(true, notionalItemIndex, maximumResults,
                (id, factor, direction) => new NotionalLoadContribution(id, factor, (NotionalLoadDirection)direction)));
        }
        private LoadQueryResult<IEnumerable<T>> ReadContributions<T>(bool notional, int index, int maximumResults, Func<int, double, int, T> make)
        {
            string op = notional ? "GetNotionalLoadByIndex" : "GetRepeatLoadByIndex";
            string countOp = notional ? "GetNoLoadFactorDirectionInNotionalLoad" : "GetNoLoadFactorInRepeatLoad";
            var failureStatus = notional ? LoadQueryStatus.NativeFailure : LoadQueryStatus.NotFound;
            int count = ContributionCount(notional, index);
            if (count == -1) return Result<IEnumerable<T>>(op, failureStatus, code: count, observed: countOp);
            if (count > maximumResults) return Result<IEnumerable<T>>(op, LoadQueryStatus.LimitExceeded, code: count, observed: countOp);
            var result = new T[count];
            int returned = count;
            if (count > 0)
            {
                object ids = new int[count], factors = Enumerable.Repeat(double.NaN, count).ToArray(), directions = new int[count];
                object response = notional ? Binding().GetNotionalLoadByIndex(index, ref ids, ref factors, ref directions)
                    : Binding().GetRepeatLoadByIndex(index, ref ids, ref factors);
                returned = Count(response, op);
                if (returned == -1) return Result<IEnumerable<T>>(op, failureStatus, code: returned);
                if (returned != count) return Result<IEnumerable<T>>(op, LoadQueryStatus.StateChanged, code: returned,
                    diagnostic: "Contribution count changed between sizing and retrieval.");
                int[] caseIds = ArrayValues<int>(ids, count, op);
                double[] values = ArrayValues<double>(factors, count, op);
                int[] axes = notional ? ArrayValues<int>(directions, count, op) : new int[count];
                for (int i = 0; i < count; i++)
                {
                    int id = caseIds[i];
                    if (notional ? id == 0 || id == int.MinValue : id <= 0)
                        throw new InvalidOperationException(op + " returned an invalid or unwritten case ID.");
                    if (double.IsNaN(values[i]) || double.IsInfinity(values[i]))
                        throw new InvalidOperationException(op + " returned an unwritten or nonfinite factor.");
                    if (notional && (axes[i] < 1 || axes[i] > 3))
                        throw new InvalidOperationException(op + " returned an invalid or unwritten global direction.");
                    result[i] = make(id, values[i], axes[i]);
                }
            }
            int after = ContributionCount(notional, index);
            if (after != count) return Result<IEnumerable<T>>(op, LoadQueryStatus.StateChanged, code: after, observed: countOp);
            return Result<IEnumerable<T>>(op, LoadQueryStatus.Success, Array.AsReadOnly(result), returned, count == 0 ? countOp : op);
        }
        private static T[] ArrayValues<T>(object value, int count, string operation)
        {
            if (!(value is Array array) || array.Rank != 1 || array.Length != count || array.GetType().GetElementType() != typeof(T))
                throw new InvalidOperationException(operation + " returned an invalid output array shape/type.");
            var copy = new T[count];
            int lower = array.GetLowerBound(0);
            for (int i = 0; i < count; i++) copy[i] = (T)array.GetValue(lower + i);
            return copy;
        }
        private static void PositiveNativeId(int id, string component)
        { if (id <= 0) throw new InvalidOperationException("OpenSTAAD returned an invalid " + component + " number."); }
    }
}
