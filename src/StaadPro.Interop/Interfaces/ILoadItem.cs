using StaadPro.Interop.Entities;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Interfaces
{
    /// <summary>
    /// Contract defining a structural load item associated with a load case.
    /// </summary>
    public interface ILoadItem
    {
        ILoadCase LoadCase { get; set; }
        LoadItemType LoadItemType { get; }
    }
}
