using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums.Support;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Records one Support mutation acknowledgement and its exact requested or created identity.</summary>
    /// <remarks>No automatic save, retry, rollback or effect verification occurs. Native failure/exception may follow partial model changes. Void completion and empty selection are not successful acknowledgements.</remarks>
    public sealed class SupportMutationResult
    {
        internal SupportMutationResult(string function,SupportMutationStatus status,object raw=null,int? supportId=null,int[] nodes=null,Guid? uniqueId=null,SupportEntityKind? entityKind=null,int[] entities=null)
        { NativeFunction=function;Status=status;SupportId=supportId;NodeIds=Array.AsReadOnly(nodes==null?new int[0]:(int[])nodes.Clone());RequestedUniqueId=uniqueId;EntityKind=entityKind??(nodes==null?(SupportEntityKind?)null:SupportEntityKind.Nodes);
          EntityIds=Array.AsReadOnly((int[])(entities??nodes??new int[0]).Clone());
          if(raw is bool flag)NativeBooleanReturn=flag;else if(raw!=null)NativeReturnCode=Services.Interop.NativeValues.ReadInt32(raw,function); }
        /// <summary>Gets the exact native operation, including when an empty selection avoided invocation.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the operation-specific acknowledgement.</summary>
        public SupportMutationStatus Status { get; }
        /// <summary>Gets true only for an explicit successful native acknowledgement.</summary>
        public bool IsSuccessful => Status==SupportMutationStatus.Succeeded;
        /// <summary>Gets the positive created/requested support item ID when available; no node/index is substituted.</summary>
        public int? SupportId { get; }
        /// <summary>Gets owned requested node IDs in their original order, retaining duplicates.</summary>
        public IReadOnlyList<int> NodeIds { get; }
        /// <summary>Gets the caller-specified node/plate family when supplied; it does not verify the native definition type.</summary>
        public SupportEntityKind? EntityKind { get; }
        /// <summary>Gets owned requested entity IDs, preserving order/duplicates; use EntityKind to interpret them. Definition-level operations have an empty list.</summary>
        public IReadOnlyList<int> EntityIds { get; }
        /// <summary>Gets the GUID requested by the void identity setter; it does not prove persistence.</summary>
        public Guid? RequestedUniqueId { get; }
        /// <summary>Gets actual native integral code or created ID; null for Boolean/void/uninvoked paths.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets an actual native Boolean acknowledgement, without converting numeric zero-success codes.</summary>
        public bool? NativeBooleanReturn { get; }
    }
}
