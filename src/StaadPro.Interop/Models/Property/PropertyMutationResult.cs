using System;
using System.Collections.Generic;
using StaadPro.Interop.Enums.Property;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains a native mutation acknowledgement, requested identity and the actual return evidence.</summary>
    /// <remarks>Success is an acknowledgement, not a read-back, save, transaction or engineering certification.
    /// Failures and transport exceptions can leave changes applied. No retry, rollback or implicit save occurs.</remarks>
    public sealed class PropertyMutationResult
    {
        internal PropertyMutationResult(string operation, PropertyMutationStatus status, object response,
            PropertyMutationTargetKind targetKind, int[] targetIds = null, string materialName = null,
            int? specificationId = null, AssignedSectionProfile? profileType = null, int? createdPropertyId = null)
        {
            NativeFunction = operation; Status = status; TargetKind = targetKind;
            TargetIds = Array.AsReadOnly(targetIds == null ? new int[0] : (int[])targetIds.Clone());
            MaterialName = materialName; SpecificationId = specificationId; ProfileType = profileType;
            CreatedPropertyId = createdPropertyId;
            if (response is bool flag) NativeBooleanReturn = flag;
            else NativeReturnCode = Services.Property.PropertyValueReader.Integer(response, operation);
        }
        /// <summary>Gets the native function that acknowledged or rejected the requested mutation.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the operation-specific status; creation statuses are independent of property IDs.</summary>
        public PropertyMutationStatus Status { get; }
        /// <summary>Gets whether STAAD acknowledged success, creation or update.</summary>
        public bool IsSuccessful => Status == PropertyMutationStatus.Succeeded || Status == PropertyMutationStatus.Created || Status == PropertyMutationStatus.Updated;
        /// <summary>Gets the actual integral native return, or null for an actual Boolean return.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the actual Boolean native return, or null for an integral return.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets the scope of the requested operation, independent of its success.</summary>
        public PropertyMutationTargetKind TargetKind { get; }
        /// <summary>Gets an owned read-only sequence of requested member, plate or solid IDs in input order, including duplicates; empty for model/material/definition operations.</summary>
        public IReadOnlyList<int> TargetIds { get; }
        /// <summary>Gets the exact requested material name for material operations and solid material assignment; otherwise null.</summary>
        public string MaterialName { get; }
        /// <summary>Gets the requested existing specification ID for plate specification assignment; otherwise null.</summary>
        public int? SpecificationId { get; }
        /// <summary>Gets the requested ASSIGN profile selector for profile creation; otherwise null.</summary>
        public AssignedSectionProfile? ProfileType { get; }
        /// <summary>Gets the positive created section reference only for successful assigned-profile creation; otherwise null. Material status zero/one is never exposed as an ID.</summary>
        public int? CreatedPropertyId { get; }
    }
}
