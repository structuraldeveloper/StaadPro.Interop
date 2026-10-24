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
            int? specificationId = null, AssignedSectionProfile? profileType = null, int? createdPropertyId = null,
            int? createdSpecificationId = null, int? createdSpecificationIndex = null, int? controlNodeId = null,
            int? propertyId = null, string uniqueId = null, int? plateNodeIndex = null,
            PropertyAttribute attribute = null, PropertyAttributeScope? attributeScope = null,
            int? tableId = null, int? createdTableId = null, UserProvidedTableType? tableType = null, string sectionName = null,
            string folderPath = null, Version nativeContractVersion = null, StaadPro.Interop.Enums.MemberEnd? memberEnd = null, CableDefinitionKind? cableDefinitionKind = null, int? nodeId = null)
        {
            NativeFunction = operation; Status = status; TargetKind = targetKind;
            TargetIds = Array.AsReadOnly(targetIds == null ? new int[0] : (int[])targetIds.Clone());
            MaterialName = materialName; SpecificationId = specificationId; ProfileType = profileType;
            CreatedPropertyId = createdPropertyId;
            CreatedSpecificationId = createdSpecificationId; CreatedSpecificationIndex = createdSpecificationIndex;
            ControlNodeId = controlNodeId; PropertyId = propertyId; UniqueId = uniqueId; PlateNodeIndex = plateNodeIndex;
            Attribute = attribute; AttributeScope = attributeScope; TableId = tableId; CreatedTableId = createdTableId;
            TableType = tableType; SectionName = sectionName;
            FolderPath = folderPath; NativeContractVersion = nativeContractVersion;
            MemberEnd = memberEnd; CableDefinitionKind = cableDefinitionKind; NodeId = nodeId;
            if (response is bool flag) NativeBooleanReturn = flag;
            else if (response != null) NativeReturnCode = Services.Property.PropertyValueReader.Integer(response, operation);
        }
        /// <summary>Gets the native function that acknowledged or rejected the requested mutation.</summary>
        public string NativeFunction { get; }
        /// <summary>Gets the operation-specific status; creation statuses are independent of property IDs.</summary>
        public PropertyMutationStatus Status { get; }
        /// <summary>Gets whether STAAD acknowledged success, creation or update.</summary>
        public bool IsSuccessful => Status == PropertyMutationStatus.Succeeded || Status == PropertyMutationStatus.Created || Status == PropertyMutationStatus.Updated;
        /// <summary>Gets the actual integral native return, or null for Boolean and void returns; void invocation never fabricates a zero.</summary>
        public int? NativeReturnCode { get; }
        /// <summary>Gets the actual Boolean native return, or null for integral and void returns.</summary>
        public bool? NativeBooleanReturn { get; }
        /// <summary>Gets the scope of the requested operation, independent of its success.</summary>
        public PropertyMutationTargetKind TargetKind { get; }
        /// <summary>Gets owned requested member, plate, solid or dependent-node IDs in input order, including duplicates; empty for model/material/definition operations.</summary>
        public IReadOnlyList<int> TargetIds { get; }
        /// <summary>Gets the exact requested material name for material operations and solid material assignment; otherwise null.</summary>
        public string MaterialName { get; }
        /// <summary>Gets the requested existing specification definition ID for assignment or definition deletion; otherwise null. It is not an analytical member ID.</summary>
        public int? SpecificationId { get; }
        /// <summary>Gets the requested ASSIGN profile selector for profile creation; otherwise null.</summary>
        public AssignedSectionProfile? ProfileType { get; }
        /// <summary>Gets the positive created section reference for successful section creation; otherwise null. Material status zero/one is never exposed as an ID.</summary>
        public int? CreatedPropertyId { get; }
        /// <summary>Gets a positive created specification ID only for native functions whose reviewed contract returns an ID; status-only operations and fireproofing indices leave this null.</summary>
        public int? CreatedSpecificationId { get; }
        /// <summary>Gets the native zero-based fireproofing creation index, including zero, only on successful fireproofing creation. This is not a specification ID and must not be incremented or assigned by analogy.</summary>
        public int? CreatedSpecificationIndex { get; }
        /// <summary>Gets the positive requested control-node ID for a control/dependent relation; otherwise null. TargetIds then contains dependent nodes only.</summary>
        public int? ControlNodeId { get; }
        /// <summary>Gets the positive requested native property reference for metadata writes or definition deletion; otherwise null. It is not a member number or created identity.</summary>
        public int? PropertyId { get; }
        /// <summary>Gets the exact requested unique-ID text for metadata writes; otherwise null. No GUID syntax, uniqueness or persistence is implied.</summary>
        public string UniqueId { get; }
        /// <summary>Gets the requested plate corner index 1..4 for local/global offset creation; otherwise null. This is not an analytical node ID.</summary>
        public int? PlateNodeIndex { get; }
        /// <summary>Gets the exact immutable requested attribute pair for definition/assignment/removal operations; otherwise null.</summary>
        public PropertyAttribute Attribute { get; }
        /// <summary>Gets the native member, element or ambiguous member/plate attribute domain; otherwise null.</summary>
        public PropertyAttributeScope? AttributeScope { get; }
        /// <summary>Gets the requested positive existing UPT table ID for section insertion; otherwise null. Not a section property ID.</summary>
        public int? TableId { get; }
        /// <summary>Gets the positive created UPT table ID only for successful table creation; otherwise null. Never inferred from an insertion acknowledgement.</summary>
        public int? CreatedTableId { get; }
        /// <summary>Gets the requested native UPT table type for table creation; otherwise null.</summary>
        public UserProvidedTableType? TableType { get; }
        /// <summary>Gets the exact requested section name for UPT insertion; otherwise null. No created section property reference is fabricated.</summary>
        public string SectionName { get; }
        /// <summary>Gets the exact requested standard-profile folder path for a database-folder mutation; otherwise null. No normalization, creation or filesystem existence check is implied.</summary>
        public string FolderPath { get; }
        /// <summary>Gets the immutable exact acquired native version selecting the database-folder return policy; otherwise null.</summary>
        public Version NativeContractVersion { get; }
        /// <summary>Gets the requested native member-end selector for release/offset removal; otherwise null.</summary>
        public StaadPro.Interop.Enums.MemberEnd? MemberEnd { get; }
        /// <summary>Gets the requested tension/unstressed-length cable-definition selector; otherwise null. This is not a member end.</summary>
        public CableDefinitionKind? CableDefinitionKind { get; }
        /// <summary>Gets the requested analytical node ID for plate node-release removal; otherwise null. This is not a corner index.</summary>
        public int? NodeId { get; }

    }
}
