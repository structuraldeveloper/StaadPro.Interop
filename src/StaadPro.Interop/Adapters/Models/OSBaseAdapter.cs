using System;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Adapters.Models
{
    /// <summary>
    /// Abstract base class for OpenSTAAD adapters wrapping COM interfaces.
    /// </summary>
    public abstract class OSBaseAdapter : IOSBase
    {
        protected OSBaseAdapter(OpenStaadWrapper wrapper)
        {
            Wrapper = wrapper ?? throw new ArgumentNullException(nameof(wrapper));
        }

        /// <include file="../../Documentation/ApiMembers.xml" path="doc/members/member[@name='OSBaseAdapter.P:Wrapper']/*" />
        public OpenStaadWrapper Wrapper { get; }
    }
}
