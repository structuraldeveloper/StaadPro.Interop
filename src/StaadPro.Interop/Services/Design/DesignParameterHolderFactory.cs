using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace StaadPro.Interop.Services.Design
{
    internal interface IDesignParameterHolderFactory
    {
        object Create();
        void Release(object holder);
        object ReadIndexedValue(object holder, string propertyName, int index);
    }

    internal sealed class DesignParameterHolderFactory : IDesignParameterHolderFactory
    {
        // CoClass GUID matches both inspected 2025/2026 vendor assemblies.
        private static readonly Guid ClassId = new Guid("d780a6bb-ee59-450c-a342-ba5e90f24a9a");
        public object Create()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                throw new InvalidOperationException("GetMemberDesignParameters must activate its private native holder on an STA thread. Use the caller's established COM apartment.");
            return Activator.CreateInstance(Type.GetTypeFromCLSID(ClassId, true));
        }

        public void Release(object holder)
        {
            // Only the exclusively created temporary holder is released; never the shared Design/root object.
            if (holder != null && Marshal.IsComObject(holder))
                Marshal.ReleaseComObject(holder);
        }

        public object ReadIndexedValue(object holder, string propertyName, int index)
        {
            // The automation property is Name(index), not the generated interop accessor get_Name(index).
            // Request PROPERTYGET explicitly; do not retry using an alternate dispatch name.
            try
            {
                return holder.GetType().InvokeMember(propertyName, BindingFlags.GetProperty,
                    null, holder, new object[] { index }, CultureInfo.InvariantCulture);
            }
            catch (TargetInvocationException error) when (error.InnerException != null)
            {
                ExceptionDispatchInfo.Capture(error.InnerException).Throw();
                throw;
            }
        }
    }
}
