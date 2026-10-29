using System;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.View;

namespace StaadPro.Interop.Services.View
{
    internal sealed class ViewService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate = new object();
        internal ViewService(OpenStaadWrapper wrapper, object native) { this.wrapper = wrapper; this.native = native; }
        private void CheckBinding()
        {
            if (wrapper.RawOpenStaad == null || !ReferenceEquals((object)wrapper.RawView, native))
                throw new InvalidOperationException("The View binding is disposed or replaced. Reacquire wrapper.View.");
        }
        internal ViewOperationResult ShowFront()
        {
            lock (gate)
            {
                CheckBinding();
                ((dynamic)native).ShowFront();
                CheckBinding();
                return new ViewOperationResult("ShowFront");
            }
        }
    }
}
