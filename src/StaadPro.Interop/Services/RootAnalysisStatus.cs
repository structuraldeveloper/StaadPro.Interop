using StaadPro.Interop.Enums;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services
{
    internal static class RootAnalysisStatus
    {
        internal static AnalysisResult Read(object value) => (AnalysisResult)NativeValues.ReadInt32(value, "analysis status");
    }
}
