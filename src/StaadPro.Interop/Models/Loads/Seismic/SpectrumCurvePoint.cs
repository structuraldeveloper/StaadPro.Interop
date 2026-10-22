namespace StaadPro.Interop.Models.Loads.Seismic
{
    /// <summary>
    /// Represents a period vs acceleration/displacement data point for custom response spectrum curves.
    /// </summary>
    public class SpectrumCurvePoint
    {
        /// <summary>Natural period in seconds.</summary>
        public double Period { get; set; }

        /// <summary>Spectral value (acceleration in length/sec^2 or g, or displacement in length units).</summary>
        public double Value { get; set; }

        /// <summary>Creates an editable zero/zero spectrum point.</summary>
        public SpectrumCurvePoint()
        {
        }

        /// <summary>Stores a spectrum pair without converting units or precision.</summary>
        /// <param name="period">Finite nonnegative natural period in seconds, checked at submission.</param>
        /// <param name="value">Finite spectrum ordinate in native caller-selected acceleration/displacement units, checked at submission.</param>
        public SpectrumCurvePoint(double period, double value)
        {
            Period = period;
            Value = value;
        }
    }
}
