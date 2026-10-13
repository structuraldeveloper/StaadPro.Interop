using StaadPro.Interop.Common;

namespace StaadPro.Interop.Interfaces
{
    /// <summary>Identifies a managed load combination and its title.</summary>
    public interface ILoadCombination : IEntity
    {
        /// <summary>Gets the combination title.</summary>
        string Title { get; }
    }
}
