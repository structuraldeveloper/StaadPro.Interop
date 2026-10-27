using System;
using StaadPro.Interop.Enums.Design;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Design;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Design
{
    internal sealed class DesignService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly IDesignParameterHolderFactory holders;
        private readonly object gate = new object ();
        internal DesignService(OpenStaadWrapper wrapper, object native, IDesignParameterHolderFactory holders)
        {
            this.wrapper = wrapper;
            this.native = native;
            this.holders = holders ?? throw new ArgumentNullException(nameof(holders));
        }

        private dynamic Binding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawDesign, native))
                throw new InvalidOperationException("The Design binding is disposed or replaced. Reacquire wrapper.Design.");
            return native;
        }

        private T Run<T>(Func<dynamic, T> operation)
        {
            lock (gate)
            {
                dynamic p = Binding();
                T result = operation(p);
                Binding();
                return result;
            }
        }

        private static int Status(object raw, string function)
        {
            int value = NativeValues.ReadInt32(raw, function);
            if (value != 0 && value != -1)
                throw new InvalidOperationException(function + " returned an undocumented assignment/query status.");
            return value;
        }

        internal DesignBriefResult CreateDesignBrief(StaadDesignCode designCode)
        {
            DesignInputs.Code(designCode);
            return Run(p => Create(p, designCode));
        }

        private DesignBriefResult Create(dynamic p, StaadDesignCode code)
        {
            Binding();
            int raw = NativeValues.ReadInt32((object)p.CreateDesignBrief((int)code), "CreateDesignBrief");
            Binding();
            if (raw < -1)
                throw new InvalidOperationException("CreateDesignBrief returned an undocumented negative reference.");
            return new DesignBriefResult(code, raw);
        }

        private sealed class Assignment
        {
            internal string Function, Name, Value;
            internal int[] Ids;
            internal int Order, Index;
        }

        private Assignment Parameter(DesignParameterAssignment item, int index)
        {
            if (item == null)
                throw new ArgumentNullException("designParameter");
            return new Assignment
            {
                Function = "AssignDesignParameter",
                Name = item.ParameterName,
                Value = item.NativeValue,
                Ids = item.MemberIds.ToArray(),
                Order = item.Order,
                Index = index
            };
        }

        private Assignment Command(DesignCommandAssignment item, int index)
        {
            if (item == null)
                throw new ArgumentNullException("designCommand");
            if (item.Command == DesignCommandKind.Group && wrapper.StaadVersion?.Major != 25 && wrapper.StaadVersion?.Major != 26)
                throw new NotSupportedException("Single-option GROUP grammar is qualified only for acquired STAAD.Pro 2025/2026. The 2024/unknown profile is unresolved.");
            string[] names =
            {
                "CHECK CODE",
                "FIXED GROUP",
                "GROUP",
                "MEMBER TAKE OFF",
                "SELECT",
                "SELECT OPTIMIZED",
                "SELECT WELD",
                "SELECT WELD TRUSS",
                "TAKE OFF"
            };
            return new Assignment
            {
                Function = "AssignDesignCommand",
                Name = names[(int)item.Command],
                Value = item.Command == DesignCommandKind.Group ? ((int)item.GroupProperty).ToString(CultureInfo.InvariantCulture) : "",
                Ids = item.MemberIds.ToArray(),
                Order = item.Order,
                Index = index
            };
        }

        private static Assignment[] Selection<T>(IEnumerable<T> values, string name, int maximum, Func<T, int, Assignment> convert)
        {
            if (values == null)
                throw new ArgumentNullException(name);
            DesignInputs.Limit(maximum, "maximumResults");
            var input = values.Take(maximum + 1).ToArray();
            if (input.Length > maximum)
                throw new ArgumentException("Selection exceeds maximumResults.", name);
            return input.Select(convert).OrderBy(i => i.Order).ThenBy(i => i.Index).ToArray();
        }

        private DesignMutationResult Assign(dynamic p, int brief, Assignment item, int? index)
        {
            object ids = (int[])item.Ids.Clone();
            object raw = item.Function == "AssignDesignParameter" ? p.AssignDesignParameter(brief, item.Name, item.Value, ids) : p.AssignDesignCommand(brief, item.Name, item.Value, ids);
            Binding();
            return new DesignMutationResult(item.Function, brief, item.Name, item.Ids, Status(raw, item.Function), index);
        }

        internal DesignMutationResult AssignDesignParameter(int designBriefId, DesignParameterAssignment designParameter)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            var item = Parameter(designParameter, 0);
            return Run(p => Assign(p, designBriefId, item, null));
        }

        internal DesignMutationResult AssignDesignCommand(int designBriefId, DesignCommandAssignment designCommand)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            var item = Command(designCommand, 0);
            return Run(p => Assign(p, designBriefId, item, null));
        }

        private DesignBatchResult Batch(dynamic p, int? brief, DesignBriefResult creation, Assignment[] items)
        {
            bool stopped = creation != null && !creation.IsSuccessful;
            var results = new List<DesignMutationResult>(items.Length);
            foreach (var item in items)
            {
                Binding();
                var result = stopped ? new DesignMutationResult(item.Function, brief, item.Name, item.Ids, null, item.Index) : Assign(p, brief.Value, item, item.Index);
                results.Add(result);
                if (!result.IsSuccessful)
                    stopped = true;
            }

            return new DesignBatchResult(brief, creation, results);
        }

        internal DesignBatchResult AssignDesignCommands(int designBriefId, IEnumerable<DesignCommandAssignment> designCommands, int maximumResults)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            var items = Selection(designCommands, nameof(designCommands), maximumResults, Command);
            return Run(p => Batch(p, designBriefId, null, items));
        }

        internal DesignBatchResult AssignDesignParameters(int designBriefId, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            var items = Selection(designParameters, nameof(designParameters), maximumResults, Parameter);
            return Run(p => Batch(p, designBriefId, null, items));
        }

        internal DesignBatchResult AssignDesignParameters(StaadDesignCode designCode, IEnumerable<DesignParameterAssignment> designParameters, int maximumResults)
        {
            DesignInputs.Code(designCode);
            var items = Selection(designParameters, nameof(designParameters), maximumResults, Parameter);
            return Run(p =>
            {
                if (items.Length == 0)
                    return new DesignBatchResult(null, null, new DesignMutationResult[0]);
                var creation = Create(p, designCode);
                return Batch(p, creation.DesignBriefId, creation, items);
            });
        }

        private DesignQueryResult<DesignBriefCodeObservation> BriefCode(dynamic p, int brief)
        {
            const string function = "GetDesignBriefCode";
            Binding();
            int raw = NativeValues.ReadInt32((object)p.GetDesignBriefCode(brief), function);
            Binding();
            if (raw < 0)
                throw new InvalidOperationException(function + " returned an undocumented negative code.");
            return new DesignQueryResult<DesignBriefCodeObservation>(function, brief, null, raw, raw == 0 ? DesignOperationStatus.NotFound : DesignOperationStatus.Success, raw == 0 ? null : new DesignBriefCodeObservation(raw));
        }

        internal DesignQueryResult<DesignBriefCodeObservation> GetDesignBriefCode(int designBriefId)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            return Run(p => BriefCode(p, designBriefId));
        }

        internal IReadOnlyList<DesignQueryResult<DesignBriefCodeObservation>> GetDesignBriefCodes(IEnumerable<int> designBriefIds, int maximumResults)
        {
            var ids = DesignInputs.Ids(designBriefIds, maximumResults, nameof(designBriefIds), allowZero: true);
            return Run(p => (IReadOnlyList<DesignQueryResult<DesignBriefCodeObservation>>)Array.AsReadOnly(ids.Select(i => (DesignQueryResult<DesignBriefCodeObservation>)BriefCode(p, i)).ToArray()));
        }

        private DesignQueryResult<MemberDesignParametersResult> Parameters(dynamic p, int brief, int member, int maximum)
        {
            const string function = "GetMemberDesignParameters";
            Binding();
            object holder = holders.Create() ?? throw new InvalidOperationException("The private Design holder factory returned null.");
            Exception primaryError = null;
            try
            {
                dynamic h = holder;
                // Keep the holder's native initialization; Count setters have no documented negative-sentinel contract.
                Binding();
                int raw = Status((object)p.GetMemberDesignParameters(brief, member, h), function);
                Binding();
                if (raw == -1)
                    return new DesignQueryResult<MemberDesignParametersResult>(function, brief, member, raw, DesignOperationStatus.NativeFailure);
                int count = NativeValues.ReadInt32((object)h.Count, "design parameter count");
                if (count < 0 || count > maximum)
                    throw new InvalidOperationException("The Design parameter count is unwritten, negative or exceeds maximumParameters.");
                var rows = new List<MemberDesignParameterInfo>(count);
                for (int index = 0; index < count; index++)
                {
                    string name = NativeValues.ReadText(holders.ReadIndexedValue(holder, "Name", index), "parameter name");
                    if (string.IsNullOrWhiteSpace(name))
                        throw new InvalidOperationException("Native parameter name is blank.");
                    rows.Add(new MemberDesignParameterInfo(index, name, NativeValues.ReadText(holders.ReadIndexedValue(holder, "Value", index), "parameter value"), NativeValues.ReadText(holders.ReadIndexedValue(holder, "Unit", index), "parameter unit"), NativeValues.ReadText(holders.ReadIndexedValue(holder, "Description", index), "parameter description"), NativeValues.ReadInt32(holders.ReadIndexedValue(holder, "Default", index), "parameter default flag")));
                }

                if (NativeValues.ReadInt32((object)h.Count, "design parameter count") != count)
                    throw new InvalidOperationException("The Design holder count changed during extraction.");
                Binding();
                return new DesignQueryResult<MemberDesignParametersResult>(function, brief, member, raw, DesignOperationStatus.Success, new MemberDesignParametersResult(brief, member, rows));
            }
            catch (Exception error)
            {
                primaryError = error;
                throw;
            }
            finally
            {
                try
                {
                    holders.Release(holder);
                }
                catch (Exception cleanupError)when (primaryError != null)
                {
                    primaryError.Data["DesignHolderCleanupError"] = cleanupError;
                }
            }
        }

        internal DesignQueryResult<MemberDesignParametersResult> GetMemberDesignParameters(int designBriefId, int memberId, int maximumParameters)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            if (memberId <= 0)
                throw new ArgumentOutOfRangeException(nameof(memberId));
            DesignInputs.Limit(maximumParameters, nameof(maximumParameters));
            return Run(p => Parameters(p, designBriefId, memberId, maximumParameters));
        }

        internal IReadOnlyList<DesignQueryResult<MemberDesignParametersResult>> GetMemberDesignParameters(int designBriefId, IEnumerable<int> memberIds, int maximumResults, int maximumParameters)
        {
            DesignInputs.Nonnegative(designBriefId, nameof(designBriefId));
            DesignInputs.Limit(maximumParameters, nameof(maximumParameters));
            var ids = DesignInputs.Ids(memberIds, maximumResults, nameof(memberIds));
            return Run(p => (IReadOnlyList<DesignQueryResult<MemberDesignParametersResult>>)Array.AsReadOnly(ids.Select(i => (DesignQueryResult<MemberDesignParametersResult>)Parameters(p, designBriefId, i, maximumParameters)).ToArray()));
        }
    }
}
