using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Preserves one native Support DOF state and its recorded spring constant.</summary>
    public sealed class SupportDof
    {
        internal SupportDof(SupportDofState value0, double value1)
        { State=value0; SpringStiffness=value1; }
        /// <summary>Gets the native fixed, released or spring state; not a member partial-release mode.</summary>
        public SupportDofState State { get; }
        /// <summary>Gets the finite native stiffness; translational force/length or rotational moment/angle units depend on the containing slot.</summary>
        public double SpringStiffness { get; }
    }
}
