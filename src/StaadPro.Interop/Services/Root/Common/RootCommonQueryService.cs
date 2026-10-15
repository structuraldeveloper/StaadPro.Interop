using System;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Root.Common
{
    // Common contracts documented for 2024-2026; no global session discovery or model mutation.
    internal static class RootCommonQueryService
    {
        private const string UnwrittenFolder = "<StaadPro.Interop.UnwrittenFolder>";

        internal static string GetStaadFileFolder(object root)
        {
            object folder = UnwrittenFolder;
            ((dynamic)root).GetSTAADFileFolder(ref folder);
            string result = NativeValues.ReadText(folder, "model folder");
            if (result == UnwrittenFolder)
                throw new InvalidOperationException("OpenSTAAD GetSTAADFileFolder left its output unwritten.");
            return result;
        }

        internal static bool IsPhysicalModel(object root)
        {
            object returned = ((dynamic)root).IsPhysicalModel();
            return NativeValues.ReadBoolean(returned, "IsPhysicalModel flag");
        }

        internal static string GetErrorMessage(object root)
        {
            object returned = ((dynamic)root).GetErrorMessage();
            return NativeValues.ReadText(returned, "error message");
        }

        internal static uint GetProcessId(object root)
        {
            object returned = ((dynamic)root).GetProcessId();
            return NativeValues.ReadProcessId(returned);
        }
    }
}
