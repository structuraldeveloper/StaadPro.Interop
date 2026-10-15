using System;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Interop;
namespace StaadPro.Interop.Services.Root.Analysis
{
    internal static class RootAnalysisMessagesService
    {
        internal const int MaximumMessageCount = 100000;
        internal static AnalysisMessagesResult Read(object root, string modelFilePath, bool errors)
        {
            int attempts = 0;
            int? lastNativeStatus = null;
            while (true)
            {
                var state = RootAnalysisQueryService.GetAnalysisStatus(root, modelFilePath);
                int? count = errors ? state.ErrorCount : state.WarningCount;
                if (!count.HasValue)
                    return Result(AnalysisMessageQueryOutcome.AnalysisCountUnavailable, lastNativeStatus, state, attempts);
                if (count.Value > MaximumMessageCount)
                    return Result(AnalysisMessageQueryOutcome.MessageLimitExceeded, lastNativeStatus, state, attempts);
                string sentinel = "<StaadPro.Interop.UnwrittenMessage." + Guid.NewGuid().ToString("N") + ">";
                var allocated = new string[count.Value];
                for (int i = 0; i < allocated.Length; i++) allocated[i] = sentinel;
                object buffer = allocated;
                object returned = errors
                    ? ((dynamic)root).GetAnalysisErrorMessages(modelFilePath, ref buffer)
                    : ((dynamic)root).GetAnalysisWarningMessages(modelFilePath, ref buffer);
                attempts++;
                int code = NativeValues.ReadInt32(returned, "analysis message retrieval status");
                lastNativeStatus = code;
                if (code == -108 && attempts == 1) continue;
                if (code == 0) return Result(AnalysisMessageQueryOutcome.NoMessages, code, state, attempts);
                if (code != 1) return Result(AnalysisMessageQueryOutcome.NativeFailure, code, state, attempts);
                return new AnalysisMessagesResult(AnalysisMessageQueryOutcome.Retrieved, code, state,
                    ReadMessages(buffer, sentinel), attempts);
            }
        }
        private static AnalysisMessagesResult Result(AnalysisMessageQueryOutcome outcome, int? status,
            AnalysisStatusResult state, int attempts) =>
            new AnalysisMessagesResult(outcome, status, state, new string[0], attempts);

        private static string[] ReadMessages(object buffer, string sentinel)
        {
            if (!(buffer is Array array) || array.Rank != 1 || array.GetType().GetElementType() != typeof(string))
                throw new InvalidOperationException("OpenSTAAD returned a successful message response without a one-dimensional BSTR array.");
            if (array.Length > MaximumMessageCount)
                throw new InvalidOperationException("OpenSTAAD returned a message array exceeding the managed allocation limit.");
            var result = new string[array.Length];
            int lower = array.GetLowerBound(0);
            for (int i = 0; i < result.Length; i++)
            {
                string text = NativeValues.ReadText(array.GetValue(lower + i), "analysis message");
                if (text == sentinel) throw new InvalidOperationException("OpenSTAAD left a successful message output unwritten.");
                result[i] = text;
            }
            return result;
        }
    }
}
