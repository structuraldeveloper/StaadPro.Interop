using StaadPro.Interop.Models;

namespace StaadPro.Interop.Interfaces
{
    /// <summary>
    /// Contract for resolving and acquiring OpenSTAAD wrapper instances.
    /// </summary>
    public interface IOpenStaadWrapperResolver
    {
        /// <summary>
        /// Acquires an <see cref="OpenStaadWrapper"/> for the given optional STAAD file.
        /// </summary>
        /// <param name="fileFullPath">Optional full path to a STAAD (.std) file.</param>
        /// <returns>An <see cref="OpenStaadWrapper"/> instance.</returns>
        OpenStaadWrapper Get(string fileFullPath = null);
    }
}
