using System;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Records actual status evidence from one native-backed query in a managed Output workflow.</summary>
    /// <remarks>This carries no engineering output buffer. A documented NoSpecification Property observation can be accepted by an offset workflow while remaining unsuccessful as an individual query.</remarks>
    public sealed class OutputObservation
    {
        internal OutputObservation(string function, string status, bool success, int? code, bool? flag)
        {
            NativeFunction = function;
            Status = status;
            IsSuccessful = success;
            NativeReturnCode = code;
            NativeBooleanReturn = flag;
        }
        /// <summary>Gets the exact native member supplying this observation.</summary>
        public string NativeFunction
        {
            get;
        }
        /// <summary>Gets the operation-specific managed status name, without conflating Property and Output enums.</summary>
        public string Status
        {
            get;
        }
        /// <summary>Gets whether this individual query supplied its ordinary qualified payload.</summary>
        public bool IsSuccessful
        {
            get;
        }
        /// <summary>Gets actual integral native evidence when recorded, not an invented Boolean conversion.</summary>
        public int? NativeReturnCode
        {
            get;
        }
        /// <summary>Gets actual Boolean native evidence when recorded.</summary>
        public bool? NativeBooleanReturn
        {
            get;
        }
    }
}
