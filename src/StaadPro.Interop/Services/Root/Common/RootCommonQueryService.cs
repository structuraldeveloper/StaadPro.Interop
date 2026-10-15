using System;
using StaadPro.Interop.Services.Interop;
using StaadPro.Interop.Models.Root;

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

        internal static NativeProcessHandleValue GetProcessHandle(object root)
        {
            object returned = ((dynamic)root).GetProcessHandle();
            ulong raw;
            if (returned is ulong unsigned) raw = unsigned;
            else if (returned is UIntPtr unsignedPointer) raw = unsignedPointer.ToUInt64();
            else if (returned is IntPtr signedPointer) raw = unchecked((ulong)signedPointer.ToInt64());
            else raw = unchecked((ulong)NativeValues.ReadInt64(returned, "process handle"));
            return new NativeProcessHandleValue(raw);
        }

        internal static uint GetProcessId(object root)
        {
            object returned = ((dynamic)root).GetProcessId();
            return NativeValues.ReadProcessId(returned);
        }
    }
}
