using System;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;

namespace StaadPro.Interop.Services.Wrapper
{
    // Unlike the source application's process-name scan, this service stays on the acquired root.
    internal sealed class BoundStaadModelServices : IStaadModelServices
    {
        private object root;
        internal BoundStaadModelServices(object root) { this.root = root; }
        internal void Detach() { root = null; }

        public IntPtr GetStaadApplicationWindowHandle()
        {
            if (root == null) return IntPtr.Zero;
            object value;
            try { value = ((dynamic)root).GetMainWindowHandle(); }
            catch (RuntimeBinderException) { return IntPtr.Zero; }
            catch (COMException ex) when (IsMissingMember(ex)) { return IntPtr.Zero; }
            if (value is IntPtr pointer)
            {
                if (pointer == new IntPtr(-1)) throw new InvalidOperationException("OpenSTAAD returned an invalid window handle.");
                return pointer;
            }
            long integer;
            switch (value)
            {
                case sbyte n: integer = n; break;
                case byte n: integer = n; break;
                case short n: integer = n; break;
                case ushort n: integer = n; break;
                case int n: integer = n; break;
                case uint n: integer = n; break;
                case long n: integer = n; break;
                case ulong n when n <= long.MaxValue: integer = (long)n; break;
                default: throw new InvalidOperationException("OpenSTAAD returned an invalid window-handle type.");
            }
            if (integer == -1 || (IntPtr.Size == 4 && (integer < int.MinValue || integer > uint.MaxValue)))
                throw new InvalidOperationException("OpenSTAAD returned a window handle outside the supported range.");
            return IntPtr.Size == 4 ? new IntPtr(unchecked((int)integer)) : new IntPtr(integer);
        }

        internal static bool IsMissingMember(COMException exception)
            => exception.ErrorCode == unchecked((int)0x80020003) || exception.ErrorCode == unchecked((int)0x80020006);
    }
}
