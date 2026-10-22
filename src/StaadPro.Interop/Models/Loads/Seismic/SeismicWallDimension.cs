namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Represents dimensions of a shear wall for IS 1893-2016 wall area seismic definition.
    /// </summary>
    public class SeismicWallDimension
    {
        /// <summary>
        /// Gets or sets wall length in current model length units.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets wall thickness/width in current model length units.
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>Creates an editable wall dimension whose positive length and thickness must be set before submission.</summary>
        public SeismicWallDimension()
        {
        }

        /// <summary>Stores a wall length/thickness pair in current native model length units.</summary>
        /// <param name="length">Wall length; must be finite and positive when submitted.</param>
        /// <param name="thickness">Wall thickness; must be finite and positive when submitted.</param>
        public SeismicWallDimension(double length, double thickness)
        {
            Length = length;
            Thickness = thickness;
        }
    }
}
