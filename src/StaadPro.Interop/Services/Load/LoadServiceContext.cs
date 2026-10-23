using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Loads.Contracts;
using StaadPro.Interop.Models.Loads.Queries;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Load
{
    // One captured binding and lock for the expansion services and inspection queries on this adapter.
    internal sealed class LoadServiceContext
    {
        internal readonly object Gate = new object();
        internal readonly OpenStaadWrapper Wrapper;
        private readonly object native;
        internal LoadServiceContext(OpenStaadWrapper wrapper, object native) { Wrapper = wrapper; this.native = native; }
        internal dynamic Binding()
        {
            if (Wrapper.RawOpenStaad == null || !ReferenceEquals((object)Wrapper.RawLoad, native))
                throw new InvalidOperationException("The captured Load binding is disposed or replaced. Reacquire wrapper.Load.");
            return native;
        }
        internal T Run<T>(Func<T> body) { lock (Gate) { Binding(); var value = body(); Binding(); return value; } }
        internal static int Integer(object value, string op) => NativeValues.ReadInt32(value, op);
        internal static int Count(object value, string op)
        {
            int count = Integer(value, op);
            if (count < -1) throw new InvalidOperationException(op + " returned an undocumented negative count.");
            return count;
        }
        internal static int[] Ids(IEnumerable<int> values, string name)
        {
            if (values == null) throw new ArgumentNullException(name);
            int[] ids = values.ToArray();
            if (ids.Length == 0) throw new ArgumentException("An explicit nonempty selection is required.", name);
            foreach (int id in ids) LoadContractArguments.Positive(id, name);
            return ids;
        }
        internal static int[] IdsForGeneration(LoadCaseSelection selection) => selection.IncludesAllCases ? new int[0] : selection.LoadCaseIds.ToArray();
        internal static LoadCaseTarget Target(ILoadCase value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            int id = value.Id; LoadCaseType family = value.CaseType;
            LoadContractArguments.Positive(id, "targetCase.Id");
            if (family != LoadCaseType.PrimaryLoad && family != LoadCaseType.ReferenceLoad)
                throw new ArgumentOutOfRangeException(nameof(value), "Select a primary or reference load case.");
            return new LoadCaseTarget(id, family);
        }
        internal LoadOperationStep Activate(LoadCaseTarget target)
        {
            if (target.Family == LoadCaseType.ReferenceLoad)
            {
                const string op = "SetReferenceLoadActive";
                int code = Integer((object)Binding().SetReferenceLoadActive(target.Id), op);
                if (code == target.Id) return new LoadOperationStep(op, code, null, LoadOperationStatus.Success);
                if (code != -1 && code != -8002) throw new InvalidOperationException(op + " returned an unexpected case number.");
                return new LoadOperationStep(op, code, null, LoadOperationStatus.ActivationFailed);
            }
            object raw = Binding().SetLoadActive(target.Id);
            bool? flag = raw is bool b ? b : (bool?)null;
            int? status = flag.HasValue ? (int?)null : Integer(raw, "SetLoadActive");
            if (!flag.HasValue && status != 0 && status != 1 && status != -1)
                throw new InvalidOperationException("SetLoadActive returned an undocumented flag.");
            bool ok = flag ?? status != 0;
            return new LoadOperationStep("SetLoadActive", status, flag, ok ? LoadOperationStatus.Success : LoadOperationStatus.ActivationFailed);
        }
        private int ActiveId() => Integer((object)Binding().GetActiveLoad(), "GetActiveLoad");
        internal static LoadOperationStep Step(string op, object raw, int success, bool boolean = false)
        {
            if ((boolean || success == 1) && raw is bool flag) return new LoadOperationStep(op, null, flag, flag ? LoadOperationStatus.Success : LoadOperationStatus.NativeFailure);
            int code = Integer(raw, op);
            // Integral FALSE/TRUE may be returned for VARIANT_BOOL contracts, including COM's -1 TRUE.
            bool ok = boolean ? code == 1 || code == -1 : code == success;
            if (boolean && code != 0 && code != 1 && code != -1) throw new InvalidOperationException(op + " returned an undocumented Boolean flag.");
            return new LoadOperationStep(op, code, null, ok ? LoadOperationStatus.Success : LoadOperationStatus.NativeFailure);
        }
        internal LoadOperationResult Mutate(string op, Func<object> call, int success, bool boolean = false, LoadCaseTarget target = null)
        {
            return Run(() =>
            {
                var steps = new List<LoadOperationStep>();
                if (target != null)
                {
                    var activation = Activate(target); steps.Add(activation);
                    if (activation.Status == LoadOperationStatus.Success)
                    {
                        int active = ActiveId();
                        steps.Add(new LoadOperationStep("GetActiveLoad", active, null, active == target.Id ? LoadOperationStatus.Success : LoadOperationStatus.ActivationFailed));
                    }
                    if (steps[steps.Count - 1].Status != LoadOperationStatus.Success)
                        return new LoadOperationResult(LoadOperationStatus.ActivationFailed, steps, false, "The target case could not be verified; assignment was not attempted.");
                }
                object response;
                try { response = call(); }
                catch (Exception error) { error.Data["MayHaveChangedModel"] = true; throw; }
                var step = Step(op, response, success, boolean); steps.Add(step);
                bool changed = false;
                if (target != null)
                {
                    int active = ActiveId(); changed = active != target.Id;
                    steps.Add(new LoadOperationStep("GetActiveLoad", active, null, changed ? LoadOperationStatus.PartialCompletion : LoadOperationStatus.Success));
                }
                if (changed)
                    return new LoadOperationResult(LoadOperationStatus.PartialCompletion, steps, true, "The active case changed during assignment; effects cannot be attributed safely. No rollback was attempted.");
                return new LoadOperationResult(step.Status, steps, true, step.Status == LoadOperationStatus.Success ? null : "Native code " + step.NativeReturnCode + ". Native failure does not guarantee rollback.");
            });
        }
        internal static LoadQueryResult<T> Query<T>(string op, LoadQueryStatus status, T data = default, int? code = null, string observed = null, string diagnostic = null) =>
            new LoadQueryResult<T>(op, status, data, code, observed, diagnostic);
        internal LoadQueryResult<T> ReadActive<T>(LoadCaseTarget target, string op, Func<LoadQueryResult<T>> body)
        {
            return Run(() =>
            {
                var activation = Activate(target);
                if (activation.Status != LoadOperationStatus.Success) return Query<T>(op, LoadQueryStatus.ActivationFailed, code: activation.NativeReturnCode, observed: activation.NativeFunction);
                int before = ActiveId();
                if (before != target.Id) return Query<T>(op, before == -1 ? LoadQueryStatus.NativeFailure : LoadQueryStatus.StateChanged, code: before, observed: "GetActiveLoad");
                var result = body();
                int after = ActiveId();
                return after == target.Id ? result : Query<T>(op, after == -1 ? LoadQueryStatus.NativeFailure : LoadQueryStatus.StateChanged, code: after, observed: "GetActiveLoad");
            });
        }
        internal static double[] Doubles(int count) => Enumerable.Repeat(double.NaN, count).ToArray();
        internal static T[] ArrayValues<T>(object value, int count, string op)
        {
            if (!(value is Array a) || a.Rank != 1 || a.Length != count || a.GetType().GetElementType() != typeof(T))
                throw new InvalidOperationException(op + " returned an invalid output array shape/type.");
            var copy = new T[count]; int lower = a.GetLowerBound(0);
            for (int i = 0; i < count; i++)
            {
                copy[i] = (T)a.GetValue(lower + i);
                if (copy[i] is double number && (double.IsNaN(number) || double.IsInfinity(number)))
                    throw new InvalidOperationException(op + " returned nonfinite or unwritten data.");
            }
            return copy;
        }
        internal static LoadDirection Direction(int code, string op)
        { if (code < 1 || code > 9) throw new InvalidOperationException(op + " returned an invalid direction."); return (LoadDirection)code; }
        internal static int NativeId(int id, string op)
        { if (id <= 0) throw new InvalidOperationException(op + " returned an invalid or unwritten entity ID."); return id; }
        internal static void Index(int index, string name) { if (index < 0) throw new ArgumentOutOfRangeException(name); }
    }
    internal sealed class LoadCaseTarget
    {
        internal readonly int Id; internal readonly LoadCaseType Family;
        internal LoadCaseTarget(int id, LoadCaseType family) { Id = id; Family = family; }
    }
}
