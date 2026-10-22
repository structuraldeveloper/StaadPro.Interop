using StaadPro.Interop.Services.Load;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Contract for structure-specific geometric and dynamic parameters passed to OpenSTAAD ASCE 7 wind loading routines.
    /// </summary>
    public interface IASCE7StructureData
    {
        /// <summary>
        /// Gets the ASCE 7 structure classification type.
        /// </summary>
        ASCE7StructureType StructureType { get; }

        /// <summary>
        /// Gets or sets the fundamental natural frequency of the structure in Hertz (Hz).
        /// </summary>
        double NaturalFrequency { get; set; }

        /// <summary>
        /// Gets or sets the structural damping ratio (e.g. 0.01 for steel, 0.02 for concrete).
        /// </summary>
        double DampingRatio { get; set; }

        /// <summary>
        /// Validates that geometric dimensions and dynamic properties are physically valid.
        /// </summary>
        void Validate();

        /// <summary>
        /// Marshals the structure parameters into the exact 7-element double array expected by OpenSTAAD COM.
        /// </summary>
        /// <param name="publication">The ASCE 7 publication code, which controls edition-specific index mappings.</param>
        /// <returns>A double array of size 7.</returns>
        double[] ToDoubleArray(ASCE7Publication publication);
    }
}
