using System.Collections.Generic;
using System.ComponentModel;
using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>Provides editable managed metadata for an OpenSTAAD load envelope.</summary>
    /// <remarks>Id is the envelope identifier, not a load-case identifier. Editing this object does not update STAAD.
    /// Query results have a non-null membership set whose size matches LoadCasesCount. PropertyChanged reports
    /// property replacements; edits within the HashSet do not raise notifications or synchronize the count.</remarks>
    public interface ILoadEnvelopeDetail : IEntity, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the native envelope classification.</summary>
        LoadEnvelopType EnvelopType { get; set; }
        /// <summary>Gets or sets the reported number of member load cases; editing membership does not update it automatically.</summary>
        int LoadCasesCount { get; set; }
        /// <summary>Gets or sets the managed membership set of load-case IDs; ordering is unspecified.</summary>
        HashSet<int> LoadCaseIds { get; set; }
    }
}
