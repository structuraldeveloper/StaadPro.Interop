using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace StaadPro.Interop.Helpers
{
    /// <summary>
    /// Helper utilities for querying the Windows Running Object Table (ROT).
    /// </summary>
    public static class RotHelpers
    {
        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        /// <summary>
        /// Retrieves all active OpenSTAAD COM objects registered in the Running Object Table.
        /// </summary>
        public static IList<object> GetRunningOpenStaadInstances()
        {
            var results = new List<object>();

            try
            {
                if (GetRunningObjectTable(0, out IRunningObjectTable rot) != 0 || rot == null)
                    return results;

                if (CreateBindCtx(0, out IBindCtx bindCtx) != 0 || bindCtx == null)
                    return results;

                rot.EnumRunning(out IEnumMoniker enumMoniker);
                if (enumMoniker == null) return results;

                enumMoniker.Reset();
                IMoniker[] monikers = new IMoniker[1];

                while (enumMoniker.Next(1, monikers, IntPtr.Zero) == 0)
                {
                    try
                    {
                        monikers[0].GetDisplayName(bindCtx, null, out string displayName);

                        if (!string.IsNullOrEmpty(displayName) &&
                            (displayName.IndexOf("Staad", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             displayName.IndexOf(".std", StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            if (rot.GetObject(monikers[0], out object runningObj) == 0 && runningObj != null)
                            {
                                results.Add(runningObj);
                            }
                        }
                    }
                    catch
                    {
                        // Ignore individual moniker failure
                    }
                }
            }
            catch
            {
                // Return whatever collected so far
            }

            // Also check Marshal.GetActiveObject as direct fallback
            if (results.Count == 0)
            {
                try
                {
                    object active = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                    if (active != null) results.Add(active);
                }
                catch
                {
                    // No active object registered under ProgID
                }
            }

            return results;
        }
    }
}
