using System;

namespace PackageVerification
{
    // Managed-only fixture for installed-package XML examples. No native activation or model mutation occurs.
    public sealed class LoadInspectionRoot
    {
        public object Geometry => null;
        public LoadInspectionNative Load { get; } = new LoadInspectionNative();
    }
    public sealed class LoadInspectionNative
    {
        private int active = 17;
        public bool SetLoadActive(int id) { active = id; return true; }
        public int GetActiveLoad() => active;
        public int GetLoadListCount() => 1;
        public int GetLoadCountInLoadList(int index) => 3;
        public bool GetLoadsInLoadList(int index, ref object ids) { WriteIds(ref ids, false); return true; }
        public int GetLoadTypeCount(int kind) => 2;
        public int GetListSizeForLoadType(int kind, int index) => 3;
        public int GetLoadItemsCount(int id) => 2;
        public int GetLoadItemType(int id, int index) => 3110;
        public int IsDynamicLoadIncluded(int id) => 0;
        public int GetNoLoadFactorInRepeatLoad(int index) => 3;
        public int GetRepeatLoadByIndex(int index, ref object ids, ref object factors)
        { WriteIds(ref ids, false); WriteFactors(ref factors); return 3; }
        public int GetNotionalLoadCount() => 1;
        public int GetNoLoadFactorDirectionInNotionalLoad(int index) => 3;
        public int GetNotionalLoadByIndex(int index, ref object ids, ref object factors, ref object directions)
        { WriteIds(ref ids, true); WriteFactors(ref factors); Write(ref directions, new[] { 1, 2, 3 }); return 3; }
        public int GetReferenceLoadCount() => 1;
        private static void WriteIds(ref object ids, bool signed) => Write(ref ids, signed ? new[] { 17, -41, 17 } : new[] { 17, 41, 17 });
        private static void WriteFactors(ref object factors) => Write(ref factors, new[] { -1.25, 0.0, 2.5 });
        private static void Write<T>(ref object buffer, T[] values)
        {
            if (!(buffer is T[] array) || array.Length != values.Length)
                throw new InvalidOperationException("Incorrect installed-package Load inspection buffer.");
            Array.Copy(values, array, values.Length);
        }
    }
}
