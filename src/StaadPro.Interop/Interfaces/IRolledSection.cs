using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Interfaces
{
    /// <summary>Carries the selected native table identity needed by section creation, without a private catalog dependency.</summary>
    public interface IRolledSection
    {
        /// <summary>Gets the native table selector.</summary>
        Country Country { get; }
        /// <summary>Gets the exact native database name.</summary>
        string StaadName { get; }
        /// <summary>Gets a caller-provided designation used in definition equality; it is not sent as a native rename.</summary>
        string Designation { get; }
    }
    /// <summary>Exposes copied additional definition values in their native creation order and units.</summary>
    public interface IAdditionalSpecifications
    {
        /// <summary>Copies the specification values without converting units.</summary>
        /// <returns>A new array in the concrete specification's documented order.</returns>
        double[] ToArray();
    }
}
