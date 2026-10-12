using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Helpers
{
    /// <summary>Helpers for acquiring and attaching to OpenSTAAD instances.</summary>
    public static class OpenStaadWrapperHelpers
    {
        private static readonly TimeSpan ConnectionTimeout = TimeSpan.FromMinutes(2);
        private static readonly TimeSpan PollInterval = TimeSpan.FromMilliseconds(250);

        public static OpenStaadWrapper GetWhenRunningInstancesExist(string fileFullPath, IList<object> runningInstances)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath))
                return WaitForReadyWrapper(null, runningInstances, RotHelpers.GetRunningOpenStaadInstances,
                    ConnectionTimeout, PollInterval);

            return AcquireModel(Path.GetFullPath(fileFullPath), runningInstances,
                RotHelpers.GetRunningOpenStaadInstances, LaunchModel, ConnectionTimeout, PollInterval);
        }

        public static OpenStaadWrapper GetWhenNoRunningInstancesExist(string fileFullPath)
        {
            return string.IsNullOrWhiteSpace(fileFullPath)
                ? StartStaadApplication()
                : OpenExistingOrNewModel(fileFullPath);
        }

        public static OpenStaadWrapper OpenExistingStaadModel(string fileFullPath)
        {
            return OpenExistingOrNewModel(fileFullPath);
        }

        private static OpenStaadWrapper OpenExistingOrNewModel(string fileFullPath)
        {
            try
            {
                return AcquireModel(Path.GetFullPath(fileFullPath), RotHelpers.GetRunningOpenStaadInstances(),
                    RotHelpers.GetRunningOpenStaadInstances, LaunchModel, ConnectionTimeout, PollInterval);
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Could not acquire STAAD model '{0}': {1}", fileFullPath, ex.Message);
                return null;
            }
        }

        internal static OpenStaadWrapper AcquireModel(string targetFullPath, IList<object> runningInstances,
            Func<IList<object>> discoverInstances, Action<string> launchModel, TimeSpan timeout, TimeSpan pollInterval)
        {
            var wrapper = FindReadyWrapper(targetFullPath, runningInstances, out bool modelIsRunning);
            if (wrapper != null) return wrapper;

            // An identified model may still be loading; wait for it instead of opening it twice.
            if (!modelIsRunning) launchModel(targetFullPath);

            return WaitForReadyWrapper(targetFullPath, runningInstances, discoverInstances, timeout, pollInterval);
        }

        internal static OpenStaadWrapper WaitForReadyWrapper(string targetFullPath, IList<object> initialInstances,
            Func<IList<object>> discoverInstances, TimeSpan timeout, TimeSpan pollInterval, bool isDedicated = false)
        {
            var timer = Stopwatch.StartNew();
            do
            {
                // Retry original candidates: busy objects may temporarily disappear from the ROT.
                var wrapper = FindReadyWrapper(targetFullPath, initialInstances, out _, isDedicated);
                if (wrapper != null) return wrapper;

                wrapper = FindReadyWrapper(targetFullPath, discoverInstances(), out _, isDedicated);
                if (wrapper != null) return wrapper;

                TimeSpan remaining = timeout - timer.Elapsed;
                if (remaining <= TimeSpan.Zero) break;
                Thread.Sleep(remaining < pollInterval ? remaining : pollInterval);
            }
            while (timer.Elapsed < timeout);

            Trace.TraceWarning("Timed out waiting for STAAD model '{0}' with Geometry and Load available.",
                targetFullPath ?? "<active model>");
            return null;
        }

        private static OpenStaadWrapper FindReadyWrapper(string targetFullPath, IList<object> instances,
            out bool modelIsRunning, bool isDedicated = false)
        {
            modelIsRunning = false;
            if (instances == null) return null;

            foreach (object instance in instances)
            {
                try
                {
                    if (instance == null) continue;
                    dynamic root = instance;
                    if (targetFullPath != null)
                    {
                        // Bentley's API uses an output VARIANT; the return value is not the filename.
                        object filename = string.Empty;
                        root.GetSTAADFile(ref filename, true);
                        string openFile = filename as string;
                        if (string.IsNullOrWhiteSpace(openFile) ||
                            !string.Equals(Path.GetFullPath(openFile), targetFullPath, StringComparison.OrdinalIgnoreCase))
                            continue;
                    }

                    modelIsRunning = true;
                    object geometry = root.Geometry;
                    object load = root.Load;
                    if (geometry == null || load == null) continue;

                    // Probe read-only calls before returning adapters. A root alone is not readiness.
                    object nodeCount = ((dynamic)geometry).GetNodeCount();
                    object loadCaseCount = ((dynamic)load).GetPrimaryLoadCaseCount();
                    if (nodeCount == null || loadCaseCount == null ||
                        Convert.ToInt32(nodeCount) < 0 || Convert.ToInt32(loadCaseCount) < 0) continue;

                    return new OpenStaadWrapper(instance, isDedicated: isDedicated || targetFullPath != null);
                }
                catch (Exception ex)
                {
                    Trace.TraceInformation("OpenSTAAD candidate is unavailable: {0}", ex.Message);
                }
            }

            return null;
        }

        private static void LaunchModel(string fileFullPath)
        {
            if (!File.Exists(fileFullPath)) WriteBlankModel(fileFullPath);
            Process.Start(new ProcessStartInfo { FileName = fileFullPath, UseShellExecute = true });
        }

        public static OpenStaadWrapper CreateBlankStaadModel(string fileFullPath)
        {
            try
            {
                WriteBlankModel(fileFullPath);
                return OpenExistingStaadModel(fileFullPath);
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Could not create STAAD model '{0}': {1}", fileFullPath, ex.Message);
                return null;
            }
        }

        private static void WriteBlankModel(string fileFullPath)
        {
            string dir = Path.GetDirectoryName(fileFullPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllLines(fileFullPath, new[]
            {
                "STAAD SPACE", "START JOB INFORMATION", $"ENGINEER DATE {DateTime.Today:dd-MMM-yy}",
                "END JOB INFORMATION", "INPUT WIDTH 79", "UNIT METER KN", "JOINT COORDINATES",
                "MEMBER INCIDENCES", "FINISH"
            });
        }

        public static OpenStaadWrapper StartStaadApplication()
        {
            string basePath = @"C:\Program Files\Bentley\Engineering";
            string[] versions = { "STAAD.Pro 2026", "STAAD.Pro 2025", "STAAD.Pro 2024" };
            foreach (string version in versions)
            {
                string exePath = Path.Combine(basePath, version, "STAAD", "Bentley.Staad.exe");
                if (!File.Exists(exePath)) continue;
                try
                {
                    Process.Start(exePath);
                    return WaitForReadyWrapper(null, null, RotHelpers.GetRunningOpenStaadInstances,
                        ConnectionTimeout, PollInterval, isDedicated: true);
                }
                catch (Exception ex)
                {
                    Trace.TraceWarning("Could not start STAAD: {0}", ex.Message);
                    break;
                }
            }

            return null;
        }
    }
}
