using System;
using System.Linq;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Property
{
    internal static class PropertyValueReader
    {
        internal static void Positive(int value, string name) { if (value <= 0) throw new ArgumentOutOfRangeException(name); }
        internal static void Nonnegative(int value, string name) { if (value < 0) throw new ArgumentOutOfRangeException(name); }
        internal static void MaterialName(string materialName)
        {
            if (materialName == null) throw new ArgumentNullException(nameof(materialName));
            if (string.IsNullOrWhiteSpace(materialName) || materialName.IndexOf('\0') >= 0) throw new ArgumentException("Specify an exact nonblank material name without a NUL character.", nameof(materialName));
        }
        internal static int Integer(object value, string op) => NativeValues.ReadInt32(value, op);
        internal static double Number(object value, string op, bool allowInteger = false)
        {
            double number;
            if (value is double d) number = d;
            else if (value is float f) number = f;
            else if (allowInteger) number = NativeValues.ReadInt64(value, op);
            else throw new InvalidOperationException(op + " returned a non-floating-point scalar.");
            if (double.IsNaN(number) || double.IsInfinity(number)) throw new InvalidOperationException(op + " returned unwritten or nonfinite data.");
            return number;
        }
        internal static bool Flag(object value, string op, bool variantBoolean = false)
        {
            if (value is bool flag) return flag;
            int code = Integer(value, op);
            if (code == 0) return false;
            if (code == 1 || (variantBoolean && code == -1)) return true;
            throw new InvalidOperationException(op + " returned an undocumented success flag.");
        }
        internal static int Count(object value, string op)
        {
            int count = Integer(value, op);
            if (count < 0) throw new InvalidOperationException(op + " returned an undocumented negative count.");
            return count;
        }
        internal static double[] Buffer(int size) => Enumerable.Repeat(double.NaN, size).ToArray();
        internal static int[] IntegerArray(object value, int size, string op)
        {
            var a = Shape(value, size, op, typeof(int)); var output = new int[size]; int lower = a.GetLowerBound(0);
            for (int i = 0; i < size; i++) output[i] = (int)a.GetValue(lower + i);
            return output;
        }
        internal static double[] FloatingArray(object value, int size, string op, bool validateAll = true)
        {
            var a = Shape(value, size, op, typeof(double), typeof(float)); var output = new double[size]; int lower = a.GetLowerBound(0);
            for (int i = 0; i < size; i++)
            {
                object item = a.GetValue(lower + i);
                output[i] = validateAll ? Number(item, op) : item is float f ? f : (double)item;
            }
            return output;
        }
        private static Array Shape(object value, int size, string op, params Type[] allowed)
        {
            if (!(value is Array a) || a.Rank != 1 || a.Length != size || !allowed.Contains(a.GetType().GetElementType()))
                throw new InvalidOperationException(op + " returned an invalid output array shape/type.");
            return a;
        }
    }
}
