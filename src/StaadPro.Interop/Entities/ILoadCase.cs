using StaadPro.Interop.Common;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a structural load case in STAAD.Pro.
    /// </summary>
    public interface ILoadCase : IEntity
    {
        /// <summary>
        /// Gets the descriptive title of the load case.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets or sets the classification type of the load case.
        /// </summary>
        LoadCaseType CaseType { get; set; }
    }
}
