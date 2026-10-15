using System;
using StaadPro.Interop.Enums.Root;

namespace StaadPro.Interop.Services.Root.Analysis
{
    internal static class RootAnalysisRequestService
    {
        internal static void Analyze(object root) => ((dynamic)root).Analyze();

        internal static void AnalyzeModel(object root, AnalysisEngine engine)
        {
            if (engine != AnalysisEngine.StaadPro) throw new ArgumentOutOfRangeException(nameof(engine));
            ((dynamic)root).AnalyzeModel((int)engine);
        }
    }
}
