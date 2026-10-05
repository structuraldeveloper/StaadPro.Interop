using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Common abstract base class for all load items associated with a load case.
    /// </summary>
    public abstract class LoadItem : ILoadItem
    {
        protected LoadItem(LoadItemType liType)
        {
            LoadItemType = liType;
        }

        #region Public Properties

        /// <summary>
        /// Gets the type category of this load item (nodal, member, plate, etc.).
        /// </summary>
        public LoadItemType LoadItemType { get; private set; }

        /// <summary>
        /// Gets or sets the parent load case to which this load item belongs.
        /// </summary>
        public ILoadCase LoadCase { get; set; }

        #endregion
    }
}
