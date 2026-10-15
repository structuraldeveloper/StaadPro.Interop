using System;
using System.Runtime.InteropServices;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services
{
    internal interface IRootWindowApi
    {
        bool IsWindow(IntPtr window);
        uint GetProcessId(IntPtr window);
        bool PostShowState(IntPtr window, int command);
    }

    internal sealed class RootWindowApi : IRootWindowApi
    {
        public bool IsWindow(IntPtr window)
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) throw new PlatformNotSupportedException("Window state requests require Windows.");
            return NativeIsWindow(window);
        }
        public uint GetProcessId(IntPtr window) { GetWindowThreadProcessId(window, out uint process); return process; }
        public bool PostShowState(IntPtr window, int command) => ShowWindowAsync(window, command);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool NativeIsWindow(IntPtr window);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr window, out uint process);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindowAsync(IntPtr window, int command);
    }

    internal static class RootWindowControl
    {
        internal static void Request(object rawHandle, object rawProcessId, int command, IRootWindowApi api)
        {
            long handleValue = rawHandle is IntPtr pointer ? pointer.ToInt64() : NativeValues.ReadInt64(rawHandle, "window handle");
            if (handleValue == 0 || handleValue == -1 || (IntPtr.Size == 4 && (handleValue < int.MinValue || handleValue > uint.MaxValue)))
                throw new InvalidOperationException("OpenSTAAD did not supply a valid pointer-sized window handle.");
            var window = IntPtr.Size == 4 ? new IntPtr(unchecked((int)handleValue)) : new IntPtr(handleValue);
            uint processId = NativeValues.ReadProcessId(rawProcessId);
            if (!api.IsWindow(window) || api.GetProcessId(window) != processId)
                throw new InvalidOperationException("The reported window is unavailable or belongs to another process.");
            if (!api.PostShowState(window, command)) throw new InvalidOperationException("Windows did not accept the window-state request.");
        }

    }
}
