using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace StaadPro.Interop.Services.Acquisition
{
    internal sealed class StaadProcessIdentity
    {
        internal StaadProcessIdentity(int id, DateTime startedUtc) { Id = id; StartedUtc = startedUtc; }
        internal int Id { get; }
        internal DateTime StartedUtc { get; }
        internal string Key => Id.ToString(CultureInfo.InvariantCulture) + ":" + StartedUtc.Ticks.ToString(CultureInfo.InvariantCulture);
    }

    internal sealed class OpenStaadRootDiscovery
    {
        internal readonly List<object> Roots = new List<object>();
        internal readonly List<string> Diagnostics = new List<string>();
        internal bool Complete = true;
    }

    internal interface IOpenStaadAcquisitionEnvironment : IDisposable
    {
        TimeSpan Now { get; }
        IList<StaadProcessIdentity> Inventory();
        OpenStaadRootDiscovery Discover();
        void Wait(TimeSpan duration, CancellationToken cancellation);
        IDisposable TryCoordinate(string targetPath);
        void ValidateLaunch(string executablePath);
        void PrepareTarget(string targetPath, bool createMissing, Action checkpoint);
        StaadProcessIdentity Launch(string executablePath, string targetPath);
    }

    internal sealed class WindowsOpenStaadEnvironment : IOpenStaadAcquisitionEnvironment
    {
        private readonly Stopwatch clock = Stopwatch.StartNew();
        private bool ownsComInitialization;
        internal WindowsOpenStaadEnvironment()
        {
            int hr = CoInitializeEx(IntPtr.Zero, 2);
            Marshal.ThrowExceptionForHR(hr);
            ownsComInitialization = true;
        }
        public TimeSpan Now => clock.Elapsed;
        public IList<StaadProcessIdentity> Inventory()
        {
            var results = new List<StaadProcessIdentity>();
            int session;
            using (var current = Process.GetCurrentProcess()) session = current.SessionId;
            foreach (var process in Process.GetProcessesByName("Bentley.Staad"))
            using (process)
            {
                try
                {
                    if (!process.HasExited && process.SessionId == session)
                        results.Add(new StaadProcessIdentity(process.Id, process.StartTime.ToUniversalTime()));
                }
                catch (InvalidOperationException) { /* Process exited during the snapshot. */ }
                // Access denied must not be silently interpreted as zero applications.
            }
            return results;
        }
        public OpenStaadRootDiscovery Discover() => OpenStaadNativeDiscovery.Discover();
        public void Wait(TimeSpan duration, CancellationToken cancellation)
        {
            var end = Now + duration;
            while (Now < end)
            {
                cancellation.ThrowIfCancellationRequested();
                uint milliseconds = (uint)Math.Max(1, Math.Min(50, Math.Ceiling((end - Now).TotalMilliseconds)));
                MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, milliseconds, 0x4ff, 4);
                while (PeekMessage(out NativeMessage message, IntPtr.Zero, 0, 0, 1))
                {
                    if (message.Message == 0x12) { PostQuitMessage((int)message.WParam); return; }
                    TranslateMessage(ref message);
                    DispatchMessage(ref message);
                    cancellation.ThrowIfCancellationRequested();
                }
            }
        }
        public IDisposable TryCoordinate(string targetPath)
        {
            string hash;
            using (var algorithm = System.Security.Cryptography.SHA256.Create())
                hash = BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(targetPath.ToUpperInvariant()))).Replace("-", "");
            var mutex = new Mutex(false, @"Local\StaadPro.Interop.Target." + hash);
            bool acquired;
            try { acquired = mutex.WaitOne(0); }
            catch (AbandonedMutexException) { acquired = true; }
            catch { mutex.Dispose(); throw; }
            if (acquired) return new MutexLease(mutex);
            mutex.Dispose(); return null;
        }
        public void PrepareTarget(string targetPath, bool createMissing, Action checkpoint)
        {
            OpenStaadTargetFiles.Prepare(targetPath, createMissing, checkpoint);
        }
        public void ValidateLaunch(string executablePath)
        {
            if (string.IsNullOrEmpty(executablePath) || !File.Exists(executablePath))
                throw new FileNotFoundException("Configure an existing STAAD executable before launching a target.", executablePath);
            if (!string.Equals(Path.GetFileName(executablePath), "Bentley.Staad.exe", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("This provider supports the Bentley.Staad.exe application inventory and launcher.", nameof(executablePath));
        }
        public StaadProcessIdentity Launch(string executablePath, string targetPath)
        {
            ValidateLaunch(executablePath);
            using (var process = Process.Start(new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = "\"" + targetPath + "\"",
                WorkingDirectory = Path.GetDirectoryName(targetPath),
                UseShellExecute = false
            }))
            {
                if (process == null) throw new InvalidOperationException("STAAD startup returned no process identity.");
                return new StaadProcessIdentity(process.Id, process.StartTime.ToUniversalTime());
            }
        }
        public void Dispose()
        {
            if (ownsComInitialization) { ownsComInitialization = false; CoUninitialize(); }
        }
        private sealed class MutexLease : IDisposable
        {
            private Mutex mutex;
            internal MutexLease(Mutex mutex) { this.mutex = mutex; }
            public void Dispose() { if (mutex == null) return; try { mutex.ReleaseMutex(); } finally { mutex.Dispose(); mutex = null; } }
        }
        [StructLayout(LayoutKind.Sequential)] private struct NativeMessage
        {
            internal IntPtr Window; internal uint Message; internal UIntPtr WParam;
            internal IntPtr LParam; internal uint Time; internal int X; internal int Y; internal uint Private;
        }
        [DllImport("ole32.dll")] private static extern int CoInitializeEx(IntPtr reserved, uint mode);
        [DllImport("ole32.dll")] private static extern void CoUninitialize();
        [DllImport("user32.dll")] private static extern uint MsgWaitForMultipleObjectsEx(uint count, IntPtr handles, uint milliseconds, uint mask, uint flags);
        [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessage(out NativeMessage message, IntPtr window, uint min, uint max, uint remove);
        [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(ref NativeMessage message);
        [DllImport("user32.dll")] private static extern IntPtr DispatchMessage(ref NativeMessage message);
        [DllImport("user32.dll")] private static extern void PostQuitMessage(int code);
    }

    internal static class OpenStaadTargetFiles
    {
        internal static void Prepare(string targetPath, bool createMissing, Action checkpoint)
        {
            checkpoint();
            if (File.Exists(targetPath)) return;
            if (!createMissing) throw new FileNotFoundException("The target does not exist and creation is disabled.", targetPath);
            string directory = Path.GetDirectoryName(targetPath);
            Directory.CreateDirectory(directory);
            string temporary = Path.Combine(directory, ".staad-" + Guid.NewGuid().ToString("N") + ".tmp");
            bool ownsTemporary = false;
            try
            {
                checkpoint();
                using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    ownsTemporary = true;
                    using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                    {
                        foreach (var line in new[] { "STAAD SPACE", "START JOB INFORMATION",
                            "ENGINEER DATE " + DateTime.Today.ToString("dd-MMM-yy", CultureInfo.InvariantCulture),
                            "END JOB INFORMATION", "INPUT WIDTH 79", "UNIT METER KN", "JOINT COORDINATES", "MEMBER INCIDENCES", "FINISH" })
                            writer.WriteLine(line);
                    }
                }
                checkpoint();
                try { File.Move(temporary, targetPath); } // Two-argument Move never replaces an existing target.
                catch (IOException) { if (!File.Exists(targetPath)) throw; }
            }
            finally
            {
                if (ownsTemporary && File.Exists(temporary)) File.Delete(temporary);
            }
        }
    }

    internal static class OpenStaadNativeDiscovery
    {
        internal static OpenStaadRootDiscovery Discover()
        {
            var discovery = new OpenStaadRootDiscovery();
            IRunningObjectTable rot = null; IBindCtx context = null; IEnumMoniker enumeration = null;
            Guid classId = Guid.Empty;
            try
            {
                // Read registration rather than asking OLE to synthesize/install an absent ProgID.
                try { classId = ReadRegisteredClassId(); }
                catch (Exception ex) { Warn(discovery, "OpenSTAAD registration: " + ex.Message); }
                Marshal.ThrowExceptionForHR(GetRunningObjectTable(0, out rot));
                Marshal.ThrowExceptionForHR(CreateBindCtx(0, out context));
                rot.EnumRunning(out enumeration);
                var monikers = new IMoniker[1];
                int next;
                while ((next = enumeration.Next(1, monikers, IntPtr.Zero)) == 0)
                {
                    try
                    {
                        monikers[0].GetDisplayName(context, null, out string name);
                        if (IsCandidate(name, classId))
                        {
                            int hr = rot.GetObject(monikers[0], out object root);
                            if (hr == 0 && root != null) Add(discovery.Roots, root);
                            else Warn(discovery, "A candidate ROT registration could not be read.");
                        }
                    }
                    catch (Exception ex) { Warn(discovery, "ROT entry: " + ex.Message); }
                    finally { Release(monikers[0]); monikers[0] = null; }
                }
                if (next != 1) Marshal.ThrowExceptionForHR(next);
            }
            catch (Exception ex) { Warn(discovery, "ROT discovery: " + ex.Message); }
            finally { Release(enumeration); Release(context); Release(rot); }
            if (classId != Guid.Empty)
            {
                int hr = GetActiveObject(ref classId, IntPtr.Zero, out object active);
                if (hr == 0) Add(discovery.Roots, active);
                else if (hr != unchecked((int)0x800401E3)) Warn(discovery, "Active-object lookup HRESULT 0x" + hr.ToString("X8"));
            }
            return discovery;
        }
        internal static bool IsCandidate(string name, Guid classId) => !string.IsNullOrEmpty(name) &&
            (name.EndsWith(".std", StringComparison.OrdinalIgnoreCase) || name.IndexOf("StaadPro.OpenSTAAD", StringComparison.OrdinalIgnoreCase) >= 0 ||
             (classId != Guid.Empty && name.IndexOf(classId.ToString("B"), StringComparison.OrdinalIgnoreCase) >= 0));
        private static Guid ReadRegisteredClassId()
        {
            foreach (var view in new[] { Microsoft.Win32.RegistryView.Registry64, Microsoft.Win32.RegistryView.Registry32 })
            using (var classes = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.ClassesRoot, view))
            using (var registration = classes.OpenSubKey(@"StaadPro.OpenSTAAD\CLSID", false))
            {
                if (registration == null) continue;
                if (Guid.TryParse(registration.GetValue(null) as string, out Guid classId)) return classId;
                throw new InvalidOperationException("The registered OpenSTAAD CLSID is malformed.");
            }
            return Guid.Empty;
        }
        private static void Add(List<object> roots, object value) { if (value != null && !roots.Exists(root => ReferenceEquals(root, value))) roots.Add(value); }
        private static void Warn(OpenStaadRootDiscovery discovery, string text) { discovery.Complete = false; discovery.Diagnostics.Add(text); }
        private static void Release(object value) { if (value != null && Marshal.IsComObject(value)) Marshal.ReleaseComObject(value); }
        [DllImport("ole32.dll")] private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable table);
        [DllImport("ole32.dll")] private static extern int CreateBindCtx(int reserved, out IBindCtx context);
        [DllImport("oleaut32.dll")] private static extern int GetActiveObject(ref Guid classId, IntPtr reserved, [MarshalAs(UnmanagedType.IUnknown)] out object active);
    }
}
