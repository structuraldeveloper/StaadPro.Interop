using System.Collections.Generic;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Stores editable envelope identity, native classification, count and member load-case IDs.</summary>
    /// <remarks>This standalone model uses the repository's property store and has no private application dependency.
    /// Properties may be edited independently without native writes or automatic count reconciliation.
    /// Instances use reference equality, and mutable membership edits do not raise PropertyChanged.</remarks>
    public class LoadEnvelopeDetail : ValidatablePropertyStore, ILoadEnvelopeDetail
    {
        /// <summary>Creates an unassigned envelope with ID/count zero, type None and its own empty membership set.</summary>
        public LoadEnvelopeDetail() { LoadCaseIds = new HashSet<int>(); }

        /// <summary>Creates managed envelope metadata with an empty membership set.</summary>
        /// <param name="id">Envelope ID to store; no native existence check is performed.</param>
        /// <param name="envelopType">Envelope classification to store.</param>
        /// <param name="nLoadCases">Reported count to store independently of the membership set.</param>
        public LoadEnvelopeDetail(int id, LoadEnvelopType envelopType, int nLoadCases) : this()
        {
            Id = id;
            EnvelopType = envelopType;
            LoadCasesCount = nLoadCases;
        }

        /// <summary>Creates metadata with an independent copy of the supplied membership set.</summary>
        /// <param name="id">Envelope ID to store; no native existence check is performed.</param>
        /// <param name="envelopType">Envelope classification to store.</param>
        /// <param name="nLoadCases">Reported count to store; it is not recalculated from loadCaseList.</param>
        /// <param name="loadCaseList">IDs to copy, or null for an empty set. Later source-set edits do not affect this model.</param>
        public LoadEnvelopeDetail(int id, LoadEnvelopType envelopType, int nLoadCases, HashSet<int> loadCaseList)
            : this(id, envelopType, nLoadCases)
        {
            LoadCaseIds = loadCaseList == null ? new HashSet<int>() : new HashSet<int>(loadCaseList);
        }

        /// <summary>Gets or sets the envelope ID; changing it does not update STAAD.</summary>
        public int Id { get => Get<int>(); set => Set(value); }
        /// <summary>Gets or sets the envelope classification stored in this managed object.</summary>
        public LoadEnvelopType EnvelopType { get => Get<LoadEnvelopType>(); set => Set(value); }
        /// <summary>Gets or sets the reported membership count independently of LoadCaseIds.</summary>
        public int LoadCasesCount { get => Get<int>(); set => Set(value); }
        /// <summary>Gets or sets the editable membership set; replacing it raises PropertyChanged.</summary>
        /// <remarks>Constructors and queries supply non-null sets. Callers can replace the set, including with null.
        /// Changing its contents does not raise notifications or update LoadCasesCount.</remarks>
        public HashSet<int> LoadCaseIds { get => Get<HashSet<int>>(); set => Set(value); }
    }
}
