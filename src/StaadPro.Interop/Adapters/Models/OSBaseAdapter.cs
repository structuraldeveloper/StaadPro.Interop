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
        protected OSBaseAdapter(StaadGeometrySession session)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public StaadGeometrySession Session { get; }
    }
}
