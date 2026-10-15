using System;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Root.Analysis
{
    // Shared 2024-2026 documented query contract. All dispatch stays on the supplied root and calling thread.
    internal static class RootAnalysisQueryService
    {
        private const int UnwrittenCount = int.MinValue;

        internal static bool IsAnalyzing(object root)
        {
            object returned = ((dynamic)root).IsAnalyzing();
            return NativeValues.ReadBoolean(returned, "IsAnalyzing flag");
        }

        internal static AnalysisStatusResult GetAnalysisStatus(object root, string modelFilePath)
        {
            int warningCount = UnwrittenCount, errorCount = UnwrittenCount;
            double cpuTimeSeconds = double.NaN;
            object returned = ((dynamic)root).GetAnalysisStatus(modelFilePath, ref warningCount, ref errorCount, ref cpuTimeSeconds);
            int status = NativeValues.ReadInt32(returned, "analysis status");
            bool recognizedState = status == -1 || (status >= 1 && status <= 5);
            if (!recognizedState)
                return new AnalysisStatusResult(modelFilePath, status, null, null, null);

            bool completed = status >= 2 && status <= 4;
            return new AnalysisStatusResult(modelFilePath, status,
                ReadCount(warningCount, completed, "warning count"),
                ReadCount(errorCount, completed, "error count"),
                ReadCpuTime(cpuTimeSeconds, completed));
        }

        private static int? ReadCount(int value, bool required, string name)
        {
            if (value == UnwrittenCount && !required) return null;
            if (value < 0)
                throw new InvalidOperationException("OpenSTAAD GetAnalysisStatus returned an unwritten or negative " + name + ".");
            return value;
        }

        private static double? ReadCpuTime(double value, bool required)
        {
            if (double.IsNaN(value) && !required) return null;
            if (double.IsNaN(value) || double.IsInfinity(value) || value < 0)
                throw new InvalidOperationException("OpenSTAAD GetAnalysisStatus returned an unwritten or invalid CPU time.");
            return value;
        }
    }
}
