using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Owns a node’s six restraint states and all six native spring constants.</summary>
    public sealed class SupportInformation
    {
        internal SupportInformation(int value0, SupportType value1, SupportDof value2, SupportDof value3, SupportDof value4, SupportDof value5, SupportDof value6, SupportDof value7)
        { NodeId=value0; Type=value1; Fx=value2; Fy=value3; Fz=value4; Mx=value5; My=value6; Mz=value7; }
        /// <summary>Gets the requested positive analytical node ID.</summary>
        public int NodeId { get; }
        /// <summary>Gets the reviewed native support type; unknown types are not decoded into this layout.</summary>
        public SupportType Type { get; }
        /// <summary>Gets the the Fx restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof Fx { get; }
        /// <summary>Gets the the Fy restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof Fy { get; }
        /// <summary>Gets the the Fz restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof Fz { get; }
        /// <summary>Gets the the Mx restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof Mx { get; }
        /// <summary>Gets the the My restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof My { get; }
        /// <summary>Gets the the Mz restraint and stiffness in native local support axes; no transformation is applied.</summary>
        public SupportDof Mz { get; }
    }
}
