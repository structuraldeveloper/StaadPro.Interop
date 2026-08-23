namespace StaadPro.Interop.Common
{
    /// <summary>
    /// Represents an entity in the STAAD.Pro model with an integer identifier.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the unique integer identifier of the entity in the STAAD model.
        /// </summary>
        int Id { get; }
    }
}
