using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace StaadPro.Interop.Helpers
{
    /// <summary>Helper utilities for querying the Windows Running Object Table (ROT).</summary>
    public static class RotHelpers
    {
        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        /// <summary>Retrieves active OpenSTAAD COM objects registered by filename, class ID or ProgID.</summary>
        /// <returns>A new, non-null list of candidate objects. Returns an empty list when no registrations
        /// can be read. Candidate discovery does not guarantee that a model or its interfaces are ready.</returns>
        /// <remarks>Reads the Windows ROT and includes the global active ProgID candidate. Individual
        /// stale or inaccessible registrations are skipped. This method does not start STAAD.</remarks>
        public static IList<object> GetRunningOpenStaadInstances()
        {
            var results = new List<object>();
            IRunningObjectTable rot = null;
            IBindCtx bindCtx = null;
            IEnumMoniker enumMoniker = null;

            try
            {
                Guid classId = Type.GetTypeFromProgID("StaadPro.OpenSTAAD")?.GUID ?? Guid.Empty;
                if (GetRunningObjectTable(0, out rot) == 0 && rot != null &&
                    CreateBindCtx(0, out bindCtx) == 0 && bindCtx != null)
                {
                    rot.EnumRunning(out enumMoniker);
                    if (enumMoniker != null)
                    {
                        enumMoniker.Reset();
                        var monikers = new IMoniker[1];
                        while (enumMoniker.Next(1, monikers, IntPtr.Zero) == 0)
                        {
                            try
                            {
                                monikers[0].GetDisplayName(bindCtx, null, out string displayName);
                                if (IsOpenStaadMoniker(displayName, classId) &&
                                    rot.GetObject(monikers[0], out object runningObj) == 0)
                                    AddInstance(results, runningObj);
                            }
                            catch
                            {
                                // A stale or busy registration must not hide other models.
                            }
                            finally
                            {
                                ReleaseComObject(monikers[0]);
                                monikers[0] = null;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Still attempt the ProgID lookup even when ROT enumeration fails.
            }
            finally
            {
                ReleaseComObject(enumMoniker);
                ReleaseComObject(bindCtx);
                ReleaseComObject(rot);
            }

            // Always include this candidate: other registrations may be unrelated or stale.
            try
            {
                AddInstance(results, Marshal.GetActiveObject("StaadPro.OpenSTAAD"));
            }
            catch
            {
                // No active object registered under the ProgID.
            }

            return results;
        }

        internal static bool IsOpenStaadMoniker(string displayName, Guid classId)
        {
            return !string.IsNullOrEmpty(displayName) &&
                (displayName.IndexOf("StaadPro.OpenSTAAD", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 displayName.EndsWith(".std", StringComparison.OrdinalIgnoreCase) ||
                 (classId != Guid.Empty && displayName.IndexOf(classId.ToString("B"), StringComparison.OrdinalIgnoreCase) >= 0));
        }

        private static void AddInstance(IList<object> results, object instance)
        {
            if (instance != null && !results.Contains(instance)) results.Add(instance);
        }

        private static void ReleaseComObject(object value)
        {
            if (value != null && Marshal.IsComObject(value)) Marshal.ReleaseComObject(value);
        }
    }
}
