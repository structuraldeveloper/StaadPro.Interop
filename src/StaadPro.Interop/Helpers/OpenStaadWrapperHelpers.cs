using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Helpers
{
    /// <summary>
    /// Internal helpers for acquiring and attaching to OpenSTAAD instances.
    /// </summary>
    public static class OpenStaadWrapperHelpers
    {
        public static OpenStaadWrapper GetWhenRunningInstancesExist(string fileFullPath, IList<object> runningInstances)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath))
            {
                if (runningInstances != null && runningInstances.Count > 0)
                {
                    return new OpenStaadWrapper(runningInstances[0], isDedicated: false);
                }

                try
                {
                    object active = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                    return new OpenStaadWrapper(active, isDedicated: false);
                }
                catch
                {
                    return null;
                }
            }

            string targetFullPath = Path.GetFullPath(fileFullPath);

            if (runningInstances != null)
            {
                foreach (object instance in runningInstances)
                {
                    try
                    {
                        dynamic dyn = instance;
                        string openFile = dyn.GetSTAADFile(true);
                        if (!string.IsNullOrEmpty(openFile) && string.Equals(Path.GetFullPath(openFile), targetFullPath, StringComparison.OrdinalIgnoreCase))
                        {
                            return new OpenStaadWrapper(instance, isDedicated: true);
                        }
                    }
                    catch
                    {
                        // Ignore individual instance inspection failure
                    }
                }
            }

            // If file exists, open it, else create blank model
            if (File.Exists(targetFullPath))
            {
                return OpenExistingStaadModel(targetFullPath);
            }
            else
            {
                return CreateBlankStaadModel(targetFullPath);
            }
        }

        public static OpenStaadWrapper GetWhenNoRunningInstancesExist(string fileFullPath)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath))
            {
                return StartStaadApplication();
            }

            string targetFullPath = Path.GetFullPath(fileFullPath);
            if (File.Exists(targetFullPath))
            {
                return OpenExistingStaadModel(targetFullPath);
            }
            else
            {
                return CreateBlankStaadModel(targetFullPath);
            }
        }

        public static OpenStaadWrapper OpenExistingStaadModel(string fileFullPath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = fileFullPath,
                    UseShellExecute = true
                });

                // Wait briefly for STAAD to initialize and register in ROT
                System.Threading.Thread.Sleep(3000);
                object active = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                return new OpenStaadWrapper(active, isDedicated: true);
            }
            catch
            {
                return null;
            }
        }

        public static OpenStaadWrapper CreateBlankStaadModel(string fileFullPath)
        {
            try
            {
                string dir = Path.GetDirectoryName(fileFullPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllLines(fileFullPath, new[]
                {
                    "STAAD SPACE",
                    "START JOB INFORMATION",
                    $"ENGINEER DATE {DateTime.Today:dd-MMM-yy}",
                    "END JOB INFORMATION",
                    "INPUT WIDTH 79",
                    "UNIT METER KN",
                    "JOINT COORDINATES",
                    "MEMBER INCIDENCES",
                    "FINISH"
                });

                return OpenExistingStaadModel(fileFullPath);
            }
            catch
            {
                return null;
            }
        }

        public static OpenStaadWrapper StartStaadApplication()
        {
            string basePath = @"C:\Program Files\Bentley\Engineering";
            string[] versions = { "STAAD.Pro 2026", "STAAD.Pro 2025", "STAAD.Pro 2024" };

            foreach (var version in versions)
            {
                string exePath = Path.Combine(basePath, version, "STAAD", "Bentley.Staad.exe");
                if (File.Exists(exePath))
                {
                    try
                    {
                        Process.Start(exePath);
                        System.Threading.Thread.Sleep(4000);
                        object active = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                        return new OpenStaadWrapper(active, isDedicated: true);
                    }
                    catch
                    {
                        break;
                    }
                }
            }

            return null;
        }
    }
}
